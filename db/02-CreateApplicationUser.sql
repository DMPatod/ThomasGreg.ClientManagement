USE ThomasGreg;

CREATE LOGIN application_login WITH PASSWORD = 'xGN0KSXm7nd9';
CREATE USER application_user FOR LOGIN application_login;

GRANT SELECT, INSERT, UPDATE, DELETE, EXECUTE TO application_user;