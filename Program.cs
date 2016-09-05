using System;
using System.Collections.Generic;
//using System.Linq;
using System.Windows.Forms;

namespace PhoneBase
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
		static void Main()
        {
			bool firstInstance = true;
			System.Diagnostics.Process aProcess = System.Diagnostics.Process.GetCurrentProcess();
			string aProcName = aProcess.ProcessName;

			if(System.Diagnostics.Process.GetProcessesByName(aProcName).Length > 1)
			{
				firstInstance = false;
			}

			/*
			System.Threading.Mutex mutex = new System.Threading.Mutex(false, "Local\\PhoneBook", out firstInstance);
			*/

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			if (firstInstance)
			{
				Application.Run(new Form1());
			}
			else
			{
				Application.Run(new Form3());
			}

			//mutex.Close();
		}
    }
}
