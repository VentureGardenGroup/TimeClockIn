DELETE FROM Location WHERE id= 1
GO

ALTER TABLE Location
ALTER COLUMN Latitude float NOT NULL
GO

ALTER TABLE Location
ALTER COLUMN Longitude float NOT NULL
GO

ALTER TABLE EmployeeLocationDetails
ALTER COLUMN Latitude float NOT NULL
GO

ALTER TABLE EmployeeLocationDetails
ALTER COLUMN Longitude float NOT NULL
GO