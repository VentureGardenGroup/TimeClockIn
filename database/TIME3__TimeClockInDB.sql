ALTER TABLE Location 
ALTER COLUMN Latitude Decimal (18,7);

ALTER TABLE Location
ALTER COLUMN Longitude Decimal (18,7);

ALTER TABLE EmployeeLocationDetails
ALTER COLUMN Latitude Decimal (18,7);

ALTER TABLE EmployeeLocationDetails
ALTER COLUMN Longitude Decimal (18,7);