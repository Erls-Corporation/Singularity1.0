/*-------------------------------------------------
 * 
 * http://RafeySoft.com
 * 
 * mailto:RafeySoft@gmail.com 
 *
 * Created by RafeySoft.
 *
 *------------------------------------------------*/


/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE SaleOfTheDay.EmailList ADD
	SubscribeStatus int NOT NULL CONSTRAINT DF_EmailList_SubscribeStatus DEFAULT 2
GO
COMMIT


set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go

/************************************************************************
StoredProc:		SaleOfTheDay.EmailList_GetListToSend
Description:   	Return list of emails to send to.
				Only send one email per 24hr period
Created By:    	MM
Created On:    	10/11/2007
************************************************************************/
ALTER   PROCEDURE [SaleOfTheDay].[EmailList_GetListToSend] 

AS
	SET NOCOUNT ON

	SELECT * FROM EmailList 
		WHERE SubscribeStatus=2 AND ISNULL(DATEDIFF(DAY, LastSent, GETDATE()), 1) > 0

set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go


/************************************************************************
StoredProc:		SaleOfTheDay.EmailList_Insert
Description:   	Add an email to the list.
Created By:    	MM
Created On:    	10/11/2007
************************************************************************/
ALTER   PROCEDURE [SaleOfTheDay].[EmailList_Insert] 
@Email varchar(255),
	@Subscribe bit
AS
	SET NOCOUNT ON

	-- SubscribeStatus valid values
	-- 1=Subscribe confirmation email sent
	-- 2=Subscribe confirmed
	-- 3=Unsubscribe confirmation email sent
	-- 4=Unsubscribe confirmed
	-- 5=Already subscribed

	DECLARE @SubscribeStatus int

	SELECT 
		@Email=Email, 
		@SubscribeStatus=SubscribeStatus
	FROM 
		EmailList WHERE Email = @Email

	IF (@SubscribeStatus IS NULL)
	BEGIN
		IF(@Subscribe = 1)
		BEGIN
			INSERT INTO EmailList (Email, SubscribeStatus) VALUES (@Email, 1) 
			SET @SubscribeStatus = 1
		END
	END
	ELSE 
	BEGIN
		IF(@Subscribe = 1)
		BEGIN
			IF (@SubscribeStatus = 1) -- When subscribe is confirmed
				SET @SubscribeStatus = 2
			ELSE
				SET @SubscribeStatus = 5 -- Already subscribed
		END
		ELSE 
		BEGIN
			IF (@SubscribeStatus = 2) -- When unsubscribe email sent
				SET @SubscribeStatus = 3
			ELSE IF (@SubscribeStatus = 3) -- When unsubscribe is confirmed
				SET @SubscribeStatus = 4
			ELSE
				SET @SubscribeStatus = 0 -- This should not happen! Some error.
		END

		IF @SubscribeStatus > 0 AND @SubscribeStatus < 4
			UPDATE EmailList SET SubscribeStatus = @SubscribeStatus WHERE Email = @Email

		IF @SubscribeStatus = 4
			DELETE FROM EmailList WHERE Email = @Email
	END

	-- Email = NULL means email does not exists in db
	SELECT @Email AS Email, @SubscribeStatus AS SubscribeStatus 

	SET ANSI_NULLS ON
	SET QUOTED_IDENTIFIER ON


