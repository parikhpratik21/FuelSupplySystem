using FuelSupply.BAL.Manager.Common;
using FuelSupply.DAL.Provider.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace FuelSupply.APP
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region "Declaration"
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImportAttribute("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        MainWindow oMainWindow;

        #endregion
        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                base.OnStartup(e);

                Process thisProc = Process.GetCurrentProcess();
                Process[] existingProcessList = Process.GetProcessesByName(thisProc.ProcessName);
                if (existingProcessList.Length > 1)
                {
                    IntPtr Handle = FindWindow(null, "FuelSupply");
                    ShowWindow(Handle, 9);
                    Environment.Exit(1);
                }

                System.Windows.Forms.Application.ThreadException += new ThreadExceptionEventHandler(LogUnhandledThreadException);

                // Set the unhandled exception mode to force all Windows Forms errors to go through 
                // our handler.
                System.Windows.Forms.Application.SetUnhandledExceptionMode(System.Windows.Forms.UnhandledExceptionMode.CatchException);

                // Add the event handler for handling non-UI thread exceptions to the event. 
                AppDomain.CurrentDomain.UnhandledException +=
                    new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

                oMainWindow = new MainWindow();
                oMainWindow.ShowDialog();

                if (FuelSupply.APP.ViewModel.CustomerDisplayViewModel.ofisFingerPrintSensor != null)
                    FuelSupply.APP.ViewModel.CustomerDisplayViewModel.ofisFingerPrintSensor.EndOfis();
                FuelSupply.APP.ViewModel.CustomerDisplayViewModel.ofisFingerPrintSensor = null;

                Process.GetCurrentProcess().Kill();
                //Application.Current.Shutdown();
               // Environment.Exit(0);           
            }
            catch(Exception ex)
            {
                LogManager.logExceptionMessage("FuelSupplySystem", "OnStartup", ex);
            }                         
        }
                
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;          
            LogManager.logExceptionMessage("FuelSupplySystem", "Application_DispatcherUnhandledException", e.Exception);

           // MessageManager.ShowErrorMessage(e.Exception.Message, oMainWindow);
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            LogManager.logExceptionMessage("FuelSupplySystem", "CurrentDomain_UnhandledException", ex);

           // MessageManager.ShowErrorMessage(ex.Message, oMainWindow);
        }

        private void LogUnhandledThreadException(object sender, ThreadExceptionEventArgs ex)
        {
            LogManager.logExceptionMessage("FuelSupplySystem", "LogUnhandledThreadException", ex.Exception);

           // MessageManager.ShowErrorMessage(ex.Exception.Message, oMainWindow);
        }      
    }
}
