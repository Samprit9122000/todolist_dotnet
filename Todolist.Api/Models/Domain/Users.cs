using System.ComponentModel.DataAnnotations;

namespace Todolist.Api.Models.Domain
{
    public class Users
    {
        [Required]
        public Guid id { get; set; } = Guid.NewGuid();

        [Required]
        public string username { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        public string password { get; set; }

        [Required]
        public bool is_active { get; set; } = true;

        public DateTime created_at { get; set; }

        // Navigation properties
        public List<Todos> todos { get; set; }
    }
}
