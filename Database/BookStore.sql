drop database BookStore
GO
create database BookStore
GO
use BookStore
CREATE TABLE Users(
	UserId int identity(1,1) primary key not null,
	DateOfBirth datetime not null,
	Address nvarchar(100),
	Gender varchar(10),
	UserName nvarchar(30) not null,
	Password nvarchar(20) not null,
	Email varchar(50)not null,
	role varchar(10)
	)
GO
CREATE TABLE OrderBooks(
	OrderId int identity(1,1) primary key not null,
	OrderDate datetime null ,
	OrderReturnDate datetime null,
	Status int not null,
	UserId int foreign key references Users(UserId )
)
GO
CREATE TABLE Categories(
	CateId int identity(1,1) primary key not null,
	CateName nvarchar(50)
)
GO
CREATE TABLE Books(
	BookId int identity(1,1) primary key not null,
	Price int not null,
	BookImg nvarchar(50) not null,
	BookName nvarchar(50) not null,
	CurrentQuantity int not null,
	BookDetail nvarchar(1000),
	CateId int foreign key references Categories(CateId)
)
GO

CREATE TABLE OrderDetails(
	OrderDetailId int identity(1,1) primary key not null,
	OrderId int foreign key references OrderBooks(OrderId),
	BookId int foreign key references Books(BookId),
	Quantity int not null,
	Price int
)
GO
use BookStore
Insert into Users values ('2002-10-18','Ho Chi Minh City','Male','admin','admin','admin@gmail.com','Admin')
Insert into Users values ('2000-09-18 ','Ho Chi Minh City','Female','vinhhung123','123456','vinhhung@gmail.com','Customer')
Insert into Users values ('2001/01/01','Ha Noi','Male','hieuvuong123','123456','hieuvuong@gmail.com','Customer')
Insert into Users values ('2002/10/16','Thua Thien Hue','Female','hanh123','123456','hanh@gmail.com','Customer')
Insert into Users values ('2002/04/22','Da Nang','Male','mytam123','123456','mytam@gmail.com','Customer')
Insert into Users values ('2002/05/30','Tay Ninh','Female','sontung123','123456','sontung@gmail.com','Customer')

insert into Categories(CateName) values('Phe binh van hoc'),('Tieu thuyet'),('Truyen & Tho Ca Dan Gian'),('Truyen Gia Tuong – Than Bi'),('Phong Su, Ky Sy'),('Tho Ca ')

Insert into Books values(30000,'book1.png','Ra Bo Suoi Ngam Hoa Ken Hong',30,'Ra Bo Suoi Ngam Hoa Ken Hong la tac pham trong treo, tran đay tinh yeu thương mat lanh, trai ra truoc mat nguoi đoc khu vuon trai ruc ro co hoa cua vung que thanh binh',2)
Insert into Books values(40000,'book2.png','Chua Kip Lon Đa Truong Thanh',25,'Chung ta của hien tai, deu chua kip lon đa phai truong thanh.',4)
Insert into Books values(45000,'book3.png','Mui Hech',15,'Cau chuyen khong ke ve nhân vat chinh voi cuoc đoi đay bao to, nhung co nang Mui Hech (biet danh đang yeu cua nhan vat Le Ha Anh trong truyen) đa song mot doi that tron ven.',5)
Insert into Books values(75000,'book4.png','Qua dua hau khong lo',20,'Tac pham cua Naoko Shono duoc xuat ban lan dau nam 2014 voi nhan vat chinh la hai chu meo Rukkio và Furifuri',3)
Insert into Books values(80000,'book5.png','Ethon-Mot Sach Mogu-Xin Moi Ngoi',10,'Hinh anh cac ban dong vat vo cung dang yeu, moi ban deu co mot chiec ghe ngoi yeu thich cua minh. Cac ban nho hay cung quan sat chiec ghe ngoi cua moi ban co diem gu thu vi nhe!',6)


Insert into OrderBooks values('2023-01-18 18:00:00','2023-03-18 18:00:00',1,2)
Insert into OrderBooks values('2023-01-09 10:00:00','2023-03-09 10:00:00',1,2)
Insert into OrderBooks values('2022-10-16 22:00:00','2022-12-16 22:00:00',0,2)
Insert into OrderBooks values('2022-10-10 18:00:00','2022-12-10 18:00:00',1,3)
Insert into OrderBooks values('2023-02-07 10:00:00','2023-04-07 10:00:00',1,3)
Insert into OrderBooks values('2022-09-05 22:00:00','2022-11-05 22:00:00',0,3)
Insert into OrderBooks values('2022-01-01 18:00:00','2022-03-01 18:00:00',1,4)
Insert into OrderBooks values('2023-02-01 10:00:00','2023-04-01 10:00:00',1,4)
Insert into OrderBooks values('2022-09-20 22:00:00','2022-11-20 22:00:00',0,4)

Insert into OrderDetails values(1,1,2,60000)
Insert into OrderDetails values(1,2,1,40000)
Insert into OrderDetails values(1,3,2,90000)
Insert into OrderDetails values(2,1,3,90000)
Insert into OrderDetails values(2,4,1,75000)
Insert into OrderDetails values(3,4,2,75000)
Insert into OrderDetails values(3,2,1,40000)
Insert into OrderDetails values(3,3,2,90000)
Insert into OrderDetails values(4,5,2,160000)
Insert into OrderDetails values(4,1,2,60000)
Insert into OrderDetails values(5,4,2,150000)
Insert into OrderDetails values(6,3,1,90000)
Insert into OrderDetails values(7,3,2,90000)
Insert into OrderDetails values(8,1,2,60000)
Insert into OrderDetails values(9,2,1,40000)
Insert into OrderDetails values(9,3,2,90000)