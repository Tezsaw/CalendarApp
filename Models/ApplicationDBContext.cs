using System;
using Microsoft.EntityFrameworkCore;

namespace CalendarApp.Models
{

    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        public DbSet<CalendarActivity> Activities { get; set; }
    }
}


