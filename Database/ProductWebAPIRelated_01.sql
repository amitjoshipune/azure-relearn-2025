/*
-- Create database
CREATE DATABASE HouseholdTracker;
GO
USE HouseholdTracker;
GO

*/

/*
-- Category table
CREATE TABLE Categories (
  Id INT IDENTITY(1,1) PRIMARY KEY,
  Name NVARCHAR(100) NOT NULL,
  Description NVARCHAR(250) NULL
);

-- Product table
CREATE TABLE Products (
  Id INT IDENTITY(1,1) PRIMARY KEY,
  Name NVARCHAR(100) NOT NULL,
  Description NVARCHAR(250) NULL,
  CategoryId INT NULL,
  FOREIGN KEY (CategoryId) REFERENCES Categories(Id)
);

-- Sample data
INSERT INTO Categories (Name, Description) VALUES
('Oils', 'Cooking & edible oils'),
('Dairy', 'Milk, Butter, Curd'),
('Spices', 'Cooking spices');

INSERT INTO Products (Name, Description, CategoryId) VALUES
('Rice Bran Oil', 'Healthy cooking oil', 1),
('Cow Ghee', 'Clarified butter', 2),
('Red Chilli Powder', 'Spicy red powder', 3);
*/

USE HouseholdTracker;
SELECT * FROM __EFMigrationsHistory;
SELECT * FROM Products;
Select * from Categories