﻿
namespace TestProjectDemo {
   partial class frmLogin {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing) {
         if (disposing && (components != null)) {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent() {
         this.textBox1 = new System.Windows.Forms.TextBox();
         this.label1 = new System.Windows.Forms.Label();
         this.txtPassword = new System.Windows.Forms.TextBox();
         this.label2 = new System.Windows.Forms.Label();
         this.button1 = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // textBox1
         // 
         this.textBox1.Location = new System.Drawing.Point(83, 17);
         this.textBox1.Name = "textBox1";
         this.textBox1.Size = new System.Drawing.Size(131, 20);
         this.textBox1.TabIndex = 0;
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(24, 20);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(58, 13);
         this.label1.TabIndex = 1;
         this.label1.Text = "User name";
         // 
         // txtPassword
         // 
         this.txtPassword.Location = new System.Drawing.Point(83, 43);
         this.txtPassword.Name = "txtPassword";
         this.txtPassword.PasswordChar = '*';
         this.txtPassword.Size = new System.Drawing.Size(131, 20);
         this.txtPassword.TabIndex = 2;
         this.txtPassword.UseSystemPasswordChar = true;
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Location = new System.Drawing.Point(26, 50);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(56, 13);
         this.label2.TabIndex = 3;
         this.label2.Text = "Paswword";
         // 
         // button1
         // 
         this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
         this.button1.Location = new System.Drawing.Point(99, 76);
         this.button1.Name = "button1";
         this.button1.Size = new System.Drawing.Size(64, 20);
         this.button1.TabIndex = 4;
         this.button1.Text = "Ok";
         this.button1.UseVisualStyleBackColor = true;
         this.button1.Click += new System.EventHandler(this.button1_Click);
         // 
         // frmLogin
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(243, 105);
         this.Controls.Add(this.button1);
         this.Controls.Add(this.label2);
         this.Controls.Add(this.txtPassword);
         this.Controls.Add(this.label1);
         this.Controls.Add(this.textBox1);
         this.Name = "frmLogin";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Log on";
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.TextBox textBox1;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.TextBox txtPassword;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Button button1;
   }
}