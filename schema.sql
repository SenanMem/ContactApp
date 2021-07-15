DROP DATABASE ContactDb;
GO
CREATE DATABASE ContactDb;
GO
USE ContactDb;
GO
CREATE TABLE contacts(
	id NVARCHAR(255) NOT NULL PRIMARY KEY,
	contactName NVARCHAR(255) NOT NULL,
	phoneNumber NVARCHAR(255) NOT NULL,
);


INSERT INTO contacts(id, contactName, phoneNumber) VALUES(
	'c4caa3b5-b69e-4e22-a6e0-3ecbcabd9136',
	'John Doe',
	'0554512451'
);


INSERT INTO contacts(id, contactName, phoneNumber) VALUES(
	'3d61fec6-40e3-45e0-a6f8-aa2dae2ee55f',
	'Mark',
	'0517658236'
);