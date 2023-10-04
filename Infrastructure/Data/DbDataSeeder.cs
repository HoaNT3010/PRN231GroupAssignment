using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Data
{
    public static class DbDataSeeder
    {
        public static void SeedStaffData(StoreDbContext context)
        {
            if (!context.Staff.Any())
            {
                context.Staff.AddRange(
                    new Staff()
                    {
                        Username = "Manager",
                        PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword("111111"),
                        Role = StaffRole.Manager,
                        Status = StaffStatus.Active,
                        FirstName = "Hòa",
                        LastName = "Nguyễn Thái",
                        DateOfBirth = new DateTime(2002, 10, 30),
                        Image = null,
                        Gender = Gender.Male,
                        PhoneNumber = "0346476019",
                        Email = "hoa41300@gmail.com",
                        Address = "Thành phố Thủ Đức, Thành phố Hồ Chí Minh",
                        CreateDate = DateTime.UtcNow,
                        UpdateDate = DateTime.UtcNow,

                    },
                    new Staff()
                    {
                        Username = "Employee_1",
                        PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword("111111"),
                        Role = StaffRole.Employee,
                        Status = StaffStatus.Active,
                        FirstName = "Hoàng",
                        LastName = "Nguyễn Thái",
                        DateOfBirth = new DateTime(2002, 10, 31),
                        Image = null,
                        Gender = Gender.Male,
                        PhoneNumber = "0346476020",
                        Email = "hoa41301@gmail.com",
                        Address = "Thành phố Thủ Đức, Thành phố Hồ Chí Minh",
                        CreateDate = DateTime.UtcNow,
                        UpdateDate = DateTime.UtcNow,
                    },
                    new Staff()
                    {
                        Username = "Employee_2",
                        PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword("111111"),
                        Role = StaffRole.Employee,
                        Status = StaffStatus.Active,
                        FirstName = "Hoàng",
                        LastName = "Nguyễn",
                        DateOfBirth = new DateTime(2002, 10, 21),
                        Image = null,
                        Gender = Gender.Male,
                        PhoneNumber = "0346476022",
                        Email = "hoa41302@gmail.com",
                        Address = "Thành phố Thủ Đức, Thành phố Hồ Chí Minh",
                        CreateDate = DateTime.UtcNow,
                        UpdateDate = DateTime.UtcNow,
                    });
            };
            context.SaveChanges();
        }
        public static void SeedCategoriesData(StoreDbContext context)
        {
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category()
                    {
                        Name = "Thức ăn",
                        Status = CategoryStatus.Active
                    },
                    new Category()
                    {
                        Name = "Đồ uống",
                        Status = CategoryStatus.Active
                    },
                    new Category()
                    {
                        Name = "Đồ dùng cá nhân",
                        Status = CategoryStatus.Active
                    },
                    new Category()
                    {
                        Name = "Dụng cụ học tập",
                        Status = CategoryStatus.Active
                    },
                    new Category()
                    {
                        Name = "Đồ gia dụng",
                        Status = CategoryStatus.Active
                    }
                    );
            }
            context.SaveChanges();
        }
        public static void SeedProductsData(StoreDbContext context)
        {
            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product()
                    {
                        Name = "Cơm cuộn",
                        UnitPrice = 20000m,
                        Quantity = 67,
                        Status = ProductStatus.Active,
                        CategoryId = 1,
                    },
                    new Product()
                    {
                        Name = "Mỳ ăn liền",
                        UnitPrice = 15000m,
                        Quantity = 45,
                        Status = ProductStatus.Active,
                        CategoryId = 1,
                    },
                    new Product()
                    {
                        Name = "Snack khoai tây",
                        UnitPrice = 12000m,
                        Quantity = 42,
                        Status = ProductStatus.Active,
                        CategoryId = 1,
                    },
                    new Product()
                    {
                        Name = "Trà tắc",
                        UnitPrice = 10000m,
                        Quantity = 78,
                        Status = ProductStatus.Active,
                        CategoryId = 2,
                    },
                    new Product()
                    {
                        Name = "Coca cola",
                        UnitPrice = 12000m,
                        Quantity = 28,
                        Status = ProductStatus.Active,
                        CategoryId = 2,
                    },
                    new Product()
                    {
                        Name = "Sting dâu",
                        UnitPrice = 15000m,
                        Quantity = 31,
                        Status = ProductStatus.Active,
                        CategoryId = 2,
                    },
                    new Product()
                    {
                        Name = "Kem đánh răng",
                        UnitPrice = 18000m,
                        Quantity = 30,
                        Status = ProductStatus.Active,
                        CategoryId = 3,
                    },
                    new Product()
                    {
                        Name = "Sửa rửa mặt",
                        UnitPrice = 40000m,
                        Quantity = 27,
                        Status = ProductStatus.Active,
                        CategoryId = 3,
                    },
                    new Product()
                    {
                        Name = "Bàn chải đánh răng",
                        UnitPrice = 10000m,
                        Quantity = 74,
                        Status = ProductStatus.Active,
                        CategoryId = 3,
                    },
                    new Product()
                    {
                        Name = "Vở",
                        UnitPrice = 7000m,
                        Quantity = 52,
                        Status = ProductStatus.Active,
                        CategoryId = 4,
                    },
                    new Product()
                    {
                        Name = "Bút chì",
                        UnitPrice = 6000m,
                        Quantity = 49,
                        Status = ProductStatus.Active,
                        CategoryId = 4,
                    },
                    new Product()
                    {
                        Name = "Tẩy",
                        UnitPrice = 5000m,
                        Quantity = 83,
                        Status = ProductStatus.Active,
                        CategoryId = 4,
                    },
                    new Product()
                    {
                        Name = "Băng keo hai mặt",
                        UnitPrice = 13000m,
                        Quantity = 39,
                        Status = ProductStatus.Active,
                        CategoryId = 5,
                    },
                    new Product()
                    {
                        Name = "Lược",
                        UnitPrice = 17000m,
                        Quantity = 61,
                        Status = ProductStatus.Active,
                        CategoryId = 5,
                    },
                    new Product()
                    {
                        Name = "Khăn giấy",
                        UnitPrice = 20000m,
                        Quantity = 52,
                        Status = ProductStatus.Active,
                        CategoryId = 5,
                    }
                    );
            }
            context.SaveChanges();
        }
        public static void SeedCustomersData(StoreDbContext context)
        {
            if (!context.Customers.Any())
            {
                context.Customers.AddRange(
                    new Customer()
                    {
                        Status = CustomerStatus.Active,
                        FirstName = "Hà",
                        LastName = "Trần Thị",
                        PhoneNumber = "0123456789",
                        DateOfBirth = new DateTime(2002, 1, 1),
                        Gender = Gender.Female,
                        Email = "ha@gmail.com",
                        Address = "Thành phố Thủ Đức, Thành phố Hồ Chí Minh",
                        CreateDate = DateTime.UtcNow,
                        UpdateDate = DateTime.UtcNow,
                    },
                    new Customer()
                    {
                        Status = CustomerStatus.Active,
                        FirstName = "Hưng",
                        LastName = "Phạm Quốc",
                        PhoneNumber = "1234567890",
                        DateOfBirth = new DateTime(2002, 2, 2),
                        Gender = Gender.Male,
                        Email = "hung@gmail.com",
                        Address = "Thành phố Thủ Đức, Thành phố Hồ Chí Minh",
                        CreateDate = DateTime.UtcNow,
                        UpdateDate = DateTime.UtcNow,
                    });
            }
            context.SaveChanges();
        }
        public static void SeedCardsData(StoreDbContext context)
        {
            if (!context.Cards.Any())
            {
                context.Cards.AddRange(
                    new Card()
                    {
                        CreateDate = DateTime.UtcNow,
                        Status = CardStatus.Active,
                        CustomerId = 1,
                    },
                    new Card()
                    {
                        CreateDate = DateTime.UtcNow,
                        Status = CardStatus.Active,
                        CustomerId = 2,
                    });
            }
            context.SaveChanges();
        }
        public static void SeedWalletsData(StoreDbContext context)
        {
            if (!context.Wallets.Any())
            {
                context.Wallets.AddRange(
                    new Wallet()
                    {
                        Balance = 80000m,
                        CreateDate = DateTime.UtcNow,
                        Status = WalletStatus.Active,
                        CardId = 1,
                    },
                    new Wallet()
                    {
                        Balance = 0,
                        CreateDate = DateTime.UtcNow,
                        Status = WalletStatus.Active,
                        CardId = 2,
                    });
            }
            context.SaveChanges();
        }
        public static void SeedInvoicesData(StoreDbContext context)
        {
            if (!context.Invoices.Any())
            {
                context.Invoices.Add(
                    new Invoice()
                    {
                        TotalPrice = 20000m,
                        CreateDate = DateTime.UtcNow.AddHours(2),
                        CustomerId = 1,
                        StaffId = 3,
                    });
            }
            context.SaveChanges();
        }
        public static void SeedInvoiceDetailData(StoreDbContext context)
        {
            if (!context.InvoiceDetails.Any())
            {
                context.InvoiceDetails.Add(
                    new InvoiceDetail()
                    {
                        Quantity = 1,
                        UnitPrice = 20000m,
                        ItemTotal = 20000m,
                        ProductId = 1,
                        InvoiceId = 1,
                    });
            }
            context.SaveChanges();
        }
        public static void SeedTransactionsData(StoreDbContext context)
        {
            if (!context.Transactions.Any())
            {
                context.Transactions.AddRange(
                    new Transaction()
                    {
                        Amount = 100000m,
                        TransactionType = TransactionType.RechargeBalance,
                        TransactionMethod = TransactionMethod.Cash,
                        Description = "Recharge 100000 VND to balance",
                        EWalletTransaction = null,
                        CreateDate = DateTime.UtcNow.AddHours(1),
                        Status = TransactionStatus.Completed,
                        WalletId = 1,
                        StaffId = 2,
                        InvoiceId = null,
                    },
                    new Transaction()
                    {
                        Amount = 20000m,
                        TransactionType = TransactionType.Purchase,
                        TransactionMethod = TransactionMethod.Card,
                        Description = "Purchase goods in the store. Cost: 20000 VND",
                        EWalletTransaction = null,
                        CreateDate = DateTime.UtcNow.AddHours(2),
                        Status = TransactionStatus.Completed,
                        WalletId = 1,
                        StaffId = 3,
                        InvoiceId = 1,
                    });
            }
            context.SaveChanges();
        }
        public static void SeedData(StoreDbContext context)
        {
            SeedStaffData(context);
            SeedCategoriesData(context);
            SeedProductsData(context);
            SeedCustomersData(context);
            SeedCardsData(context);
            SeedWalletsData(context);
            SeedInvoicesData(context);
            SeedInvoiceDetailData(context);
            SeedTransactionsData(context);
        }
    }
}
