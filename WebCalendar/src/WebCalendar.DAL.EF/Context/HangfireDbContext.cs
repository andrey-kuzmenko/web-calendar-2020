using Microsoft.EntityFrameworkCore;

namespace WebCalendar.DAL.EF.Context
{
    public class HangfireDbContext : DbContext
    {
        public HangfireDbContext(DbContextOptions<HangfireDbContext> options) : base(options)
        {
            
        }
    }
}