using Microsoft.EntityFrameworkCore;

namespace UserApi.Models
{
    public class JWTContext:DbContext
    {
        public JWTContext(DbContextOptions options) : base(options)
        {

        } 
        public DbSet<User> Users { get; set; }
    }
}
