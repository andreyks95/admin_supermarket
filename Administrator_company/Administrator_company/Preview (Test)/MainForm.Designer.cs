namespace Administrator_company.Preview__Test_
{
    partial class MainForm
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
            this.OpenStock = new System.Windows.Forms.Button();
            this.OpenTestForm = new System.Windows.Forms.Button();
            this.TestFormTwo = new System.Windows.Forms.Button();
            this.PositionButton = new System.Windows.Forms.Button();
            this.EmployeesButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ProductsButton = new System.Windows.Forms.Button();
            this.stock = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // OpenStock
            // 
            this.OpenStock.Location = new System.Drawing.Point(117, 12);
            this.OpenStock.Name = "OpenStock";
            this.OpenStock.Size = new System.Drawing.Size(96, 23);
            this.OpenStock.TabIndex = 0;
            this.OpenStock.Text = "Склад";
            this.OpenStock.UseVisualStyleBackColor = true;
            this.OpenStock.Click += new System.EventHandler(this.OpenStock_Click);
            // 
            // OpenTestForm
            // 
            this.OpenTestForm.Location = new System.Drawing.Point(117, 53);
            this.OpenTestForm.Name = "OpenTestForm";
            this.OpenTestForm.Size = new System.Drawing.Size(96, 23);
            this.OpenTestForm.TabIndex = 1;
            this.OpenTestForm.Text = "Test Form";
            this.OpenTestForm.UseVisualStyleBackColor = true;
            this.OpenTestForm.Click += new System.EventHandler(this.OpenTestForm_Click);
            // 
            // TestFormTwo
            // 
            this.TestFormTwo.Location = new System.Drawing.Point(12, 13);
            this.TestFormTwo.Name = "TestFormTwo";
            this.TestFormTwo.Size = new System.Drawing.Size(96, 38);
            this.TestFormTwo.TabIndex = 2;
            this.TestFormTwo.Text = "Информация о сотрудниках";
            this.TestFormTwo.UseVisualStyleBackColor = true;
            this.TestFormTwo.Click += new System.EventHandler(this.TestFormTwo_Click);
            // 
            // PositionButton
            // 
            this.PositionButton.Location = new System.Drawing.Point(12, 70);
            this.PositionButton.Name = "PositionButton";
            this.PositionButton.Size = new System.Drawing.Size(96, 23);
            this.PositionButton.TabIndex = 3;
            this.PositionButton.Text = "Должности";
            this.PositionButton.UseVisualStyleBackColor = true;
            this.PositionButton.Click += new System.EventHandler(this.PositionButton_Click);
            // 
            // EmployeesButton
            // 
            this.EmployeesButton.Location = new System.Drawing.Point(12, 109);
            this.EmployeesButton.Name = "EmployeesButton";
            this.EmployeesButton.Size = new System.Drawing.Size(96, 23);
            this.EmployeesButton.TabIndex = 4;
            this.EmployeesButton.Text = "Сотрудники";
            this.EmployeesButton.UseVisualStyleBackColor = true;
            this.EmployeesButton.Click += new System.EventHandler(this.EmployeesButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(114, 109);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "подчинённая";
            // 
            // ProductsButton
            // 
            this.ProductsButton.Location = new System.Drawing.Point(12, 147);
            this.ProductsButton.Name = "ProductsButton";
            this.ProductsButton.Size = new System.Drawing.Size(96, 23);
            this.ProductsButton.TabIndex = 6;
            this.ProductsButton.Text = "Продукты";
            this.ProductsButton.UseVisualStyleBackColor = true;
            this.ProductsButton.Click += new System.EventHandler(this.ProductsButton_Click);
            // 
            // stock
            // 
            this.stock.Location = new System.Drawing.Point(12, 185);
            this.stock.Name = "stock";
            this.stock.Size = new System.Drawing.Size(96, 23);
            this.stock.TabIndex = 7;
            this.stock.Text = "Склад";
            this.stock.UseVisualStyleBackColor = true;
            this.stock.Click += new System.EventHandler(this.stock_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(114, 190);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "подчинённая";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(234, 231);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.stock);
            this.Controls.Add(this.ProductsButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.EmployeesButton);
            this.Controls.Add(this.PositionButton);
            this.Controls.Add(this.TestFormTwo);
            this.Controls.Add(this.OpenTestForm);
            this.Controls.Add(this.OpenStock);
            this.MinimumSize = new System.Drawing.Size(250, 270);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OpenStock;
        private System.Windows.Forms.Button OpenTestForm;
        private System.Windows.Forms.Button TestFormTwo;
        private System.Windows.Forms.Button PositionButton;
        private System.Windows.Forms.Button EmployeesButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ProductsButton;
        private System.Windows.Forms.Button stock;
        private System.Windows.Forms.Label label2;
    }
}