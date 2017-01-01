using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Administrator_supermarket;
using MySql.Data.MySqlClient;

namespace Administrator_company
{
    public partial class TableStock : Form
    {
        public TableStock()
        {
            InitializeComponent();
        }

        
        //для отображения, вставки, изменения, удаления данных 
        private readonly Connection connect = new Connection();
        //для выполнение математических расчётов в поле таблицы
        private readonly Сalculations calculations = new Сalculations();
        //для проверки вводимых данных на пустоту и sql-инъекции
        private readonly  Checking checking  = new Checking();

        #region CaclulationField Считаем значение общей цены в "stock.price"
        /// <summary>
        /// Строим запрос для обчисления значения в ячейке таблицы ячейки поля
        /// </summary>
        /// <param name="textBoxsIdField">id полей таблиц которые необходимы для вычисления ячейки поля</param>
        /// <returns>Запрос для обновления данных</returns>
        private string CaclulationField(params TextBox[] textBoxsIdField)
        {
            string nameDatabase = "sql7150982";//"grocery_supermarket_manager";
            //Таблиы которые принимают участвие в вычислении
            string[] nameTables = { "products", "stock" };
            //Название полей выше указанных таблиц. У них есть значения для нужного нам вычисления
            string[,] nameFields =
            {
                {"price_for_one" },
                {"quantity" }
            };
            //Название полей id-ов выше указанных таблиц
            string[] nameIdTables = { "id_products", "id_stock" };
            //этих две переменные принимают учавствие в коопировани id с БД в  функции программы
            uint id_products = default(uint),
                id_stock = default(uint);
            //массив для сохранения текущих (нужных) id 
            uint[] id = new uint[2];

            //Если Id один. Для функции вставки данных в таблицу
            if (textBoxsIdField.Length <= 1)
            {
                string idField = textBoxsIdField[0].Text; //вытаскиваем  id 
                id_products = Convert.ToUInt32(idField); //конвертируем в число
                //получаем последнее добавленное id в поле таблицы. "stock.id_stock"
                idField = calculations.GetValueFromFieldTable(nameDatabase, nameTables[1], nameIdTables[1],"max"); 
                id_stock = Convert.ToUInt32(idField);

                id[0] = id_products; // id для id_products
                id[1] = id_stock;  // id для id_stock
            }
            //Для функции обновления данных в таблицу
            else
            {
                string idField = textBoxsIdField[0].Text; //получаем id с первого переданного textBox
                id_products = Convert.ToUInt32(idField);
                idField = textBoxsIdField[1].Text; //получаем id с второго переданного textBox
                id_stock = Convert.ToUInt32(idField);

                id[0] = id_products; // id для id_products
                id[1] = id_stock; // id для id_stock
            }
            //int[] id = { 1, 1 };
            //Подписать таблицы "как" в запросе select это будет select ... as T2 и т.д.
            string[] nameTable_AS = { "T2", "T3" };
          
                    //Получаем запрос select. Выбираем все значения с полей таблиц, необходимых для конечного расчёта
                    string SELECT = calculations.GetSelectAllFieldTables(nameDatabase, nameTables, nameFields, nameIdTables, id, nameTable_AS);

            
            string nameTableResult_AS = "ResultTable"; //Результирующую таблицу подписать как (для Set)
            string nameFieldResult = "price"; //название поля таблицы в которой необходимо сделать вычисление
            //string[] Fields = { "price_for_one", "quantity" };

                    //Получаем Set 
                    string SET = calculations.GetSet(nameTableResult_AS, nameFieldResult, nameTable_AS, nameFields, "*");

            string nameTableResult = "stock"; //название таблицы где будет происходить вычисление
            string nameFieldIdResult = "id_stock"; //название id поля таблицы
                    
                    //Получаем Полностью готовый Update запрос
                    string updateQuery = calculations.GetUpdateQuery(nameDatabase, nameTableResult, nameTableResult_AS,
                                                            nameFieldIdResult, Convert.ToUInt32(id_stock), SELECT, SET);
            
            return updateQuery;
        }
        #endregion

        #region Загрузить форму и отобразить таблицу
        private void TableStock_Load(object sender, EventArgs e)
        {
            connect.ShowTable("sql7150982", "stock", dataGridView1);//grocery_supermarket_manager
        }
        #endregion

        #region Вставляем данные в таблицу
        private void InsertData_Click(object sender, EventArgs e)
        {
            //возвращаем результаты проверок всех полей
            bool resultSecurity = checking.SecurityAll(textBox1, textBox2, textBox3, textBox4, textBox5),
                resultVoid = checking.VoidAll(textBox1, textBox2, textBox3); ////Проверяем только обязательные для ввода поля
            //если результаты вернулись положительные, тогда можно добавить данные, иначе вывести ошибку
            if (resultSecurity == true && resultVoid == true)
            {
                string[] fieldsTable = {"id_products", "available", "entered", "sold", "quantity"}; //, "price" };
                connect.InsertDataTable("sql7150982", "stock", fieldsTable, textBox1, textBox2,textBox3,
                                        textBox4, textBox5); //, textBox6);grocery_supermarket_manager
                //вычисляем только одно поле, передаём значение текущего id_product, а id_stock получаем автоматически из последнего добавленного.
                connect.FieldDateTableCalculation(CaclulationField(textBox1));
            }
            else
            {
                checking.ErrorMessage(this);
            }
        }
        #endregion

        #region Обновляем данные в таблице 
        private void button1_Click(object sender, EventArgs e)
        {
            //возвращаем результаты проверок всех полей
            bool resultSecurity = checking.SecurityAll(textBox7, textBox8, textBox9, textBox10, textBox11, textBox13),
                 resultVoid = checking.VoidAll(textBox7, textBox8, textBox9, textBox13); ////Проверяем только обязательные для ввода поля
            //если результаты вернулись положительные, тогда можно обновить данные, иначе вывести ошибку
            if (resultSecurity == true && resultVoid == true)
            {
                string[] fieldsTable = { "id_products", "available", "entered", "sold", "quantity", /*"price",*/ "id_stock" };
                connect.UpdateDataTable("sql7150982", "stock", fieldsTable, textBox7, textBox8,textBox9,
                                        textBox10, textBox11, /*textBox12,*/ textBox13);//grocery_supermarket_manager
                //передаём два id 
                connect.FieldDateTableCalculation(CaclulationField(textBox7, textBox13));
            }
            else
            {
                checking.ErrorMessage(this);
            }
        }
        #endregion

        #region Удаляем строку в таблице
        private void DeleteData_Click(object sender, EventArgs e)
        {
            bool resultSecurity = checking.SecurityAll(textBox14),
                 resultVoid = checking.VoidAll(textBox14);
            if (resultSecurity == true && resultVoid == true)
            {
                string[] fieldsTable = {"id_stock"};
                connect.DeleteDataTable("sql7150982", "stock", fieldsTable, textBox14);//grocery_supermarket_manager
            }
            else
            {
                checking.ErrorMessage(this);
            }
        }
        #endregion


    }
}
