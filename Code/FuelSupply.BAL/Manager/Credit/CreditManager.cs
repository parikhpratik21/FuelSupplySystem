using FuelSupply.DAL.Entity.CreditEntity;
using FuelSupply.DAL.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelSupply.BAL.Manager
{
    public class CreditManager
    {
        #region "Methods"
        public static List<CreditHistory> GetAllCreditHistory()
        {
            return CreditProvider.GetAllCreditHistory();            
        }

        public static List<Fetch_CreditHistory_Result> GetCreditHistoryByPaymentId(int pPaymentType, DateTime? pStartDate, DateTime? pEndDate)
        {
            return CreditProvider.GetCreditHistoryByPaymentId(pPaymentType,pStartDate,pEndDate);            
        }

        public static List<Fetch_CreditHistory_Result> GetCreditHistoryByCustomerId(int pCustomerId, DateTime? pStartDate, DateTime? pEndDate)
        {
            return CreditProvider.GetCreditHistoryByCustomerId(pCustomerId, pStartDate, pEndDate);            
        }

        public static List<Fetch_CreditHistory_Result> GetCreditHistoryByKeyCustomerId(int pKeyCustomerId, DateTime? pStartDate, DateTime? pEndDate)
        {
            return CreditProvider.GetCreditHistoryByKeyCustomerId(pKeyCustomerId, pStartDate, pEndDate);            
        }

        public static List<Fetch_CreditHistory_Result> FetCreditHistoryByUserId(int pUserId, DateTime? pStartDate, DateTime? pEndDate)
        {
            return CreditProvider.FetCreditHistoryByUserId(pUserId, pStartDate, pEndDate);            
        }

        public static List<CreditHistory> FetCreditHistoryByFuelStationId(int pFuelStationId, DateTime? pStartDate, DateTime? pEndDate)
        {
            return CreditProvider.FetCreditHistoryByFuelStationId(pFuelStationId, pStartDate, pEndDate);            
        }

        public static List<Fetch_CreditHistory_Result> GetCreditHistoryBetweenDates(DateTime? pStartDate, DateTime? pEndDate)
        {
            return CreditProvider.GetFuelHistoryBetweenDates(pStartDate, pEndDate);
        }

        public static bool AddCreditHistory(CreditHistory pHistory)
        {
            return CreditProvider.AddCreditHistory(pHistory);            
        }

        public static bool DeleteCreditHistory(int pCreditHistoryId)
        {
            return CreditProvider.DeleteCreditHistory(pCreditHistoryId);
        }

        public static bool DeleteCreditHistoryByUserId(int pUserId)
        {
            return CreditProvider.DeleteCreditHistoryByUserId(pUserId);
        }

        public static bool DeleteCreditHistoryByCustomerId(int pCustomerId)
        {
            return CreditProvider.DeleteCreditHistoryByCustomerId(pCustomerId);
        }

        public static bool UpdateCreditHistory(CreditHistory pHistory)
        {
            return CreditProvider.UpdateCreditHistory(pHistory);
        }
        #endregion
    }
}
