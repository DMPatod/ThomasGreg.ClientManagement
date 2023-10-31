CREATE PROCEDURE DeleteClient
	@Id uniqueidentifier,
	@Output int OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM Clients
	WHERE Id = @Id;

	SET @Output = @@ROWCOUNT;
END;