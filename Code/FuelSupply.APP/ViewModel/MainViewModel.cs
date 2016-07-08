using FuelSupply.APP.View;
using GalaSoft.MvvmLight;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace FuelSupply.APP.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase, INotifyPropertyChanged
    {
        #region "Declaration"
        private UserControl _content;
        private Profile oProfile;
        #endregion

        #region EventHandlers (1)
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(Window pOwnerForm)
        {         
            //oProfile = new Profile(pOwnerForm);
            //this.ContentWindow = oProfile;
        }

        public UserControl ContentWindow
        {
            get { return _content; }
            set
            {
                _content = value;                
                OnPropertyChanged("ContentWindow");                
            }
        }
    }
}