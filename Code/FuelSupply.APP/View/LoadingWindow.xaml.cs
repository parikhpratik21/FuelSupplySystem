using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FuelSupply.APP.View
{
    /// <summary>
    /// Interaction logic for LoadingWindow.xaml
    /// </summary>
    public partial class LoadingWindow : Window
    {
        private System.Windows.Forms.Timer LoaderTimer = new System.Windows.Forms.Timer();
        private delegate void LoaderDelegate(object oSender, EventArgs eEvent);
        MainWindow _oMainWindow;

        public MainWindow OwnerForm
        {
            get
            {
                return _oMainWindow;
            }
        }

        public LoadingWindow(Window oWnerForm)
        {
            InitializeComponent();
            _oMainWindow = (MainWindow)oWnerForm;

            LoaderTimer.Tick += _oMainWindow.Loading_Timer_Tick;
            LoaderTimer.Interval = 60000;
            LoaderTimer.Start();
            this.Closing += Close;
        }

        private void Close(object sender, System.ComponentModel.CancelEventArgs e)
        {
            LoaderTimer.Stop();
        }
    }
}
