﻿SET IDENTITY_INSERT bingol.optiongroups ON
INSERT INTO bingol.optiongroups (OptionGroupID, OptionGroupName) VALUES
(1, 'color'),
(2, 'size');
SET IDENTITY_INSERT bingol.optiongroups OFF

SET IDENTITY_INSERT bingol.options ON
INSERT INTO bingol.options (OptionID, OptionName, OptionsGroupID) VALUES
(1,  'red',1),
(2, 'blue',1),
(3,  'green',1),
(4,  'S',2),
(5, 'M',2),
(6, 'L',2),
(7,  'XL',2),
(8, 'XXL',2);
SET IDENTITY_INSERT bingol.options OFF

SET IDENTITY_INSERT bingol.productcategories ON
INSERT INTO bingol.productcategories (CategoryID, CategoryName) VALUES
(1, 'Running'),
(2, 'Walking'),
(3, 'HIking'),
(4, 'Track and Trail'),
(5, 'Short Sleave'),
(6, 'Long Sleave');
SET IDENTITY_INSERT bingol.productcategories OFF

SET IDENTITY_INSERT bingol.products ON
INSERT INTO bingol.products (ProductID, ProductSKU, ProductName, ProductPrice, ProductWeight, ProductCartDesc, ProductShortDesc, ProductLongDesc, ProductThumb, ProductImage, ProductCategoryID, ProductUpdateDate, ProductStock, ProductLive, ProductUnlimited, ProductLocation) VALUES
(1, '000-0001', 'Cotton T-Shirt', 9.99, 3, 'Light Cotton T-Shirt', 'A light cotton T-Shirt made with 100% real cotton.', 'A light cotton T-Shirt made with 100% real cotton.\r\n\r\nMade right here in the USA for over 15 years, this t-shirt is lightweight and durable.', '', '', 5, '2013-06-13 01:00:50', 100, 1, 0, NULL),
(2, '000-0004', 'Los Angeles', 179.99, 8, 'Track and Trail', 'A rugged track and trail athletic shoe', 'A rugged track and trail athletic shoe', '', '', 4, '2013-07-25 19:04:36', NULL, 0, 1, NULL),
(3, '000-0001', 'Cotton T-Shirt', 9.99, 3, 'Light Cotton T-Shirt', 'A light cotton T-Shirt made with 100% real cotton.', 'A light cotton T-Shirt made with 100% real cotton.\r\n\r\nMade right here in the USA for over 15 years, this t-shirt is lightweight and durable.', '', '', 5, '2013-06-13 01:00:50', 100, 1, 0, NULL),
(4, '000-0004', 'Los Angeles', 179.99, 8, 'Track and Trail', 'A rugged track and trail athletic shoe', 'A rugged track and trail athletic shoe', '', '', 4, '2013-07-25 19:04:36', NULL, 0, 1, NULL),
(5, '000-0001', 'Cotton T-Shirt', 9.99, 3, 'Light Cotton T-Shirt', 'A light cotton T-Shirt made with 100% real cotton.', 'A light cotton T-Shirt made with 100% real cotton.\r\n\r\nMade right here in the USA for over 15 years, this t-shirt is lightweight and durable.', '', '', 5, '2013-06-13 01:00:50', 100, 1, 0, NULL),
(6, '000-0004', 'Los Angeles', 179.99, 8, 'Track and Trail', 'A rugged track and trail athletic shoe', 'A rugged track and trail athletic shoe', '', '', 4, '2013-07-25 19:04:36', NULL, 0, 1, NULL),
(7, '000-0001', 'Cotton T-Shirt', 9.99, 3, 'Light Cotton T-Shirt', 'A light cotton T-Shirt made with 100% real cotton.', 'A light cotton T-Shirt made with 100% real cotton.\r\n\r\nMade right here in the USA for over 15 years, this t-shirt is lightweight and durable.', '', '', 5, '2013-06-13 01:00:50', 100, 1, 0, NULL),
(8, '000-0004', 'Los Angeles', 179.99, 8, 'Track and Trail', 'A rugged track and trail athletic shoe', 'A rugged track and trail athletic shoe', '', '', 4, '2013-07-25 19:04:36', NULL, 0, 1, NULL),
(9, '000-0001', 'Cotton T-Shirt', 9.99, 3, 'Light Cotton T-Shirt', 'A light cotton T-Shirt made with 100% real cotton.', 'A light cotton T-Shirt made with 100% real cotton.\r\n\r\nMade right here in the USA for over 15 years, this t-shirt is lightweight and durable.', '', '', 5, '2013-06-13 01:00:50', 100, 1, 0, NULL),
(10, '000-0004', 'Los Angeles', 179.99, 8, 'Track and Trail', 'A rugged track and trail athletic shoe', 'A rugged track and trail athletic shoe', '', '', 4, '2013-07-25 19:04:36', NULL, 0, 1, NULL),
(11, '000-0001', 'Cotton T-Shirt', 9.99, 3, 'Light Cotton T-Shirt', 'A light cotton T-Shirt made with 100% real cotton.', 'A light cotton T-Shirt made with 100% real cotton.\r\n\r\nMade right here in the USA for over 15 years, this t-shirt is lightweight and durable.', '', '', 5, '2013-06-13 01:00:50', 100, 1, 0, NULL),
(12, '000-0004', 'Los Angeles', 179.99, 8, 'Track and Trail', 'A rugged track and trail athletic shoe', 'A rugged track and trail athletic shoe', '', '', 4, '2013-07-25 19:04:36', NULL, 0, 1, NULL),
(13, '000-0001', 'Cotton T-Shirt', 9.99, 3, 'Light Cotton T-Shirt', 'A light cotton T-Shirt made with 100% real cotton.', 'A light cotton T-Shirt made with 100% real cotton.\r\n\r\nMade right here in the USA for over 15 years, this t-shirt is lightweight and durable.', '', '', 5, '2013-06-13 01:00:50', 100, 1, 0, NULL),
(14, '000-0004', 'Los Angeles', 179.99, 8, 'Track and Trail', 'A rugged track and trail athletic shoe', 'A rugged track and trail athletic shoe', '', '', 4, '2013-07-25 19:04:36', NULL, 0, 1, NULL),
(15, '000-0001', 'Cotton T-Shirt', 9.99, 3, 'Light Cotton T-Shirt', 'A light cotton T-Shirt made with 100% real cotton.', 'A light cotton T-Shirt made with 100% real cotton.\r\n\r\nMade right here in the USA for over 15 years, this t-shirt is lightweight and durable.', '', '', 5, '2013-06-13 01:00:50', 100, 1, 0, NULL),
(16, '000-0004', 'Los Angeles', 179.99, 8, 'Track and Trail', 'A rugged track and trail athletic shoe', 'A rugged track and trail athletic shoe', '', '', 4, '2013-07-25 19:04:36', NULL, 0, 1, NULL);
SET IDENTITY_INSERT bingol.products OFF

SET IDENTITY_INSERT bingol.productoptions ON
INSERT INTO bingol.productoptions (ProductOptionID, ProductID, OptionID, OptionPriceIncrement, OptionGroupID) VALUES
(1, 1, 1, 0, 1),
(2, 1, 2, 0, 1),
(3, 1, 3, 0, 1),
(4, 1, 4, 0, 2),
(5, 1, 5, 0, 2),
(6, 1, 6, 0, 2),
(7, 1, 7, 2, 2),
(8, 1, 8, 2, 2);
SET IDENTITY_INSERT bingol.productoptions OFF