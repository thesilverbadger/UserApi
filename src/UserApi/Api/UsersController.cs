using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserApi.Dto;
using UserApi.Repositories;

namespace UserApi.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _userRepository.GetAsync(id);

            if (user != null)
            {
                return Ok(user);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserDto userDto)
        {
            if (ModelState.IsValid)
            {
                var createdUser = await _userRepository.CreateAsync(userDto);
                return Created($"api/users/{createdUser.Id}", createdUser);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UserDto userDto)
        {
            if (ModelState.IsValid)
            {
                await _userRepository.UpdateAsync(userDto);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _userRepository.DeleteAsync(id);
            return Ok();
        }
    }
}
