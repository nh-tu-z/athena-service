using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AthenaService.Scratch.Models;

namespace AthenaService.Scratch.Interfaces
{
    public class Message
    {
        public int Id { get; set; }
        public string? Text { get; set; }
    }

    //public class AppDbContext : DbContext
    //{
    //    public AppDbContext(DbContextOptions options)
    //        : base(options)
    //    {
    //    }

    //    public DbSet<Message>? Messages { get; set; }
    //}

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) :
            base(dbContextOptions)
        {
        }

        public DbSet<BrainstormSession> BrainstormSessions { get; set; }
    }

    public interface IBrainStormSessionRepository
    {
        Task<BrainstormSession> GetByIdAsync(int id);
        Task<List<BrainstormSession>> ListAsync();
        Task AddAsync(BrainstormSession session);
        Task UpdateAsync(BrainstormSession session);
    }

    public class EFStormSessionRepository : IBrainStormSessionRepository
    {
        private readonly AppDbContext _dbContext;

        public EFStormSessionRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<BrainstormSession> GetByIdAsync(int id)
        {
            return _dbContext.BrainstormSessions
                .Include(s => s.Ideas)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<List<BrainstormSession>> ListAsync()
        {
            return _dbContext.BrainstormSessions
                .Include(s => s.Ideas)
                .OrderByDescending(s => s.DateCreated)
                .ToListAsync();
        }

        public Task AddAsync(BrainstormSession session)
        {
            _dbContext.BrainstormSessions.Add(session);
            return _dbContext.SaveChangesAsync();
        }

        public Task UpdateAsync(BrainstormSession session)
        {
            _dbContext.Entry(session).State = EntityState.Modified;
            return _dbContext.SaveChangesAsync();
        }
    }
}
