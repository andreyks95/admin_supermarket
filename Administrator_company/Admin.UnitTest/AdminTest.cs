using Administrator_supermarket;
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;


namespace Admin.UnitTest
{
    [TestClass]
    public class AdminTest 
    {

        [TestMethod]
        public void GetQuerySearchTest()
        {
            //arrange
            string nameDatabase = "supermarket",
                nameTable = "info",
                valueToSearch = "Краматорск";
            string[] nameFields = {"id_info", "full_name", "passport_id", "age", "address", "phone", "photo"},
                newNameFieldsAS = {"id", "Имя", "Серия и номер паспорта", "Возраст", "Адрес", "Телефон", "Фото"};

            string WaitResult = " SELECT  id_info AS 'id',  full_name AS 'Имя',  passport_id AS 'Серия и номер паспорта',  " +
                                            "age AS 'Возраст',  address AS 'Адрес',  phone AS 'Телефон',  photo AS 'Фото' " +
                                    "FROM supermarket.info" +
                                    " WHERE CONCAT ( id_info,  full_name,  passport_id,  age,  address,  phone,  photo ) " +
                                    " LIKE '%Краматорск%'";
            //act
            Connection connection = new Connection();
            string query = connection.GetQuerySearch(nameTable, nameFields, newNameFieldsAS, valueToSearch);

            //assert
            Assert.AreEqual(WaitResult.Length, query.Length);
            // Assert.AreEqual(WaitResult.ToLower(), query.ToLower());
        }

        [TestMethod]
        public void GetSelectAllFieldTablesTest()
        {
            //arrange

            string nameDatabase = "grocery_supermarket_manager";
            string[] nameTables = { "products", "stock" };
            string[] nameFields =
            {
                "price_for_one" ,
                "quantity" 
            };
            string[] nameIdFieldTables = { "id_products", "id_stock" };
            uint[] id = { 2, 5 };
            string[] nameTable_AS = { "T2", "T3" };

            string WaitResult = " ( SELECT price_for_one FROM grocery_supermarket_manager.products WHERE id_products = 2 ) AS T2, " +
                                " ( SELECT quantity FROM grocery_supermarket_manager.stock WHERE id_stock = 5 ) AS T3 ";

            //act
            Administrator_supermarket.Сalculations calc = new Administrator_supermarket.Сalculations();
            string SELECT = calc.GetSelectAllFieldTables(nameDatabase, nameTables, nameFields, nameIdFieldTables, id, nameTable_AS);


            //assert
            Assert.AreEqual(WaitResult.Length, SELECT.Length);
            Assert.AreEqual(WaitResult, SELECT);
        }

       [TestMethod]
        public void GetSelectAllFieldTablesOverloadTest()
        {
            //arrange
    
            string nameDatabase = "grocery_supermarket_manager";
            string[] nameTables = { "products", "stock" };
            string[,] nameFields =
            {
                {"price_for_one" },
                {"quantity" }
            };
            string[] nameIdFieldTables = { "id_products", "id_stock" };
            uint[] id = { 2, 5 };
            string[] nameTable_AS = { "T2", "T3" };

            string WaitResult = " ( SELECT price_for_one FROM grocery_supermarket_manager.products WHERE id_products = 2 ) AS T2, " +
                                " ( SELECT quantity FROM grocery_supermarket_manager.stock WHERE id_stock = 5 ) AS T3 ";

            //act
            Administrator_supermarket.Сalculations calc = new Administrator_supermarket.Сalculations();
            string SELECT = calc.GetSelectAllFieldTables(nameDatabase, nameTables, nameFields, nameIdFieldTables, id, nameTable_AS);


            //assert
            Assert.AreEqual(WaitResult.Length, SELECT.Length);
            Assert.AreEqual(WaitResult, SELECT);
        }

        [TestMethod]
        public void GetSetOverloadTest()
        {
            //arrange
            string nameTableResult_AS = "ResultTable";
            string nameFieldResult = "price";
            string[] nameTables_AS = { "T2", "T3" };
            string[,] nameFields =
            {
                { "price_for_one" },
                { "quantity" }
            };
            string mathOperation = "*";

            string WaitResult = " SET ResultTable.price = T2.price_for_one * T3.quantity ";

            //act
            Administrator_supermarket.Сalculations calc = new Administrator_supermarket.Сalculations();
            string SET = calc.GetSet(nameTableResult_AS, nameFieldResult, nameTables_AS, nameFields, mathOperation);

            //assert
            Assert.AreEqual(WaitResult, SET);
        }

        [TestMethod]
        public void GetSetTest()
        {
            //arrange
            string nameTableResult_AS = "ResultTable";
            string nameFieldResult = "price";
            string[] nameTables_AS = { "T2", "T3" };
            string[ ] nameFields =
            {
                "price_for_one" ,
                 "quantity" 
            };
            string mathOperation = "*";

            string WaitResult = " SET ResultTable.price = T2.price_for_one * T3.quantity ";

            //act
            Administrator_supermarket.Сalculations calc = new Administrator_supermarket.Сalculations();
            string SET = calc.GetSet(nameTableResult_AS, nameFieldResult, nameTables_AS, nameFields, mathOperation);

            //assert
            Assert.AreEqual(WaitResult, SET);
        }

        [TestMethod]
        public void GetMaxIdTest()
        {
            //arrange
            string nameDatabase = "grocery_supermarket_manager", //sql7150982 //
                nameTable = "stock",
                nameField = "id_stock",
                nameFunc = "max";
            string WaitResult = "10";


            //act
            Administrator_supermarket.Сalculations calc = new Administrator_supermarket.Сalculations();
            string result = calc.GetValueFromFieldTable(nameDatabase, nameTable, nameField, nameFunc);
            
            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(WaitResult, result);
    }

        [TestMethod]
        public void CheckingBoolTest()
        {
            //arrange
            string[] data = { "select", "s'emi'colo'n"};
            bool waitResult = false; //не давать разрешение на ввод строки

            //act
            Administrator_supermarket.Checking check = new Checking();
            bool result = check.SecurityAllString(data);

            //assert
            Assert.AreEqual(waitResult, result);


        }

        [TestMethod]
        public void GetSetTestForExecuteScalar()
        {
            //arrange
            string nameDatabase = "supermarket",
                table = "products",
                field = "price_for_one",
                idField = "id_products",
                id = "2",
                WaitResult = " SELECT price_for_one" +
                                " FROM supermarket.products" +
                                " WHERE id_products = 2; ";

            //act
            Administrator_supermarket.Сalculations calc = new Administrator_supermarket.Сalculations();
            string select = calc.GetSelectQuery(nameDatabase, table, field, idField, id);

            //assert
            Assert.AreEqual(WaitResult.ToLower(), select.ToLower());
        }

        [TestMethod]
        public void GetSelectValueTest()
        {
            //arrange 
            string query = " SELECT price_for_one" +
                                " FROM supermarket.products" +
                                " WHERE id_products = 2; ";
            Single WaitResult = 9.45f;

            //act
            Connection connect = new Connection();
            connect.OpenConnection();
            Administrator_supermarket.Сalculations calc = new Administrator_supermarket.Сalculations();
            Single result = calc.GetSelectValue(query);
            connect.CloseConnection();

            //assert
            Assert.AreEqual(WaitResult, Convert.ToSingle(result));
        }

        [TestMethod]
        public void GetAllSelectValuesTest()
        {
            //arrange
            string nameDatabase = "supermarket";
            string[] tables = new string[] {"products","products"},
                idFields = new string[] {"id_products","id_products"},
                ids = new string[] {"2","3"};
            string[][] fields = new string[][]
            {
                new string[] {"price_for_one"},
                new string[] {"price_for_one"},
            };

            List<float> WaitResult = new List<float>() { 9.45f, 95.45f };
            float WaitSum = 9.45f + 95.45f;

            //act
            Connection connect = new Connection();
            connect.OpenConnection();
            Administrator_supermarket.Сalculations calc = new Administrator_supermarket.Сalculations();
            List<float> result = calc.GetAllSelectValues( nameDatabase, tables, fields, idFields, ids);
            connect.CloseConnection();
            float sum = 0.0f;
            foreach (var i in result)
                sum += i;


            //assert
            Assert.AreEqual(WaitResult.GetType(), result.GetType());
            Assert.AreEqual(WaitSum,sum);
        }

        [TestMethod]
        public void GetCalcTest()
        {
            //arrange
            List<float> data = new List<float>() { 9.45f, 95.45f, 1.45f};
            string mathDiv = "/",
                   mathMult = "*",
                   mathAdd = "+",
                    mathSub = "-";

            float WaitResultDiv = 9.45f  / 95.45f / 1.45f,
                  WaitResultMult = 9.45f * 95.45f * 1.45f,
                  WaitResultAdd = 9.45f + 95.45f + 1.45f,
                  WaitResultSub = 9.45f - 95.45f - 1.45f;


            //act
            Administrator_supermarket.Сalculations calc = new Administrator_supermarket.Сalculations();
            float resultDiv = calc.GetCalc(data, mathDiv),
                  resultMult = calc.GetCalc(data, mathMult),
                  resultAdd = calc.GetCalc(data, mathAdd),
                  resultSub = calc.GetCalc(data, mathSub);

            //assert
            Assert.AreEqual(WaitResultDiv, resultDiv);
            Assert.AreEqual(WaitResultMult, resultMult);
            Assert.AreEqual(WaitResultAdd, resultAdd);
            Assert.AreEqual(WaitResultSub, resultSub);
        }

        [TestMethod]
        public void GetUpdateQueryTest()
        {
            //arrange
            string nameDatabase = "supermarket",
                   table = "stock",
                   field = "price",
                   idField = "id_stock",
                   id = "1",
                   WaitResult = " UPDATE supermarket.stock AS T1 " +
                              " SET T1.price = 10 " +
                              " WHERE T1.id_stock = 1; ";
            float result = 10.0f;

            //act
            Administrator_supermarket.Сalculations calc = new Administrator_supermarket.Сalculations();
            string query = calc.GetUpdateQuery(nameDatabase, table, field, idField, id, result);

            //assert
            Assert.AreEqual(WaitResult.ToLower(), query.ToLower());
        }

        /*
        [TestMethod]
        public void GetSecondValue_ProductsAndPriceforone_notnullAndDoubleReturned()
        {
            //arrange
            string nameTable = "products";
            string nameField = "price_for_one";

            //act
            Administrator_supermarket.Сalculations calc = new Administrator_supermarket.Сalculations();
            double result = calc.GetSecondValue(nameTable, nameField);

            //assert
            //проверяем не пустое ли значение
            Assert.IsNotNull(result);
            //проверяем соответствует ли значение нужному нам типу
            Assert.IsInstanceOfType(result, typeof(double));
            Assert.AreEqual(typeof(double), result.GetType());
        }*/
    }
}
