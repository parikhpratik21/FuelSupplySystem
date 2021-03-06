﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FuelSupply.DAL.Database_Entity
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    using FuelSupply.DAL.Entity.CustomerEntity;
    using FuelSupply.DAL.Entity.FuelEntity;
    using FuelSupply.DAL.Entity.UserEntity;
    using FuelSupply.DAL.Entity.CreditEntity;
    
    public partial class FuelSupplySystemEntities1 : DbContext
    {
        public FuelSupplySystemEntities1()
            : base("name=FuelSupplySystemEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CustomerType> CustomerTypes { get; set; }
        public virtual DbSet<FuelHistory> FuelHistories { get; set; }
        public virtual DbSet<FuelLimitType> FuelLimitTypes { get; set; }
        public virtual DbSet<FuelStation> FuelStations { get; set; }
        public virtual DbSet<FuelType> FuelTypes { get; set; }
        public virtual DbSet<PaymentType> PaymentTypes { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserType> UserTypes { get; set; }
        public virtual DbSet<CreditHistory> CreditHistories { get; set; }
        public virtual DbSet<CustomerFingerPrint> CustomerFingerPrints { get; set; }
        public virtual DbSet<FingerPrintType> FingerPrintTypes { get; set; }
        public virtual DbSet<Shift> Shifts { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
    
        public virtual ObjectResult<Fetch_CreditHistory_Result> Fetch_CreditHistory(Nullable<int> keyCustomerId, Nullable<System.DateTime> startTime, Nullable<System.DateTime> endTime, Nullable<int> userId, Nullable<int> customerId, Nullable<int> paymentId)
        {
            var keyCustomerIdParameter = keyCustomerId.HasValue ?
                new ObjectParameter("KeyCustomerId", keyCustomerId) :
                new ObjectParameter("KeyCustomerId", typeof(int));
    
            var startTimeParameter = startTime.HasValue ?
                new ObjectParameter("StartTime", startTime) :
                new ObjectParameter("StartTime", typeof(System.DateTime));
    
            var endTimeParameter = endTime.HasValue ?
                new ObjectParameter("EndTime", endTime) :
                new ObjectParameter("EndTime", typeof(System.DateTime));
    
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            var customerIdParameter = customerId.HasValue ?
                new ObjectParameter("CustomerId", customerId) :
                new ObjectParameter("CustomerId", typeof(int));
    
            var paymentIdParameter = paymentId.HasValue ?
                new ObjectParameter("PaymentId", paymentId) :
                new ObjectParameter("PaymentId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Fetch_CreditHistory_Result>("Fetch_CreditHistory", keyCustomerIdParameter, startTimeParameter, endTimeParameter, userIdParameter, customerIdParameter, paymentIdParameter);
        }
    
        public virtual ObjectResult<Fetch_FuelHistory_Result> Fetch_FuelHistory(Nullable<int> keyCustomerId, Nullable<System.DateTime> startTime, Nullable<System.DateTime> endTime, Nullable<int> userId, Nullable<int> customerId, Nullable<int> fuelId)
        {
            var keyCustomerIdParameter = keyCustomerId.HasValue ?
                new ObjectParameter("KeyCustomerId", keyCustomerId) :
                new ObjectParameter("KeyCustomerId", typeof(int));
    
            var startTimeParameter = startTime.HasValue ?
                new ObjectParameter("StartTime", startTime) :
                new ObjectParameter("StartTime", typeof(System.DateTime));
    
            var endTimeParameter = endTime.HasValue ?
                new ObjectParameter("EndTime", endTime) :
                new ObjectParameter("EndTime", typeof(System.DateTime));
    
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            var customerIdParameter = customerId.HasValue ?
                new ObjectParameter("CustomerId", customerId) :
                new ObjectParameter("CustomerId", typeof(int));
    
            var fuelIdParameter = fuelId.HasValue ?
                new ObjectParameter("FuelId", fuelId) :
                new ObjectParameter("FuelId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Fetch_FuelHistory_Result>("Fetch_FuelHistory", keyCustomerIdParameter, startTimeParameter, endTimeParameter, userIdParameter, customerIdParameter, fuelIdParameter);
        }
    }
}
