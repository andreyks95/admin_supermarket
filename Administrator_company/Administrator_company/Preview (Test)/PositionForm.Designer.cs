namespace Administrator_company.Preview__Test_
{
    partial class PositionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Delete = new System.Windows.Forms.Button();
            this.Update = new System.Windows.Forms.Button();
            this.Clear = new System.Windows.Forms.Button();
            this.Insert = new System.Windows.Forms.Button();
            this.Find = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.PreviousRecordButton = new System.Windows.Forms.Button();
            this.NextRecordButton = new System.Windows.Forms.Button();
            this.LastRecordButton = new System.Windows.Forms.Button();
            this.FirstRecordButton = new System.Windows.Forms.Button();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "ИД";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Должность";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Зарплата";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(97, 8);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(148, 20);
            this.textBox1.TabIndex = 3;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(97, 42);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(148, 20);
            this.textBox2.TabIndex = 4;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(97, 75);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(148, 20);
            this.textBox3.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Delete);
            this.panel1.Controls.Add(this.Update);
            this.panel1.Controls.Add(this.Clear);
            this.panel1.Controls.Add(this.Insert);
            this.panel1.Controls.Add(this.Find);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textBox3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(344, 491);
            this.panel1.TabIndex = 6;
            // 
            // Delete
            // 
            this.Delete.Location = new System.Drawing.Point(251, 117);
            this.Delete.Name = "Delete";
            this.Delete.Size = new System.Drawing.Size(75, 23);
            this.Delete.TabIndex = 16;
            this.Delete.Text = "Удалить";
            this.Delete.UseVisualStyleBackColor = true;
            this.Delete.Click += new System.EventHandler(this.Delete_Click);
            // 
            // Update
            // 
            this.Update.Location = new System.Drawing.Point(170, 117);
            this.Update.Name = "Update";
            this.Update.Size = new System.Drawing.Size(75, 23);
            this.Update.TabIndex = 15;
            this.Update.Text = "Обновить";
            this.Update.UseVisualStyleBackColor = true;
            this.Update.Click += new System.EventHandler(this.Update_Click);
            // 
            // Clear
            // 
            this.Clear.Location = new System.Drawing.Point(7, 117);
            this.Clear.Name = "Clear";
            this.Clear.Size = new System.Drawing.Size(75, 23);
            this.Clear.TabIndex = 13;
            this.Clear.Text = "Очистить";
            this.Clear.UseVisualStyleBackColor = true;
            this.Clear.Click += new System.EventHandler(this.Clear_Click);
            // 
            // Insert
            // 
            this.Insert.Location = new System.Drawing.Point(88, 117);
            this.Insert.Name = "Insert";
            this.Insert.Size = new System.Drawing.Size(75, 23);
            this.Insert.TabIndex = 14;
            this.Insert.Text = "Вставить";
            this.Insert.UseVisualStyleBackColor = true;
            this.Insert.Click += new System.EventHandler(this.Insert_Click);
            // 
            // Find
            // 
            this.Find.Location = new System.Drawing.Point(251, 5);
            this.Find.Name = "Find";
            this.Find.Size = new System.Drawing.Size(86, 25);
            this.Find.TabIndex = 6;
            this.Find.Text = "Найти";
            this.Find.UseVisualStyleBackColor = true;
            this.Find.Click += new System.EventHandler(this.Find_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.PreviousRecordButton);
            this.panel2.Controls.Add(this.NextRecordButton);
            this.panel2.Controls.Add(this.LastRecordButton);
            this.panel2.Controls.Add(this.FirstRecordButton);
            this.panel2.Controls.Add(this.textBoxSearch);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(344, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(600, 75);
            this.panel2.TabIndex = 7;
            // 
            // PreviousRecordButton
            // 
            this.PreviousRecordButton.Location = new System.Drawing.Point(311, 40);
            this.PreviousRecordButton.Name = "PreviousRecordButton";
            this.PreviousRecordButton.Size = new System.Drawing.Size(147, 23);
            this.PreviousRecordButton.TabIndex = 23;
            this.PreviousRecordButton.Text = "Предыдущая запись";
            this.PreviousRecordButton.UseVisualStyleBackColor = true;
            this.PreviousRecordButton.Click += new System.EventHandler(this.PreviousRecordButton_Click);
            // 
            // NextRecordButton
            // 
            this.NextRecordButton.Location = new System.Drawing.Point(147, 40);
            this.NextRecordButton.Name = "NextRecordButton";
            this.NextRecordButton.Size = new System.Drawing.Size(147, 23);
            this.NextRecordButton.TabIndex = 22;
            this.NextRecordButton.Text = "Следующая запись";
            this.NextRecordButton.UseVisualStyleBackColor = true;
            this.NextRecordButton.Click += new System.EventHandler(this.NextRecordButton_Click);
            // 
            // LastRecordButton
            // 
            this.LastRecordButton.Location = new System.Drawing.Point(473, 40);
            this.LastRecordButton.Name = "LastRecordButton";
            this.LastRecordButton.Size = new System.Drawing.Size(112, 23);
            this.LastRecordButton.TabIndex = 24;
            this.LastRecordButton.Text = "Последняя запись";
            this.LastRecordButton.UseVisualStyleBackColor = true;
            this.LastRecordButton.Click += new System.EventHandler(this.LastRecordButton_Click);
            // 
            // FirstRecordButton
            // 
            this.FirstRecordButton.Location = new System.Drawing.Point(13, 40);
            this.FirstRecordButton.Name = "FirstRecordButton";
            this.FirstRecordButton.Size = new System.Drawing.Size(112, 23);
            this.FirstRecordButton.TabIndex = 21;
            this.FirstRecordButton.Text = "Первая запись";
            this.FirstRecordButton.UseVisualStyleBackColor = true;
            this.FirstRecordButton.Click += new System.EventHandler(this.FirstRecordButton_Click);
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Location = new System.Drawing.Point(185, 11);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(400, 20);
            this.textBoxSearch.TabIndex = 20;
            this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(162, 13);
            this.label6.TabIndex = 25;
            this.label6.Text = "Введите значение для поиска:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(344, 75);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(600, 416);
            this.dataGridView1.TabIndex = 8;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            this.dataGridView1.Click += new System.EventHandler(this.dataGridView1_Click);
            // 
            // PositionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 491);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(960, 200);
            this.Name = "PositionForm";
            this.Text = "PositionForm";
            this.Load += new System.EventHandler(this.PositionForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button Find;
        private System.Windows.Forms.Button Delete;
        private System.Windows.Forms.Button Update;
        private System.Windows.Forms.Button Clear;
        private System.Windows.Forms.Button Insert;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button PreviousRecordButton;
        private System.Windows.Forms.Button NextRecordButton;
        private System.Windows.Forms.Button LastRecordButton;
        private System.Windows.Forms.Button FirstRecordButton;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Label label6;
    }
}