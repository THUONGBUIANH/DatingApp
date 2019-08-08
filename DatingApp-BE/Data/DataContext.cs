using Microsoft.EntityFrameworkCore;
using PetAppAPIAngular.API.Models;

namespace DatingApp_BE.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}