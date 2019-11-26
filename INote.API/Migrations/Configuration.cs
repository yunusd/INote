namespace INote.API.Migrations
{
    using INote.API.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<INote.API.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(INote.API.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            if (!context.Users.Any())
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                var user = new ApplicationUser
                {
                    UserName = "yunusdemirpolatt@gmail.com",
                    Email = "yunusdemirpolatt@gmail.com",
                    EmailConfirmed = true
                };

                userManager.Create(user, "Ankara1.");

                context.Notes.Add(new Note
                {
                    AuthorId = user.Id,
                    Title = "Sample Note 1",
                    Content = "Yazý 1",
                    CreatedAt = DateTime.Now
                });

                context.Notes.Add(new Note
                {
                    AuthorId = user.Id,
                    Title = "Sample Note 2",
                    Content = "Yazý 2",
                    CreatedAt = DateTime.Now
                });
            }
        }
    }
}
