using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TestProjectDemo.Model;

namespace TestProjectDemo {

   public interface IBaseViewModel {
      void load();
      void saveChanges();
      void add(string[] _array);
      void remove(int index);
      void modify(int index, string[] _array);
   }

   public interface IBaseViewModel<T> : IBaseViewModel {
      void add(T item);
      void modify(int index, T item);
      void remove(T item);
   }

   public class XmlProjectsVM  :  IBaseViewModel<XmlProject>,   IDisposable {
      private bool disposedValue;
      private IDataSource<XmlProject> m_dataSource;
      private XmlProjects m_projects;
      public XmlProjectsVM() {
         m_dataSource = Program.dataSource  as IDataSource<XmlProject>;
         if(m_dataSource == null) {
            LogWriter.Default.error("Bad Data Source !!!");
            throw new ArgumentNullException();
         }
         m_projects = new XmlProjects();
      }

      protected virtual void Dispose(bool disposing) {
         if (!disposedValue) {
            if (disposing) {
               ((IDisposable)m_dataSource).Dispose();
               m_dataSource = null;
               // TODO: dispose managed state (managed objects)
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            disposedValue = true;
         }
      }

      // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
      // ~XmlProjectsVM()
      // {
      //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
      //     Dispose(disposing: false);
      // }

      public BindingList<XmlProject> projects { 
         get {
            return new BindingList<XmlProject>(m_projects.Where( x=> x.state != ChangeTypeEnum.Removed).ToList());
         }
      }

      public void Dispose() {
         // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
         Dispose(disposing: true);
         GC.SuppressFinalize(this);
      }

      public void add(XmlProject row) {
         if (row == null) return;
         m_projects.Add(row);
      }

      public void load() {
         LogWriter.Default.debug("Loading data from Data Source....");
         if (m_projects.Count > 0) m_projects.Clear();
         var srclist = m_dataSource.getDataFromSource();

         LogWriter.Default.debug("Fill from data Source");
         foreach (var item in srclist) {
            m_projects.Add(item);
         }

         LogWriter.Default.log("Loaded {0} items", new object[] { m_projects.Count });
      }

      public void saveChanges() {
         var updRows = m_projects.Where(x => x.state != ChangeTypeEnum.NoChanges).ToArray();
         if (updRows.Length == 0) LogWriter.Default.log("There are no changes ....");
         else {
            var icnt = m_dataSource.updateDataSource(updRows);
            LogWriter.Default.log("There was updated {0} items", new object[] { icnt });
            m_projects.release();
         }
      }

      public void  add(string[] _array) {
         var _newRow = new XmlProject(_array[0], _array[1], _array[2], _array[3]) { state = ChangeTypeEnum.Added };
         m_projects.Add(_newRow);
         LogWriter.Default.log("Added new row {0}", new object[] { _array.ToString() });
      }
      public void remove(int index) {
         m_projects.RemoveAt(index);
         LogWriter.Default.log("Removing row at position {0}", new object[] { index });
      }

      public void modify(int index, string[] _array) {
         var _updRow  = new XmlProject(_array[0], _array[1], _array[2], _array[3]) { state = ChangeTypeEnum.Replaced };
         var _origRow = m_projects[index];

         if (_updRow != _origRow) m_projects[index] = _updRow;
      }

      public void modify(int index, XmlProject item) {
         var _origRow = m_projects[index];
         if(_origRow != item) m_projects[index] = item;
      }

      public void remove(XmlProject item) {
         m_projects.Remove(item.project_id);
      }
   }
}
