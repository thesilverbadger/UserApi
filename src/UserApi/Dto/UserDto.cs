using System;
using System.ComponentModel.DataAnnotations;

namespace UserApi.Dto
{
    public class UserDto
    {
        public int Id { get; set; }

        [MaxLength(255)]
        public string Email { get; set; }

        [MaxLength(255)]
        public string GivenName { get; set; }

        [MaxLength(255)]
        public string FamilyName { get; set; }

        public DateTime Created { get; set; }
    }
}
