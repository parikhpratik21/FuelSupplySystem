using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelSupply.DAL.Provider.Common
{
    public class LogManager
    {
        #region "Declarations(1)"

        public static bool IsLogEnable = true;
        public static bool _isDetailLogsEnable = true;

        #endregion

        #region "Enumerations(0)"
        #endregion


        #region "Methods(4)"

        #region "Method: LogWriterForLibrary()"
        /// <summary>
        /// Return the textwritter for library logs
        /// </summary>
        /// <returns></returns>
        public static void LogWriterForLibrary(string pMessage)
        {
            try
            {
                System.IO.TextWriter integrationLog = default(System.IO.TextWriter);

                DateTime currentTime = DateTime.Now;

                string folderName = currentTime.ToString("ddMMyyyy");
                string directoryPath = ".\\LOGS\\";

                string path = directoryPath + folderName;
                if ((System.IO.Directory.Exists(path) == false))
                {
                    System.IO.Directory.CreateDirectory(path);
                }


                string fullFilePath = path + "\\App_Log" + folderName + ".log";
                integrationLog = new System.IO.StreamWriter(fullFilePath, true);
                string writelogmessage = "# T: [ " + currentTime.ToString("hh:mm:ss:fff tt");
                if (!(string.IsNullOrEmpty(pMessage)))
                {
                    writelogmessage += " ::  Msg: " + pMessage;
                }

                integrationLog.WriteLine(writelogmessage);
                integrationLog.Close();
            }
            catch (Exception ex)
            {
                return;
            }
        }
        #endregion

        #region "Method: logMessage(3)"
        /// <summary>
        /// Logs the string message.
        /// </summary>
        /// <param name="pClassname">classname.</param>
        /// <param name="pMethodname">methodname.</param>
        /// <param name="pMessage">message.</param>
        public static void logMessage(string pClassname, string pMethodname, string pMessage = "")
        {
            try
            {
                if (IsLogEnable == true)
                {
                    System.IO.TextWriter integrationLog = default(System.IO.TextWriter);

                    DateTime currentTime = DateTime.Now;

                    string folderName = currentTime.ToString("ddMMyyyy");
                    string directoryPath = ".\\LOGS\\";

                    string path = directoryPath + folderName;
                    if ((System.IO.Directory.Exists(path) == false))
                    {
                        System.IO.Directory.CreateDirectory(path);
                    }


                    string fullFilePath = path + "\\Log" + folderName + ".log";
                    integrationLog = new System.IO.StreamWriter(fullFilePath, true);

                    string writelogmessage = "# T: [ " + currentTime.ToString("hh:mm:ss:fff tt") + " ] ::  C: " + pClassname + " ::  M: " + pMethodname;
                    if (!(string.IsNullOrEmpty(pMessage)))
                    {
                        writelogmessage += " ::  Msg: " + pMessage;
                    }

                    //Console.WriteLine(writelogmessage);
                    integrationLog.WriteLine(writelogmessage);
                    integrationLog.Close();
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }

        #endregion

        #region "Method: logExceptionMessage(3)"
        /// <summary>
        /// Logs the exception message.
        /// </summary>
        /// <param name="pclassname">The pclassname.</param>
        /// <param name="pmethodname">The pmethodname.</param>
        /// <param name="pex">The pex.</param>
        public static void logExceptionMessage(string pclassname, string pmethodname, Exception pex = null)
        {
            try
            {
                if (IsLogEnable == true)
                {
                    System.IO.TextWriter integrationLog = default(System.IO.TextWriter);

                    DateTime currentTime = DateTime.Now;

                    string folderName = currentTime.ToString("ddMMyyyy");
                    string directoryPath = ".\\LOGS\\";

                    string path = directoryPath + folderName;
                    if ((System.IO.Directory.Exists(path) == false))
                    {
                        System.IO.Directory.CreateDirectory(path);
                    }


                    string fullFilePath = path + "\\Log" + folderName + ".log";
                    integrationLog = new System.IO.StreamWriter(fullFilePath, true);

                    string writelogmessage = "# T: [ " + currentTime.ToString("hh:mm:ss:fff tt") + " ] ::  C: " + pclassname + " ::  M: " + pmethodname;

                    if (pex != null)
                    {
                        writelogmessage += " ::  exMsg: " + pex.Message + " ::  exStack: " + pex.StackTrace;
                    }

                    integrationLog.WriteLine(writelogmessage);
                    integrationLog.Close();
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }

        #endregion

        #endregion

        #region "Events(0)"
        #endregion
    }
}
