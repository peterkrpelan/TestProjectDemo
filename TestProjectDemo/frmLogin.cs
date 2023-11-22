using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestProjectDemo {
   public partial class frmLogin : Form {
      public frmLogin() {
         InitializeComponent();
      }

      private void button1_Click(object sender, EventArgs e) {
         var user = new User(textBox1.Text, txtPassword.Text);
         var _src = Program.dataSource;
         if (_src.connectTo(user)) User.save(textBox1.Text, txtPassword.Text);
         Close();
      }
   }
}
