using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using UserApi.Dto;
using UserApi.Repositories;

namespace UserApi.Api
{
    [Route("api/[controller]")]
    public class AuthorizationController : ControllerBase
    {
        private IConfiguration _config;
        private readonly IClientRepository _clientRepository;

        public AuthorizationController(IConfiguration config, IClientRepository clientRepository)
        {
            _config = config;
            _clientRepository = clientRepository;
        }

        [HttpPost]
        [Route("token")]
        [AllowAnonymous]
        public async Task<ActionResult<TokenDto>> GetToken([FromBody] CredentialDto credentialDto)
        {
            var client = await _clientRepository.GetClientByNameAsync(credentialDto.ClientName);

            if (client == null)
            {
                return Unauthorized();
            }

            //Validate username/password
            if (client.Key != credentialDto.Key)
            {
                return Unauthorized();
            }

            //Build token
            var validity = 120;
            var expires = DateTime.UtcNow.AddMinutes(validity);
            var tokenData = new TokenDto()
            {
                Token = BuildToken(credentialDto, validity),
                ExpiryUtc = expires
            };

            return Ok(tokenData);
        }


        private string BuildToken(CredentialDto credentialDto, int validity)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, credentialDto.ClientName, ClaimValueTypes.String, _config["Jwt:Issuer"])
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"], claims: claims,
              expires: DateTime.Now.AddMinutes(validity),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
