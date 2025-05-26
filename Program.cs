// Program.cs 
// This file is the main entry point for the application.
using System;
using System.Windows.Forms;

namespace iShop // Ensure this matches your project's namespace
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread] // Essential attribute for Windows Forms applications
        static void Main()
        {
            // Enables visual enhancements for controls before any are created.
            Application.EnableVisualStyles();

            // Sets the default text rendering GDI+ (false) vs GDI (true). 
            // 'false' is generally recommended for modern UIs.
            Application.SetCompatibleTextRenderingDefault(false);

            // Starts the application message loop and makes Form1 the main form.
            Application.Run(new Form1());
        }
    }
}
