using System;
using System.Windows.Forms;
using Administrator_company.LogicProgram;

namespace Administrator_company.TableOld
{
    public partial class TableAdministrator : Form
    {
        public TableAdministrator()
        {
            InitializeComponent();
        }

        //создаём объект класса Connection, где будем иметь доступ ко всем функциям 
        private readonly Connection connect = new Connection(); //Для отображения, вставки, обновления, удаления данных в таблице
        private readonly Checking checking = new Checking(); //Для проверки ячеек на вредные запросы и пустоту значений

        //public object DataGridViewDataSource => dataGridView1.DataSource;

        #region Загрузка формы и отображения таблицы 
        //Отображение записей в таблице при загрузке
        private void TableAdministrator_Load(object sender, EventArgs e)
        {
                connect.ShowTable("grocery_supermarket_manager", "administrator", dataGridView1);//grocery_supermarket_manager//sql7150982
        }
        #endregion

        #region Вставка данных в таблицу
        //Вставка записей в таблицу
        private void InsertData_Click(object sender, EventArgs e)
        {
            //возвращаем результаты проверок всех полей
            bool resultSecurity = checking.SecurityAll(textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, textBox7, textBox8),
                resultVoid = checking.VoidAll(textBox1, textBox2, textBox3); //Проверяем только обязательные для ввода поля
            //если результаты вернулись положительные, тогда можно добавить данные, иначе вывести ошибку
            if (resultSecurity == true && resultVoid == true)
            {
                //создаём массив из списка полей в таблице "administrator"
                string[] fieldsTable = { "id_department", "full_name", "passport_id", "experience", "address", "phone_number", "age", "photo" };
            connect.InsertDataTable("grocery_supermarket_manager", "administrator", fieldsTable, textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, textBox7, textBox8);
                //grocery_supermarket_manager//sql7150982
            }
            else
            {
                checking.ErrorMessage(this);
            }
        }
        #endregion

        #region Обновление данных в таблице
        private void button1_Click(object sender, EventArgs e)
        {
            //возвращаем результаты проверок всех полей
            bool resultSecurity = checking.SecurityAll(textBox9, textBox10, textBox11, textBox12, textBox13, textBox14, textBox15, textBox16, textBox17),
                resultVoid = checking.VoidAll(textBox9, textBox10, textBox11, textBox17); //Проверяем только обязательные для ввода поля
            //если результаты вернулись положительные, тогда можно добавить данные, иначе вывести ошибку
            if (resultSecurity == true && resultVoid == true)
            {
                string[] fieldsTable = { "id_department", "full_name", "passport_id", "experience", "address", "phone_number", "age", "photo", "id_administrator" };
            connect.UpdateDataTable("grocery_supermarket_manager", "administrator", fieldsTable, textBox9, textBox10, textBox11, textBox12, textBox13, textBox14, textBox15, textBox16, textBox17);
                //grocery_supermarket_manager//sql7150982
            }
            else
            {
                checking.ErrorMessage(this);
            }
        }
        #endregion

        #region Удаление данных в таблице
        private void buttonDeleteData_Click(object sender, EventArgs e)
        {
            //возвращаем результаты проверок всех полей
            bool resultSecurity = checking.SecurityAll(textBoxDelete),
                resultVoid = checking.VoidAll(textBoxDelete); //Проверяем только обязательные для ввода поля
            //если результаты вернулись положительные, тогда можно добавить данные, иначе вывести ошибку
            if (resultSecurity == true && resultVoid == true)
            {
                string[] fieldsTable = {"id_administrator"};
                connect.DeleteDataTable("grocery_supermarket_manager", "administrator", fieldsTable, textBoxDelete);
                //grocery_supermarket_manager //sql7150982
            }
            else
            {
                checking.ErrorMessage(this);
            }
        }
        #endregion
    }
}

