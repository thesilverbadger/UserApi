using System;
using System.ComponentModel.DataAnnotations;

namespace UserApi.Dto
{
    public class UserDto
    {
        public int Id { get; set; }

        [MaxLength(255)]
        [Required]
        public string Email { get; set; }

        [MaxLength(255)]
        [Required]
        public string GivenName { get; set; }

        [MaxLength(255)]
        [Required]
        public string FamilyName { get; set; }

        public DateTime Created { get; set; }
    }
}
