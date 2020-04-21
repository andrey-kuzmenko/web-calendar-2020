using WebCalendar.Common.Contracts;
using WebCalendar.DAL.EF.Context;

namespace WebCalendar.DAL.EF
{
    public class HangfireDbInitializer : IDataInitializer
    {
        private readonly HangfireDbContext _context;

        public HangfireDbInitializer(HangfireDbContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            _context.Database.EnsureCreated();
        }
    }
}