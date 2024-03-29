﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using EducationApp.DataAcessLayer.Entities;
namespace EducationApp.DataAcessLayer.AppContext
{
    public class ApplicationContext:IdentityDbContext<ApplicationUser>
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<AuthorInPrintingEditons> AuthorInPrintingEditons{ get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems{ get; set; }
        public DbSet<Payment> Payments{ get; set; }
        public DbSet<PrintingEdition> PrintingEditions{ get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
