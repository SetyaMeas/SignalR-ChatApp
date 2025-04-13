using ChatApp.Application.Commons.Interfaces;
using ChatApp.Domain.Entities;
using ChatApp.Infrastucture.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Infrastucture.Persistence.Context
{
    internal class AppDbContext : DbContext, IApplicationDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new MessageConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}
