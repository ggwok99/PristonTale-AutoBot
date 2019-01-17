using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Math.Geometry;
using AutoClicker.Collections;
using AutoClicker.Enums;
using AutoClicker.Models;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace AutoClicker.Helpers
{
    public static class ImageProcessing
    {
        public static unsafe List<Color> ReadBitmap(Bitmap bitmap)
        {
            List<Color> colors = new List<Color>();

            BitmapData bData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);

            int bitsPerPixel = System.Drawing.Image.GetPixelFormatSize(bData.PixelFormat);

            /*This time we convert the IntPtr to a ptr*/
            byte* scan0 = (byte*)bData.Scan0.ToPointer();

            for (int i = 0; i < bData.Height; ++i)
            {
                List<Color> colorsByWidth = new List<Color>();
                for (int j = 0; j < bData.Width; ++j)
                {
                    byte* data = scan0 + i * bData.Stride + j * bitsPerPixel / 8;

                    Color color = Color.FromArgb(data[2], data[1], data[0]);
                    Color detected = ColorDetection.GetClosestColor(color);
                    colorsByWidth.Add(detected);
                }
                Color modeColor = colorsByWidth.GroupBy(c => c).OrderByDescending(grp => grp.Count()).Select(grp => grp.Key).First();
                colors.Add(modeColor);
            }

            bitmap.UnlockBits(bData);

            return colors;
        }

        public static List<CharacterAttributePosition> GetCharacterAttributePositions(Bitmap image)
        {
            List<CharacterAttributePosition> results = new List<CharacterAttributePosition>();
            // locating objects
            BlobCounter blobCounter = new BlobCounter
            {
                FilterBlobs = true,
                MinHeight = 10,
                MinWidth = 3
            };

            blobCounter.ProcessImage(image);
            Blob[] blobs = blobCounter.GetObjectsInformation();

            SimpleShapeChecker shapeChecker = new SimpleShapeChecker();

            foreach (Blob blob in blobs)
            {
                List<AForge.IntPoint> edgePoints = blobCounter.GetBlobsEdgePoints(blob);

                if (shapeChecker.IsQuadrilateral(edgePoints, out List<AForge.IntPoint> cornerPoints))
                {
                    if (shapeChecker.CheckPolygonSubType(cornerPoints) == PolygonSubType.Rectangle)
                    {
                        int top = 0, bottom = 0, left = 0, right = 0;
                        foreach (AForge.IntPoint point in cornerPoints)
                        {
                            if (top == 0 || top > point.Y)
                            {
                                top = point.Y;
                            }

                            if (bottom == 0 || bottom < point.Y)
                            {
                                bottom = point.Y;
                            }

                            if (left == 0 || left > point.X)
                            {
                                left = point.X;
                            }

                            if (right == 0 || right < point.X)
                            {
                                right = point.X;
                            }

                        }

                        CharacterAttributePosition attribute = new CharacterAttributePosition(left, top, right, bottom, blob.ColorMean);
                        results.Add(attribute);
                    }
                }
            }

            return results;
        }

        public static Bitmap ConvertToFormat(this System.Drawing.Image image, PixelFormat format)
        {
            Bitmap copy = new Bitmap(image.Width, image.Height, format);
            using (Graphics gr = Graphics.FromImage(copy))
            {
                gr.DrawImage(image, new Rectangle(0, 0, copy.Width, copy.Height));
            }
            return copy;
        }

        public static Bitmap FilterImageForAttributePositions(Bitmap image)
        {
            // create filter
            HSLFiltering filter = new HSLFiltering
            {
                // set color ranges to keep
                Hue = new AForge.IntRange(70, 0),
                Saturation = new AForge.Range(0.6f, 1),
                Luminance = new AForge.Range(0.1f, 1)
            };
            // apply the filter
            using (Bitmap converted = image.ConvertToFormat(PixelFormat.Format32bppRgb))
            {
                Bitmap clone = (Bitmap)converted.Clone();
                filter.ApplyInPlace(clone);
                return clone;
            }
        }

        public static RECT GetMatchingImageLocation(Bitmap sourceImage, AttributeType type)
        {
            List<RECT> results = new List<RECT>();

            Bitmap template;
            Bitmap filtered;
            switch (type)
            {
                case AttributeType.HP:
                    filtered = FilterForRedColor(sourceImage);
                    template = (Bitmap)System.Drawing.Image.FromFile(TemplateNames.lifePot);
                    break;
                case AttributeType.MP:
                    filtered = FilterForBlueColor(sourceImage);
                    template = (Bitmap)System.Drawing.Image.FromFile(TemplateNames.manaPot);
                    break;
                default:
                    filtered = FilterForYellowGreenColor(sourceImage);
                    template = (Bitmap)System.Drawing.Image.FromFile(TemplateNames.stmPot);
                    break;
            }

            TemplateMatch[] matchings = TryExhaustiveTemplateMatchingWithExactOneResult(template, filtered);
            foreach (TemplateMatch m in matchings)
            {
                results.Add(m.Rectangle);
            }
            template.Dispose();
            filtered.Dispose();

            return results.FirstOrDefault();
        }

        public static Rectangle GetRectangleAreaFromCenter(Bitmap sourceImage, int targetArea)
        {
            int width = sourceImage.Width * targetArea / 100;
            int height = sourceImage.Height * targetArea / 100;
            int x = (sourceImage.Width / 2) - (width / 2);
            int y = (sourceImage.Height / 2) - (height / 2);
            return new Rectangle(new Point(x, y), new Size(width, height));
        }

        private static TemplateMatch[] TryExhaustiveTemplateMatchingWithExactOneResult(Bitmap template, Bitmap filtered)
        {
            TemplateMatch[] templateMatches;

            int i = 50;
            float similarity = 0.965f;
            do
            {
                templateMatches = ExhaustiveTemplateMatching(template, filtered, similarity);
                if (templateMatches.Length == 1)
                {
                    break;
                }
                else if (templateMatches.Length > 1)
                {
                    similarity += .005f;
                    if (similarity > 1)
                    {
                        break;
                    }
                }
                else
                {
                    similarity -= .005f;
                }

                i--;
            } while (i > 0 && (templateMatches.Length != 1 || similarity > .8f || similarity < 1));

            return templateMatches;
        }

        private static TemplateMatch[] ExhaustiveTemplateMatching(Bitmap template, Bitmap filtered, float similarity)
        {
            ExhaustiveTemplateMatching tm = new ExhaustiveTemplateMatching(similarity);
            using (Bitmap templateConverted = template.ConvertToFormat(PixelFormat.Format24bppRgb))
            {
                using (Bitmap filteredConverted = filtered.ConvertToFormat(PixelFormat.Format24bppRgb))
                {
                    return tm.ProcessImage(filteredConverted, templateConverted);
                }
            }
        }

        private static Bitmap FilterForRedColor(Bitmap image)
        {
            EuclideanColorFiltering filter = new EuclideanColorFiltering
            {
                CenterColor = new RGB(215, 30, 30),
                Radius = 150
            };
            using (Bitmap converted = image.ConvertToFormat(PixelFormat.Format24bppRgb))
            {
                Bitmap clone = (Bitmap)converted.Clone();
                filter.ApplyInPlace(clone);
                return clone;
            }
        }

        private static Bitmap FilterForBlueColor(Bitmap image)
        {
            EuclideanColorFiltering filter = new EuclideanColorFiltering
            {
                CenterColor = new RGB(30, 30, 235),
                Radius = 150
            };
            using (Bitmap converted = image.ConvertToFormat(PixelFormat.Format24bppRgb))
            {
                Bitmap clone = (Bitmap)converted.Clone();
                filter.ApplyInPlace(clone);
                return clone;
            }
        }

        private static Bitmap FilterForYellowGreenColor(Bitmap image)
        {
            EuclideanColorFiltering filter = new EuclideanColorFiltering
            {
                CenterColor = new RGB(30, 215, 30),
                Radius = 150
            };
            using (Bitmap converted = image.ConvertToFormat(PixelFormat.Format24bppRgb))
            {
                Bitmap clone = (Bitmap)converted.Clone();
                filter.ApplyInPlace(clone);
                return clone;
            }
        }

        public static Bitmap FilterForBlackTags(this Bitmap image)
        {
            EuclideanColorFiltering colorFilter = new EuclideanColorFiltering
            {
                CenterColor = new RGB(Color.Black),
                Radius = 25,
                FillColor = new RGB(Color.White)
            };
            colorFilter.ApplyInPlace(image);
            return image;
        }
    }
}
