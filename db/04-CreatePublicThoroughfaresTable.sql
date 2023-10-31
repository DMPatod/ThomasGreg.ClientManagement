CREATE TABLE PublicThoroughfares(
	Id uniqueidentifier PRIMARY KEY DEFAULT NEWID(),
	Street varchar(255),
	Number int,
	City varchar(50),
	State varchar(50),
	AditionalInformation varchar(255),
	ClientId uniqueidentifier,
	FOREIGN KEY (ClientId) REFERENCES Clients(Id) ON DELETE CASCADE
);