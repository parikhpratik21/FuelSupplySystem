using FuelSupply.DAL.Entity.CustomerEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FuelSupply.DAL.Provider;

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

        public static bool AddCustomer(Customer pCustomer)
        {
            return CustomerProvider.AddCustomer(pCustomer);
        }

        public static bool UpdateCustomer(Customer pCustomer)
        {
            return CustomerProvider.UpdateCustomer(pCustomer);
        }

        public static bool DeleteCustomer(int pCustomerId)
        {
            return CustomerProvider.DeleteCustomer(pCustomerId);
        }

        public static bool IncreaseCredit(int pCustomerId, decimal pAmount)
        {
            return CustomerProvider.IncreaseCredit(pCustomerId, pAmount);
        }

        public static bool DeductAmount(int pCustomerId, decimal pAmount)
        {
            return CustomerProvider.DeductAmount(pCustomerId, pAmount);
        }

        public static bool CheckDeductionAvailibility(int pCustomerId, decimal pAmount)
        {
            return CustomerProvider.CheckDeductionAvailibility(pCustomerId, pAmount);
        }

        #endregion
    }
}
