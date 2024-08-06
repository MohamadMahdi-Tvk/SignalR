using Microsoft.EntityFrameworkCore;
using SignalR.Models.Entities;

namespace SignalR.Contexts;

public class DataBaseContext : DbContext
{
    public DataBaseContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<ChatRoom> ChatRooms { get; set; }
}
