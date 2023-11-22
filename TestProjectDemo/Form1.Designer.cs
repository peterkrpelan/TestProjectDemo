
namespace TestProjectDemo {
   partial class Form1 {
      /// <summary>
      ///  Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      ///  Clean up any resources being used.
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
      ///  Required method for Designer support - do not modify
      ///  the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent() {
         this.components = new System.ComponentModel.Container();
         this.dataGridView1 = new System.Windows.Forms.DataGridView();
         this.projectidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.abreviationDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.customerDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
         this.button1 = new System.Windows.Forms.Button();
         ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
         this.SuspendLayout();
         // 
         // dataGridView1
         // 
         this.dataGridView1.AutoGenerateColumns = false;
         this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
         this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.projectidDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn,
            this.abreviationDataGridViewTextBoxColumn,
            this.customerDataGridViewTextBoxColumn});
         this.dataGridView1.DataSource = this.bindingSource1;
         this.dataGridView1.Location = new System.Drawing.Point(12, 23);
         this.dataGridView1.Name = "dataGridView1";
         this.dataGridView1.Size = new System.Drawing.Size(515, 153);
         this.dataGridView1.TabIndex = 0;
         this.dataGridView1.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_RowLeave);
         this.dataGridView1.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dataGridView1_UserDeletingRow);
         // 
         // projectidDataGridViewTextBoxColumn
         // 
         this.projectidDataGridViewTextBoxColumn.DataPropertyName = "project_id";
         this.projectidDataGridViewTextBoxColumn.HeaderText = "project_id";
         this.projectidDataGridViewTextBoxColumn.Name = "projectidDataGridViewTextBoxColumn";
         // 
         // nameDataGridViewTextBoxColumn
         // 
         this.nameDataGridViewTextBoxColumn.DataPropertyName = "name";
         this.nameDataGridViewTextBoxColumn.HeaderText = "name";
         this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
         // 
         // abreviationDataGridViewTextBoxColumn
         // 
         this.abreviationDataGridViewTextBoxColumn.DataPropertyName = "abreviation";
         this.abreviationDataGridViewTextBoxColumn.HeaderText = "abreviation";
         this.abreviationDataGridViewTextBoxColumn.Name = "abreviationDataGridViewTextBoxColumn";
         // 
         // customerDataGridViewTextBoxColumn
         // 
         this.customerDataGridViewTextBoxColumn.DataPropertyName = "customer";
         this.customerDataGridViewTextBoxColumn.HeaderText = "customer";
         this.customerDataGridViewTextBoxColumn.Name = "customerDataGridViewTextBoxColumn";
         // 
         // bindingSource1
         // 
         this.bindingSource1.AllowNew = true;
         this.bindingSource1.DataSource = typeof(TestProjectDemo.Model.XmlProjects);
         // 
         // button1
         // 
         this.button1.Location = new System.Drawing.Point(12, 199);
         this.button1.Name = "button1";
         this.button1.Size = new System.Drawing.Size(92, 23);
         this.button1.TabIndex = 1;
         this.button1.Text = "Save Changes";
         this.button1.UseVisualStyleBackColor = true;
         this.button1.Click += new System.EventHandler(this.button1_Click);
         // 
         // Form1
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(544, 228);
         this.Controls.Add(this.button1);
         this.Controls.Add(this.dataGridView1);
         this.Name = "Form1";
         this.Text = "Form1";
         this.Load += new System.EventHandler(this.Form1_Load);
         ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.DataGridView dataGridView1;
      private System.Windows.Forms.DataGridViewTextBoxColumn projectidDataGridViewTextBoxColumn;
      private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
      private System.Windows.Forms.DataGridViewTextBoxColumn abreviationDataGridViewTextBoxColumn;
      private System.Windows.Forms.DataGridViewTextBoxColumn customerDataGridViewTextBoxColumn;
      private System.Windows.Forms.BindingSource bindingSource1;
      private System.Windows.Forms.Button button1;
   }
}

