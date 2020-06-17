using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using UserApi.Api;
using UserApi.Repositories;

namespace UserApi.Tests
{
    public class ClientRepositoryTests
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
        public async Task Get_Client()
        {
            IClientRepository repo = new ClientRepository(context);
            var client = await repo.GetClientByNameAsync("client1");

            Assert.IsNotNull(client);
        }

        [Test]
        public async Task Invalid_Username()
        {
            IClientRepository repo = new ClientRepository(context);
            var client = await repo.GetClientByNameAsync("client2");

            Assert.IsNull(client);
        }

        [Test]
        public async Task ValidateClient()
        {
            //TODO: This needs a mocked IConfiguration to pass
            var authorizationController = new AuthorizationController(null, new ClientRepository(context));

            var result = await authorizationController.GetToken(new Dto.CredentialDto() { ClientName = "client1", Key = "password" });

            Assert.IsNotNull(result.Value);
        }
    }
}