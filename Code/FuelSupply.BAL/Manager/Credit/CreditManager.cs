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

        public static List<CreditHistory> GetCreditHistoryByPaymentId(int pPaymentType)
        {
            return CreditProvider.GetCreditHistoryByPaymentId(pPaymentType);            
        }

        public static List<CreditHistory> GetCreditHistoryByCustomerId(int pCustomerId)
        {
            return CreditProvider.GetCreditHistoryByCustomerId(pCustomerId);            
        }

        public static List<CreditHistory> FetCreditHistoryByUserId(int pUserId)
        {
            return CreditProvider.FetCreditHistoryByUserId(pUserId);            
        }

        public static List<CreditHistory> FetCreditHistoryByFuelStationId(int pFuelStationId)
        {
            return CreditProvider.FetCreditHistoryByFuelStationId(pFuelStationId);            
        }

        public static bool AddCreditHistory(CreditHistory pHistory)
        {
            return CreditProvider.AddCreditHistory(pHistory);            
        }

        public static bool DeleteCreditHistory(int pCreditHistoryId)
        {
            return CreditProvider.DeleteCreditHistory(pCreditHistoryId);
        }

        public static bool UpdateCreditHistory(CreditHistory pHistory)
        {
            return CreditProvider.UpdateCreditHistory(pHistory);
        }
        #endregion
    }
}
