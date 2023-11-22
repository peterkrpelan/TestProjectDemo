using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

using TestProjectDemo.Model;

namespace TestProjectDemo {

   public interface IDataSource {
      bool connectTo(IUser aUser);

   }
   public interface IDataSource<T> : IDataSource {
      int updateDataSource(IList<T> items);
      IEnumerable<T> getDataFromSource(Func<T, bool> func = null);
   }

   public class ProjXmlDataSource : IDataSource, IDataSource<XmlProject>, IDisposable {
      
      private string m_strFpath;
      private bool disposedValue;

      public ProjXmlDataSource() {
         m_strFpath = ConfigurationManager.AppSettings["DataSource"];
      }
      public bool connectTo(IUser aUser) {
         if (String.IsNullOrWhiteSpace(aUser.password)) return false;
         return System.IO.File.Exists(m_strFpath);
      }


      public IEnumerable<XmlProject> getDataFromSource(Func<XmlProject, bool> func = null) {
         LogWriter.Default.debug("Before open {0}, Params: ", new string[] { m_strFpath });
         try {
            using (var _reader = XmlReader.Create(m_strFpath)) {
               XElement xml = XElement.Load(m_strFpath);
               var _lstProj = xml.Elements("project").Select(
                  e => new XmlProject(e.Attribute("id").Value, e.Element("name").Value, e.Element("abbreviation").Value, e.Element("customer").Value));

               LogWriter.Default.log("There was read {0}", new object[] { _lstProj.Count() });

               if (func != null) return _lstProj.Where(func);
               else return _lstProj;
            }
         }
         catch (Exception ex) {
            LogWriter.Default.error("Can't read {0}. Error {1}", new object[] { m_strFpath, ex.Message });
            throw ex;
         }
      }

      public int updateDataSource(IList<XmlProject> _changedItems) {
         LogWriter.Default.debug("Starting update DataSource {0} items", new object[]{ _changedItems.Count });
         var _keys = _changedItems.Where(x =>x.state == ChangeTypeEnum.Removed || x.state == ChangeTypeEnum.Replaced).Select(e => e.project_id).ToHashSet<string>();
         try {
            List<XmlProject> _unchanged = null; 
            using (var _reader = XmlReader.Create(m_strFpath)) {
                XElement xml   = XElement.Load(_reader);
               _unchanged = xml.Elements("project").Where(x => !_keys.Contains(x.Attribute("id").Value)).Select(
                  e => new XmlProject(e.Attribute("id").Value, e.Element("name").Value, e.Element("abbreviation").Value, e.Element("customer").Value)).ToList();
               LogWriter.Default.debug("Read {0} unchanged items ", new object[] { _unchanged.Count });
            }
                        
            _unchanged.AddRange(_changedItems.Where( x=> x.state != ChangeTypeEnum.Removed));
            _unchanged.Sort((x, y) => String.Compare(x.project_id, y.project_id, true));

            XElement xwProj = new XElement("projects");
            foreach (var item in _unchanged) {
               xwProj.Add(
                  new XElement("project", new XAttribute("id", item.project_id),
                  new XElement("name", item.name),
                  new XElement("abbreviation", item.abreviation),
                  new XElement("customer", item.customer)
                  ));
            }
            xwProj.Save(m_strFpath);
            return _changedItems.Count;
         }
         catch (Exception ex) {
            LogWriter.Default.error("Can't save to {0}. Error {1}", new string[] { m_strFpath, ex.Message });
            return 0;
         }
         
      }

      protected virtual void Dispose(bool disposing) {
         if (!disposedValue) {
            if (disposing) {
               // TODO: dispose managed state (managed objects)
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            disposedValue = true;
         }
      }

      // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
      // ~ProjXmlDataSource()
      // {
      //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
      //     Dispose(disposing: false);
      // }

      public void Dispose() {
         // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
         Dispose(disposing: true);
         GC.SuppressFinalize(this);
      }
   }
}
