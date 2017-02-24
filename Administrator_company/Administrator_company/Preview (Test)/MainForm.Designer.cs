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
            this.SuspendLayout();
            // 
            // OpenStock
            // 
            this.OpenStock.Location = new System.Drawing.Point(12, 12);
            this.OpenStock.Name = "OpenStock";
            this.OpenStock.Size = new System.Drawing.Size(96, 23);
            this.OpenStock.TabIndex = 0;
            this.OpenStock.Text = "Склад";
            this.OpenStock.UseVisualStyleBackColor = true;
            this.OpenStock.Click += new System.EventHandler(this.OpenStock_Click);
            // 
            // OpenTestForm
            // 
            this.OpenTestForm.Location = new System.Drawing.Point(12, 55);
            this.OpenTestForm.Name = "OpenTestForm";
            this.OpenTestForm.Size = new System.Drawing.Size(96, 23);
            this.OpenTestForm.TabIndex = 1;
            this.OpenTestForm.Text = "Test Form";
            this.OpenTestForm.UseVisualStyleBackColor = true;
            this.OpenTestForm.Click += new System.EventHandler(this.OpenTestForm_Click);
            // 
            // TestFormTwo
            // 
            this.TestFormTwo.Location = new System.Drawing.Point(12, 110);
            this.TestFormTwo.Name = "TestFormTwo";
            this.TestFormTwo.Size = new System.Drawing.Size(96, 38);
            this.TestFormTwo.TabIndex = 2;
            this.TestFormTwo.Text = "Информация о сотрудниках";
            this.TestFormTwo.UseVisualStyleBackColor = true;
            this.TestFormTwo.Click += new System.EventHandler(this.TestFormTwo_Click);
            // 
            // PositionButton
            // 
            this.PositionButton.Location = new System.Drawing.Point(12, 167);
            this.PositionButton.Name = "PositionButton";
            this.PositionButton.Size = new System.Drawing.Size(96, 23);
            this.PositionButton.TabIndex = 3;
            this.PositionButton.Text = "Должности";
            this.PositionButton.UseVisualStyleBackColor = true;
            this.PositionButton.Click += new System.EventHandler(this.PositionButton_Click);
            // 
            // EmployeesButton
            // 
            this.EmployeesButton.Location = new System.Drawing.Point(12, 206);
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
            this.label1.Location = new System.Drawing.Point(114, 211);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "подчинённая";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(233, 260);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.EmployeesButton);
            this.Controls.Add(this.PositionButton);
            this.Controls.Add(this.TestFormTwo);
            this.Controls.Add(this.OpenTestForm);
            this.Controls.Add(this.OpenStock);
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
    }
}