CREATE TABLE Clients(
	Id uniqueidentifier PRIMARY KEY DEFAULT NEWID(),
	FirstName varchar(50),
	LastName varchar(50),
	Email varchar(255) UNIQUE,
	Logo varchar(255),
);