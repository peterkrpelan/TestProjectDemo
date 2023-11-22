using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TestProjectDemo.Model {
   public interface IStateItem {
      ChangeTypeEnum state { get;  }
   }
   public class XmlProject :INotifyPropertyChanged, IStateItem, IEquatable<XmlProject> {
      private string m_strName;
      private string m_strAbreviation;
      private string m_strCustomer;
      private string m_strPrjID;
      internal XmlProjects m_collection;

      public event PropertyChangedEventHandler PropertyChanged;
      
      public XmlProject() {
         m_strPrjID       = "";
         m_strName        = "";
         m_strAbreviation = "";
         m_strCustomer    = ""; ;
         state            = ChangeTypeEnum.Added;
      }
      public XmlProject(string aProjID) {
         m_strPrjID       = aProjID;
         m_strName        = "";
         m_strAbreviation = "";
         m_strCustomer    = ""; ;
         state            = ChangeTypeEnum.Added;
      }

      public XmlProject(string aProjID, string aName, string abrev, string aCustomer) {
         m_strName        = aName;
         m_strAbreviation = abrev;
         m_strCustomer    = aCustomer;
         m_strPrjID       = aProjID;
         state            = ChangeTypeEnum.NoChanges;
      }
      public string project_id {
         get {
            return m_strPrjID;
         }
         set {
            if(m_strPrjID != value) {
               if (m_collection != null) m_collection.ChangeKey(this, value);
               m_strPrjID = value;
               notifyPropertyChanged();
            }
         }
      }
     
      public string name {
         get { return m_strName; }
         set {
            if (m_strName != value) {
               m_strName = value;
               notifyPropertyChanged();
            }
         }  
      }


      public string abreviation {
         get { return m_strAbreviation; }
         set {
            if (m_strAbreviation != value) {
               m_strAbreviation = value;
               notifyPropertyChanged();
            }
         }
      }

      public string customer {
         get { return m_strCustomer; }
         set {
            if(m_strCustomer != value) {
               m_strCustomer = value;
               notifyPropertyChanged();
            }
         }
      }

      public ChangeTypeEnum state { get; internal set; }

      private void notifyPropertyChanged([CallerMemberName] String propertyName = "") {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
         if (state == ChangeTypeEnum.NoChanges) state = ChangeTypeEnum.Replaced;

      }

      public override bool Equals(object obj) {
         if (obj == null) return false;
         return Equals(obj as XmlProject);
      }

      public override int GetHashCode() {
         var _t = Tuple.Create<string, string, string, string>(m_strPrjID, m_strName, m_strAbreviation, m_strCustomer);
         return _t.GetHashCode();
      }

      public static bool operator == (XmlProject project1, XmlProject project2) {
         if(((object)project1) == null || ((object)project2) == null) return Object.Equals(project1, project2);
         return project1.Equals(project2);
      }

      public override string ToString() {
         return String.Format("[{0}, {1}, {2}, {3}]", m_strPrjID, m_strName, m_strAbreviation, m_strCustomer);
      }

      public static bool operator != (XmlProject project1, XmlProject project2) {
         if (((object)project1) == null || ((object)project2) == null) return !Object.Equals(project1, project2); ;
         return !(project1.Equals(project2));

      }

      public bool Equals(XmlProject other) {
         if (other == null) return false;
         if (m_strPrjID == other.project_id && m_strName == other.name && m_strAbreviation == other.abreviation && m_strCustomer == other.customer) return true;
         return false;
      }
   }

  
}
