using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Administrator_company.Preview__Test_;

namespace Administrator_supermarket
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
            Application.Run(new MainForm());
            //Application.Run(new Form1());
        }
    }
}
