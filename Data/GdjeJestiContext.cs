using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GdjeJesti.Models;

    public class GdjeJestiContext : DbContext
    {
        public GdjeJestiContext (DbContextOptions<GdjeJestiContext> options)
            : base(options)
        {
        }

        public DbSet<GdjeJesti.Models.Restoran> Restoran { get; set; }

        public DbSet<GdjeJesti.Models.Jelo> Jelo { get; set; }
    }
