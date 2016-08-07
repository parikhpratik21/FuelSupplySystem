using FuelSupply.DAL.Database_Entity;
using FuelSupply.DAL.Entity.FuelEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelSupply.DAL.Provider
{
    public class FuelProvider
    {
         #region "Declaration"
        private static FuelSupplySystemEntities1 fuelDbObject;
        #endregion

        #region "Constructor"
        static FuelProvider()
        {
            fuelDbObject = new FuelSupplySystemEntities1();
        }
        #endregion

        #region "Methods"
        public static List<FuelHistory> GetAllFuelHistory()
        {
            List<FuelHistory> FuelHostoryList = fuelDbObject.FuelHistories.ToList();
            return FuelHostoryList;
        }

        public static List<FuelType> GetAllFuelTypeList()
        {
            List<FuelType> FuelTypeList = fuelDbObject.FuelTypes.ToList();
            return FuelTypeList;
        }

        public static List<FuelHistory> GetFuelHistoryByKeyCustomerId(int pKeyCustomerId, DateTime? pStartDate, DateTime? pEndDate)
        {
            return fuelDbObject.FuelHistories.Where(x => (x.KeyCustomerId == pKeyCustomerId || x.CustomerId == pKeyCustomerId) && (x.Time > pStartDate && x.Time < pEndDate)).ToList();
        }

        public static List<FuelHistory> GetFuelHistoryByCustomerId(int pCustomerId, DateTime? pStartDate, DateTime? pEndDate)
        {
            return fuelDbObject.FuelHistories.Where(x => x.CustomerId == pCustomerId && (x.Time > pStartDate && x.Time < pEndDate)).ToList();
        }

        public static List<FuelHistory> GetFuelHistoryByUserId(int pUserId, DateTime? pStartDate, DateTime? pEndDate)
        {
            return fuelDbObject.FuelHistories.Where(x => x.UserId == pUserId && (x.Time > pStartDate && x.Time < pEndDate)).ToList();
        }

        public static List<FuelHistory> GetFuelHistoryByFuelTypeId(int pFuelTypeId, DateTime? pStartDate, DateTime? pEndDate)
        {
            return fuelDbObject.FuelHistories.Where(x => x.FuelType == pFuelTypeId && (x.Time > pStartDate && x.Time < pEndDate)).ToList();
        }

        public static List<FuelHistory> GetFuelHistoryBetweenDates(DateTime? pStartDate, DateTime? pEndDate)
        {
            return fuelDbObject.FuelHistories.Where(x => x.Time > pStartDate && x.Time < pEndDate).ToList();
        }

        public static List<FuelHistory> FetFuelHistoryByFuelStationId(int pFuelStationId)
        {
            return fuelDbObject.FuelHistories.Where(x => x.FuelStationId == pFuelStationId).ToList();
        }

        public static List<FuelHistory> FetFuelHistoryByFuelType(int pFuelTypeId)
        {
            return fuelDbObject.FuelHistories.Where(x => x.FuelType == pFuelTypeId).ToList();
        }

        public static bool AddFuelHistory(FuelHistory pHistory)
        {
            pHistory.Time = DateTime.Now;
            fuelDbObject.FuelHistories.Add(pHistory);
            fuelDbObject.SaveChanges();
            return true;
        }

        public static bool DeleteFuelHistory(int pFuelHistoryId)
        {
            FuelHistory oHistory = fuelDbObject.FuelHistories.Where(x => x.Id == pFuelHistoryId).FirstOrDefault();
            if (oHistory != null)
            {
                fuelDbObject.FuelHistories.Remove(oHistory);
                fuelDbObject.SaveChanges();
                return true;
            }
            return false;              
        }

        public static bool UpdateFuelHistory(FuelHistory pHistory)
        {
            FuelHistory oHistory = fuelDbObject.FuelHistories.Where(x => x.Id == pHistory.Id).FirstOrDefault();
            if (oHistory != null)
            {
                oHistory.FuelVolume = oHistory.FuelVolume;
                oHistory.FuelAmount = oHistory.FuelAmount;
                oHistory.FuelType = oHistory.FuelType;
                oHistory.Time = DateTime.Now;              

                fuelDbObject.SaveChanges();

                return true;
            }
            return false;
        }

        public static FuelStation GetFuelStationById(int pFuelStationId)
        {
            return fuelDbObject.FuelStations.Where(x => x.Id == pFuelStationId).FirstOrDefault();
        }
        public static List<FuelStation> GetAllFuelStation()
        {
            return fuelDbObject.FuelStations.ToList();
        }
        #endregion
    }
}
