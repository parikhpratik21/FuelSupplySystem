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

        public static List<FuelHistory> GetFuelHistoryByKeyCustomerId(int pKeyCustomerId)
        {
            return fuelDbObject.FuelHistories.Where(x => x.KeyCustomerId == pKeyCustomerId).ToList();
        }

        public static List<FuelHistory> GetFuelHistoryByCustomerId(int pCustomerId)
        {
            return fuelDbObject.FuelHistories.Where(x => x.CustomerId == pCustomerId).ToList();
        }

        public static List<FuelHistory> FetFuelHistoryByUserId(int pUserId)
        {
            return fuelDbObject.FuelHistories.Where(x => x.UserId == pUserId).ToList();
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
        #endregion
    }
}
