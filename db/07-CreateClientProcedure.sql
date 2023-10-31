CREATE PROCEDURE CreateClient
	@FirstName varchar(50),
	@LastName varchar(50),
	@Email varchar(255),
	@Logo varchar(255),
	@PublicThoroughfares PublicThoroughfareType READONLY,
	@Output uniqueidentifier OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @ClientId uniqueidentifier = NEWID();
	
	INSERT INTO Clients (Id, FirstName, LastName, Email, Logo)
	VALUES (@ClientId, @FirstName, @LastName, @Email, @Logo);

	INSERT INTO PublicThoroughfares (Id, Street, Number, City, State, AditionalInformation, ClientId)
		SELECT NEWID(), Street, Number, City, State, AditionalInformation, @ClientId
		FROM @PublicThoroughfares;

	Set @Output = @ClientId;
END;