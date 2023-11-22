using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProjectDemo.Model {
   public class XmlProjects : KeyedCollection<string, XmlProject> {

      public event EventHandler<ChangedEventArgs<XmlProject> > xmlProjectChanged;
      protected override string GetKeyForItem(XmlProject item) {
         return item.project_id;
      }

      protected override void InsertItem(int index, XmlProject item) {
         checkItem(item, ChangeTypeEnum.Added);
         base.InsertItem(index, item);
         if (item.state != ChangeTypeEnum.NoChanges) item.state = ChangeTypeEnum.Added;
         item.m_collection = this;  
         onChanged(ChangeTypeEnum.Added, item, null);
      }

      protected override void SetItem(int index, XmlProject item) {
         var _origItem = Items[index];
         checkItem(item, ChangeTypeEnum.Replaced);

         base.SetItem(index, item);

         item.m_collection      = this;
         _origItem.m_collection = null;

         onChanged(ChangeTypeEnum.Replaced, item, _origItem);
      }

      private void checkItem(XmlProject item, ChangeTypeEnum _changeType) {
         if (item.m_collection != null) {
            LogWriter.Default.debug("Throw ArgumentException The item already belongs to a collection. ChangeType {0}", new object[] {_changeType.ToString() });
            throw new ArgumentException("The item already belongs to a collection.");
         }
      }


      protected virtual  void onChanged(ChangeTypeEnum _changeType, XmlProject  newItem, XmlProject oldItem) {
         if(xmlProjectChanged != null) {
            LogWriter.Default.debug("Raise event xmlProjectChanged - {0}", new object[] {_changeType.ToString() });
            xmlProjectChanged(this, new ChangedEventArgs<XmlProject>(_changeType, newItem, oldItem));
         } 

      }
      protected override void RemoveItem(int index) {
         var _removedItem          = Items[index];
         _removedItem.state        = ChangeTypeEnum.Removed;
         _removedItem.m_collection = null;
         onChanged(ChangeTypeEnum.Removed, _removedItem, null);
         //base.RemoveItem(index);
      }
      public virtual void release() {
         var _items = this.Where(x => x.state == ChangeTypeEnum.Removed);
         var ixLst = new List<int>();
         foreach (var item in _items) ixLst.Add(IndexOf(item));

         for (int i = 0; i < ixLst.Count; i++) base.RemoveItem(ixLst[i]);
      }

      protected override void ClearItems() {
         foreach (var it in Items) {
            it.m_collection = null;
         }
         base.ClearItems();
         onChanged(ChangeTypeEnum.Cleared, null, null);
      }

      internal void ChangeKey(XmlProject item, string newKey) {
         base.ChangeItemKey(item, newKey);

      }

   }
}
