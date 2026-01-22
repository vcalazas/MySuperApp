using Microsoft.EntityFrameworkCore;
using MSP.Domain.Entities;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MSP.Data
{
    public class MSPContext : DbContext
    {
        public virtual DbSet<MSPSystemSettings> MSPSystemSettings { get; set; }
        public static int InstanceCount;

        public MSPContext(DbContextOptions options) : base(options)
            => Interlocked.Increment(ref InstanceCount);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MSPSystemSettings>(entity =>
            {
                entity.HasIndex(p => p.SettingKey).IsUnique();
            });
        }
    }
}
