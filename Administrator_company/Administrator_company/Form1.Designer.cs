namespace Administrator_supermarket
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.OpenConnection_Button = new System.Windows.Forms.Button();
            this.CloseConnection_Button = new System.Windows.Forms.Button();
            this.TableAdministrator = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // OpenConnection_Button
            // 
            this.OpenConnection_Button.Location = new System.Drawing.Point(12, 12);
            this.OpenConnection_Button.Name = "OpenConnection_Button";
            this.OpenConnection_Button.Size = new System.Drawing.Size(207, 23);
            this.OpenConnection_Button.TabIndex = 0;
            this.OpenConnection_Button.Text = "Open Connection";
            this.OpenConnection_Button.UseVisualStyleBackColor = true;
            this.OpenConnection_Button.Click += new System.EventHandler(this.OpenConnection_Button_Click);
            // 
            // CloseConnection_Button
            // 
            this.CloseConnection_Button.Location = new System.Drawing.Point(12, 41);
            this.CloseConnection_Button.Name = "CloseConnection_Button";
            this.CloseConnection_Button.Size = new System.Drawing.Size(207, 23);
            this.CloseConnection_Button.TabIndex = 1;
            this.CloseConnection_Button.Text = "Close Connection";
            this.CloseConnection_Button.UseVisualStyleBackColor = true;
            this.CloseConnection_Button.Click += new System.EventHandler(this.CloseConnection_Button_Click);
            // 
            // TableAdministrator
            // 
            this.TableAdministrator.Location = new System.Drawing.Point(12, 80);
            this.TableAdministrator.Name = "TableAdministrator";
            this.TableAdministrator.Size = new System.Drawing.Size(113, 23);
            this.TableAdministrator.TabIndex = 2;
            this.TableAdministrator.Text = "Администратор";
            this.TableAdministrator.UseVisualStyleBackColor = true;
            this.TableAdministrator.Click += new System.EventHandler(this.TableAdministrator_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 109);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Отдел";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 138);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(113, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Сотрудники";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 167);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(113, 23);
            this.button3.TabIndex = 5;
            this.button3.Text = "Продукты";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(12, 196);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(113, 25);
            this.button4.TabIndex = 6;
            this.button4.Text = "Склад";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(262, 261);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.TableAdministrator);
            this.Controls.Add(this.CloseConnection_Button);
            this.Controls.Add(this.OpenConnection_Button);
            this.Name = "Form1";
            this.Text = "АРМ администратора продуктового супермаркета";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button OpenConnection_Button;
        private System.Windows.Forms.Button CloseConnection_Button;
        private System.Windows.Forms.Button TableAdministrator;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}

