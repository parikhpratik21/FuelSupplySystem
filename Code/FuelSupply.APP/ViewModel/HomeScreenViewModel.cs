using FuelSupply.BAL.Manager.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelSupply.APP.ViewModel
{
    public class HomeScreenViewModel : INotifyPropertyChanged
    {
        #region Property
        public string LoggedUserName
        {
            get
            {
                if (SharedData.LoggedUser != null)
                {
                    return SharedData.LoggedUser.Name;
                }
                else
                    return string.Empty;
            }
        }

        public System.Windows.Visibility IsLableVisible
        {
            get
            {
                if (SharedData.LoggedUser == null)
                    return System.Windows.Visibility.Hidden;
                else
                    return System.Windows.Visibility.Visible;
            }
        }
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

    }
}
