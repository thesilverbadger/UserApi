using System;
namespace UserApi.Repositories
{
    public static class DatabaseInitialiser
    {
        public static void Initialise(DataContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            //Users
            context.Users.Add(new Models.User()
            {
                Id = 1,
                Created = DateTime.UtcNow,
                Email = "joe.bloggs@example.com",
                FamilyName = "Bloggs",
                GivenName = "Joe"
            });

            context.Users.Add(new Models.User()
            {
                Id = 2,
                Created = DateTime.UtcNow,
                Email = "jane.doe@internet.org",
                FamilyName = "Doe",
                GivenName = "Jane"
            });

            context.Users.Add(new Models.User()
            {
                Id = 3,
                Created = DateTime.UtcNow,
                Email = "sam.smith@home.net",
                FamilyName = "Smith",
                GivenName = "Sam"
            });

            //Clients
            context.Clients.Add(new Models.Client()
            {
                Id = 1,
                Name = "client1",
                Key = BCrypt.Net.BCrypt.HashPassword("password")
            });

            context.SaveChanges();
        }
    }
}
