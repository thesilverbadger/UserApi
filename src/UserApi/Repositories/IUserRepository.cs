using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserApi.Dto;

namespace UserApi.Repositories
{
    public interface IUserRepository
    {
        Task<UserDto> GetAsync(int id);

        Task<UserDto> CreateAsync(UserDto userDto);

        Task UpdateAsync(UserDto userDto);

        Task DeleteAsync(int id);

        Task<List<UserDto>> GetAsync();
    }
}
