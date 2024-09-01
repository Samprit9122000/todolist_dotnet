using System.ComponentModel.DataAnnotations;

namespace Todolist.Api.Models.DTO
{
    public class UpdateUserDto
    {
        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }
    }
}
