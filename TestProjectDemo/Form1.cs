using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using TestProjectDemo.Model;

namespace TestProjectDemo {
   public partial class Form1 : Form {

      private IBaseViewModel<XmlProject> m_projVm;
      int m_rowsCnt;
      public Form1() {
         InitializeComponent();
         m_projVm = new XmlProjectsVM();
         bindingSource1.DataSource = ((XmlProjectsVM)m_projVm).projects;
      }

      private void button1_Click(object sender, EventArgs e) {
         m_projVm.saveChanges();
      }


      private void Form1_Load(object sender, EventArgs e) {
         m_projVm.load();
         bindingSource1.DataSource = ((XmlProjectsVM)m_projVm).projects;
         m_rowsCnt = bindingSource1.Count;
      }

      private void dataGridView1_RowLeave(object sender, DataGridViewCellEventArgs e) {
         if (m_rowsCnt > 0  && dataGridView1.Rows.Count  > m_rowsCnt +1) {
            var r = dataGridView1.Rows[bindingSource1.Count ];
            if (r.IsNewRow) {
               m_projVm.add((XmlProject)bindingSource1.CurrencyManager.List[bindingSource1.Count -1]);
               m_rowsCnt = bindingSource1.Count;
            }
         }
      }

      private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e) {
         m_projVm.remove((XmlProject)bindingSource1.Current);
         m_rowsCnt = bindingSource1.Count - 1;
      }
   }
}
