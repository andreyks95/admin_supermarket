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
        
        //При загрузке отобразить таблицу
        private void TestFormTwo_Load(object sender, EventArgs e)
        {
            FillDataGridView();
        }

        //Заполняем DataGridView и корректируем её
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

        //для выбора изображения 
        private void BTN_CHOOSE_IMAGE_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Choose Image(*.JPG; *.PNG, *.GIF) | *.jpg; *.png; *.gif";

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFile.FileName); // если пользователь выбрал картинку то вставляем в pictureBox
            }
        }

        //Когда пользователь будет кликать мышью по нужной строке, то в textBox-ах и pictuerBox будут отображаться данные текущей строки
        private void dataGridView1_Click(object sender, EventArgs e)
        {
            //отображаем текущую картинку из DataGridView в pictureBox
            CurrentRowCellsIMG(6, pictureBox1, dataGridView1);
            //Массив куда будут (в textBox-ы) отображаться значения текущей строки из DataGridView
            TextBox[] textBoxs = {textBox1, textBox2, textBox3, textBox4, textBox5, textBox6};
            //отображаем все текстовые записи из DataGridView в textBox-ы
            CurrentRowCellsTEXT(textBoxs, dataGridView1);
        }

        //Когда пользователь будет нажимать стрелками клавиш "вверх" и "вниз" то будут в textBox-ах и pictuerBox отображаться данные текущей строки
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            //отображаем текущую картинку из DataGridView в pictureBox
            CurrentRowCellsIMG(6, pictureBox1, dataGridView1);
            //Массив куда будут (в textBox-ы) отображаться значения текущей строки из DataGridView
            TextBox[] textBoxs = { textBox1, textBox2, textBox3, textBox4, textBox5, textBox6 };
            //отображаем все текстовые записи из DataGridView в textBox-ы
            CurrentRowCellsTEXT(textBoxs, dataGridView1);
        }

        /// <summary>
        /// Отображает из текущей ячейки строи DataGridView картинку в PicureBox
        /// </summary>
        /// <param name="number">номер столбца (ячейки) DataGridView в котором содержиться картинка</param>
        /// <param name="pictureBox">pictureBox куда нужно вставить картинку</param>
        /// <param name="dataGridView">текущий dataGridView</param>
        public void CurrentRowCellsIMG(int number, PictureBox pictureBox, DataGridView dataGridView)
        {
            //Если ячейка пустая, то в pictureBox ничего не отображать
            if (dataGridView.CurrentRow.Cells[number].Value  == DBNull.Value)
            {
                pictureBox.Image = null;
            }
            else
            {
                try
                {
                    Byte[] img = (Byte[]) dataGridView.CurrentRow.Cells[number].Value; //Получаем изображание
                    MemoryStream ms = new MemoryStream(img);
                    pictureBox.Image = Image.FromStream(ms); // вставляем в pictureBox это изображение
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                        
            }
        }
        /// <summary>
        /// Помещает в textBox значение с ячейки текущей строки DataGridView
        /// </summary>
        /// <param name="number">Номер столбца (ячейки) DataGridView в которой содержиться текстовое значение</param>
        /// <param name="textBox">textBox куда нужно вставить значение</param>
        /// <param name="dataGridView">текущий dataGridView</param>
        public void CurrentRowCellsTEXT(int number, TextBox textBox, DataGridView dataGridView)
        {
            textBox.Text = dataGridView.CurrentRow.Cells[number].Value.ToString();
        }
        /// <summary>
        /// Помещает во ВСЕ textBox-ы значения из текущей строки DataGridView
        /// </summary>
        /// <param name="textBoxs">Массив textBox-ов куда нужно вставить значения</param>
        /// <param name="dataGridView">текущий dataGridView</param>
        public void CurrentRowCellsTEXT(TextBox[] textBoxs, DataGridView dataGridView)
        {

            for (var i=0; i< textBoxs.Length; i++)
            {
                //передаёт номер ячейки и сам textBox куда нужно вставить значения из строки DataGridView
                CurrentRowCellsTEXT(i, textBoxs[i], dataGridView); 
            }
        }

    }
}
