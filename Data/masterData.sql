DELETE FROM dbo.categories
DELETE FROM dbo.products
DELETE FROM dbo.combodetails
DELETE FROM dbo.orders
DELETE FROM dbo.orderdetails
DELETE FROM dbo.coupons
DELETE FROM dbo.coupontypes
DELETE FROM dbo.customers
DBCC CHECKIDENT ('orderdetails', RESEED, 0);
DBCC CHECKIDENT ('coupontypes', RESEED, 0);

select * from dbo.products;
select * from dbo.categories;
select * from dbo.coupontypes;
select * from dbo.coupons;
select * from dbo.orders;
select * from dbo.orderdetails;

-- Insert data into categories table
INSERT INTO [categories] ([Id], [Name], [IsAvailable], [Description])
VALUES 
    ('303CF809-C86B-41A5-9D2E-03C93B19F0D2', 'Burgers', 1, 'A variety of delicious burgers including beef, chicken, and vegetarian options'),
    ('1333C9DE-0D0A-43F5-953F-226F4209F192', 'Pizza', 1, 'Assorted pizzas with different toppings and crust styles'),
    ('3FA85F64-5717-4562-B3FC-2C963F66AFA6', 'Drinks', 1, 'Beverages including soft drinks, juices, and shakes'),
    ('5D8B0E10-C5A2-485A-82CC-3C0F6D9FF529', 'Sides', 1, 'Side dishes such as fries, onion rings, and salads'),
    ('ADD55F27-5AB8-4458-836B-3F28F53908E4', 'Desserts', 0, 'Sweet treats including ice cream, cakes, and pastries'),
    ('4EC426B3-972F-4BA0-A33D-4F8CDD15D0BF', 'Sandwiches', 1, 'A selection of sandwiches including subs, wraps, and paninis'),
    ('6A3F2E0C-81B7-43DF-9344-8E84A69664FF', 'Chicken', 0, 'Fried and grilled chicken dishes'),
    ('14FBD7F4-CB1C-4561-A98D-A9507F2CAB45', 'Breakfast', 1, 'Breakfast items including eggs, pancakes, and coffee'),
    ('78E1BFD5-89E7-49FF-95D1-E6525531584B', 'Seafood', 1, 'Seafood dishes such as fish fillets, shrimp, and crab cakes'),
    ('D3353EBE-83E3-4F6F-B043-EFB4457171C3', 'Vegan', 1, 'A range of vegan-friendly meals and snacks');

GO

-- Example category IDs
DECLARE @BurgersId uniqueidentifier = (SELECT Id FROM [categories] WHERE [Name] = 'Burgers');
DECLARE @PizzaId uniqueidentifier = (SELECT Id FROM [categories] WHERE [Name] = 'Pizza');
DECLARE @DrinksId uniqueidentifier = (SELECT Id FROM [categories] WHERE [Name] = 'Drinks');
DECLARE @SidesId uniqueidentifier = (SELECT Id FROM [categories] WHERE [Name] = 'Sides');
DECLARE @DessertsId uniqueidentifier = (SELECT Id FROM [categories] WHERE [Name] = 'Desserts');
DECLARE @SandwichesId uniqueidentifier = (SELECT Id FROM [categories] WHERE [Name] = 'Sandwiches');
DECLARE @ChickenId uniqueidentifier = (SELECT Id FROM [categories] WHERE [Name] = 'Chicken');
DECLARE @BreakfastId uniqueidentifier = (SELECT Id FROM [categories] WHERE [Name] = 'Breakfast');
DECLARE @SeafoodId uniqueidentifier = (SELECT Id FROM [categories] WHERE [Name] = 'Seafood');
DECLARE @VeganId uniqueidentifier = (SELECT Id FROM [categories] WHERE [Name] = 'Vegan');

-- Insert data into products table
INSERT INTO dbo.products ([Id], [Name], [UnitPrice], [HardDiscount], [PercentDiscount], [IsAvailable], [Image], [Description], [CategoryId])
VALUES 
    ('17B42C17-D0D7-4FE3-A661-03851AEB4899', 'Classic Beef Burger', 5.99, 1.00, 0, 1, '1.png', 'Juicy beef patty with lettuce, tomato, and cheese', @BurgersId),
    ('FFA3C5AD-BBE1-4196-B0E9-061D30A786D1', 'Chicken Burger', 5, 0, 20, 1, '2.png', 'Grilled chicken burger with lettuce and mayo', @BurgersId),
    ('85329574-D3A6-4327-B4E2-2E78BBC41AD6', 'Veggie Burger', 5.49, 0.50, 0, 1, '3.png', 'Vegetarian burger with a blend of fresh veggies', @BurgersId),
    ('F4256849-9087-448A-9F1E-61A165709750', 'Cheese Burger', 6.49, 1.00, 0, 1, '4.png', 'Beef burger with melted cheese and pickles', @BurgersId),
    ('70949F54-BE46-44C0-94A5-678A3448F997', 'Bacon Burger', 6.99, 1.00, 0, 1, '5.png', 'Burger with crispy bacon and BBQ sauce', @BurgersId),
    ('BAAB492C-526B-4C15-8E7D-69FDB2213C71', 'Pepperoni Pizza', 8.99, 1.00, 0, 1, '6.png', 'Pizza topped with pepperoni and mozzarella cheese', @PizzaId),
    ('CAC25CAE-1533-47BD-91AA-7CD919BDE817', 'Margherita Pizza', 7.99, 2.00, 0, 1, '7.png', 'Classic pizza with fresh tomatoes, mozzarella, and basil', @PizzaId),
    ('FBB0AED3-CC6D-4ADC-B3FE-86750449C069', 'BBQ Chicken Pizza', 9.49, 1.00, 0, 1, '8.png', 'Pizza with BBQ chicken, onions, and cheese', @PizzaId),
    ('C21DEC2F-778A-4D0A-BEA3-869313D8A082', 'Veggie Pizza', 8.49, 1.00, 0, 0, '9.png', 'Pizza loaded with fresh vegetables', @PizzaId),
    ('C10FBD78-FF32-403F-BED9-8BE6209D7EA2', 'Hawaiian Pizza', 10.00, 0, 20, 1, '10.png', 'Pizza with ham and pineapple', @PizzaId),
    ('97C8B42D-978C-43E8-8E55-8C30C3DEFB57', 'Cola', 1.99, 0.50, 0, 1, '11.png', 'Refreshing cola drink', @DrinksId),
    ('DA58B999-4CF0-4C49-B14B-900468EDA135', 'Orange Juice', 2.49, 1.00, 0, 1, '12.png', 'Freshly squeezed orange juice', @DrinksId),
    ('1BF6092C-5F26-4DC1-9102-90F5C4FC2586', 'Milkshake', 5.00, 0, 20, 0, '13.png', 'Thick and creamy milkshake', @DrinksId),
    ('8F7ADF96-3A47-4029-BD19-A01317999B2E', 'Lemonade', 3.00, 1.00, 0, 1, '14.png', 'Fresh lemonade with a tangy twist', @DrinksId),
    ('AD5DB70A-2CA1-44CA-AB2B-A0E1EF2EC390', 'Iced Tea', 2.49, 1.00, 0, 1, '15.png', 'Cool and refreshing iced tea', @DrinksId),
    ('AE1BB559-2151-40CA-AB2E-AD6AFA171CC4', 'French Fries', 2.99, 1.00, 0, 1, '16.png', 'Crispy golden French fries', @SidesId),
    ('BB24B5DC-304F-4EDD-8C43-AD8F9E3C0E6C', 'Onion Rings', 3.49, 1.00, 0, 1, '17.png', 'Crispy and delicious onion rings', @SidesId),
    ('DC911956-87D7-4EB1-A012-B26F7DC24BA3', 'Mozzarella Sticks', 4.49, 1, 0, 1, '18.png', 'Breaded mozzarella sticks with marinara sauce', @SidesId),
    ('3ABBA483-513C-4387-840E-C10DC6DA96C2', 'Chicken Wings', 6.00, 2.00, 0, 0, '19.png', 'Spicy chicken wings', @SidesId),
    ('8628F7F6-E483-4E0B-B842-C2AE81410C6D', 'Garlic Bread', 5.00, 0, 10, 1, '20.png', 'Toasted garlic bread with herbs', @SidesId),
    ('EDF7A624-3083-4A32-8088-C569EFA994BA', 'Chocolate Cake', 3.99, 1.00, 0, 1, '21.png', 'Rich and moist chocolate cake slice', @DessertsId),
    ('F0842F77-FD74-4606-991A-C5975B9013C8', 'Ice Cream', 2.99, 1.00, 0, 1, '22.png', 'Creamy ice cream with various flavors', @DessertsId),
    ('A5FEEFB9-A7E9-413F-8706-C82AA6DB6E32', 'Apple Pie', 3.49, 2.00, 0, 1, '23.png', 'Classic apple pie with a flaky crust', @DessertsId),
    ('80C7F165-D7C9-4E75-8405-CD9E57A7EBD4', 'Brownie', 2.99, 1.00, 0, 1, '24.png', 'Chocolate brownie with nuts', @DessertsId),
    ('2B911869-D202-4FC2-B8A6-D77BB9876C8B', 'Cheesecake', 5.00, 0, 20, 1, '25.png', 'Creamy cheesecake with a graham cracker crust', @DessertsId),
    ('A36EE4C6-6770-4EE8-B5BA-D79795CABDBC', 'Ham Sandwich', 4.99, 1.00, 0, 1, '26.png', 'Sandwich with ham, cheese, and lettuce', @SandwichesId),
    ('EAC095A5-5ECC-498A-AE8B-DE29941B6246', 'Turkey Sandwich', 5.49, 3.00, 0, 1, '27.png', 'Sandwich with turkey, cranberry sauce, and lettuce', @SandwichesId),
    ('DF04378C-2BDD-4343-8D81-E9B57F97FBF7', 'Grilled Cheese', 3.99, 1.00, 0, 1, '28.png', 'Classic grilled cheese sandwich', @SandwichesId),
    ('CF067CFA-225A-4AAC-8261-EB0FE14C83A2', 'BLT Sandwich', 5.99, 2.00, 0, 1, '29.png', 'Bacon, lettuce, and tomato sandwich', @SandwichesId),
    ('FB14E769-35B2-49F9-86FB-FC81E34CC8D4', 'Egg Sandwich', 4.49, 2.00, 0, 1, '30.png', 'Egg sandwich with cheese and ham', @BreakfastId),

	-- combo dishes
    ('CA7E96F8-EA05-4FF4-BCEC-7B73C93A057C', 'Combo Grilled Cheese', 14.00, 3.00, 0, 1, '5.png', 'Classic grilled cheese sandwich with some ...', @SandwichesId),
    ('0F0EC795-ECE6-456B-889F-BB516B563917', 'Combo Ham Sandwich', 15.00, 0, 30, 1, '8.png', 'You gonna love this combo', @SandwichesId),
    ('B8B893D7-5383-48E8-9537-3B8ADC59F9A5', 'Combo Bacon Burger and Veggie Pizza', 15, 0, 20, 1, '17.png', 'Very mind-blowing combo for alone people', @BurgersId),
    ('DA9F776C-91CE-4E67-A43B-96A9FB096D85', 'Summer Combo', 15.00, 0, 20, 1, '12.png', 'Summer - hot, need this one', @PizzaId),
    ('4947DD97-0A4F-4BEF-832D-CB52ADC50A86', 'Winter Combo', 15.00, 0, 20, 1, '20.png', 'Winter - cold, need this one', @PizzaId),
    ('F5646E44-2E44-4730-80CA-C978CCF654C4', 'Deadpool and Wolverine Combo', 18, 4.00, 0, 1, '21.png', 'Absolutly insane with ketchup and mustard', @BurgersId);

GO

INSERT INTO combodetails ([Quantity], [ProductId], [ComboId])
VALUES
	(2, 'DF04378C-2BDD-4343-8D81-E9B57F97FBF7', 'CA7E96F8-EA05-4FF4-BCEC-7B73C93A057C'),
	(1, 'EDF7A624-3083-4A32-8088-C569EFA994BA', 'CA7E96F8-EA05-4FF4-BCEC-7B73C93A057C'),
	(2, 'F0842F77-FD74-4606-991A-C5975B9013C8', 'CA7E96F8-EA05-4FF4-BCEC-7B73C93A057C'),
	(2, 'A36EE4C6-6770-4EE8-B5BA-D79795CABDBC', '0F0EC795-ECE6-456B-889F-BB516B563917'),
	(2, '80C7F165-D7C9-4E75-8405-CD9E57A7EBD4', '0F0EC795-ECE6-456B-889F-BB516B563917'),
	(2, 'AD5DB70A-2CA1-44CA-AB2B-A0E1EF2EC390', '0F0EC795-ECE6-456B-889F-BB516B563917'),
	(1, '70949F54-BE46-44C0-94A5-678A3448F997', 'B8B893D7-5383-48E8-9537-3B8ADC59F9A5'),
	(1, '85329574-D3A6-4327-B4E2-2E78BBC41AD6', 'B8B893D7-5383-48E8-9537-3B8ADC59F9A5'),
	(2, '97C8B42D-978C-43E8-8E55-8C30C3DEFB57', 'B8B893D7-5383-48E8-9537-3B8ADC59F9A5'),
	(2, 'F0842F77-FD74-4606-991A-C5975B9013C8', 'B8B893D7-5383-48E8-9537-3B8ADC59F9A5'),
	(4, 'F0842F77-FD74-4606-991A-C5975B9013C8', 'DA9F776C-91CE-4E67-A43B-96A9FB096D85'),
	(4, '8F7ADF96-3A47-4029-BD19-A01317999B2E', 'DA9F776C-91CE-4E67-A43B-96A9FB096D85'),
	(3, '3ABBA483-513C-4387-840E-C10DC6DA96C2', '4947DD97-0A4F-4BEF-832D-CB52ADC50A86'),
	(3, '1BF6092C-5F26-4DC1-9102-90F5C4FC2586', '4947DD97-0A4F-4BEF-832D-CB52ADC50A86'),
	(1, 'DC911956-87D7-4EB1-A012-B26F7DC24BA3', 'F5646E44-2E44-4730-80CA-C978CCF654C4'),
	(2, 'C10FBD78-FF32-403F-BED9-8BE6209D7EA2', 'F5646E44-2E44-4730-80CA-C978CCF654C4'),
	(1, '1BF6092C-5F26-4DC1-9102-90F5C4FC2586', 'F5646E44-2E44-4730-80CA-C978CCF654C4');

GO


INSERT INTO coupontypes ([Name], [StartTime], [EndTime], [HardValue], [PercentValue], [MinOrderSubTotalCondition])
VALUES
	('Spring Coupon', '2024-08-01 00:00:00', '2024-08-31 23:59:59', 2.00, 0, 5.00),
    ('Summer Coupon', '2024-08-01 00:00:00', '2024-8-31 23:59:59', 2.99, 0, 10.00),
    ('Fall Coupon', '2024-10-01 00:00:00', '2024-10-31 23:59:59', 4.00, 0, 10.00),
    ('Winter Coupon', '2024-8-01 00:00:00', '2024-8-31 23:59:59', 5.00, 0, 12.00),
    ('Deadpool and Wolverine Coupon', '2024-8-01 00:00:00', '2024-8-31 23:59:59', 8.00, 0, 20.00);

GO

-- Assuming we have the following CouponTypeIDs
DECLARE @SpringCouponTypeID1 int = (SELECT Id FROM [coupontypes] WHERE [Name] = 'Spring Coupon');
DECLARE @SummerCouponTypeID2 int = (SELECT Id FROM [coupontypes] WHERE [Name] = 'Summer Coupon');
DECLARE @FallCouponTypeID3 int = (SELECT Id FROM [coupontypes] WHERE [Name] = 'Fall Coupon');
DECLARE @WinterCouponTypeID4 int = (SELECT Id FROM [coupontypes] WHERE [Name] = 'Winter Coupon');
DECLARE @DnPCouponTypeID5 int = (SELECT Id FROM [coupontypes] WHERE [Name] = 'Deadpool and Wolverine Coupon');

-- Insert 25 coupons (5 for each CouponTypeID)
INSERT INTO coupons ([Id], [IsUsed], [CustomerId], [CouponTypeID])
VALUES
    ('9C1523B3-8F5F-4A9D-BAA0-9F62FD5521FE', 1, NULL, @SpringCouponTypeID1),
    ('D45F0FD5-88F1-4A0F-B9EF-7D769ABE7C0B', 1, NULL, @SpringCouponTypeID1),
    ('8487CB0A-7792-4232-AFC9-2BD96E3ED6D0', 0, NULL, @SpringCouponTypeID1),
    ('A79A9289-98CE-4BEA-B9A6-9B08620FDA12', 0, NULL, @SpringCouponTypeID1),
    ('2411CFE3-5582-41AD-9686-71749B0FED16', 0, NULL, @SpringCouponTypeID1),
    
    ('D3A66701-777C-4118-ABBB-1D1C4A60B5EC', 1, NULL, @SummerCouponTypeID2),
    ('BE70E997-A758-4705-BF67-90B998D43D15', 0, NULL, @SummerCouponTypeID2),
    ('C7EF9176-DFE3-4AA0-BD62-C5D7BD50B60A', 0, NULL, @SummerCouponTypeID2),
    ('E50336EA-4DAB-4BB2-AC4F-D8F6C7E124BD', 1, NULL, @SummerCouponTypeID2),
    ('357416C0-3584-41EB-92CF-078A4B722FEF', 0, NULL, @SummerCouponTypeID2),
    
    ('68636A93-C9A3-4A29-B935-9A0B619D4564', 1, NULL, @FallCouponTypeID3),
    ('55EC8795-E473-4E2D-AFB6-CE60AFE6FEBD', 1, NULL, @FallCouponTypeID3),
    ('356DEF39-EF7D-47A5-9AD1-BBD6F7040021', 1, NULL, @FallCouponTypeID3),
    ('E45CDBE1-E910-45EE-9EC6-AEF08DB10DDE', 0, NULL, @FallCouponTypeID3),
    ('640B6866-A1C4-4B0A-8C8D-FBB09559BCE3', 0, NULL, @FallCouponTypeID3),
    
    ('63C7B5CF-99B2-4506-87A5-8375463DF031', 1, NULL, @WinterCouponTypeID4),
    ('F869C0F5-9A2D-4C44-9759-4E69E2D3D771', 1, NULL, @WinterCouponTypeID4),
    ('3C79432F-5420-475B-B9BC-064F09783591', 0, NULL, @WinterCouponTypeID4),
    ('A797F837-0A6D-4534-99E9-72187B7A1491', 0, NULL, @WinterCouponTypeID4),
    ('DBD26963-3714-437F-917B-F0A290BF3EC3', 0, NULL, @WinterCouponTypeID4),
    
    ('99FBE40A-54B5-411A-AD30-43D05A90C081', 0, NULL, @DnPCouponTypeID5),
    ('DDCB82BE-AFBF-4AFA-8A7D-AECCEF7CFBA8', 1, NULL, @DnPCouponTypeID5),
    ('97C478F1-485D-4000-AA8F-7D07293361ED', 0, NULL, @DnPCouponTypeID5),
    ('60443DBA-3B7A-4DDD-93ED-B0D34CCA4ABC', 0, NULL, @DnPCouponTypeID5),
    ('9888A24F-0F6A-4D98-B600-D92D8EAC3026', 1, NULL, @DnPCouponTypeID5);

GO
