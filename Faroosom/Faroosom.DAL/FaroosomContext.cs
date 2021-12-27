using Faroosom.DAL.Entities;
using Faroosom.DAL.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Faroosom.DAL
{
    public class FaroosomContext : DbContext
    {
        public virtual DbSet<User> Users { get; private set; }
        public virtual DbSet<Subscription> Subscriptions { get; private set; }
        public virtual DbSet<Message> Messages { get; private set; }

        public FaroosomContext(DbContextOptions<FaroosomContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Configure();
            modelBuilder.Seed();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}