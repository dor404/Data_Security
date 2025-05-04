﻿using Microsoft.EntityFrameworkCore;
using eBookLoginHW1.Models;
using System.Collections.Generic;

namespace eBookLoginHW1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
