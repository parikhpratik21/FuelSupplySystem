using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FuelSupply.DAL.Provider;
using FuelSupply.DAL.Entity.FuelEntity;

namespace FuelSupply.BAL.Manager
{
    public class FuelManager
    {
        #region "Methods"
        public static List<FuelHistory> GetAllFuelHistory()
        {
            return FuelProvider.GetAllFuelHistory();
        }

        public static List<FuelHistory> GetFuelHistoryByKeyCustomerId(int pKeyCustomerId)
        {
            return FuelProvider.GetFuelHistoryByKeyCustomerId(pKeyCustomerId);
        }

        public static List<FuelHistory> GetFuelHistoryByCustomerId(int pCustomerId)
        {
            return FuelProvider.GetFuelHistoryByCustomerId(pCustomerId);
        }

        public static List<FuelHistory> FetFuelHistoryByUserId(int pUserId)
        {
            return FuelProvider.FetFuelHistoryByUserId(pUserId);
        }

        public static List<FuelHistory> FetFuelHistoryByFuelStationId(int pFuelStationId)
        {
            return FuelProvider.FetFuelHistoryByFuelStationId(pFuelStationId);
        }

        public static List<FuelHistory> FetFuelHistoryByFuelType(int pFuelTypeId)
        {
            return FuelProvider.FetFuelHistoryByFuelType(pFuelTypeId);
        }

        public static bool AddFuelHistory(FuelHistory pHistory)
        {
            return FuelProvider.AddFuelHistory(pHistory);
        }

        public static bool DeleteFuelHistory(int pFuelHistoryId)
        {
            return FuelProvider.DeleteFuelHistory(pFuelHistoryId);
        }

        public static bool UpdateFuelHistory(FuelHistory pHistory)
        {
            return FuelProvider.UpdateFuelHistory(pHistory);
        }
        #endregion
    }
}
