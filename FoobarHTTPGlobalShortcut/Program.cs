using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FoobarHTTPGlobalShortcut
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (Properties.Settings.Default.Prefix == "")
            {
                Choose_url url = new Choose_url();
                url.ShowDialog();
                string prefix = url.Url;
                Properties.Settings.Default.Prefix = prefix;
                Properties.Settings.Default.Save();
            }
            Application.Run(new Main(Properties.Settings.Default.Prefix));
        }
    }
}
