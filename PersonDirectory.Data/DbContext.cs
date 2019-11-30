using Microsoft.EntityFrameworkCore;
using PersonDirectory.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
            modelBuilder.Entity<Gender>().Property(x => x.Id).ValueGeneratedNever();
            modelBuilder.Entity<RelationType>().Property(x => x.Id).ValueGeneratedNever();
            modelBuilder.Entity<City>().Property(x => x.Id).UseIdentityColumn();

            modelBuilder.Entity<Person>().HasMany(x => x.PhoneNumbers).WithOne(x => x.Person).HasForeignKey(x => x.PersonId);
            modelBuilder.Entity<Person>().HasMany(x => x.RelatedPeople).WithOne(x => x.Person).HasForeignKey(x => x.PersonId);
            modelBuilder.Entity<Person>().HasOne(x => x.City).WithMany(x => x.Persons).HasForeignKey(x => x.CityId);
            modelBuilder.Entity<Person>().HasOne(x => x.Gender).WithMany(x => x.Persons).HasForeignKey(x => x.GenderId);
        }
    }
}
