using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

using TestProjectDemo.Properties;

namespace TestProjectDemo {
   static class Program {
      private static IDataSource _source = null;
      /// <summary>
      ///  The main entry point for the application.
      /// </summary>
      [STAThread]
      static void Main() {
         LogWriter.create("TestDemo.log");
         LogWriter.Default.debug("Read user and password from configuration file..");
         var _user = new User();
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);
         if (String.IsNullOrWhiteSpace(_user.password)) {
            var form = new frmLogin();
            form.ShowDialog();
         }
         Application.Run(new Form1());
      }

      public static IDataSource dataSource {
         get {
            if(_source == null) {
               LogWriter.Default.debug("Creating Data source by configuration");
               if (ConfigurationManager.AppSettings.AllKeys.Contains("DataSourceType")) {
                  var sourceType = ConfigurationManager.AppSettings["DataSourceType"];
                  if(sourceType != "XmlFile") {
                     LogWriter.Default.error("Not implemeted Data Source");
                     throw new NotImplementedException("Unsupported Data Source");
                  }
               }//if (ConfigurationManager.AppSettings.AllKeys.Contains("DataSourceType")) {
               _source = new ProjXmlDataSource();
            }//if(_source == null) {
            return _source;
         }
      }

   }
}
