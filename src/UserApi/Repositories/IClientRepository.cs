using System;
using System.Threading.Tasks;
using UserApi.Dto;

namespace UserApi.Repositories
{
    public interface IClientRepository
    {
        Task<ClientDto> GetClientByNameAsync(string username);
    }
}
