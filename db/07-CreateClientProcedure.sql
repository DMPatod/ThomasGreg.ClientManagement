CREATE PROCEDURE CreateClient
	@Client ClientType READONLY,
	@PublicThoroughfares PublicThoroughfareType READONLY,
	@Output uniqueidentifier OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	
	DECLARE @ClientId uniqueidentifier;

	INSERT INTO Clients (Id, FirstName, LastName, Email, Logo)
		SELECT Id, FirstName, LastName, Email, Logo
		FROM @Client;

	SET @ClientId = (SELECT Id FROM @Client);

	INSERT INTO PublicThoroughfares (Id, Street, Number, City, State, AditionalInformation, ClientId)
		SELECT Id, Street, Number, City, State, AditionalInformation, @ClientId
		FROM @PublicThoroughfares;

	SET @Output = @ClientId;
END;