
ALTER TABLE [dbo].[tblCalf] ADD [HerdtraxAnimalId] int NULL ;
GO


CREATE PROCEDURE [dbo].[ReserveSN] @numToReserve int, @lastSN int OUTPUT
AS
BEGIN
	UPDATE stblSN SET Last_SN = Last_SN + @numToReserve
	SELECT @lastSN = Last_SN FROM stblSN
END

