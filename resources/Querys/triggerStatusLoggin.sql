SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ========================================================
-- Author:		juan.rangel@fgjtam.gob.mx
-- Create date: 2024-10-24
-- Description:	The application updates continuously the
--  column `updated_at` on the table `clientsStatusLog`,
--  this trigger prevents to insert a duplicated record 
--  with the same name and address
-- ========================================================

CREATE TRIGGER [dbo].[clientsStatusLogInsert]
   ON  [dbo].[clientsStatusLog]
   INSTEAD OF INSERT
AS 
BEGIN
	SET NOCOUNT ON;
	-- Update the 'updated_at' column if the name and address exist
	UPDATE L
	SET L.updated_at = GETDATE()
	FROM [dbo].[clientsStatusLog] L
	INNER JOIN inserted I 
	ON L.[name] = I.[name] AND L.[address] = I.[address];

	-- Insert the new record if there is no existing match
	INSERT INTO [dbo].[clientsStatusLog] (name, address, updated_at)
	SELECT I.name, I.address, GETDATE()
	FROM inserted I
	WHERE NOT EXISTS (
		SELECT 1
		FROM [dbo].[clientsStatusLog] L
		WHERE L.[name] = I.[name] AND L.[address] = I.[address]
	);

END
GO