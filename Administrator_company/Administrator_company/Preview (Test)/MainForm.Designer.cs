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
            this.TestFormTwo.Size = new System.Drawing.Size(96, 23);
            this.TestFormTwo.TabIndex = 2;
            this.TestFormTwo.Text = "Test Form Two";
            this.TestFormTwo.UseVisualStyleBackColor = true;
            this.TestFormTwo.Click += new System.EventHandler(this.TestFormTwo_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(199, 145);
            this.Controls.Add(this.TestFormTwo);
            this.Controls.Add(this.OpenTestForm);
            this.Controls.Add(this.OpenStock);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button OpenStock;
        private System.Windows.Forms.Button OpenTestForm;
        private System.Windows.Forms.Button TestFormTwo;
    }
}