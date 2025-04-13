using ChatApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Commons.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
