using System;
using System.Data;
using System.Windows.Forms;
using Administrator_company.LogicProgram;

namespace Administrator_company.TableOld
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        Connection Connect = new Connection();

        private void OpenConnection_Button_Click(object sender, EventArgs e)
        {
            
            if (Connect.connection.State == ConnectionState.Closed)
            {
                Connect.connection.Open();
                MessageBox.Show("База успешно подключена!");
            }
        }

        private void CloseConnection_Button_Click(object sender, EventArgs e)
        {
            if (Connect.connection.State == ConnectionState.Open)
            {
                Connect.connection.Close();
                MessageBox.Show("Сессия завершена!");
            }
        }

        private void TableAdministrator_Click(object sender, EventArgs e)
        {
            TableAdministrator tableAdministrator = new TableAdministrator();
            tableAdministrator.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TableDepartment tableDepartment = new TableDepartment();
            tableDepartment.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TableEmployees tableEmployees = new TableEmployees();
            tableEmployees.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TableProducts tableProducts = new TableProducts();
            tableProducts.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TableStock tableStock = new TableStock();
            tableStock.Show();
        }
    }
}
