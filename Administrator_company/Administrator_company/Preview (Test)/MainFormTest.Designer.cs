﻿namespace Administrator_company
{
    partial class MainFormTest
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
            this.OpenTestForm = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // OpenTestForm
            // 
            this.OpenTestForm.Location = new System.Drawing.Point(12, 12);
            this.OpenTestForm.Name = "OpenTestForm";
            this.OpenTestForm.Size = new System.Drawing.Size(96, 23);
            this.OpenTestForm.TabIndex = 3;
            this.OpenTestForm.Text = "Test Form";
            this.OpenTestForm.UseVisualStyleBackColor = true;
            this.OpenTestForm.Click += new System.EventHandler(this.OpenTestForm_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 60);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(121, 57);
            this.button1.TabIndex = 4;
            this.button1.Text = "Тестовая форма\r\nСотрудники";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainFormTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.OpenTestForm);
            this.Name = "MainFormTest";
            this.Text = "MainFormTest";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button OpenTestForm;
        private System.Windows.Forms.Button button1;
    }
}