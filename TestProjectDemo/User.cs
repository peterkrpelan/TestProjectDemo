using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using static System.Windows.Forms.AxHost;

namespace TestProjectDemo {

   public interface IUser {
      string userName { get; }
      string password { get; }
   }

   public class User : IUser {
      private readonly string m_strUser;
      private readonly string m_strPassord;

      public User() {
         m_strUser = Properties.Settings.Default.user;
         var pwd = Properties.Settings.Default.password;
         if(!String.IsNullOrWhiteSpace(pwd)) m_strPassord = DecryptString(pwd);

      }

      public User(string aUser, string aPaswd) {
         m_strUser    = aUser;
         m_strPassord = aPaswd;
      }
      public string userName {
         get { return m_strUser; }
      }
      public string password { 
         get { return m_strPassord; }
      }

      static byte[] entropy = Encoding.Unicode.GetBytes("SaLtY bOy 6970 ePiC");

      public static string EncryptString(string input) {
         byte[] encryptedData = ProtectedData.Protect(Encoding.Unicode.GetBytes(input), entropy, DataProtectionScope.CurrentUser);
         return Convert.ToBase64String(encryptedData);
      }

      public static void save(string aUser, string aPaswd) {
         Properties.Settings.Default.user      = aUser;
         var _encPwnd                          = EncryptString(aPaswd);
          Properties.Settings.Default.password = _encPwnd;
         Properties.Settings.Default.Save();
      }

      public static string DecryptString(string encryptedData) {
         try {
            byte[] decryptedData = ProtectedData.Unprotect(Convert.FromBase64String(encryptedData), entropy, DataProtectionScope.CurrentUser);
            return Encoding.Unicode.GetString(decryptedData);
         }
         catch(Exception ex) {
            LogWriter.Default.error("Error {0} at decrypted ", new string[] { ex.Message });
            return "";
         }
      }
   }
}
