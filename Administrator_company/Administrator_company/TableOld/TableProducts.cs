using System;
using System.Windows.Forms;
using Administrator_company.LogicProgram;

namespace Administrator_company.TableOld
{
    public partial class TableProducts : Form
    {
        public TableProducts()
        {
            InitializeComponent();
        }
        private readonly Connection connect = new Connection(); //Для отображения, вставки, обновления, удаления данных в таблице
        private readonly Checking checking = new Checking(); //Для проверки ячеек на вредные запросы и пустоту значений

        #region Загрузка формы и отображения таблицы 
        private void TableProducts_Load(object sender, EventArgs e)
        {
            connect.ShowTable("grocery_supermarket_manager", "products", dataGridView1);//grocery_supermarket_manager //sql7150982
        }
        #endregion

        #region Вставка данных в таблицу
        private void InsertData_Click(object sender, EventArgs e)
        {
            //возвращаем результаты проверок всех полей
            bool resultSecurity = checking.SecurityAll(textBox1, textBox2, textBox3),
                resultVoid = checking.VoidAll(textBox1, textBox2, textBox3); //Проверяем только обязательные для ввода поля
            //если результаты вернулись положительные, тогда можно добавить данные, иначе вывести ошибку
            if (resultSecurity == true && resultVoid == true)
            {
                string[] fieldsTable = { "name", "category", "price_for_one" };
                connect.InsertDataTable("grocery_supermarket_manager", "products", fieldsTable, textBox1, textBox2, textBox3);
            }//grocery_supermarket_manager //sql7150982
            else
            {
                checking.ErrorMessage(this);
            }
        }
        #endregion

        #region Обновление данных в таблице
        private void button1_Click(object sender, EventArgs e)
        {
            
            bool resultSecurity = checking.SecurityAll(textBox4, textBox5, textBox6, textBox7),
                resultVoid = checking.VoidAll(textBox4, textBox5, textBox6, textBox7);
            
            if (resultSecurity == true && resultVoid == true)
            {
                string[] fieldsTable = { "name", "category", "price_for_one", "id_products" };
            connect.UpdateDataTable("grocery_supermarket_manager", "products", fieldsTable, textBox4, textBox5, textBox6, textBox7);
            }//grocery_supermarket_manager //sql7150982
            else
            {
                checking.ErrorMessage(this);
            }
        }
        #endregion

        #region Удаление данных в таблице
        private void DeleteData_Click(object sender, EventArgs e)
        {
            bool resultSecurity = checking.SecurityAll(textBox8),
                resultVoid = checking.VoidAll(textBox8);
            if (resultSecurity == true && resultVoid == true)
            {
                string[] fieldsTable = { "id_products" };
            connect.DeleteDataTable("grocery_supermarket_manager", "products", fieldsTable, textBox8);
            }//grocery_supermarket_manager //sql7150982
            else
            {
                checking.ErrorMessage(this);
            }
        }
        #endregion
    }
}
