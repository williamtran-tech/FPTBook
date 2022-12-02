namespace FPTBook.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using FPTBook.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<FPTBook.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "FPTBook.Models.ApplicationDbContext";
        }

        protected override void Seed(FPTBook.Models.ApplicationDbContext context)
        {
            if (!context.Users.Any())
            {
                CreateUser(context, "admin@gmail.com", "Aaa123!", "System Administrator");
                CreateUser(context, "ductran@gmail.com", "Aaa123!", "Duc Tran");

                CreateRole(context, "Administrators");
                AddUserToRole(context, "admin@gmail.com", "Administrators");

                CreateBook(context,
                    title: "Ignore the goal",
                    description: "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy. Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like).",
                    price: (double)30.0,
                    date: new DateTime(2022, 03, 27, 17, 53, 48),
                    author: "admin@gmail.com");
                context.SaveChanges();
            };
            if (!context.Books.Any())
            {
                CreateBook(context,
                    title: "Ignore the goal",
                    description: "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy. Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like).",
                    price: 30.00,
                    date: new DateTime(2022, 03, 27, 17, 53, 48),
                    author: "admin@gmail.com");
                context.SaveChanges();
            }
        }

        private void CreateUser(ApplicationDbContext context, string email, string password, string fullName)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            userManager.PasswordValidator = new PasswordValidator { RequiredLength = 1, RequireNonLetterOrDigit = false, RequireDigit = false, RequireLowercase = false, RequireUppercase = false, };
            var user = new ApplicationUser { UserName = email, Email = email, FullName = fullName };
            var userCreateResult = userManager.Create(user, password);
            if (!userCreateResult.Succeeded)
            {
                throw new Exception(string.Join("; ", userCreateResult.Errors));
            }
        }
        private void CreateRole(ApplicationDbContext context, string roleName)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var roleCreateResult = roleManager.Create(new IdentityRole(roleName));

            if (!roleCreateResult.Succeeded)
            {
                throw new Exception(string.Join("; ", roleCreateResult.Errors));
            }
        }

        private void AddUserToRole(ApplicationDbContext context, string userName, string roleName)
        {
            var user = context.Users.First(u => u.UserName == userName);
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var addAdminRoleResult = userManager.AddToRole(user.Id, roleName);
            if (!addAdminRoleResult.Succeeded)
            {
                throw new Exception(string.Join("; ", addAdminRoleResult.Errors));
            }
        }
        private void CreateBook(ApplicationDbContext context, string title, string description, double price, DateTime date, string author)
        {
            var book = new Book();
            book.Title = title;
            book.Description = description;
            book.Price = price;
            book.Date = date;
            book.Author = context.Users.Where(u => u.UserName == author).FirstOrDefault(); context.Books.Add(book);
        }
    }
}
