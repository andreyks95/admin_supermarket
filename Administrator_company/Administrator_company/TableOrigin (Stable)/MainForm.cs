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

        PositionForm positionForm = new PositionForm();
        EmployeesForm employeesForm = new EmployeesForm();
        ProductsForm productsForm = new ProductsForm();
        StockForm stockForm = new StockForm();
        InfoForm infoForm = new InfoForm();
        AboutProgram aboutProgram = new AboutProgram();

        private void PositionButton_Click(object sender, EventArgs e)
        {
           positionForm.Show();
        }

        private void EmployeesButton_Click(object sender, EventArgs e)
        {
            employeesForm.Show();
        }

        private void ProductsButton_Click(object sender, EventArgs e)
        {
            productsForm.Show();
        }

        private void StockButton_Click(object sender, EventArgs e)
        {
            stockForm.Show();
        }

        private void InfoButton_Click(object sender, EventArgs e)
        {
            infoForm.Show();
        }

        #region Пункты меню

        #region Таблицы
        private void информацияОСотрудникахToolStripMenuItem_Click(object sender, EventArgs e)
        {
            infoForm.Show();
        }

        private void должностиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            positionForm.Show();
        }

        private void сотрудникиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            employeesForm.Show();
        }

        private void продуктыToolStripMenuItem_Click(object sender, EventArgs e)
        {
           productsForm.Show();
        }

        private void складToolStripMenuItem_Click(object sender, EventArgs e)
        {
           stockForm.Show();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aboutProgram.Show();
        }
        #endregion


        #endregion
    }
}
