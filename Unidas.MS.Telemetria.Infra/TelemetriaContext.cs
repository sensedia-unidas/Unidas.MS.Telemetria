using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unidas.MS.Telemetria.Domain.Models.EventFilter;

namespace Unidas.MS.Telemetria.Infra
{
    public class TelemetriaContext : DbContext
    {

        public TelemetriaContext(DbContextOptions<TelemetriaContext> options) : base(options)
        {
            //Database.EnsureCreated();
            Database.Migrate();
        }


        public DbSet<EventFilter> Events { get; set; }
    }
}
