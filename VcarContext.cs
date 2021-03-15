using System;
using Microsoft.EntityFrameworkCore;
using vcar.Models;

namespace DataAnnotations
{
    public class VcarContext : DbContext
    {
        public DbSet<Model> Models { get; set; }
        public DbSet<Make> Makes { get; set; }
        public DbSet<Car> Cars { get; set; }

        public VcarContext(DbContextOptions<VcarContext> options)
        : base(options)
        { }
    }
}
