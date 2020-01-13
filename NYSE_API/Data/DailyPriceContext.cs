using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace NYSE.API.Models
{
    public class DailyPriceContext : DbContext
    {
        public DailyPriceContext (DbContextOptions<DailyPriceContext> options)
            : base(options)
        {
        }

        public DbSet<NYSE.API.Model.DailyPrice> DailyPrice { get; set; }
    }
}
