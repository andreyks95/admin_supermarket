using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//Дальше подключаемые 
using MySql.Data.MySqlClient;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace Administrator_company.Preview__Test_
{
    public partial class TestFormTwo : Form
    {
        public TestFormTwo()
        {
            InitializeComponent();
        }
        public MySqlConnection connection = new MySqlConnection("datasource=localhost; port=3306; username = root; password = andrey_1a6c2b");
        private void TestFormTwo_Load(object sender, EventArgs e)
        {
            FillDataGridView();
        }

        public void FillDataGridView()
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM supermarket.info", connection); //Создаём запрос для выполнения команды
            MySqlDataAdapter adapter = new MySqlDataAdapter(command); //Выполняем команду

            /*//connection.Open(); //открыть соединение
            DataSet ds = new DataSet(); //создать новый DataSet
            adapter.Fill(ds, "Table");//nameTable); //заполнить 
                                      //Подключение таблицы
            dataGridView1.DataSource = ds.Tables["Table"];*/
        

            //Для отображения в таблице
            DataTable table = new DataTable(); //Создаём таблицу
            adapter.Fill(table); //Вставляем данные при выполнении команды в таблицу

            dataGridView1.RowTemplate.Height = 60; //высота строк
            dataGridView1.AllowUserToAddRows = false; //нельзя пользователю добавлять самому строки


            dataGridView1.DataSource = table; //подключаем заполненную таблицу и отображаем

            //Для отображения картинки в DataGridView
            DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
            imgCol = (DataGridViewImageColumn)dataGridView1.Columns[6];
            imgCol.ImageLayout = DataGridViewImageCellLayout.Stretch;

            //Как будет отображаться таблица
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; //Растягивать таблицу (колонки) под окно dataGridView
            

        }

        private void BTN_CHOOSE_IMAGE_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Choose Image(*.JPG; *.PNG, *.GIF) | *.jpg; *.png; *.gif";

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFile.FileName); // если пользователь выбрал картинку то вставляем в pictureBox
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {

           // Byte[] img = (Byte[])dataGridView1.CurrentRow.Cells[6].Value;
           // MemoryStream ms = new MemoryStream(img);
           // pictureBox1.Image = Image.FromStream(ms);
           //или можно так:

            CurrentRowCellsIMG(6, pictureBox1, dataGridView1);
            //CurrentRowCellsTEXT(0, textBox1, dataGridView1); 
            TextBox[] textBoxs = {textBox1, textBox2, textBox3, textBox4, textBox5, textBox6};
            CurrentRowCellsTEXT(textBoxs, dataGridView1);
        }


        public void CurrentRowCellsIMG(int number, PictureBox pictureBox, DataGridView dataGridView)
        {
            if (dataGridView.CurrentRow.Cells[number].Value  == DBNull.Value)
            {
                pictureBox.Image = null;
            }
            else
            {
                try
                {
                    Byte[] img = (Byte[]) dataGridView.CurrentRow.Cells[number].Value;
                    MemoryStream ms = new MemoryStream(img);
                    pictureBox.Image = Image.FromStream(ms);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                        
            }
        }

        public void CurrentRowCellsTEXT(int number, TextBox textBox, DataGridView dataGridView)
        {
            textBox.Text = dataGridView.CurrentRow.Cells[number].Value.ToString();
        }

        public void CurrentRowCellsTEXT(TextBox[] textBoxs, DataGridView dataGridView)
        {
            for (var i=0; i< textBoxs.Length; i++)
            {
                CurrentRowCellsTEXT(i, textBoxs[i], dataGridView);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            CurrentRowCellsIMG(6, pictureBox1, dataGridView1);
            TextBox[] textBoxs = { textBox1, textBox2, textBox3, textBox4, textBox5, textBox6 };
            CurrentRowCellsTEXT(textBoxs, dataGridView1);
        }
    }
}
