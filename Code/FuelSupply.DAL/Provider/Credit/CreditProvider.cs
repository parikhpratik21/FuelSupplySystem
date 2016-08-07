using FuelSupply.DAL.Database_Entity;
using FuelSupply.DAL.Entity.CreditEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelSupply.DAL.Provider
{
    public class CreditProvider
    {
        #region "Declaration"
        private static FuelSupplySystemEntities1 creditDbObject;
        #endregion

        #region "Constructor"
        static CreditProvider()
        {
            creditDbObject = new FuelSupplySystemEntities1();
        }
        #endregion

        #region "Methods"
        public static List<CreditHistory> GetAllCreditHistory()
        {
            List<CreditHistory> FuelHostoryList = creditDbObject.CreditHistories.ToList();
            return FuelHostoryList;
        }

        public static List<CreditHistory> GetCreditHistoryByPaymentId(int pPaymentType, DateTime? pStartDate, DateTime? pEndDate)
        {
            return creditDbObject.CreditHistories.Where(x => x.PaymentType == pPaymentType && (x.Time > pStartDate && x.Time < pEndDate)).ToList();
        }

        public static List<CreditHistory> GetCreditHistoryByCustomerId(int pCustomerId, DateTime? pStartDate, DateTime? pEndDate)
        {
            return creditDbObject.CreditHistories.Where(x => x.CustomerId == pCustomerId && (x.Time > pStartDate && x.Time < pEndDate)).ToList();
        }

        public static List<CreditHistory> GetCreditHistoryByKeyCustomerId(int pCustomerId, DateTime? pStartDate, DateTime? pEndDate)
        {
            List<CreditHistory> oHistoryListByKeyCustomer = creditDbObject.Fetch_CreditHistoryBy_KeyCustomer(pCustomerId, pStartDate, pEndDate).ToList();
            return oHistoryListByKeyCustomer;
        }
        
        public static List<CreditHistory> FetCreditHistoryByUserId(int pUserId, DateTime? pStartDate, DateTime? pEndDate)
        {
            return creditDbObject.CreditHistories.Where(x => x.UserId == pUserId && (x.Time > pStartDate && x.Time < pEndDate)).ToList();
        }

        public static List<CreditHistory> FetCreditHistoryByFuelStationId(int pFuelStationId, DateTime? pStartDate, DateTime? pEndDate)
        {
            return creditDbObject.CreditHistories.Where(x => x.FuelStationId == pFuelStationId && (x.Time > pStartDate && x.Time < pEndDate)).ToList();
        }

        public static List<CreditHistory> GetFuelHistoryBetweenDates(DateTime? pStartDate, DateTime? pEndDate)
        {
            return creditDbObject.CreditHistories.Where(x => x.Time > pStartDate && x.Time < pEndDate).ToList();
        }

        public static bool AddCreditHistory(CreditHistory pHistory)
        {
            pHistory.Time = DateTime.Now;
            creditDbObject.CreditHistories.Add(pHistory);
            creditDbObject.SaveChanges();
            return true;
        }

        public static bool DeleteCreditHistory(int pCreditHistoryId)
        {
            CreditHistory oHistory = creditDbObject.CreditHistories.Where(x => x.Id == pCreditHistoryId).FirstOrDefault();
            if (oHistory != null)
            {
                creditDbObject.CreditHistories.Remove(oHistory);
                creditDbObject.SaveChanges();
                return true;
            }
            return false;              
        }

        public static bool UpdateCreditHistory(CreditHistory pHistory)
        {
            CreditHistory oHistory = creditDbObject.CreditHistories.Where(x => x.Id == pHistory.Id).FirstOrDefault();
            if (oHistory != null)
            {
                oHistory.PaymentType = oHistory.PaymentType;
                oHistory.CreditAmount = oHistory.CreditAmount;
                oHistory.PaymentType = oHistory.PaymentType;
                oHistory.Time = DateTime.Now;              

                creditDbObject.SaveChanges();

                return true;
            }
            return false;
        }
        #endregion
    }
}
