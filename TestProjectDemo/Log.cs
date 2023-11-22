using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace TestProjectDemo {

   public interface ILog {

      void log(string text, object[] args = null, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0);

      void error(string message, object[] args = null, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0);

      void warn(string message, object[] args = null, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0);

      void write(string text, params object[] args);

      void debug(string message, object[] args = null, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0);
   }

   [Flags]
   public enum LogLevel {

      /// <summary>
      /// debug-level
      /// </summary>
      Default = 0,

      Trace = 1,

      /// <summary>
      /// info-level
      /// </summary>
      Debug = 2,

      /// <summary>
      /// warn-level
      /// </summary>
      Info = 3,

      /// <summary>
      /// errorlevel
      /// </summary>
      Warn = 4,

      /// <summary>
      /// user-defined level 1
      /// </summary>
      Error = 5,

      /// <summary>
      /// user-defined level 2
      /// </summary>
      /// <summary>
      /// all levels
      /// </summary>
   }
   public class LogWriter : IDisposable, ILog {

      /// <summary>
      /// Supported log-types
      /// </summary>
      public class StreamInfo {
         private bool m_isOpened = false;
         private const long BUFFER_SIZE = 4096;
         internal static bool isConsoleOpened { get; private set; }

         //empty file name Console output
         internal StreamInfo(string fileName, bool append = false, LogLevel aLvl = LogLevel.Info) {
            level = aLvl;
            if (String.IsNullOrWhiteSpace(fileName) && !isConsoleOpened) {
               isConsoleOpened = true;
               m_isOpened = true;
               logName = "console";
               logfile = Console.Out;
            }
            else if (!String.IsNullOrWhiteSpace(fileName)) {
               open(fileName, (append) ? FileMode.Append : FileMode.Create);
            }
            else {
               throw new ArgumentException("Empty file name!");
            }
         }

         private void open(string fileName, FileMode aMode) {
            try {
               var fsi = new FileStream(fileName, aMode, FileAccess.Write, FileShare.ReadWrite);
               logfile = new StreamWriter(fsi);
               m_isOpened = true;
               logName = fileName;
            }
            catch (IOException) {
               m_isOpened = false;
            }
         }

         private void copyStream(System.IO.FileStream inputStream, System.IO.Stream outputStream) {
            long bufferSize = inputStream.Length < BUFFER_SIZE ? inputStream.Length : BUFFER_SIZE;
            byte[] buffer = new byte[bufferSize];
            int bytesRead = 0;
            long bytesWritten = 0;
            while ((bytesRead = inputStream.Read(buffer, 0, buffer.Length)) != 0) {
               outputStream.Write(buffer, 0, bytesRead);
               bytesWritten += bufferSize;
            }
         }

         internal TextWriter logfile { get; private set; }
         internal LogLevel level { get; private set; }
         internal string logName { get; private set; }

         public static implicit operator bool(StreamInfo aFsi) {
            return aFsi.m_isOpened;
         }

         public void close() {
            if (m_isOpened && !isConsole) logfile.Close();
            m_isOpened = false;
         }

         public bool isConsole {
            get {
               return ReferenceEquals(logfile, Console.Out);
            }
         }

         public void move(string target) {
            if (isConsole) return;

            var src_folder = Path.GetDirectoryName(logName);
            var dest_folder = Path.GetDirectoryName(target);
            var src_fname = Path.GetFileName(logName);
            var dest_fname = Path.GetFileName(target);

            if (String.IsNullOrWhiteSpace(src_folder)) src_folder = Environment.CurrentDirectory;
            if (String.IsNullOrWhiteSpace(dest_fname)) dest_fname = src_fname;
            if (String.IsNullOrWhiteSpace(dest_folder)) dest_folder = Environment.CurrentDirectory;

            var source = Path.Combine(src_folder, src_fname);
            target = Path.Combine(dest_folder, dest_fname);
            source = source.Trim();
            target = target.Trim();

            if (0 != String.Compare(source, target, StringComparison.CurrentCultureIgnoreCase)) {
               if (m_isOpened) close();
               try {
                  if (File.Exists(target)) File.Delete(target);
                  File.Move(source, target);
                  logName = target;
                  open(target, FileMode.Append);
               }
               catch (IOException) {
               }
            }
         }

         public bool isOpen { get { return m_isOpened; } }
      }

      /** PRIVATE GLOBAL VARS *****/
      private static LogWriter logInstance;
      private List<StreamInfo> m_streams;

      ///<summary>Constructor with filename and append-info(no append), and logging mode  </summary>
      ///<param name="filename">Filename</param>
      ///<param name="append">Append to existing file (true/false)</param>
      ///<exception cref="System.IO.IOException"> ;
      ///IOException - some trouble with wrting header to log-file
      ///</exception>

      private LogWriter() {
         m_streams = new List<StreamInfo>();
      }

      private LogWriter(string filename, bool append, LogLevel level) : this() {
         // set global vars
         this.CurrentLogLevel = level;
         addLog(filename, level, append);
      }

      public void addLog(string fileName, LogLevel lvl, bool append = false) {
         if (String.IsNullOrWhiteSpace(fileName) && StreamInfo.isConsoleOpened) return;

         if (m_streams.FindIndex(x => x.logName.Equals(fileName, StringComparison.CurrentCultureIgnoreCase)) > -1) return;

         m_streams.Add(new StreamInfo(fileName, append, lvl));
      }

      public static void create(string filename, LogLevel lvl) {
           create(filename, false, lvl);
      }

      public static void create(string filename) {
         create(filename, "");
      }
      public static void create(string filename, string loglevel) {
         LogLevel lvl = LogLevel.Default;
         if (!String.IsNullOrWhiteSpace(loglevel)) {
            try {
               lvl = (LogLevel)Enum.Parse(typeof(LogLevel), loglevel.Trim(), true);
            }
            catch (Exception) { }
         }//if(!String.IsNullOrWhiteSpace(loglevel)) {

         create(filename, lvl);
      }

      public static void create(string filename, bool append, LogLevel lvl = LogLevel.Default) {
         var logname = (!String.IsNullOrWhiteSpace(filename)) ? Path.GetFileName(filename) : "";
         var ext = Path.GetExtension(filename);
         var logfolder = (!String.IsNullOrWhiteSpace(filename)) ? Path.GetDirectoryName(filename) : "";
         if (String.IsNullOrWhiteSpace(ext)) {
            logfolder = Path.Combine(logfolder, logname);
            logname = "";
         }
         if (String.IsNullOrWhiteSpace(logname)) logname = String.Format("{0}.log", System.Reflection.Assembly.GetEntryAssembly().GetName().Name);
         if (String.IsNullOrWhiteSpace(logfolder)) {
            try {
               var appsettings = ConfigurationManager.AppSettings;
               var key = appsettings.AllKeys.FirstOrDefault(x => x.Equals("Logs", StringComparison.InvariantCultureIgnoreCase));
               logfolder = (key != null) ? appsettings[key] : Environment.CurrentDirectory;
            }
            catch (Exception) {
               logfolder = Environment.CurrentDirectory;
            }
         }
   
         filename = Path.Combine(logfolder, logname);

         try {
            if (logInstance == null) logInstance = new LogWriter();
            if (!String.IsNullOrEmpty(filename)) logInstance.addLog(filename, (lvl == LogLevel.Default) ? LogLevel.Info : lvl, append);
         }
         catch (Exception) {
         }
      }

      public void moveLog(string targetFolder) {
         foreach (var x in m_streams) {
            if (x.isConsole) continue;
            x.move(targetFolder);
         }
      }

      public static LogWriter Default {
         get {
            if (logInstance == null) logInstance = new LogWriter();
            return logInstance;
         }
      }

      public void write(string text, params object[] args) {
         if (String.IsNullOrEmpty(text)) return;
         var sb = new StringBuilder();
         sb.AppendFormat(text, args);
         foreach (var itStream in m_streams) {
            itStream.logfile.WriteLine(sb.ToString());
            itStream.logfile.Flush();
         }
      }

      public void log(string text, LogLevel level, object[] args = null, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0) {
         // Check Level
         if (level < this.CurrentLogLevel) return;

         // format pre-string
         var sb = new StringBuilder();

         //var callStack = new StackFrame( 1, true );

         var culture = new CultureInfo("en-US");
         culture.NumberFormat.NumberGroupSeparator = " ";
         var strCallinfo = String.Format(culture, "{0, -25} {1, 6:N0}", Path.GetFileName(filePath), lineNumber);
         //var strCallinfo = String.Format( "{0, -25} {1, 6:N0}" , Path.GetFileName(callStack.GetFileName()), line);
         switch (level) {
            case (LogLevel.Debug): sb.AppendFormat("DEBUG {0} : ", strCallinfo); break;
            case (LogLevel.Info): sb.AppendFormat("INFO  {0} : ", strCallinfo); break;
            case (LogLevel.Warn): sb.AppendFormat("WARN  {0} : ", strCallinfo); break;
            case (LogLevel.Error): sb.AppendFormat("ERROR {0} : ", strCallinfo); break;
            case (LogLevel.Trace): sb.AppendFormat("TRACE {0} : ", strCallinfo); break;
            default: sb.AppendFormat("      {0} : ", strCallinfo); break;
         }

         // format text depening on type
         if (args == null) sb.Append(text);
         else sb.AppendFormat(text, args);

         foreach (var itStream in m_streams) {
            if (!itStream.isOpen || itStream.level > level) continue;
            itStream.logfile.WriteLine(sb.ToString());
            itStream.logfile.Flush();
         }
         // write to file and flush
      }

      /// <summary>
      /// returns or sets current log-level
      /// </summary>
      public LogLevel CurrentLogLevel { get; set; }


      #region IDisposable Support

      private bool disposedValue = false; // To detect redundant calls

      protected virtual void Dispose(bool disposing) {
         if (!disposedValue) {
            if (disposing) {
               m_streams.ForEach(x => x.close());
               m_streams.Clear();
               // TODO: dispose managed state (managed objects).
            }

            // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
            // TODO: set large fields to null.

            disposedValue = true;
         }
      }

      // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
      // ~LogFile() { // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
      // Dispose(false); }

      // This code added to correctly implement the disposable pattern.
      public void Dispose() {
         // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
         Dispose(true);
         // TODO: uncomment the following line if the finalizer is overridden above. GC.SuppressFinalize(this);
      }

      #endregion IDisposable Support

      #region ILog Support

      public void log(string text, object[] args = null, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0) {
         log(text, LogLevel.Info, args, filePath, lineNumber);
      }

      public void error(string message, object[] args = null, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0) {
         log(message, LogLevel.Error, args, filePath, lineNumber);
      }

      public void debug(string message, object[] args = null, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0) {
         log(message, LogLevel.Debug, args, filePath, lineNumber);
      }

      public void warn(string message, object[] args = null, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0) {
         log(message, LogLevel.Warn, args, filePath, lineNumber);
      }

      #endregion ILog Support
   }
}
