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
        public DbSet<City> Cities { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<RelatedPerson> RelatedPeople { get; set; }
        public DbSet<RelationType> RelationTypes { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
