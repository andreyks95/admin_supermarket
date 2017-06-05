using System;
using System.Windows.Forms;

namespace Administrator_company
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        PositionForm positionForm   ; // = new PositionForm();
        EmployeesForm employeesForm ; // = new EmployeesForm();
        ProductsForm productsForm   ; // = new ProductsForm();
        StockForm stockForm         ; // = new StockForm();
        InfoForm infoForm           ; // = new InfoForm();
        AboutProgram aboutProgram   ; // = new AboutProgram();
        
        private void PositionButton_Click(object sender, EventArgs e)
        {
            positionForm = new PositionForm();
            positionForm.Show();
        }

        private void EmployeesButton_Click(object sender, EventArgs e)
        {
            employeesForm = new EmployeesForm();
            employeesForm.Show();
        }

        private void ProductsButton_Click(object sender, EventArgs e)
        {
            productsForm = new ProductsForm();
            productsForm.Show();
        }

        private void StockButton_Click(object sender, EventArgs e)
        {
            stockForm = new StockForm();
            stockForm.Show();
        }

        private void InfoButton_Click(object sender, EventArgs e)
        {
            infoForm = new InfoForm();
            infoForm.Show();
        }

        #region Пункты меню

        #region Таблицы
        private void информацияОСотрудникахToolStripMenuItem_Click(object sender, EventArgs e)
        {
            infoForm = new InfoForm();
            infoForm.Show();
        }

        private void должностиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            positionForm = new PositionForm();
            positionForm.Show();
        }

        private void сотрудникиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            employeesForm = new EmployeesForm();
            employeesForm.Show();
        }

        private void продуктыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            productsForm = new ProductsForm();
            productsForm.Show();
        }

        private void складToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stockForm = new StockForm();
            stockForm.Show();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aboutProgram = new AboutProgram();
            aboutProgram.Show();
        }
        #endregion


        #endregion
    }
}
