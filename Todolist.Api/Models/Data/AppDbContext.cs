using Microsoft.EntityFrameworkCore;
using Todolist.Api.Models.Domain;

namespace Todolist.Api.Models.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }


        // initialize dbsets
        public DbSet<Todos> t_todos { get; set; }
        public DbSet<Users> t_users { get; set; }
    }
}
