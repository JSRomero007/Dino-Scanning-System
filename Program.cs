using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScaningSystem
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            int i = 1;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (i == 0) { Application.Run(new LogConstructor()); }

        }
    }
}
