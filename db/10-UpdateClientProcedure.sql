CREATE PROCEDURE UpdateClient
  @Client ClientType READONLY,
  @PublicThoroughfares PublicThoroughfareType READONLY,
  @Output uniqueidentifier OUTPUT
AS
BEGIN
  SET NOCOUNT ON;

  DECLARE @ClientId uniqueidentifier;

  UPDATE db
  SET
    db.FirstName = temp.FirstName,
    db.LastName = temp.LastName,
    db.Email = temp.Email,
    db.Logo = temp.Logo
  From Clients db
  INNER JOIN @Client temp ON db.Id = temp.Id;

  SET @ClientId = (SELECT Id FROM @Client);

  DELETE FROM PublicThoroughfares
  WHERE ClientId = @ClientId;

  INSERT INTO PublicThoroughfares (Street, Number, City, State, AditionalInformation, ClientId)
    SELECT Street, Number, City, State, AditionalInformation, @ClientId
    FROM @PublicThoroughfares;

  SET @Output = @ClientId;
END;