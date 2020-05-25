CREATE TABLE "Employees"
(
	"EmployeeID" INT NOT NULL PRIMARY KEY, 
    "Name" NVARCHAR(50) NOT NULL, 
	"Age" INT NOT NULL,
    "EmailAddress" NVARCHAR(50) NULL, 
    "UpdatedDate" DATE NOT NULL
)

INSERT "Employees" (EmployeeID, Name, Age, EmailAddress, UpdatedDate) VALUES (1, 'Name A', 20, 'hogehoge@emai.com', '2020/05/01')
INSERT "Employees" (EmployeeID, Name, Age, EmailAddress, UpdatedDate) VALUES (2, 'Name BB', 30, 'fugafuga@emai.com', '2020/05/02')
INSERT "Employees" (EmployeeID, Name, Age, EmailAddress, UpdatedDate) VALUES (3, 'Name CCC', 40, 'piyopiyo@emai.com', '2020/05/03')
INSERT "Employees" (EmployeeID, Name, Age, EmailAddress, UpdatedDate) VALUES (4, 'Name DDDD', 50, 'foooooooo@emai.com', '2020/05/04')
INSERT "Employees" (EmployeeID, Name, Age, EmailAddress, UpdatedDate) VALUES (5, 'Name EEEEE', 60, 'barbarbar@emai.com', '2020/05/05')
INSERT "Employees" (EmployeeID, Name, Age, EmailAddress, UpdatedDate) VALUES (6, 'Name F', 25, 'ffffffffff@emai.com', '2020/05/01')
INSERT "Employees" (EmployeeID, Name, Age, EmailAddress, UpdatedDate) VALUES (7, 'Name GG', 35, 'gggggggggg@emai.com', '2020/05/02')
INSERT "Employees" (EmployeeID, Name, Age, EmailAddress, UpdatedDate) VALUES (8, 'Name HHH', 45, 'hhhhhhhhhh@emai.com', '2020/05/03')
INSERT "Employees" (EmployeeID, Name, Age, EmailAddress, UpdatedDate) VALUES (9, 'Name IIII', 55, 'iiiiiiiiii@emai.com', '2020/05/04')
INSERT "Employees" (EmployeeID, Name, Age, EmailAddress, UpdatedDate) VALUES (10, 'Name JJJJJ', 65, 'jjjjjjjjjj@emai.com', '2020/05/05')