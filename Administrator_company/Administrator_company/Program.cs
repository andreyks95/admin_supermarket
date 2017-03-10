using System;
using System.Windows.Forms;

namespace Administrator_company
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Запустить сначала эту форму
            Application.Run(new MainFormTest());
            //Application.Run(new Administrator_company.TableOld.MainForm());
            //Application.Run(new MainForm());
        }
    }
}
