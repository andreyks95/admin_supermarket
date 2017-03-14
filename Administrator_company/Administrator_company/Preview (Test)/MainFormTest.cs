using Administrator_company.Preview__Test_;
using System;
using System.Windows.Forms;

namespace Administrator_company
{
    public partial class MainFormTest : Form
    {
        public MainFormTest()
        {
            InitializeComponent();
        }

        private void OpenTestForm_Click(object sender, EventArgs e)
        {
            TestForm testForm = new TestForm();
            testForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EmployeesTestForm employeesTF = new EmployeesTestForm();
            employeesTF.Show();
        }
    }
}
