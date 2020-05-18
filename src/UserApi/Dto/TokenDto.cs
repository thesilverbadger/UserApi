using System;
namespace UserApi.Dto
{
    public class TokenDto
    {
        /// <summary>
        /// Authentication token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Not implemented yet
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// As UTC
        /// </summary>
        public DateTime ExpiryUtc { get; set; }
    }
}
