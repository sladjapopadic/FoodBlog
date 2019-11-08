using Blog.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data
{
    public class BlogContext : DbContext
    {
        public BlogContext():base("name=BlogDbContext")
        {

        }
        public DbSet<Recept> Recepti { get; set; }

        public System.Data.Entity.DbSet<Blog.Data.Models.User> Users { get; set; }
    }
}
