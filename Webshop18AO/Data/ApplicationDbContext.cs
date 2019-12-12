using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Webshop18AO.Models;

namespace Webshop18AO.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Webshop18AO.Models.Category> Category { get; set; }
        public DbSet<Webshop18AO.Models.Product> Product { get; set; }
    }
}
