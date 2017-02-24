using System;
using System.Data;
using System.Windows.Forms;
using Administrator_supermarket;
using MySql.Data.MySqlClient;

namespace Administrator_company.Preview__Test_
{
    public partial class EmployeesForm : Form
    {
        public EmployeesForm()
        {
            InitializeComponent();
        }

        Connection connection = new Connection();
        Settings settings = new Settings();
        Checking checking = new Checking();
        BindingManagerBase managerBase; //для перемещения по таблице 

        private static string nameTable = "position", //таблица
                             id_field = "id_position"; //id поле таблицы
        private static string[] nameFieldsAll = { "id_position", "position", "salary" }, //все поля
                                 variables = { "@id", "@position", "@salary" }, //для переменных
                                 nameFieldsAS = { "ИД", "Должность", "Зарплата" }, //как будут отображаться
                                 numericFields = { "id_position", "salary" }; //для корректного поиска по числовым столбцам                                                    
        private static MySqlDbType[] mySqlDbTypes = { MySqlDbType.UInt32, MySqlDbType.VarChar, MySqlDbType.UInt32 };//для типов в (AddParameters)

        private void EmployeesForm_Load(object sender, EventArgs e)
        {
            FillDataGridView(""); //при загрузке отображаем таблицу
        }

        //Заполняем DataGridView и корректируем её
        public void FillDataGridView(string valueToSearch)
        {
            //получаем запрос на отображение данных с поиском
            string query = connection.GetQueryShowSearch(nameTable, nameFieldsAll, nameFieldsAS, numericFields, valueToSearch);
            DataTable table = connection.FillDataGridView(dataGridView1, 20, query: query); //заполняем таблицу данными с запроса и настраиваем
            managerBase = this.BindingContext[table]; //подключаем таблицу для передвижения по ней
        }

        private void Insert_Click(object sender, EventArgs e)
        {

        }

        private void Update_Click(object sender, EventArgs e)
        {

        }

        private void Delete_Click(object sender, EventArgs e)
        {

        }
        private void Find_Click(object sender, EventArgs e)
        {

        }
        private void Clear_Click(object sender, EventArgs e)
        {

        }
        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {

        }
        private void dataGridView1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void FirstRecordButton_Click(object sender, EventArgs e)
        {

        }

        private void NextRecordButton_Click(object sender, EventArgs e)
        {

        }

        private void PreviousRecordButton_Click(object sender, EventArgs e)
        {

        }

        private void LastRecordButton_Click(object sender, EventArgs e)
        {

        }
    }
}
