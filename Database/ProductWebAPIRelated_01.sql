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
Select * from [dbo].[TodoItems]
/*
INSERT INTO Categories ( Name, Description) VALUES
--(1,'Oils','Cooking & edible oils'),
--(2,'Dairy','Milk, Butter, Curd'),
--(3,'Spices','Cooking spices'),
('Staples','Daily essential staples'),
('Vegetables','Fresh vegetables'),
('Bakery','Bakery & bread products');

*/


/*
-- Products
INSERT INTO Products (Name, Description, CategoryId) VALUES
('Washing Soap','',4),
('Sugar','',4),

('Vim Soap Bar','',4),
('Salt','',4),
('Tea Powder','',4),
('Jaggery','',4),
('Bathing Soap','',4),
('Rice','',4),
('Wheat Flour','',4),

('Rice Flakes','',4),
('Coriander','',3),
('Green Chilli','',5),
('Ginger','',5),
('Onion','',5),
('Tomato','',5),
('Lemon','',5),
('Potato','',5),
('Amul Butter','',2),
('Curd','',2),
('Buffalo Milk','',2),

('Govardhan Cow Ghee','',2),
('Brown Bread','',6),
('Lady Fingers','',5),
('Cumin Seeds','',3),
('Cabbage','',5),
('Cheese Cube','',2);
*/

/*
CREATE DATABASE TodoRequisitionTracker
go

*/

/*
Use TodoRequisitionTracker ;
go 
*/

/*
CREATE DATABASE StockEvents
*/

