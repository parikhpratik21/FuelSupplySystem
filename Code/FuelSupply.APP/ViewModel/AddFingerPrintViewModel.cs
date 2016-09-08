using FuelSupply.BAL.Manager;
using FuelSupply.BAL.Manager.Common;
using FuelSupply.DAL.Entity.CustomerEntity;
using FuelSupply.DAL.Entity.FuelEntity;
using FuelSupply.DAL.Provider.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FuelSupply.APP.ViewModel
{
    public class AddFingerPrintViewModel : INotifyPropertyChanged
    {
        #region "Declaration"
        private Customer _SelectedCustomer;
        private List<FingerPrintType> _FingerPrintTypeList;      
        private MainWindow oMainWindow;
        private int _FingerPrintType;        
        private string _CurrentFingerPrint;
        private string _FingerPrintImage;
        #endregion

        #region "Property"
        public string FingerPrintImage
        {
            get
            {
                return _FingerPrintImage;
            }
            set
            {
                _FingerPrintImage = value;
                OnPropertyChanged("FingerPrintImage");
            }
        }
        public Customer SelectedCustomer
        {
            get
            {
                return _SelectedCustomer;
            }
            set
            {
                _SelectedCustomer = value;
                OnPropertyChanged("SelectedCustomer");
            }
        }

        public string CurrentFingerPrint
        {
            get
            {
                return _CurrentFingerPrint;
            }
            set
            {
                _CurrentFingerPrint = value;
                OnPropertyChanged("CurrentFingerPrint");
            }

        }

        public List<FingerPrintType> FingerPrintTypeList
        {
            get
            {
                return _FingerPrintTypeList;
            }
            set
            {
                _FingerPrintTypeList = value;
                OnPropertyChanged("FingerPrintTypeList");
            }
        }

        public int NoOfFingerPrint
        {
            get
            {
                if (SelectedCustomer != null && SelectedCustomer.CustomerFingerPrints != null && SelectedCustomer.CustomerFingerPrints.Count > 0 && FingerPrintType > 0)
                {
                    return SelectedCustomer.CustomerFingerPrints.Where(x => x.FingerPrintType == FingerPrintType).Count();
                }
                else
                    return 0;
            }
        }      

        public int FingerPrintType
        {
            get
            {
                return _FingerPrintType;
            }
            set
            {
                _FingerPrintType = value;
                OnPropertyChanged("FingerPrintType");
                OnPropertyChanged("NoOfFingerPrint");
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

        #region "Methods"
        public AddFingerPrintViewModel(Window pOwnerWindow, Customer pCustomer)
        {
            _FingerPrintTypeList = CustomerManager.GetAllFingerPrintTypes();           

            _FingerPrintType = _FingerPrintTypeList.FirstOrDefault().ID;

            if (pCustomer.CustomerFingerPrints == null)
                pCustomer.CustomerFingerPrints = new List<CustomerFingerPrint>();            

            oMainWindow = (MainWindow)pOwnerWindow;

            _SelectedCustomer = pCustomer;

            CustomerDisplayViewModel.ofisFingerPrintSensor.SetFPEngineVersion(CustomerDisplayViewModel.FingerPrintSensorVersion);

            CustomerDisplayViewModel.ofisFingerPrintSensor.OnFingerTouching += ofisFingerPrintSensor_OnFingerTouching;
        }

        public void DeregisterFingerPrinttouchEvent()
        {
            CustomerDisplayViewModel.ofisFingerPrintSensor.OnFingerTouching -= ofisFingerPrintSensor_OnFingerTouching;
        }

        private void ofisFingerPrintSensor_OnFingerTouching()
        {
            CustomerDisplayViewModel.ofisFingerPrintSensor.OnFingerTouching -= ofisFingerPrintSensor_OnFingerTouching;

            if (CustomerDisplayViewModel.ofisFingerPrintSensor.GetVerTemplate() == true)
            {
                FingerPrintImage = null;
                OnPropertyChanged("FingerPrintImage");

                if (Properties.Settings.Default.IsBeepEnable == true)
                    CustomerDisplayViewModel.ofisFingerPrintSensor.PlayBeep(true);
                CustomerDisplayViewModel.ofisFingerPrintSensor.PlayGreenLight(true);

                CurrentFingerPrint = CustomerDisplayViewModel.ofisFingerPrintSensor.VerifyTemplate;

                if (Properties.Settings.Default.IsBeepEnable == true)
                    CustomerDisplayViewModel.ofisFingerPrintSensor.PlayBeep(false);
                CustomerDisplayViewModel.ofisFingerPrintSensor.PlayGreenLight(false);

                DisplayFingerPrint();                
            }
            else
            {
                CustomerDisplayViewModel.ofisFingerPrintSensor.PlayRedLight(true);

                CustomerDisplayViewModel.ofisFingerPrintSensor.PlayRedLight(false);
            }

            CustomerDisplayViewModel.ofisFingerPrintSensor.OnFingerTouching += ofisFingerPrintSensor_OnFingerTouching;
        }  

        private void DisplayFingerPrint()
        {
            try
            {
                string filePath;
                filePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\FingerPrint";
                if (System.IO.Directory.Exists(filePath) == false)
                    System.IO.Directory.CreateDirectory(filePath);

                string sImagePath = filePath + "\\FingerPrint.bmp";
                try
                {
                    if (System.IO.File.Exists(sImagePath) == true)
                        System.IO.File.Delete(sImagePath);
                }
                catch(Exception ex)
                {
                    sImagePath = sImagePath.Replace("FingerPrint.bmp", "FingerPrint1.bmp");
                }

                CustomerDisplayViewModel.ofisFingerPrintSensor.SaveFingerTemplateAsBMP(sImagePath);

                FingerPrintImage = sImagePath;
            }
            catch(Exception ex)
            {
                LogManager.logExceptionMessage("AddFingerPrintViewModel", "DisplayFingerPrint", ex);
            }
        }

        public bool AddFingerPrint(ref string pErrorString)
        {
            if (CurrentFingerPrint == null || CurrentFingerPrint.Trim().Length == 0)
            {
                pErrorString = "Please scan the finger.";                
                return false;
            }         
            else
            {
                if(SelectedCustomer.CustomerFingerPrints.Where(x=> x.FingerPrint==CurrentFingerPrint).Count() > 0)
                {
                    pErrorString = "This Fingerprint are already available.";                    
                    return false;
                }
                else
                {
                    CustomerFingerPrint oFingerPrint = new CustomerFingerPrint();
                    oFingerPrint.FingerPrint = CurrentFingerPrint;
                    oFingerPrint.CustomerID = SelectedCustomer.Id;
                    oFingerPrint.FingerPrintType = FingerPrintType;
                    oFingerPrint.Policy = "9";

                    if (SelectedCustomer.CustomerFingerPrints == null)
                        SelectedCustomer.CustomerFingerPrints = new List<CustomerFingerPrint>();

                    SelectedCustomer.CustomerFingerPrints.Add(oFingerPrint);
                }
            }

            OnPropertyChanged("NoOfFingerPrint");
            pErrorString = "Fingerprint added successfully.";  
            return true;
        }

        public bool ClearSelectedTypeFingerPrint(ref string pErrorString)
        {
            if (FingerPrintType <= 0)
            {                
                pErrorString = "Please select valid fingerprint type.";  
                return false;
            }
            else
            {
                if (SelectedCustomer.CustomerFingerPrints != null)
                {
                    List<CustomerFingerPrint> removedFingerPrintList = SelectedCustomer.CustomerFingerPrints.Where(x => x.FingerPrintType == FingerPrintType).ToList();
                    if (removedFingerPrintList != null && removedFingerPrintList.Count > 0)
                    {
                        foreach (CustomerFingerPrint oFingerprint in removedFingerPrintList)
                        {
                            SelectedCustomer.CustomerFingerPrints.Remove(oFingerprint);
                        }

                        pErrorString = "Selected type fungerprint are removed successfully.";                        
                    }
                    else
                    {                        
                        pErrorString = "Customer doesn't contain this type of fingerprint.";    
                        return false;
                    }
                }
                else
                {
                    pErrorString = "Customer doesn't contain this type of fingerprint.";    
                    return false;
                }
            }

            OnPropertyChanged("NoOfFingerPrint");
            return true;
        }
        #endregion
    }
}
