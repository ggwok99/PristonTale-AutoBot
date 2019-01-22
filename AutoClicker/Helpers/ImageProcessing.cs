using AForge.Imaging;
using AForge.Imaging.Filters;
using AutoClicker.Collections;
using AutoClicker.Enums;
using AutoClicker.Models;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace AutoClicker.Helpers
{
    public static class ImageProcessing
    {
        public static Bitmap ConvertToFormat(this System.Drawing.Image image, PixelFormat format)
        {
            Bitmap copy = new Bitmap(image.Width, image.Height, format);
            using (Graphics gr = Graphics.FromImage(copy))
            {
                gr.DrawImage(image, new Rectangle(0, 0, copy.Width, copy.Height));
            }
            return copy;
        }

        public static RECT GetMatchingImageLocation(Bitmap sourceImage, AttributeType type)
        {
            Bitmap template = null;
            Bitmap filtered = null;
            try
            {
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

                TemplateMatch matching = TryExhaustiveTemplateMatchingWithExactOneResult(template, filtered);
                if (matching == null)
                {
                    return new RECT();
                }

                return matching.Rectangle;
            }
            catch
            {
                return new RECT();
            }
            finally
            {
                template.Dispose();
                filtered.Dispose();
            }
        }

        public static Rectangle GetRectangleAreaFromCenter(Bitmap sourceImage, int targetArea)
        {
            int width = sourceImage.Width * targetArea / 100;
            int height = sourceImage.Height * targetArea / 100;
            int x = (sourceImage.Width / 2) - (width / 2);
            int y = (sourceImage.Height / 2) - (height / 2);
            return new Rectangle(new Point(x, y), new Size(width, height));
        }

        private static TemplateMatch TryExhaustiveTemplateMatchingWithExactOneResult(Bitmap template, Bitmap filtered)
        {
            float similarity = 0.965f;
            TemplateMatch[] templateMatches = ExhaustiveTemplateMatching(template, filtered, similarity);

            for (int i = 0; i < 50; i++)
            {
                if (templateMatches.Length == 1)
                {
                    break;
                }

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
            }

            return templateMatches.FirstOrDefault(m => m.Similarity == templateMatches.Max(x => x.Similarity));
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

        public static Bitmap FilterForAutoAttacking(this Bitmap image)
        {
            EuclideanColorFiltering colorFilter = new EuclideanColorFiltering
            {
                CenterColor = new RGB(Color.Black),
                Radius = 275,
                FillColor = new RGB(Color.White)
            };
            colorFilter.ApplyInPlace(image);
            return image;
        }
    }
}
