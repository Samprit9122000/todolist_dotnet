using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Todolist.Api.Models.DTO
{
    public class CreateTodosDto
    {
        [Required]
        public string todo { get; set; }
        
        [Column("user_id")]
        [Required]
        public Guid user_id { get; set; }
    }
}
