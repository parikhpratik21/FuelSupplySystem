insert into FuelType(Name,Rate) values('Combo',0.0);

USE [FuelSupplySystem]
GO
/****** Object:  StoredProcedure [dbo].[Fetch_FuelHistory]    Script Date: 12/3/2017 6:44:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[Fetch_FuelHistory]
	@KeyCustomerId as int, @StartTime as datetime, @EndTime as datetime, @UserId as int, 
	@CustomerId as int, @FuelId as int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
   
   If @KeyCustomerId > 0
   Begin
			select history.Id, Time, Customer.Name as CustomerName, Customer.Id as CustomerId, FuelType.Name as FuelType, 
			ShiftName, [User].Name as AttendantName, ISNULL(history.KeyCustomerName, Customer.Name) as KeyCustomerName, history.KeyCustomerId,
			FuelAmount, FuelVolume, InvoiceNo, ActualFuelAmount, ActualFuelVolume, history.CustomerLastBalance
			from dbo.FuelHistory history, Customer, FuelType, [User]
			where history.CustomerId = Customer.Id and history.FuelType = FuelType.Id 
			and history.UserId = [User].Id and time > @StartTime and time < @EndTime and history.KeyCustomerId = @KeyCustomerId			
	End

	Else if @CustomerId > 0
	Begin
			select history.Id, Time, Customer.Name as CustomerName, Customer.Id as CustomerId, FuelType.Name as FuelType, 
			ShiftName, [User].Name as AttendantName, ISNULL(history.KeyCustomerName, Customer.Name) as KeyCustomerName, history.KeyCustomerId,
			FuelAmount, FuelVolume, InvoiceNo, ActualFuelAmount, ActualFuelVolume, history.CustomerLastBalance
			from dbo.FuelHistory history, Customer, FuelType, [User]
			where history.CustomerId = Customer.Id and history.FuelType = FuelType.Id 
			and history.UserId = [User].Id and time > @StartTime and time < @EndTime and history.CustomerId = @CustomerId					
	End

	Else if @UserId > 0
	Begin
			select history.Id, Time, Customer.Name as CustomerName, Customer.Id as CustomerId, FuelType.Name as FuelType, 
			ShiftName, [User].Name as AttendantName, ISNULL(history.KeyCustomerName, Customer.Name) as KeyCustomerName, history.KeyCustomerId,
			FuelAmount, FuelVolume, InvoiceNo, ActualFuelAmount, ActualFuelVolume, history.CustomerLastBalance
			from dbo.FuelHistory history, Customer, FuelType, [User]
			where history.CustomerId = Customer.Id and history.FuelType = FuelType.Id 
			and history.UserId = [User].Id and time > @StartTime and time < @EndTime and history.UserId = @UserId				
	End

	Else if @FuelId > 0
	Begin
			select history.Id, Time, Customer.Name as CustomerName, Customer.Id as CustomerId, FuelType.Name as FuelType, 
			ShiftName, [User].Name as AttendantName, ISNULL(history.KeyCustomerName, Customer.Name) as KeyCustomerName, history.KeyCustomerId,
			FuelAmount, FuelVolume, InvoiceNo, ActualFuelAmount, ActualFuelVolume, history.CustomerLastBalance
			from dbo.FuelHistory history, Customer, FuelType, [User]
			where history.CustomerId = Customer.Id and history.FuelType = FuelType.Id 
			and history.UserId = [User].Id and time > @StartTime and time < @EndTime and history.FuelType = @FuelId
	End

	Else 
	Begin
			select history.Id, Time, Customer.Name as CustomerName, Customer.Id as CustomerId, FuelType.Name as FuelType, 
			ShiftName, [User].Name as AttendantName, ISNULL(history.KeyCustomerName, Customer.Name) as KeyCustomerName, history.KeyCustomerId,
			FuelAmount, FuelVolume, InvoiceNo, ActualFuelAmount, ActualFuelVolume, history.CustomerLastBalance
			from dbo.FuelHistory history, Customer, FuelType, [User]
			where history.CustomerId = Customer.Id and history.FuelType = FuelType.Id 
			and history.UserId = [User].Id and time > @StartTime and time < @EndTime 
	End
END


GO
/****** Object:  StoredProcedure [dbo].[Fetch_CreditHistory]    Script Date: 12/3/2017 6:44:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[Fetch_CreditHistory]
	@KeyCustomerId as int, @StartTime as datetime, @EndTime as datetime, @UserId as int, 
	@CustomerId as int, @PaymentId as int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
   
   If @KeyCustomerId > 0
   Begin
			select history.Id, Time, Customer.Name as CustomerName, Customer.Id as CustomerId, PaymentType.Name as PaymentType, CreditAmount, 
			ShiftName, [User].Name as AttendantName, ISNULL(NULLIF(history.KeyCustomerName, ''), Customer.Name) as KeyCustomerName, history.KeyCustomerId, history.IsAdjustmentCredit, history.CustomerLastBalance 
			from dbo.CreditHistory history, Customer, PaymentType, [User]
			where history.CustomerId = Customer.Id and history.PaymentType = PaymentType.Id 
			and history.UserId = [User].Id and time > @StartTime and time < @EndTime and  history.KeyCustomerId = @KeyCustomerId
				
	End

	Else if @CustomerId > 0
	Begin
			select history.Id, Time, Customer.Name as CustomerName, Customer.Id as CustomerId, PaymentType.Name as PaymentType, CreditAmount, 
			ShiftName, [User].Name as AttendantName, ISNULL(NULLIF(history.KeyCustomerName, ''), Customer.Name) as KeyCustomerName, history.KeyCustomerId, history.IsAdjustmentCredit, history.CustomerLastBalance
			from dbo.CreditHistory history, Customer, PaymentType, [User]
			where history.CustomerId = Customer.Id and history.PaymentType = PaymentType.Id 
			and history.UserId = [User].Id and time > @StartTime and time < @EndTime and history.CustomerId = @CustomerId				
	End

	Else if @UserId > 0
	Begin
			select history.Id, Time, Customer.Name as CustomerName, Customer.Id as CustomerId, PaymentType.Name as PaymentType, CreditAmount, 
			ShiftName, [User].Name as AttendantName, ISNULL(NULLIF(history.KeyCustomerName, ''), Customer.Name) as KeyCustomerName, history.KeyCustomerId, history.IsAdjustmentCredit, history.CustomerLastBalance
			from dbo.CreditHistory history, Customer, PaymentType, [User]
			where history.CustomerId = Customer.Id and history.PaymentType = PaymentType.Id 
			and history.UserId = [User].Id and time > @StartTime and time < @EndTime and history.UserId = @UserId				
	End

	Else if @PaymentId > 0
	Begin
			select history.Id, Time, Customer.Name as CustomerName, Customer.Id as CustomerId, PaymentType.Name as PaymentType, CreditAmount, 
			ShiftName, [User].Name as AttendantName, ISNULL(NULLIF(history.KeyCustomerName, ''), Customer.Name) as KeyCustomerName, history.KeyCustomerId, history.IsAdjustmentCredit, history.CustomerLastBalance
			from dbo.CreditHistory history, Customer, PaymentType, [User]
			where history.CustomerId = Customer.Id and history.PaymentType = PaymentType.Id 
			and history.UserId = [User].Id and time > @StartTime and time < @EndTime and history.PaymentType = @PaymentId				
	End

	Else 
	Begin
			select history.Id, Time, Customer.Name as CustomerName, Customer.Id as CustomerId, PaymentType.Name as PaymentType, CreditAmount, 
			ShiftName, [User].Name as AttendantName, ISNULL(NULLIF(history.KeyCustomerName, ''), Customer.Name) as KeyCustomerName, history.KeyCustomerId, history.IsAdjustmentCredit, history.CustomerLastBalance
			from dbo.CreditHistory history, Customer, PaymentType, [User]
			where history.CustomerId = Customer.Id and history.PaymentType = PaymentType.Id 
			and history.UserId = [User].Id and time > @StartTime and time < @EndTime			
	End
END