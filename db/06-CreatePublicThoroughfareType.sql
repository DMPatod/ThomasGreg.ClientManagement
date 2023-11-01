CREATE TYPE PublicThoroughfareType AS TABLE (
	Id uniqueidentifier NULL,
	Street varchar(255) NULL,
	Number int NULL,
	City varchar(255) NULL,
	State varchar(255) NULL,
	AditionalInformation varchar(255)
);