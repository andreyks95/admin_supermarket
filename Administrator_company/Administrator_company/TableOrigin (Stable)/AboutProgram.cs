using System;
using System.Windows.Forms;

namespace Administrator_company
{
    partial class AboutProgram : Form
    {
        public AboutProgram()
        {
            InitializeComponent();
            //this.Text = String.Format("О программе {0}", AssemblyTitle);
            //this.labelProductName.Text = AssemblyProduct;
            //this.labelVersion.Text = String.Format("Версия: {0}", AssemblyVersion);
            //this.labelCopyright.Text = AssemblyCopyright;
            //this.labelCompanyName.Text = AssemblyCompany;
            //this.textBoxDescription.Text = AssemblyDescription;
            Text = "О программе";//"Проект автоматизированного рабочего места администратора продуктового супермаркета с удаленным доступом к базе данных.";
            labelProductName.Text = "Проект АРМ администратора продуктового супермаркета с удаленным доступом к базе данных.";
            labelVersion.Text = "2.11.38.166";
            labelCopyright.Text = "Авторские права: ст.гр.ИТ - 15 - 1т Когута Андрея";
            labelCompanyName.Text = "Название учебного заведения: ДГМА";
            textBoxDescription.Text = "Данный программный продукт предназначен для легкого и быстрого управления базой данных для администрирования продуктового супермаркета.";

        }

        /*#region Методы доступа к атрибутам сборки

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion => "1.5.20.75";

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "Данный программный продукт предназначен для легкого и быстрого управления базой данных для администрирования продуктового супермаркета.";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "Система администрирования продуктового супермаркета";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "Авторские права: ст.гр.ИТ - 15 - 1т Когута Андрея";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "Название организации: ДГМА";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion*/

        private void okButton_Click(object sender, EventArgs e) => Close();
        /*{
            Close();
        }*/
    }
}
