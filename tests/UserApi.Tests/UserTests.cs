using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using UserApi.Repositories;

namespace UserApi.Tests
{
    public class Tests
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

            Assert.IsNotNull(user);
        }

        [Test]
        public async Task Delete_User()
        {
            IUserRepository repo = new UserRepository(context);
            var user = await repo.GetAsync(3);
            Assert.IsNotNull(user);

            await repo.DeleteAsync(3);

            user = await repo.GetAsync(3);
            Assert.IsNull(user);
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

            Assert.IsNotNull(user);

            var newUser = await repo.GetAsync(user.Id);
            Assert.IsNotNull(newUser);
        }

        [Test]
        public async Task Update_User()
        {
            IUserRepository repo = new UserRepository(context);
            var user = await repo.GetAsync(2);

            user.FamilyName = "Turing";
            await repo.UpdateAsync(user);

            var updatedUser = await repo.GetAsync(2);

            Assert.IsTrue(updatedUser.FamilyName == user.FamilyName);
        }
    }
}