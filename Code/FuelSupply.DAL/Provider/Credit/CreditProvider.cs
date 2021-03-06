﻿using FuelSupply.DAL.Database_Entity;
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

        public static List<Fetch_CreditHistory_Result> GetCreditHistoryByPaymentId(int pPaymentType, DateTime? pStartDate, DateTime? pEndDate)
        {
            var queryResult = creditDbObject.Fetch_CreditHistory(0, pStartDate, pEndDate, 0, 0, pPaymentType);
            return queryResult.ToList();
            //return creditDbObject.CreditHistories.Where(x => x.PaymentType == pPaymentType && (x.Time > pStartDate && x.Time < pEndDate)).ToList();
        }

        public static List<Fetch_CreditHistory_Result> GetCreditHistoryByCustomerId(int pCustomerId, DateTime? pStartDate, DateTime? pEndDate)
        {
            var queryResult = creditDbObject.Fetch_CreditHistory(0, pStartDate, pEndDate, 0, pCustomerId, 0);
            return queryResult.ToList();
            //return creditDbObject.CreditHistories.Where(x => x.CustomerId == pCustomerId && (x.Time > pStartDate && x.Time < pEndDate)).ToList();
        }

        public static List<Fetch_CreditHistory_Result> GetCreditHistoryByKeyCustomerId(int pKeyCustomerId, DateTime? pStartDate, DateTime? pEndDate)
        {
            var queryResult = creditDbObject.Fetch_CreditHistory(pKeyCustomerId, pStartDate, pEndDate, 0, 0, 0);
            return queryResult.ToList();
            //return creditDbObject.CreditHistories.Where(x => (x.KeyCustomerId == pKeyCustomerId || x.CustomerId == pKeyCustomerId) && (x.Time > pStartDate && x.Time < pEndDate)).ToList();           
        }

        public static List<Fetch_CreditHistory_Result> FetCreditHistoryByUserId(int pUserId, DateTime? pStartDate, DateTime? pEndDate)
        {
            var queryResult = creditDbObject.Fetch_CreditHistory(0, pStartDate, pEndDate, pUserId, 0, 0);
            return queryResult.ToList();
            //return creditDbObject.CreditHistories.Where(x => x.UserId == pUserId && (x.Time > pStartDate && x.Time < pEndDate)).ToList();
        }

        public static List<CreditHistory> FetCreditHistoryByFuelStationId(int pFuelStationId, DateTime? pStartDate, DateTime? pEndDate)
        {
            return creditDbObject.CreditHistories.Where(x => x.FuelStationId == pFuelStationId && (x.Time > pStartDate && x.Time < pEndDate)).ToList();
        }

        public static List<Fetch_CreditHistory_Result> GetFuelHistoryBetweenDates(DateTime? pStartDate, DateTime? pEndDate)
        {
            var queryResult = creditDbObject.Fetch_CreditHistory(0, pStartDate, pEndDate, 0, 0, 0);
            return queryResult.ToList();
            //return creditDbObject.CreditHistories.Where(x => x.Time > pStartDate && x.Time < pEndDate).ToList();
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

        public static bool DeleteCreditHistoryByUserId(int pUserId)
        {
            List<CreditHistory> oHistoryList = creditDbObject.CreditHistories.Where(x => x.UserId == pUserId).ToList();
            if (oHistoryList != null && oHistoryList.Count > 0)
            {
                creditDbObject.CreditHistories.RemoveRange(oHistoryList);
                creditDbObject.SaveChanges();
                return true;
            }
            return false;
        }

        public static bool DeleteCreditHistoryByCustomerId(int pCustomerId)
        {
            List<CreditHistory> oHistoryList = creditDbObject.CreditHistories.Where(x => x.CustomerId == pCustomerId).ToList();
            if (oHistoryList != null && oHistoryList.Count > 0)
            {
                creditDbObject.CreditHistories.RemoveRange(oHistoryList);
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
