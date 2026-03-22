using Wpf = System.Windows;

namespace Windows_Dialog_Box_Generator
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var ss = new Wpf.SplashScreen("SplashScreen.png");
            ss.Show(false, true);

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            var form = new Form1();
            form.Load += (s, e) => ss.Close(TimeSpan.FromMilliseconds(250d));

            Application.Run(form);
        }
    }
}