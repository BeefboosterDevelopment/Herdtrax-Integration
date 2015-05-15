
ALTER TABLE [dbo].[tblCalf] ADD [HerdtraxAnimalId] int NULL ;
ALTER TABLE [dbo].[tblCalf] ADD [HerdtraxTwinType] varchar(10) NULL;
--ALTER TABLE [dbo].[tblCalf] ADD [HerdtraxAccount] varchar(10) NULL;

GO


CREATE PROCEDURE [dbo].[ReserveSN] @numToReserve int, @lastSN int OUTPUT
AS
BEGIN
	UPDATE stblSN SET Last_SN = Last_SN + @numToReserve
	SELECT @lastSN = Last_SN FROM stblSN
END

