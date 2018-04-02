using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetinGo.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace MetinGo.Server.Infrastructure.Database
{
    public class MetinGoDbContext : DbContext
    {
	    public MetinGoDbContext(DbContextOptions<MetinGoDbContext> options) : base(options)
	    {
	    }

		public DbSet<User> Users { get; set; }

        public DbSet<Character> Characters { get; set; }

        public DbSet<Monster> Monsters { get; set; }

        public DbSet<Entities.Fight> Fights { get; set; }
    }
}
