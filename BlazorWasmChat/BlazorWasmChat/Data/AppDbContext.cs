using ChatModels;
using Microsoft.EntityFrameworkCore;

namespace BlazorWasmChat.Data
{
    public class AppDbContext : DbContext
    {
        private readonly DbContextOptions _options;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            _options = options;
        }

        public DbSet<Chat> Chats { get; set; }
    }
}