using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Todolist.Api.Models.Domain
{
    public class Todos
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

        // Navigation properties (foreign keys)
        [ForeignKey("user_id")]     // it is required to get proper field name as f_key
        public Users user { get; set; }
    }
}
