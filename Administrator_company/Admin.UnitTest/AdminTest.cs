using Administrator_supermarket;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;


namespace Admin.UnitTest
{
    [TestClass]
    public class AdminTest 
    {

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
            string nameDatabase = "sql7150982",
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
