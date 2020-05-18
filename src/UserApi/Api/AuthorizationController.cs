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

namespace UserApi.Api
{
    [Route("api/[controller]")]
    public class AuthorizationController : ControllerBase
    {
        private IConfiguration _config;

        public AuthorizationController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        [Route("token")]
        [AllowAnonymous]
        public async Task<ActionResult<TokenDto>> GetToken([FromBody] CredentialDto credentialDto)
        {
            //TODO: Remove once we're asynchronously getting the callee
            await Task.FromResult(0);

            //TODO: Validate username/password
            if (credentialDto.Username != credentialDto.Password)
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
                new Claim(ClaimTypes.NameIdentifier, credentialDto.Username, ClaimValueTypes.String, _config["Jwt:Issuer"])
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
