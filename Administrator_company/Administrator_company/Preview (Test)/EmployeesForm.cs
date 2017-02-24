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

        private static string nameTable = "employees", //таблица
                             id_field = "id_employee"; //id поле таблицы
        private static string[] nameFieldsAll = { "id_employee", "id_info", "id_position", "department", "experience", "salary", "started_work", "fired" }, //все поля
                                variables =     { "@id_employee", "@id_info", "@id_position", "@department", "@experience", "@salary", "@started_work", "@fired" }, //для переменных
                                nameFieldsAS =  { "ИД", "ФИО сотрудника", "Должность", "Отдел", "Опыт", "Зарплата", "Принят", "Уволен" }, //как будут отображаться
                                numericFields = { "id_employee", "id_info", "id_position", "experience", "salary" }; //для корректного поиска по числовым столбцам                                                    
        private static MySqlDbType[] mySqlDbTypes = { MySqlDbType.UInt32, MySqlDbType.UInt32, MySqlDbType.UInt32, MySqlDbType.Enum, MySqlDbType.UInt32, MySqlDbType.UInt32, MySqlDbType.Date, MySqlDbType.Date };//для типов в (AddParameters)

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
            //возвращаем результаты проверок всех полей
            bool resultSecurity = checking.SecurityAll(textBox1, textBox2, textBox3),
                resultVoid = checking.VoidAll(textBox1, textBox2, textBox3); //Проверяем только обязательные для ввода поля
                                                                             //если результаты вернулись положительные, тогда можно добавить данные, иначе вывести ошибку
            if (resultSecurity == true && resultVoid == true)
            {
                //получить запрос для вставки Insert
                string query = connection.GetQueryInsert(nameTable, nameFieldsAll, variables);
                //выполняить команду с Insert
                connection.command = new MySqlCommand(query, connection.connection);
                //для объектов, у них есть данные которые нужно вставить
                object[] objects = { textBox1, textBox2, textBox3 };
                //Добавляем данные 
                connection.AddParameters(connection.command, variables, mySqlDbTypes, objects);
                //попытаться выполнить запрос
                connection.ExecuteQuery(connection.command);
                //отобразить новые данные 
                FillDataGridView("");
            }
            else
            {
                checking.ErrorMessage(this); //вывести ошибку
            }
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
