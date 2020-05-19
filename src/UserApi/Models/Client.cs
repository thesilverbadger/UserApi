using System;
using System.ComponentModel.DataAnnotations;

namespace UserApi.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(255)]
        [Required]
        public string Name { get; set; }

        [Required]
        public string Key { get; set; }
    }
}
