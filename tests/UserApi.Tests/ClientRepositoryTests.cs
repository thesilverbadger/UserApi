using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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

            Assert.That(client, Is.Not.Null);
        }

        [Test]
        public async Task Invalid_Username()
        {
            IClientRepository repo = new ClientRepository(context);
            var client = await repo.GetClientByNameAsync("client2");

            Assert.That(client, Is.Null);
        }

        [Test]
        public async Task ValidateClient()
        {
            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    ["Jwt:Key"] = "test-signing-key-long-enough-for-hmacsha256",
                    ["Jwt:Issuer"] = "test-issuer"
                })
                .Build();

            var authorizationController = new AuthorizationController(config, new ClientRepository(context));

            var result = await authorizationController.GetToken(new Dto.CredentialDto() { ClientName = "client1", Key = "password" });

            Assert.That(result.Result, Is.InstanceOf<Microsoft.AspNetCore.Mvc.OkObjectResult>());
            var okResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result.Result!;
            Assert.That(okResult.Value, Is.InstanceOf<Dto.TokenDto>());
        }
    }
}
