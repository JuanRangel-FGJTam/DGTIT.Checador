using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Markup;

namespace DGTIT.Checador
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Contains("--ch") || args.Contains("--CH"))
            {
                Application.Run(new Views.Checador());
            }
            else
            {
                Application.Run(new Main());
            }
        }
    }
}
