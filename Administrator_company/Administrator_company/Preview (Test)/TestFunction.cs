using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Administrator_supermarket;

namespace Administrator_company.Preview__Test_
{
    public class TestFunction
    {

        //для отображения, вставки, изменения, удаления данных 
        private readonly Connection connect = new Connection();
        //для выполнение математических расчётов в поле таблицы
        private readonly Сalculations calculations = new Сalculations();
        //для проверки вводимых данных на пустоту и sql-инъекции
        private readonly Checking checking = new Checking();


        //Сделать с кортежами
        //ОБОБЩИТЬ 
        //СЕЙЧАС ТЕСТОВАЯ ВЕРСИЯ
        #region CaclulationField Считаем значение общей цены в "stock.price"
        /// <summary>
        /// Строим запрос для обчисления значения в ячейке таблицы ячейки поля
        /// </summary>
        /// <param name="textBoxsIdField">id полей таблиц которые необходимы для вычисления ячейки поля</param>
        /// <returns>Запрос для обновления данных</returns>
        private string CaclulationField(params TextBox[] textBoxsIdField)
        {

            //Таблицы которые принимают участвие в вычислении
            string[] nameTables = { "stock", "products" };
            
            //Название полей выше указанных таблиц. У них есть значения для нужного нам вычисления
            string[][] nameFields = new string[][]
            {
                new string[] { "quantity"  },
                new string[] { "price_for_one" }
            };

            //Название полей id-ов выше указанных таблиц
            string[] nameIdTables = { "id_stock", "id_products" };



            string[] ids = { textBoxsIdField[0].Text, //id_stock
                             textBoxsIdField[1].Text}; //id_products
                


            //ТЕСТОВАЯ ФУНКЦИЯ РАСЧЁТА ЗНАЧЕНИЯ В ПОЛЕ

            //Получаем все числовые значения
            List<float> values = calculations.GetAllSelectValues(nameTables, nameFields, nameIdTables, ids);
            //Получаем расчёт всех значений
            float result = calculations.GetCalc(values, "*");
            //Получаем Полностью готовый Update запрос
            string updateQuery = calculations.GetUpdateQuery("stock", "price", "id_stock", ids[0], result);

            return updateQuery;
        }

       
        private Tuple<Tuple<string[], string[][], string[], string[]>,
                      Tuple<string>,
                      Tuple<string, string, string, string>> 
            SetTuple(params TextBox[] textBoxsIdField)
        {

            //Rest1
            //Таблицы которые принимают участвие в вычислении
            string[] nameTables = { "stock", "products" };

            //Название полей выше указанных таблиц. У них есть значения для нужного нам вычисления
            string[][] nameFields = new string[][]
            {
                new string[] { "quantity"  },
                new string[] { "price_for_one" }
            };

            //Название полей id-ов выше указанных таблиц
            string[] nameIdTables = { "id_stock", "id_products" };



            string[] ids = { textBoxsIdField[0].Text, //id_stock
                             textBoxsIdField[1].Text}; //id_products

            //Rest2
            string mathOperation = "*";

            //Rest3
            string resultTable = "stock";
            string resultField = "price";
            string resultIdFiedTable = "id_stock";
            string resultNumberId = ids[0];


            var dataCalculations = new Tuple<
                                            Tuple<string[], string[][], string[], string[]>, 
                                            Tuple<string>,
                                            Tuple<string, string,string, string>
                                    >(
                                      new Tuple<string[], string[][], string[], string[]>(nameTables, nameFields, nameIdTables, ids), 
                                      new Tuple<string>(mathOperation),
                                      new Tuple<string, string, string, string>( resultTable, resultField,resultIdFiedTable, resultNumberId)
                                    );


            return dataCalculations;

        }

        public string GetUpdateQuery(Tuple<
                                            Tuple<string[], string[][], string[], string[]>,
                                            Tuple<string>,
                                            Tuple<string, string, string, string>
                                    > dataCalculations)
        {
            //Получаем все числовые значения
            List<float> values = calculations.GetAllSelectValues(dataCalculations.Item1.Item1, dataCalculations.Item1.Item2, dataCalculations.Item1.Item3, dataCalculations.Item1.Item4);
            //Получаем расчёт всех значений
            float result = calculations.GetCalc(values, dataCalculations.Item2.Item1);
            //Получаем Полностью готовый Update запрос
            string updateQuery = calculations.GetUpdateQuery(dataCalculations.Item3.Item1, dataCalculations.Item3.Item2, dataCalculations.Item3.Item3, dataCalculations.Item3.Item4, result);

            return updateQuery;
        }

        #endregion
        /*
        #region Вставляем данные в таблицу
        private void InsertData_Click(object sender, EventArgs e)
        {
            //для функция вычисляет указанный id продукта, то есть введённый
                //вычисляем только одно поле, передаём значение текущего id_product, а id_stock получаем автоматически из последнего добавленного.
                connect.FieldDateTableCalculation(CaclulationField(textBox1));
         
        }
        #endregion

        #region Обновляем данные в таблице 
        private void button1_Click(object sender, EventArgs e)
        {
             //а для обновления нужно знать и введённый id продукта, и введённое значение id склада, которое нужно обновить. 
                //передаём два id 
                connect.FieldDateTableCalculation(CaclulationField(textBox7, textBox13));

        }
        #endregion*/

    }
}