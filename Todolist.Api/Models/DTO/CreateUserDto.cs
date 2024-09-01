using System.ComponentModel.DataAnnotations;

namespace Todolist.Api.Models.DTO
{
    public class CreateUserDto
    {
        [Required]
        public string name { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        public string password { get; set; }


    }
}
