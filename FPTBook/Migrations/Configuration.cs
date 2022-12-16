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
            };
            if (!context.Categories.Any())
            {
                CreateCategory(context, "Non Fiction", "non-fiction");
            }
            if (!context.Books.Any())
            {
                CreateBook(context,
                    title: "Ignore the goal",
                    slug: "ignore-the-goal",
                    description: "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy. Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like).",
                    price: 30.00,
                    date: new DateTime(2022, 03, 27, 17, 53, 48),
                    category: "Non Fiction",
                    author: "William Tran");
                CreateBook(context,
                    title: "The Best not Exist",
                    slug: "the-best-not-exist",
                    description: "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy. Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like).",
                    price: 33.30,
                    date: new DateTime(2022, 04, 27, 17, 53, 48),
                    category: "Non Fiction",
                    author: "Tam Vo");
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
        private void CreateCategory(ApplicationDbContext context, string name, string slug)
        {
            var category = new Category();
            category.Name = name;
            category.Slug = slug;
            context.Categories.Add(category);
        }
        private void CreateBook(ApplicationDbContext context, string title, string slug, string description, double price, DateTime date, string category,string author)
        {
            var book = new Book();
            book.Title = title;
            book.Slug = slug;
            book.Description = description;
            book.Price = price;
            book.Date = date;
            book.Category = context.Categories.Where(u => u.Name == category).FirstOrDefault();
            book.Author = author; 
            context.Books.Add(book);
        }
    }
}
