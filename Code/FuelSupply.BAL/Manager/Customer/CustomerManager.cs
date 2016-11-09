using FuelSupply.DAL.Entity.CustomerEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FuelSupply.DAL.Provider;
using FuelSupply.DAL.Entity.CreditEntity;
using FuelSupply.BAL.Manager.Common;

namespace FuelSupply.BAL.Manager
{
    public class CustomerManager
    {       
        #region "Methods"
        public static List<Customer> GetAllCustomers()
        {
            return CustomerProvider.GetAllCustomers();           
        }

        public static List<CustomerType> GetAllCustomerTypes()
        {
            return CustomerProvider.GetAllCustomerTypes();
        }

        public static List<PaymentType> GetAllPaymentTypes()
        {
            return CustomerProvider.GetAllPaymentTypes();
        }

        public static List<FingerPrintType> GetAllFingerPrintTypes()
        {
            return CustomerProvider.GetAllFingerPrintTypes();
        }

        public static List<Customer> GetAllKeyCustomer()
        {
            return CustomerProvider.GetAllKeyCustomer();
        }

        public static Customer GetCustomerById(int pCustomerId)
        {
            return CustomerProvider.GetCustomerById(pCustomerId);
        }

        public static List<Customer> GetCustomerListByType(int pType)
        {
            return CustomerProvider.GetCustomerListByType(pType);
        }

        public static List<Customer> GetCustomerListByKeyCustomer(int pKeyCustomerId)
        {
            return CustomerProvider.GetCustomerListByKeyCustomer(pKeyCustomerId);
        }

        public static bool AddCustomer(Customer pCustomer, ref string pErrorString)
        {
            if(CustomerProvider.ValidateCustomer(pCustomer) == false)
            {
                pErrorString = "Customer code and name must be unique";
                return false;
            }
            else
            {
                bool result = CustomerProvider.AddCustomer(pCustomer);
                if (result == false)
                {
                    pErrorString = "Error while adding Customer, Please try again.";
                    return false;
                }
                else
                    return true;
            }            
        }

        public static bool UpdateCustomer(Customer pCustomer, ref string pErrorString)
        {
            if (CustomerProvider.ValidateCustomer(pCustomer) == false)
            {
                pErrorString = "Customer code and name must be unique";
                return false;
            }
            else
            {
                bool result = CustomerProvider.UpdateCustomer(pCustomer);
                if (result == false)
                {
                    pErrorString = "Error while updating Customer, Please try again.";
                    return false;
                }
                else
                    return true;
            }                        
        }

        public static bool DeleteCustomer(int pCustomerId, bool pDeleteHistory)
        {           
            if (pDeleteHistory == true)
            {
                CreditManager.DeleteCreditHistoryByCustomerId(pCustomerId);
                FuelManager.DeleteFuelHistoryByCustomerId(pCustomerId);                
            }

            bool bResult = CustomerProvider.DeleteCustomer(pCustomerId);
            return bResult;
        }

        public static bool IncreaseCredit(int pCustomerId, decimal pAmount, int pPaymentType)
        {
            bool result = CustomerProvider.IncreaseCredit(pCustomerId, pAmount);
            if (result == true)
            {
                CreditHistory oHistory = new CreditHistory();
                oHistory.CreditAmount = pAmount;
                oHistory.CustomerId = pCustomerId;
                oHistory.Time = DateTime.Now;
                oHistory.UserId = SharedData.LoggedUser.Id;
                oHistory.PaymentType = pPaymentType;
                oHistory.Id = 0;
                oHistory.FuelStationId = SharedData.CurrentFuelStation.Id;
                bool oHistoryResult = CreditManager.AddCreditHistory(oHistory);
                return oHistoryResult;
            }
            else
                return true;
        }

        public static bool DeductAmount(int pCustomerId, decimal pAmount)
        {
            return CustomerProvider.DeductAmount(pCustomerId, pAmount);
        }

        public static bool CheckDeductionAvailibility(int pCustomerId, decimal pAmount)
        {
            return CustomerProvider.CheckDeductionAvailibility(pCustomerId, pAmount);
        }

        public static List<CustomerFingerPrint> GetAllCustomerFingerPrint()
        {
            return CustomerProvider.GetAllCustomerFingerPrint();
        }

        public static List<CustomerFingerPrint> GetCustomerFingerPrintByCustomerId(int pCustomerId)
        {
            return CustomerProvider.GetCustomerFingerPrintByCustomerId(pCustomerId);
        }

        public static Customer GetCustomerByFingerPrint(string pFingerPrint)
        {
            return CustomerProvider.GetCustomerByFingerPrint(pFingerPrint);
        }     

        #endregion
    }
}
