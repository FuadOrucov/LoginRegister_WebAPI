using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DYNAMIC_BOX_WebAPI.Models
{
    public class ApplicationDbContext: IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options): base(options)    
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
