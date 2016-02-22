-- Script Date: 21/02/2016 22:36  - ErikEJ.SqlCeScripting version 3.5.2.58
-- Database information:
-- Database: C:\Users\Payo\Documents\Visual Studio 2015\Projects\Lepos\LeposWPF\DB\lepos_DB
-- ServerVersion: 3.9.2
-- DatabaseSize: 37 KB
-- Created: 14/02/2016 10:55

-- User Table information:
-- Number of tables: 12
-- Client: -1 row(s)
-- Company: -1 row(s)
-- Product: -1 row(s)
-- ProductPrice: -1 row(s)
-- Provider: -1 row(s)
-- Purchase: -1 row(s)
-- PurchasePayment: -1 row(s)
-- PurchaseProduct: -1 row(s)
-- Sale: -1 row(s)
-- SalePayment: -1 row(s)
-- SaleProduct: -1 row(s)
-- User: -1 row(s)

SELECT 1;
PRAGMA foreign_keys=OFF;
BEGIN TRANSACTION;
CREATE TABLE [User] (
  [ID] nvarchar(100) NOT NULL
, [Password] nvarchar(100) NOT NULL
, [Type] int NOT NULL
, [Birth] datetime NOT NULL
, [Death] datetime NOT NULL
, [IsEditable] bit NOT NULL
, [IsErasable] bit NOT NULL
, [IsAlive] bit NOT NULL
, CONSTRAINT [sqlite_autoindex_User_1] PRIMARY KEY ([ID])
);
CREATE TABLE [Provider] (
  [ID] bigint NOT NULL
, [Name] nvarchar(100) NOT NULL
, [RFC] nvarchar(100) NOT NULL
, [Email] nvarchar(100) NOT NULL
, [Address] nvarchar(100) NOT NULL
, [Phone] nvarchar(100) NOT NULL
, [Birth] datetime NOT NULL
, [Death] datetime NOT NULL
, [IsAlive] bit NOT NULL
, CONSTRAINT [sqlite_master_PK_Provider] PRIMARY KEY ([ID])
);
CREATE TABLE [Purchase] (
  [ID] bigint NOT NULL
, [Provider_ID] bigint NOT NULL
, [User_ID] nvarchar(100) NOT NULL
, [IsCredit] bit NOT NULL
, [CreditDays] int NOT NULL
, [Folio] nvarchar(100) NOT NULL
, [Date] datetime NOT NULL
, [Total] real NOT NULL
, CONSTRAINT [sqlite_master_PK_Purchase] PRIMARY KEY ([ID])
, FOREIGN KEY ([User_ID]) REFERENCES [User] ([ID]) ON DELETE RESTRICT ON UPDATE CASCADE
, FOREIGN KEY ([Provider_ID]) REFERENCES [Provider] ([ID]) ON DELETE RESTRICT ON UPDATE CASCADE
);
CREATE TABLE [PurchasePayment] (
  [ID] bigint NOT NULL
, [Purchase_ID] bigint NOT NULL
, [User_ID] nvarchar(100) NOT NULL
, [Date] datetime NOT NULL
, [Payment] real NOT NULL
, CONSTRAINT [sqlite_master_PK_PurchasePayment] PRIMARY KEY ([ID])
, FOREIGN KEY ([User_ID]) REFERENCES [User] ([ID]) ON DELETE RESTRICT ON UPDATE CASCADE
, FOREIGN KEY ([Purchase_ID]) REFERENCES [Purchase] ([ID]) ON DELETE RESTRICT ON UPDATE CASCADE
);
CREATE TABLE [Product] (
  [ID] nvarchar(100) NOT NULL
, [Price] real NOT NULL
, [WholeSalePrice] real NOT NULL
, [Cost] real NOT NULL
, [Quantity] real NOT NULL
, [MinimumQuanity] real NOT NULL
, [Description] nvarchar(100) NOT NULL
, [Birth] datetime NOT NULL
, [Death] datetime NOT NULL
, [IsAlive] bit NOT NULL
, CONSTRAINT [sqlite_autoindex_Product_1] PRIMARY KEY ([ID])
);
CREATE TABLE [PurchaseProduct] (
  [Purchase_ID] bigint NOT NULL
, [Product_ID] nvarchar(100) NOT NULL
, [Quantity] real NOT NULL
, [Price] real NOT NULL
, CONSTRAINT [sqlite_autoindex_PurchaseProduct_1] PRIMARY KEY ([Purchase_ID],[Product_ID])
, FOREIGN KEY ([Product_ID]) REFERENCES [Product] ([ID]) ON DELETE RESTRICT ON UPDATE CASCADE
, FOREIGN KEY ([Purchase_ID]) REFERENCES [Purchase] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);
CREATE TABLE [ProductPrice] (
  [ID] bigint NOT NULL
, [Product_ID] nvarchar(100) NOT NULL
, [User_ID] nvarchar(100) NOT NULL
, [Purchase_ID] bigint NULL
, [Price] real NOT NULL
, [Date] datetime NOT NULL
, CONSTRAINT [sqlite_master_PK_ProductPrice] PRIMARY KEY ([ID])
, FOREIGN KEY ([Product_ID]) REFERENCES [Product] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
, FOREIGN KEY ([User_ID]) REFERENCES [User] ([ID]) ON DELETE RESTRICT ON UPDATE CASCADE
);
CREATE TABLE [Company] (
  [ID] bigint NOT NULL
, [Name] nvarchar(100) NOT NULL
, [RFC] nvarchar(100) NOT NULL
, [Description] nvarchar(100) NOT NULL
, [Address] nvarchar(100) NOT NULL
, [ZIP] nvarchar(100) NOT NULL
, [IVAType] int NOT NULL
, [Logo] nvarchar(100) NOT NULL
, [Printer] nvarchar(100) NOT NULL
, [FontSize] nvarchar(100) NOT NULL
, [CharsPerLine] nvarchar(100) NOT NULL
, [Cash] real NOT NULL
, [IsActivated] bit NOT NULL
, [DateActivated] datetime NOT NULL
, [Version] nvarchar(100) NOT NULL
, CONSTRAINT [sqlite_autoindex_Company_1] PRIMARY KEY ([ID])
);
CREATE TABLE [Client] (
  [ID] bigint NOT NULL
, [Name] nvarchar(100) NOT NULL
, [RFC] nvarchar(100) NOT NULL
, [Address] nvarchar(100) NOT NULL
, [Phone] nvarchar(100) NOT NULL
, [Email] nvarchar(100) NOT NULL
, [Birth] datetime NOT NULL
, [Death] datetime NOT NULL
, [IsEditable] bit NOT NULL
, [IsErasable] bit NOT NULL
, [IsGeneralPublic] bit NOT NULL
, [IsAlive] bit NOT NULL
, CONSTRAINT [sqlite_master_PK_Client] PRIMARY KEY ([ID])
);
CREATE TABLE [Sale] (
  [ID] bigint NOT NULL
, [Client_ID] bigint NOT NULL
, [User_ID] nvarchar(100) NOT NULL
, [Date] datetime NOT NULL
, [IsWholeSale] bit NOT NULL
, [IsCredit] bit NOT NULL
, [CreditDays] int NOT NULL
, [Discount] real NOT NULL
, [IVAType] int NOT NULL
, [SubTotal] real NOT NULL
, [Total] real NOT NULL
, CONSTRAINT [sqlite_master_PK_Sale] PRIMARY KEY ([ID])
, FOREIGN KEY ([User_ID]) REFERENCES [User] ([ID]) ON DELETE RESTRICT ON UPDATE CASCADE
, FOREIGN KEY ([Client_ID]) REFERENCES [Client] ([ID]) ON DELETE RESTRICT ON UPDATE CASCADE
);
CREATE TABLE [SaleProduct] (
  [Sale_ID] bigint NOT NULL
, [Product_ID] nvarchar(100) NOT NULL
, [Quantity] real NOT NULL
, [Price] real NOT NULL
, CONSTRAINT [sqlite_autoindex_SaleProduct_1] PRIMARY KEY ([Sale_ID],[Product_ID])
, FOREIGN KEY ([Product_ID]) REFERENCES [Product] ([ID]) ON DELETE RESTRICT ON UPDATE CASCADE
, FOREIGN KEY ([Sale_ID]) REFERENCES [Sale] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);
CREATE TABLE [SalePayment] (
  [ID] bigint NOT NULL
, [Sale_ID] bigint NOT NULL
, [User_ID] nvarchar(100) NOT NULL
, [Date] datetime NOT NULL
, [Payment] real NOT NULL
, CONSTRAINT [sqlite_master_PK_SalePayment] PRIMARY KEY ([ID])
, FOREIGN KEY ([User_ID]) REFERENCES [User] ([ID]) ON DELETE RESTRICT ON UPDATE CASCADE
, FOREIGN KEY ([Sale_ID]) REFERENCES [Sale] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);
INSERT INTO [User] ([ID],[Password],[Type],[Birth],[Death],[IsEditable],[IsErasable],[IsAlive]) VALUES (
'admin','admin',0,'2016-02-21 19:03:41.000','2016-02-21 19:03:41.000',0,0,1);
INSERT INTO [User] ([ID],[Password],[Type],[Birth],[Death],[IsEditable],[IsErasable],[IsAlive]) VALUES (
'payo','payo',0,'2016-02-21 19:03:41.000','2016-02-21 19:03:41.000',1,1,1);
INSERT INTO [User] ([ID],[Password],[Type],[Birth],[Death],[IsEditable],[IsErasable],[IsAlive]) VALUES (
'test','DELIO',0,'2016-02-21 00:00:00.000','2016-02-21 22:17:45.412',1,1,0);
INSERT INTO [Provider] ([ID],[Name],[RFC],[Email],[Address],[Phone],[Birth],[Death],[IsAlive]) VALUES (
0,' a ',' a ',' a ',' a ',' a ','0001-01-01 00:00:00.000','0001-01-01 00:00:00.000',0);
INSERT INTO [Provider] ([ID],[Name],[RFC],[Email],[Address],[Phone],[Birth],[Death],[IsAlive]) VALUES (
1,'a','f','f','f','f','0001-01-01 00:00:00.000','0001-01-01 00:00:00.000',1);
INSERT INTO [Provider] ([ID],[Name],[RFC],[Email],[Address],[Phone],[Birth],[Death],[IsAlive]) VALUES (
2,'adf','j','j','j','j','0001-01-01 00:00:00.000','0001-01-01 00:00:00.000',1);
INSERT INTO [Provider] ([ID],[Name],[RFC],[Email],[Address],[Phone],[Birth],[Death],[IsAlive]) VALUES (
3,'p','o','o','o','o','0001-01-01 00:00:00.000','0001-01-01 00:00:00.000',1);
INSERT INTO [Provider] ([ID],[Name],[RFC],[Email],[Address],[Phone],[Birth],[Death],[IsAlive]) VALUES (
4,'asdf','sdf','j','asfd','j','0001-01-01 00:00:00.000','2016-02-21 22:17:58.176',0);
INSERT INTO [Provider] ([ID],[Name],[RFC],[Email],[Address],[Phone],[Birth],[Death],[IsAlive]) VALUES (
5,'payo','y','y','y','y','0001-01-01 00:00:00.000','0001-01-01 00:00:00.000',1);
INSERT INTO [Purchase] ([ID],[Provider_ID],[User_ID],[IsCredit],[CreditDays],[Folio],[Date],[Total]) VALUES (
1,1,'payo',0,0,'','2016-02-21 19:03:31.704',3);
INSERT INTO [Purchase] ([ID],[Provider_ID],[User_ID],[IsCredit],[CreditDays],[Folio],[Date],[Total]) VALUES (
2,1,'payo',0,0,'','2016-02-21 19:06:08.405',476);
INSERT INTO [Purchase] ([ID],[Provider_ID],[User_ID],[IsCredit],[CreditDays],[Folio],[Date],[Total]) VALUES (
3,4,'payo',0,0,'','2016-02-21 19:06:14.845',0);
INSERT INTO [Purchase] ([ID],[Provider_ID],[User_ID],[IsCredit],[CreditDays],[Folio],[Date],[Total]) VALUES (
4,1,'payo',0,0,'','2016-02-21 19:06:16.129',0);
INSERT INTO [Purchase] ([ID],[Provider_ID],[User_ID],[IsCredit],[CreditDays],[Folio],[Date],[Total]) VALUES (
5,1,'payo',0,0,'','2016-02-21 19:06:16.228',0);
INSERT INTO [Purchase] ([ID],[Provider_ID],[User_ID],[IsCredit],[CreditDays],[Folio],[Date],[Total]) VALUES (
6,1,'payo',0,0,'','2016-02-21 19:06:16.249',0);
INSERT INTO [Purchase] ([ID],[Provider_ID],[User_ID],[IsCredit],[CreditDays],[Folio],[Date],[Total]) VALUES (
7,1,'payo',0,0,'','2016-02-21 19:08:45.762',3);
INSERT INTO [Purchase] ([ID],[Provider_ID],[User_ID],[IsCredit],[CreditDays],[Folio],[Date],[Total]) VALUES (
8,1,'payo',0,0,'','2016-02-21 19:08:51.869',3);
INSERT INTO [Purchase] ([ID],[Provider_ID],[User_ID],[IsCredit],[CreditDays],[Folio],[Date],[Total]) VALUES (
9,1,'payo',0,0,'','2016-02-21 19:08:59.447',3);
INSERT INTO [Purchase] ([ID],[Provider_ID],[User_ID],[IsCredit],[CreditDays],[Folio],[Date],[Total]) VALUES (
10,1,'payo',0,0,'','2016-02-21 19:10:54.814',2);
INSERT INTO [Purchase] ([ID],[Provider_ID],[User_ID],[IsCredit],[CreditDays],[Folio],[Date],[Total]) VALUES (
11,1,'payo',0,0,'','2016-02-21 19:11:43.258',2);
INSERT INTO [Purchase] ([ID],[Provider_ID],[User_ID],[IsCredit],[CreditDays],[Folio],[Date],[Total]) VALUES (
12,1,'payo',0,0,'','2016-02-21 19:12:21.809',2);
INSERT INTO [Purchase] ([ID],[Provider_ID],[User_ID],[IsCredit],[CreditDays],[Folio],[Date],[Total]) VALUES (
13,1,'payo',0,0,'','2016-02-21 19:12:35.966',2);
INSERT INTO [Purchase] ([ID],[Provider_ID],[User_ID],[IsCredit],[CreditDays],[Folio],[Date],[Total]) VALUES (
14,1,'payo',0,0,'','2016-02-21 19:12:41.411',3);
INSERT INTO [Purchase] ([ID],[Provider_ID],[User_ID],[IsCredit],[CreditDays],[Folio],[Date],[Total]) VALUES (
15,1,'payo',0,0,'','2016-02-21 19:22:33.836',522);
INSERT INTO [Purchase] ([ID],[Provider_ID],[User_ID],[IsCredit],[CreditDays],[Folio],[Date],[Total]) VALUES (
16,1,'payo',0,0,'','2016-02-21 19:24:01.526',134);
INSERT INTO [Purchase] ([ID],[Provider_ID],[User_ID],[IsCredit],[CreditDays],[Folio],[Date],[Total]) VALUES (
17,1,'payo',0,0,'','2016-02-21 19:24:20.011',4);
INSERT INTO [Purchase] ([ID],[Provider_ID],[User_ID],[IsCredit],[CreditDays],[Folio],[Date],[Total]) VALUES (
18,1,'payo',0,0,'','2016-02-21 19:24:27.195',112);
INSERT INTO [Purchase] ([ID],[Provider_ID],[User_ID],[IsCredit],[CreditDays],[Folio],[Date],[Total]) VALUES (
19,1,'payo',0,0,'','2016-02-21 19:24:46.628',3);
INSERT INTO [Purchase] ([ID],[Provider_ID],[User_ID],[IsCredit],[CreditDays],[Folio],[Date],[Total]) VALUES (
20,1,'payo',0,0,'','2016-02-21 19:24:53.450',3);
INSERT INTO [Purchase] ([ID],[Provider_ID],[User_ID],[IsCredit],[CreditDays],[Folio],[Date],[Total]) VALUES (
21,1,'payo',0,0,'','2016-02-21 19:32:29.812',11);
INSERT INTO [Purchase] ([ID],[Provider_ID],[User_ID],[IsCredit],[CreditDays],[Folio],[Date],[Total]) VALUES (
22,1,'payo',0,0,'','2016-02-21 19:33:38.081',12969);
INSERT INTO [Purchase] ([ID],[Provider_ID],[User_ID],[IsCredit],[CreditDays],[Folio],[Date],[Total]) VALUES (
23,1,'payo',0,0,'','2016-02-21 19:33:58.729',69);
INSERT INTO [Purchase] ([ID],[Provider_ID],[User_ID],[IsCredit],[CreditDays],[Folio],[Date],[Total]) VALUES (
24,1,'payo',0,0,'','2016-02-21 19:36:02.752',3);
INSERT INTO [Purchase] ([ID],[Provider_ID],[User_ID],[IsCredit],[CreditDays],[Folio],[Date],[Total]) VALUES (
25,1,'payo',0,0,'','2016-02-21 19:36:12.244',696);
INSERT INTO [Purchase] ([ID],[Provider_ID],[User_ID],[IsCredit],[CreditDays],[Folio],[Date],[Total]) VALUES (
26,1,'payo',0,0,'','2016-02-21 19:36:18.191',3);
INSERT INTO [Purchase] ([ID],[Provider_ID],[User_ID],[IsCredit],[CreditDays],[Folio],[Date],[Total]) VALUES (
27,1,'payo',0,0,'','2016-02-21 19:38:43.258',39);
INSERT INTO [Purchase] ([ID],[Provider_ID],[User_ID],[IsCredit],[CreditDays],[Folio],[Date],[Total]) VALUES (
28,1,'payo',0,0,'','2016-02-21 19:42:59.281',6);
INSERT INTO [Purchase] ([ID],[Provider_ID],[User_ID],[IsCredit],[CreditDays],[Folio],[Date],[Total]) VALUES (
29,1,'payo',0,0,'','2016-02-21 19:43:07.406',39);
INSERT INTO [Purchase] ([ID],[Provider_ID],[User_ID],[IsCredit],[CreditDays],[Folio],[Date],[Total]) VALUES (
30,1,'payo',0,0,'','2016-02-21 19:44:24.952',39);
INSERT INTO [Purchase] ([ID],[Provider_ID],[User_ID],[IsCredit],[CreditDays],[Folio],[Date],[Total]) VALUES (
31,1,'payo',0,0,'','2016-02-21 19:44:30.630',3);
INSERT INTO [Purchase] ([ID],[Provider_ID],[User_ID],[IsCredit],[CreditDays],[Folio],[Date],[Total]) VALUES (
32,1,'payo',0,0,'','2016-02-21 20:56:20.686',0);
INSERT INTO [Purchase] ([ID],[Provider_ID],[User_ID],[IsCredit],[CreditDays],[Folio],[Date],[Total]) VALUES (
33,1,'payo',0,0,'','2016-02-21 21:26:29.015',66);
INSERT INTO [Purchase] ([ID],[Provider_ID],[User_ID],[IsCredit],[CreditDays],[Folio],[Date],[Total]) VALUES (
34,1,'payo',0,0,'','2016-02-21 21:29:36.372',100);
INSERT INTO [Purchase] ([ID],[Provider_ID],[User_ID],[IsCredit],[CreditDays],[Folio],[Date],[Total]) VALUES (
35,1,'payo',0,0,'','2016-02-21 21:39:09.522',45);
INSERT INTO [Purchase] ([ID],[Provider_ID],[User_ID],[IsCredit],[CreditDays],[Folio],[Date],[Total]) VALUES (
36,1,'payo',0,0,'','2016-02-21 21:39:29.557',144);
INSERT INTO [Purchase] ([ID],[Provider_ID],[User_ID],[IsCredit],[CreditDays],[Folio],[Date],[Total]) VALUES (
37,1,'payo',0,0,'','2016-02-21 21:39:36.725',11);
INSERT INTO [Purchase] ([ID],[Provider_ID],[User_ID],[IsCredit],[CreditDays],[Folio],[Date],[Total]) VALUES (
38,1,'payo',0,0,'','2016-02-21 22:24:45.344',37.4);
INSERT INTO [Purchase] ([ID],[Provider_ID],[User_ID],[IsCredit],[CreditDays],[Folio],[Date],[Total]) VALUES (
39,1,'payo',0,0,'','2016-02-21 22:27:06.765',27.5);
INSERT INTO [Product] ([ID],[Price],[WholeSalePrice],[Cost],[Quantity],[MinimumQuanity],[Description],[Birth],[Death],[IsAlive]) VALUES (
'barras',10,2,11,4881.4699999999993,5,'ds','0001-01-01 00:00:00.000','0001-01-01 00:00:00.000',1);
INSERT INTO [Product] ([ID],[Price],[WholeSalePrice],[Cost],[Quantity],[MinimumQuanity],[Description],[Birth],[Death],[IsAlive]) VALUES (
'barras2',0,0,34,268,0,'Producto bien pinches chingon','0001-01-01 00:00:00.000','0001-01-01 00:00:00.000',1);
INSERT INTO [Product] ([ID],[Price],[WholeSalePrice],[Cost],[Quantity],[MinimumQuanity],[Description],[Birth],[Death],[IsAlive]) VALUES (
'barras3',3,0,42,3,0,'asjdf asdf','0001-01-01 00:00:00.000','0001-01-01 00:00:00.000',1);
INSERT INTO [Product] ([ID],[Price],[WholeSalePrice],[Cost],[Quantity],[MinimumQuanity],[Description],[Birth],[Death],[IsAlive]) VALUES (
'test',0,0,0,0,0,'des','0001-01-01 00:00:00.000','2016-02-21 22:18:08.268',0);
INSERT INTO [PurchaseProduct] ([Purchase_ID],[Product_ID],[Quantity],[Price]) VALUES (
1,'barras',1,1);
INSERT INTO [PurchaseProduct] ([Purchase_ID],[Product_ID],[Quantity],[Price]) VALUES (
2,'barras',74,1);
INSERT INTO [PurchaseProduct] ([Purchase_ID],[Product_ID],[Quantity],[Price]) VALUES (
2,'barras3',134,3);
INSERT INTO [PurchaseProduct] ([Purchase_ID],[Product_ID],[Quantity],[Price]) VALUES (
7,'barras',1,1);
INSERT INTO [PurchaseProduct] ([Purchase_ID],[Product_ID],[Quantity],[Price]) VALUES (
8,'barras',1,1);
INSERT INTO [PurchaseProduct] ([Purchase_ID],[Product_ID],[Quantity],[Price]) VALUES (
9,'barras',1,1);
INSERT INTO [PurchaseProduct] ([Purchase_ID],[Product_ID],[Quantity],[Price]) VALUES (
10,'barras',2,1);
INSERT INTO [PurchaseProduct] ([Purchase_ID],[Product_ID],[Quantity],[Price]) VALUES (
11,'barras',2,1);
INSERT INTO [PurchaseProduct] ([Purchase_ID],[Product_ID],[Quantity],[Price]) VALUES (
12,'barras',2,1);
INSERT INTO [PurchaseProduct] ([Purchase_ID],[Product_ID],[Quantity],[Price]) VALUES (
13,'barras',2,1);
INSERT INTO [PurchaseProduct] ([Purchase_ID],[Product_ID],[Quantity],[Price]) VALUES (
13,'barras3',1,3);
INSERT INTO [PurchaseProduct] ([Purchase_ID],[Product_ID],[Quantity],[Price]) VALUES (
14,'barras',1,1);
INSERT INTO [PurchaseProduct] ([Purchase_ID],[Product_ID],[Quantity],[Price]) VALUES (
14,'barras3',1,3);
INSERT INTO [PurchaseProduct] ([Purchase_ID],[Product_ID],[Quantity],[Price]) VALUES (
16,'barras',134,3);
INSERT INTO [PurchaseProduct] ([Purchase_ID],[Product_ID],[Quantity],[Price]) VALUES (
16,'barras2',132,1);
INSERT INTO [PurchaseProduct] ([Purchase_ID],[Product_ID],[Quantity],[Price]) VALUES (
17,'barras',1,3);
INSERT INTO [PurchaseProduct] ([Purchase_ID],[Product_ID],[Quantity],[Price]) VALUES (
17,'barras2',1,1);
INSERT INTO [PurchaseProduct] ([Purchase_ID],[Product_ID],[Quantity],[Price]) VALUES (
18,'barras',112,3);
INSERT INTO [PurchaseProduct] ([Purchase_ID],[Product_ID],[Quantity],[Price]) VALUES (
18,'barras2',132,1);
INSERT INTO [PurchaseProduct] ([Purchase_ID],[Product_ID],[Quantity],[Price]) VALUES (
19,'barras',1,3);
INSERT INTO [PurchaseProduct] ([Purchase_ID],[Product_ID],[Quantity],[Price]) VALUES (
20,'barras',1,3);
INSERT INTO [PurchaseProduct] ([Purchase_ID],[Product_ID],[Quantity],[Price]) VALUES (
21,'barras',3,3);
INSERT INTO [PurchaseProduct] ([Purchase_ID],[Product_ID],[Quantity],[Price]) VALUES (
21,'barras3',1,2);
INSERT INTO [PurchaseProduct] ([Purchase_ID],[Product_ID],[Quantity],[Price]) VALUES (
22,'barras',4323,3);
INSERT INTO [PurchaseProduct] ([Purchase_ID],[Product_ID],[Quantity],[Price]) VALUES (
23,'barras',23,3);
INSERT INTO [PurchaseProduct] ([Purchase_ID],[Product_ID],[Quantity],[Price]) VALUES (
24,'barras',1,3);
INSERT INTO [PurchaseProduct] ([Purchase_ID],[Product_ID],[Quantity],[Price]) VALUES (
25,'barras',232,3);
INSERT INTO [PurchaseProduct] ([Purchase_ID],[Product_ID],[Quantity],[Price]) VALUES (
26,'barras',1,3);
INSERT INTO [PurchaseProduct] ([Purchase_ID],[Product_ID],[Quantity],[Price]) VALUES (
27,'barras',13,3);
INSERT INTO [PurchaseProduct] ([Purchase_ID],[Product_ID],[Quantity],[Price]) VALUES (
28,'barras',2,3);
INSERT INTO [PurchaseProduct] ([Purchase_ID],[Product_ID],[Quantity],[Price]) VALUES (
29,'barras',13,3);
INSERT INTO [PurchaseProduct] ([Purchase_ID],[Product_ID],[Quantity],[Price]) VALUES (
30,'barras',13,3);
INSERT INTO [PurchaseProduct] ([Purchase_ID],[Product_ID],[Quantity],[Price]) VALUES (
31,'barras',1,3);
INSERT INTO [PurchaseProduct] ([Purchase_ID],[Product_ID],[Quantity],[Price]) VALUES (
32,'barras',1,3);
INSERT INTO [PurchaseProduct] ([Purchase_ID],[Product_ID],[Quantity],[Price]) VALUES (
32,'barras2',1,34);
INSERT INTO [PurchaseProduct] ([Purchase_ID],[Product_ID],[Quantity],[Price]) VALUES (
32,'barras3',1,3);
INSERT INTO [PurchaseProduct] ([Purchase_ID],[Product_ID],[Quantity],[Price]) VALUES (
33,'barras',2,12);
INSERT INTO [PurchaseProduct] ([Purchase_ID],[Product_ID],[Quantity],[Price]) VALUES (
33,'barras3',1,42);
INSERT INTO [PurchaseProduct] ([Purchase_ID],[Product_ID],[Quantity],[Price]) VALUES (
34,'barras',1,100);
INSERT INTO [PurchaseProduct] ([Purchase_ID],[Product_ID],[Quantity],[Price]) VALUES (
35,'barras',1,11);
INSERT INTO [PurchaseProduct] ([Purchase_ID],[Product_ID],[Quantity],[Price]) VALUES (
35,'barras2',1,34);
INSERT INTO [PurchaseProduct] ([Purchase_ID],[Product_ID],[Quantity],[Price]) VALUES (
36,'barras',10,11);
INSERT INTO [PurchaseProduct] ([Purchase_ID],[Product_ID],[Quantity],[Price]) VALUES (
36,'barras2',1,34);
INSERT INTO [PurchaseProduct] ([Purchase_ID],[Product_ID],[Quantity],[Price]) VALUES (
37,'barras',1,11);
INSERT INTO [PurchaseProduct] ([Purchase_ID],[Product_ID],[Quantity],[Price]) VALUES (
38,'barras',3.4,11);
INSERT INTO [PurchaseProduct] ([Purchase_ID],[Product_ID],[Quantity],[Price]) VALUES (
39,'barras',2.5,11);
INSERT INTO [ProductPrice] ([ID],[Product_ID],[User_ID],[Purchase_ID],[Price],[Date]) VALUES (
1,'barras','payo',22,12969,'2016-02-21 19:33:38.121');
INSERT INTO [ProductPrice] ([ID],[Product_ID],[User_ID],[Purchase_ID],[Price],[Date]) VALUES (
2,'barras','payo',23,69,'2016-02-21 19:33:58.771');
INSERT INTO [ProductPrice] ([ID],[Product_ID],[User_ID],[Purchase_ID],[Price],[Date]) VALUES (
3,'barras','payo',35,10,'2016-02-21 21:39:09.734');
INSERT INTO [Company] ([ID],[Name],[RFC],[Description],[Address],[ZIP],[IVAType],[Logo],[Printer],[FontSize],[CharsPerLine],[Cash],[IsActivated],[DateActivated],[Version]) VALUES (
1,'pa','j','j','j','j',2,'ubuntu-penguin.jpg','Microsoft XPS Document Writer','3','6',3,1,'2001-01-01 00:00:00.000','3');
INSERT INTO [Client] ([ID],[Name],[RFC],[Address],[Phone],[Email],[Birth],[Death],[IsEditable],[IsErasable],[IsGeneralPublic],[IsAlive]) VALUES (
16,'Público general','','','','','2016-02-21 19:03:41.000','2016-02-21 19:03:41.000',0,0,1,1);
INSERT INTO [Client] ([ID],[Name],[RFC],[Address],[Phone],[Email],[Birth],[Death],[IsEditable],[IsErasable],[IsGeneralPublic],[IsAlive]) VALUES (
17,'test','t','t','t','t','2016-02-21 22:10:04.707','2016-02-21 22:17:54.052',1,1,0,0);
INSERT INTO [Sale] ([ID],[Client_ID],[User_ID],[Date],[IsWholeSale],[IsCredit],[CreditDays],[Discount],[IVAType],[SubTotal],[Total]) VALUES (
1,16,'payo','2016-02-21 16:01:29.368',0,0,0,1,0,4,3.96);
INSERT INTO [Sale] ([ID],[Client_ID],[User_ID],[Date],[IsWholeSale],[IsCredit],[CreditDays],[Discount],[IVAType],[SubTotal],[Total]) VALUES (
2,16,'payo','2016-02-21 16:10:21.005',0,0,0,0,0,1,1);
INSERT INTO [Sale] ([ID],[Client_ID],[User_ID],[Date],[IsWholeSale],[IsCredit],[CreditDays],[Discount],[IVAType],[SubTotal],[Total]) VALUES (
3,14,'payo','2016-02-21 18:08:06.394',1,0,0,0,0,28,28);
INSERT INTO [Sale] ([ID],[Client_ID],[User_ID],[Date],[IsWholeSale],[IsCredit],[CreditDays],[Discount],[IVAType],[SubTotal],[Total]) VALUES (
4,16,'payo','2016-02-21 18:25:48.419',0,0,0,0,0,1,1);
INSERT INTO [Sale] ([ID],[Client_ID],[User_ID],[Date],[IsWholeSale],[IsCredit],[CreditDays],[Discount],[IVAType],[SubTotal],[Total]) VALUES (
5,16,'payo','2016-02-21 19:03:41.691',0,0,0,0,0,1,1);
INSERT INTO [Sale] ([ID],[Client_ID],[User_ID],[Date],[IsWholeSale],[IsCredit],[CreditDays],[Discount],[IVAType],[SubTotal],[Total]) VALUES (
6,16,'payo','2016-02-21 19:05:26.555',0,0,0,0,0,1,1);
INSERT INTO [Sale] ([ID],[Client_ID],[User_ID],[Date],[IsWholeSale],[IsCredit],[CreditDays],[Discount],[IVAType],[SubTotal],[Total]) VALUES (
7,16,'payo','2016-02-21 19:32:48.845',0,0,0,0,0,3,3);
INSERT INTO [Sale] ([ID],[Client_ID],[User_ID],[Date],[IsWholeSale],[IsCredit],[CreditDays],[Discount],[IVAType],[SubTotal],[Total]) VALUES (
8,16,'payo','2016-02-21 19:35:48.495',0,0,0,0,0,69,69);
INSERT INTO [Sale] ([ID],[Client_ID],[User_ID],[Date],[IsWholeSale],[IsCredit],[CreditDays],[Discount],[IVAType],[SubTotal],[Total]) VALUES (
9,16,'payo','2016-02-21 19:45:31.885',0,0,0,0,0,69,69);
INSERT INTO [Sale] ([ID],[Client_ID],[User_ID],[Date],[IsWholeSale],[IsCredit],[CreditDays],[Discount],[IVAType],[SubTotal],[Total]) VALUES (
10,16,'payo','2016-02-21 22:26:53.310',0,0,0,0,2,134,155.44);
INSERT INTO [SaleProduct] ([Sale_ID],[Product_ID],[Quantity],[Price]) VALUES (
1,'barras',1,1);
INSERT INTO [SaleProduct] ([Sale_ID],[Product_ID],[Quantity],[Price]) VALUES (
1,'barras3',1,3);
INSERT INTO [SaleProduct] ([Sale_ID],[Product_ID],[Quantity],[Price]) VALUES (
2,'barras',1,1);
INSERT INTO [SaleProduct] ([Sale_ID],[Product_ID],[Quantity],[Price]) VALUES (
3,'barras',14,2);
INSERT INTO [SaleProduct] ([Sale_ID],[Product_ID],[Quantity],[Price]) VALUES (
3,'barras3',1,0);
INSERT INTO [SaleProduct] ([Sale_ID],[Product_ID],[Quantity],[Price]) VALUES (
4,'barras',1,1);
INSERT INTO [SaleProduct] ([Sale_ID],[Product_ID],[Quantity],[Price]) VALUES (
5,'barras',1,1);
INSERT INTO [SaleProduct] ([Sale_ID],[Product_ID],[Quantity],[Price]) VALUES (
6,'barras',1,1);
INSERT INTO [SaleProduct] ([Sale_ID],[Product_ID],[Quantity],[Price]) VALUES (
7,'barras',3,1);
INSERT INTO [SaleProduct] ([Sale_ID],[Product_ID],[Quantity],[Price]) VALUES (
8,'barras',1,69);
INSERT INTO [SaleProduct] ([Sale_ID],[Product_ID],[Quantity],[Price]) VALUES (
9,'barras',1,69);
INSERT INTO [SaleProduct] ([Sale_ID],[Product_ID],[Quantity],[Price]) VALUES (
10,'barras',13.43,10);
COMMIT;

