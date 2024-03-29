﻿using System;
using Microsoft.EntityFrameworkCore;
using vcar.Core.Models;

namespace DataAnnotations
{
    public class VcarContext : DbContext
    {
        public DbSet<Model> Models { get; set; }
        public DbSet<Make> Makes { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Photo> Photos { get; set; }

        public VcarContext(DbContextOptions<VcarContext> options)
        : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarFeature>().HasKey(vf => new { vf.CarId, vf.FeatureId });
        }
    }
}

