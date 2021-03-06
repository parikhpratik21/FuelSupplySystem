﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FuelSupply.DAL.Provider;
using FuelSupply.DAL.Entity.FuelEntity;
using FuelSupply.BAL.Manager.Common;

namespace FuelSupply.BAL.Manager
{
    public class FuelManager
    {
        #region "Methods"
        public static FuelStation GetFuelStationById(int pFuelStationId)
        {
            return FuelProvider.GetFuelStationById(pFuelStationId);
        }
        public static List<FuelStation> GetAllFuelStation()
        {
            return FuelProvider.GetAllFuelStation();
        }
        public static List<FuelHistory> GetAllFuelHistory()
        {
            return FuelProvider.GetAllFuelHistory();
        }

        public static List<FuelType> GetAllFuelTypeList()
        {
            return FuelProvider.GetAllFuelTypeList();
        }

        public static bool UpdateFuelRate(int pFuelTypeId, decimal pFuelRate)
        {
            return FuelProvider.UpdateFuelRate(pFuelTypeId, pFuelRate);
        }

        public static bool UpdateActualFuelDetail(int pFuelTypeId, decimal pActualFuelVolume, decimal pActualFuelAmount)
        {
            return FuelProvider.UpdateActualFuelDetail(pFuelTypeId, pActualFuelVolume, pActualFuelAmount);
        }

        public static List<Fetch_FuelHistory_Result> GetFuelHistoryByKeyCustomerId(int pKeyCustomerId, DateTime? pStartDate, DateTime? pEndDate)
        {
            return FuelProvider.GetFuelHistoryByKeyCustomerId(pKeyCustomerId,pStartDate,pEndDate);
        }

        public static List<Fetch_FuelHistory_Result> GetFuelHistoryByCustomerId(int pCustomerId, DateTime? pStartDate, DateTime? pEndDate)
        {
            return FuelProvider.GetFuelHistoryByCustomerId(pCustomerId,pStartDate,pEndDate);
        }

        public static List<Fetch_FuelHistory_Result> GetFuelHistoryByUserId(int pUserId, DateTime? pStartDate, DateTime? pEndDate)
        {
            return FuelProvider.GetFuelHistoryByUserId(pUserId,pStartDate,pEndDate);
        }

        public static List<Fetch_FuelHistory_Result> GetFuelHistoryByFuelTypeId(int pFuelTypeId, DateTime? pStartDate, DateTime? pEndDate)
        {
            return FuelProvider.GetFuelHistoryByFuelTypeId(pFuelTypeId, pStartDate, pEndDate);

        }
        public static List<Fetch_FuelHistory_Result> GetFuelHistoryBetweenDates(DateTime? pStartDate, DateTime? pEndDate)
        {
            return FuelProvider.GetFuelHistoryBetweenDates(pStartDate, pEndDate);
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

        public static bool DeleteFuelHistoryByUserId(int pUserId)
        {
            return FuelProvider.DeleteFuelHistoryByUserId(pUserId);
        }

        public static bool DeleteFuelHistoryByCustomerId(int pCustomerId)
        {
            return FuelProvider.DeleteFuelHistoryByCustomerId(pCustomerId);
        }

        public static bool UpdateFuelHistory(FuelHistory pHistory)
        {
            return FuelProvider.UpdateFuelHistory(pHistory);
        }
        #endregion
    }
}
