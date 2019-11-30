using Microsoft.EntityFrameworkCore;
using PersonDirectory.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonDirectory.Data.Models
{
    public class PersonDirectoryContext : DbContext
    {
        public PersonDirectoryContext(DbContextOptions<PersonDirectoryContext> options) : base(options)
        {
        }

        public DbSet<Person> People { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
