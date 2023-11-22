using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProjectDemo.Model {
   public class ChangedEventArgs<T> : EventArgs {
         private T _changedItem;
         private ChangeTypeEnum _changeType;
         private T _replacedWith;

         public T changedItem { get { return _changedItem; } }
         public ChangeTypeEnum chhangeType { get { return _changeType; } }
         public T replacedWith { get { return _replacedWith; } }

         public ChangedEventArgs(ChangeTypeEnum change,  T item, T replacement){
            _changeType = change;
            _changedItem = item;
            _replacedWith = replacement;
         }
      }

   public enum ChangeTypeEnum {
      NoChanges,
      Added,
      Removed,
      Replaced,
      Cleared
   };
   
}
