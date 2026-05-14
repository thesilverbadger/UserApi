using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using UserApi.Repositories;

namespace UserApi.Tests
{
    public class UserRepositoryTests
    {
        private DataContext context;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "Users")
                .Options;

            context = new DataContext(options);
            DatabaseInitialiser.Initialise(context);
        }

        [Test]
        public async Task Get_User()
        {
            IUserRepository repo = new UserRepository(context);
            var user = await repo.GetAsync(1);

            Assert.That(user, Is.Not.Null);
        }

        [Test]
        public async Task Delete_User()
        {
            IUserRepository repo = new UserRepository(context);
            var user = await repo.GetAsync(3);
            Assert.That(user, Is.Not.Null);

            await repo.DeleteAsync(3);

            user = await repo.GetAsync(3);
            Assert.That(user, Is.Null);
        }

        [Test]
        public async Task Create_User()
        {
            IUserRepository repo = new UserRepository(context);
            var user = await repo.CreateAsync(new Dto.UserDto()
            {
                Email = "new.user@example.org",
                GivenName = "Kim",
                FamilyName = "Jones"
            });

            Assert.That(user, Is.Not.Null);

            var newUser = await repo.GetAsync(user.Id);
            Assert.That(newUser, Is.Not.Null);
        }

        [Test]
        public async Task Update_User()
        {
            IUserRepository repo = new UserRepository(context);
            var user = await repo.GetAsync(2);

            user.FamilyName = "Turing";
            await repo.UpdateAsync(user);

            var updatedUser = await repo.GetAsync(2);

            Assert.That(updatedUser.FamilyName, Is.EqualTo(user.FamilyName));
        }
    }
}
