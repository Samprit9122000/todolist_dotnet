using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Todolist.Api.Models.DTO
{
    public class GetTodosDto
    {
        [Required]
        public Guid id { get; set; }

        [Required]
        public string todo { get; set; }

        [Required]
        public bool is_active { get; set; } = true;

        [Column("user_id")]
        [Required]
        public Guid user_id { get; set; }

    }
}
