using System.ComponentModel.DataAnnotations;

namespace Todolist.Api.Models.DTO
{
    public class GetUserDto
    {
        [Required]
        public Guid id { get; set; } = Guid.NewGuid();

        [Required]
        public string username { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        public bool is_active { get; set; } = true;

        public DateTime created_at { get; set; }
    }
}
