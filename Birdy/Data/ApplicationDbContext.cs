using System;
using System.Collections.Generic;
using System.Text;
using Birdy.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Birdy.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Model CategoryGroup der Datenbank hinzufügen
        public DbSet<CategoryGroup> CategoryGroup { get; set; }

        // Model Category der Datenbank hinzufügen
        public DbSet<Category> Category { get; set; }

        // Model SearchEntry der Datenbank hinzufügen
        public DbSet<SearchEntry> SearchEntry { get; set; }
    }
}
