using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserApi.Dto;
using UserApi.Models;

namespace UserApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _db;

        public UserRepository(DataContext db)
        {
            _db = db;
        }

        public async Task<UserDto> CreateAsync(UserDto userDto)
        {
            var user = new User()
            {
                Email = userDto.Email,
                GivenName = userDto.GivenName,
                FamilyName = userDto.FamilyName,
                Created = DateTime.UtcNow,
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            userDto.Id = user.Id;
            userDto.Created = user.Created;

            return userDto;
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _db.Users.SingleOrDefaultAsync(x => x.Id == id);

            if (user != null)
            {
                _db.Users.Remove(user);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<UserDto> GetAsync(int id)
        {
            var user = await _db.Users.SingleOrDefaultAsync(x => x.Id == id);

            if (user != null)
            {
                return new UserDto()
                {
                    Id = user.Id,
                    GivenName = user.GivenName,
                    FamilyName = user.FamilyName,
                    Email = user.Email,
                    Created = user.Created
                };
            }

            return null;
        }

        public Task<UserDto> UpdateAsync(UserDto userDto)
        {
            throw new NotImplementedException();
        }
    }
}
