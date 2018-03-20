using System;
using System.Windows.Forms;
using System.IO;

namespace FileFinder
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                                            
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new searchFileForm());
            }
            catch (Exception ex)
            {
                Console.WriteLine("An Unexpected Exception Error has been thrown " +
                                    ex.Message);
                Console.WriteLine(ex.ToString());
                Console.ReadKey();
                return;
            }
            finally
            {
            }

        }
    }
}
