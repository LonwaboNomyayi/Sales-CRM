namespace Framework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUserData : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'1738abeb-0c14-4490-b38c-68f3a1b9be85', N'guest@salescrm.com', 0, N'AO/1vENe4OjLgJ1ndIqj0eHuFdALN40mcQ9J43H6Pop5YKkunyik/eJ75MDITPg7tA==', N'bd1f4d9c-025f-4374-8971-1dfa0ac2f95b', NULL, 0, 0, NULL, 1, 0, N'guest@salescrm.com')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'7167ef9c-2197-400a-bcc3-ea95ddd1c942', N'admin@salescrm.com', 0, N'AOtMWDfUHY+C57k/6JonSyY7j0GW1NMRyPqO+Pg+jQrmbHZ9uDZ6BaT6XYrVkeAXGw==', N'b2ab7707-b9f5-4f49-b9bd-c4655f0839a1', NULL, 0, 0, NULL, 1, 0, N'admin@salescrm.com')

                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'4ca454a8-899e-430b-b90d-cb15f7d8c7e3', N'CanManageOrders')
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'7167ef9c-2197-400a-bcc3-ea95ddd1c942', N'4ca454a8-899e-430b-b90d-cb15f7d8c7e3')

            ");
        }
        
        public override void Down()
        {
        }
    }
}
