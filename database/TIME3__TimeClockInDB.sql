USE [TimeClockIn]
ALTER TABLE Location 
ALTER COLUMN Latitude Decimal (18,7);

USE [TimeClockIn]
ALTER TABLE Location
ALTER COLUMN Longitude Decimal (18,7);

USE [TimeClockIn]
ALTER TABLE EmployeeLocationDetails
ALTER COLUMN Latitude Decimal (18,7);

USE [TimeClockIn]
ALTER TABLE EmployeeLocationDetails
ALTER COLUMN Longitude Decimal (18,7);