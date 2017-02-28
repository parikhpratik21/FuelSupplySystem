using FuelSupply.DAL.Database_Entity;
using FuelSupply.DAL.Entity.CustomerEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelSupply.DAL.Provider
{
    public class CustomerProvider
    {
        #region "Declaration"
        private static FuelSupplySystemEntities1 customerDbObject;
        #endregion

        #region "Constructor"
        static CustomerProvider()
        {
            customerDbObject = new FuelSupplySystemEntities1();
        }
        #endregion

        #region "Methods"
        public static List<Customer> GetAllCustomers()
        {
            List<Customer> CustomerList = customerDbObject.Customers.ToList();
            return CustomerList;
        }

        public static List<CustomerType> GetAllCustomerTypes()
        {
            List<CustomerType> CustomerTypeList = customerDbObject.CustomerTypes.ToList();
            return CustomerTypeList;
        }               

        public static List<PaymentType> GetAllPaymentTypes()
        {
            List<PaymentType> PaymentTypeList = customerDbObject.PaymentTypes.ToList();
            return PaymentTypeList;
        }

        public static List<FingerPrintType> GetAllFingerPrintTypes()
        {
            List<FingerPrintType> FingerPrintTypeList = customerDbObject.FingerPrintTypes.ToList();
            return FingerPrintTypeList;
        }

        public static Customer GetCustomerById(int pCustomerId)
        {
            return customerDbObject.Customers.Where(x => x.Id == pCustomerId).FirstOrDefault();
        }

        public static List<Customer> GetCustomerListByType(int pType)
        {
            return customerDbObject.Customers.Where(x => x.CustomerType == pType).ToList();
        }

        public static List<Customer> GetCustomerListByKeyCustomer(int pKeyCustomerId)
        {
            return customerDbObject.Customers.Where(x => x.KeyCustomerId == pKeyCustomerId).ToList();
        }

        public static List<Customer> GetAllKeyCustomer()
        {
            return customerDbObject.Customers.Where(x => x.KeyCustomerId == null || x.KeyCustomerId == 0).ToList();
        }

        public static List<CustomerFingerPrint> GetAllCustomerFingerPrint()
        {
            List<CustomerFingerPrint> CustomerFingerPrintList = customerDbObject.CustomerFingerPrints.ToList();
            return CustomerFingerPrintList;
        }

        public static List<CustomerFingerPrint> GetCustomerFingerPrintByCustomerId(int pCustomerId)
        {
            List<CustomerFingerPrint> CustomerFingerPrintList = customerDbObject.CustomerFingerPrints.Where(x => x.ID == pCustomerId).ToList();
            return CustomerFingerPrintList;
        }

        public static Customer GetCustomerByFingerPrint(string pFingerPrint)
        {
            CustomerFingerPrint oFingerPrint = customerDbObject.CustomerFingerPrints.Where(x => x.FingerPrint == pFingerPrint).FirstOrDefault();
            if (oFingerPrint == null)
                return null;
            else
            {
                Customer oCustomer = customerDbObject.Customers.Where(x => x.Id == oFingerPrint.CustomerID).FirstOrDefault();
                return oCustomer;
            }
        }

        public static bool AddCustomer(Customer pCustomer)
        {
            customerDbObject.Customers.Add(pCustomer);
            customerDbObject.SaveChanges();
            return true;
        }

        public static bool ValidateCustomer(Customer pCustomer)
        {
            if(pCustomer.Id <= 0)
            {
                if (customerDbObject.Customers.Where(x => x.Code == pCustomer.Code || x.Name == pCustomer.Name).Count() > 0)
                {
                    return false;
                }
                else
                    return true;
            }
            else
            {
                Customer oCustomer = customerDbObject.Customers.Where(x => (x.Code == pCustomer.Code || x.Name == pCustomer.Name) && x.Id != pCustomer.Id).FirstOrDefault();

                if (oCustomer != null)
                {
                    return false;
                }
                else
                    return true;
            }
        }

        public static bool UpdateCustomer(Customer pCustomer, decimal? pOldPaymentLimit)
        {
            Customer oCustomer = customerDbObject.Customers.Where(x => x.Id == pCustomer.Id).FirstOrDefault();
            if (oCustomer != null)
            {
                try
                {
                    oCustomer.Address = pCustomer.Address;
                    oCustomer.City = pCustomer.City;
                    oCustomer.Code = pCustomer.Code;
                    oCustomer.KeyCustomerId = pCustomer.KeyCustomerId;
                    oCustomer.Country = pCustomer.Country;
                    oCustomer.Name = pCustomer.Name;
                    oCustomer.Pincode = pCustomer.Pincode;
                    oCustomer.State = pCustomer.State;
                    oCustomer.PaymentLimit = pCustomer.PaymentLimit;

                    oCustomer.CustomerFingerPrints = pCustomer.CustomerFingerPrints;

                    if (pCustomer.CustomerFingerPrints != null && pCustomer.CustomerFingerPrints.Count > 0)
                    {
                        foreach (CustomerFingerPrint oFingerPrint in pCustomer.CustomerFingerPrints)
                        {
                            if (oFingerPrint.ID <= 0)
                            {
                                customerDbObject.CustomerFingerPrints.Add(oFingerPrint);
                            }
                        }
                    }

                    //check if payment limit changes or not for credit type of customer
                    if (pOldPaymentLimit != null && oCustomer.PaymentLimit != null && oCustomer.PaymentLimit != pOldPaymentLimit && oCustomer.PaymentType == (int)FuelSupply.DAL.Entity.Comman.Constants.ePaymentType.Credit)
                    {
                        if (pOldPaymentLimit.Value > oCustomer.PaymentLimit.Value)
                        {
                            oCustomer.AvailablePay = oCustomer.AvailablePay - (pOldPaymentLimit.Value - oCustomer.PaymentLimit.Value);
                        }
                        else
                        {
                            oCustomer.AvailablePay = oCustomer.AvailablePay + (oCustomer.PaymentLimit.Value - pOldPaymentLimit.Value);
                        }
                    }

                    customerDbObject.SaveChanges();
                }
               catch(Exception ex)
                {
                   string sErrorMsg = ex.ToString();
                }

                return true;
            }
            return false;
        }

        public static bool DeleteCustomer(int pCustomerId)
        {
            Customer oCustomer = customerDbObject.Customers.Where(x => x.Id == pCustomerId).FirstOrDefault();
            if (oCustomer != null)
            {
                customerDbObject.Customers.Remove(oCustomer);
                customerDbObject.SaveChanges();
                return true;
            }
            return false;
        }

        public static bool IncreaseCredit(int pCustomerId, decimal pAmount)
        {
            Customer oCustomer = customerDbObject.Customers.Where(x => x.Id == pCustomerId).FirstOrDefault();
            if (oCustomer != null)
            {
                if (oCustomer.AvailablePay == null)
                    oCustomer.AvailablePay = 0;

                oCustomer.AvailablePay = oCustomer.AvailablePay + pAmount;
                customerDbObject.SaveChanges();
                return true;
            }
            return false;
        }

        public static bool DeductAmount(int pCustomerId, decimal pAmount)
        {
            Customer oCustomer = customerDbObject.Customers.Where(x => x.Id == pCustomerId).FirstOrDefault();
            if (oCustomer != null)
            {
                if (oCustomer.AvailablePay == null)
                    oCustomer.AvailablePay = 0;

                if ((oCustomer.AvailablePay - pAmount) < 0)
                    return false;

                oCustomer.AvailablePay = oCustomer.AvailablePay - pAmount;
                customerDbObject.SaveChanges();
                return true;
            }
            return false;
        }

        public static bool IncreaseAmount(int pCustomerId, decimal pAmount)
        {
            Customer oCustomer = customerDbObject.Customers.Where(x => x.Id == pCustomerId).FirstOrDefault();
            if (oCustomer != null)
            {                
                oCustomer.AvailablePay = oCustomer.AvailablePay + pAmount;
                customerDbObject.SaveChanges();
                return true;
            }
            return false;
        }

        public static bool CheckDeductionAvailibility(int pCustomerId, decimal pAmount)
        {
            Customer oCustomer = customerDbObject.Customers.Where(x => x.Id == pCustomerId).FirstOrDefault();
            if (oCustomer != null)
            {
                if (oCustomer.AvailablePay == null)
                    oCustomer.AvailablePay = 0;

                if (oCustomer.AvailablePay >= pAmount)
                    return true;
                else
                    return false;                
            }
            return false;
        }

        public static bool ChangePassword(int pCustomerId, string pNewPassword)
        {
            Customer oCustomer = customerDbObject.Customers.Where(x => x.Id == pCustomerId).FirstOrDefault();
            if (oCustomer != null)
            {
                oCustomer.Password = pNewPassword;
                customerDbObject.SaveChanges();

                return true;
            }

            return false;
        }
        #endregion
    }
}
