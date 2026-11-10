using BlogWithEf2.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogWithEf2.Data
{
    public class BlogDbContext : DbContext
    {

        public BlogDbContext(DbContextOptions<BlogDbContext> options ) : base(options) {
            
        }

        public DbSet<Post> Posts => Set<Post>();    

    }
}
