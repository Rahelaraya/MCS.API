using Application.DTO;
using Application.DTO;
using Domain.Entities;
using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataBase
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Service> Services { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<MovingRequest> MovingRequest { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("Service");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("service_id");
                entity.Property(e => e.Name).HasColumnName("service_name");
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.Price).HasColumnName("price");
            });
            modelBuilder.Entity<MovingRequest>().ToTable("MovingRequest");


        }
    }
}
