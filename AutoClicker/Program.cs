using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace AutoClicker
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            Application.Run(MainForm.Instance);
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            using (StringWriter writer = new StringWriter())
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(writer, e.Exception);

                File.WriteAllText("error.txt", writer.ToString());
            }
        }
    }
}
