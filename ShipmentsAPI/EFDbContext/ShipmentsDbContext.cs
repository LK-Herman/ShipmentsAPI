﻿using Microsoft.EntityFrameworkCore;
using ShipmentsAPI.Entities;
using System.Data;
using System.Net;

namespace ShipmentsAPI.EFDbContext
{
    public class ShipmentsDbContext : DbContext
    {
        public ShipmentsDbContext(DbContextOptions<ShipmentsDbContext> options) : base(options)
        {

        }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Forwarder> Forwarders { get; set; }
        public DbSet<Incoterm> Incoterms { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<WarehouseArea> WarehouseAreas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .Property(i => i.Name)
                .IsRequired();

            modelBuilder.Entity<Customer>()
                .Property(i => i.ShortName)
                .IsRequired()
                .HasMaxLength(10);

            modelBuilder.Entity<Shipment>()
                .Property(r => r.ETD)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
