using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserApi.Dto;

namespace UserApi.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly DataContext _db;

        public ClientRepository(DataContext db)
        {
            _db = db;
        }

        public async Task<ClientDto> GetClientByNameAsync(string clientName)
        {
            return await _db.Clients
                .Select(x => new ClientDto()
                {
                    Id = x.Id,
                    Key = x.Key,
                    Name = x.Name
                })
                .SingleOrDefaultAsync(x => x.Name == clientName);
        }
    }
}
