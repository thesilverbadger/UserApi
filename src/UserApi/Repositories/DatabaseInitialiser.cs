using System;
namespace UserApi.Repositories
{
    public static class DatabaseInitialiser
    {
        public static void Initialise(DataContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

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

            context.SaveChanges();
        }
    }
}
