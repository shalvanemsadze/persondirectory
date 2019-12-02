using Microsoft.EntityFrameworkCore;
using PersonDirectory.Data.Models;
using PersonDirectory.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PersonDirectory.Data
{
    public class PersonDirectoryContext : DbContext
    {
        public PersonDirectoryContext(DbContextOptions<PersonDirectoryContext> options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<Person> People { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<RelatedPerson> RelatedPeople { get; set; }
        public DbSet<RelationType> RelationTypes { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public DbSet<PhoneNumberType> PhoneNumberTypes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gender>().Property(x => x.Id).ValueGeneratedNever();
            modelBuilder.Entity<RelationType>().Property(x => x.Id).ValueGeneratedNever();
            modelBuilder.Entity<PhoneNumberType>().Property(x => x.Id).ValueGeneratedNever();
            modelBuilder.Entity<PhoneNumber>().Property(x => x.Id).UseIdentityColumn();
            modelBuilder.Entity<City>().Property(x => x.Id).UseIdentityColumn();

            modelBuilder.Entity<Person>().HasMany(x => x.PhoneNumbers).WithOne(x => x.Person).HasForeignKey(x => x.PersonId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Person>().HasMany(x => x.RelatedPeople).WithOne(x => x.Person).HasForeignKey(x => x.PersonId).OnDelete(DeleteBehavior.NoAction); ;
            modelBuilder.Entity<RelatedPerson>().HasOne(x => x.RelativePerson).WithMany(x => x.PeopleByRelated).HasForeignKey(x => x.RelativePersonId).OnDelete(DeleteBehavior.NoAction); ;
            modelBuilder.Entity<Person>().HasOne(x => x.City).WithMany(x => x.Persons).HasForeignKey(x => x.CityId);
            modelBuilder.Entity<Person>().HasOne(x => x.Gender).WithMany(x => x.Persons).HasForeignKey(x => x.GenderId);
            modelBuilder.Entity<PhoneNumber>().HasOne(x => x.PhoneNumberType).WithMany(x => x.PhoneNumbers).HasForeignKey(x => x.Type);

            #region Seed

            modelBuilder.Entity<Gender>().HasData(new Gender
            {
                Id = GenderEnum.Male,
                Name = GenderEnum.Male.ToString()
            },
             new Gender
             {
                 Id = GenderEnum.Female,
                 Name = GenderEnum.Female.ToString()
             });

            modelBuilder.Entity<RelationType>().HasData(new RelationType
            {
                Id = RelationTypeEnum.Colleague,
                Name = RelationTypeEnum.Colleague.ToString()
            },
               new RelationType
               {
                   Id = RelationTypeEnum.Acquaintance,
                   Name = RelationTypeEnum.Acquaintance.ToString()
               }, new RelationType
               {
                   Id = RelationTypeEnum.Relative,
                   Name = RelationTypeEnum.Relative.ToString()
               });


            modelBuilder.Entity<PhoneNumberType>().HasData(new PhoneNumberType
            {
                Id = PhoneNumberTypeEnum.Mobile,
                Name = PhoneNumberTypeEnum.Mobile.ToString()
            },
              new PhoneNumberType
              {
                  Id = PhoneNumberTypeEnum.Office,
                  Name = PhoneNumberTypeEnum.Office.ToString()
              }, new PhoneNumberType
              {
                  Id = PhoneNumberTypeEnum.Home,
                  Name = PhoneNumberTypeEnum.Home.ToString()
              });

            modelBuilder.Entity<City>().HasData(new City
            {
                Id = 1,
                Name = "Tbilisi"
            },
           new City
           {
               Id = 2,
               Name = "Batumi"
           }, new City
           {
               Id = 3,
               Name = "Kutaisi"
           });

            #endregion
        }
    }
}
