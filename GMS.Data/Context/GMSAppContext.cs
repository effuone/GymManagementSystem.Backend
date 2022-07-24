using System.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GMS.Data.Models;

namespace GMS.Data.Context
{
    public class GMSAppContext : IdentityDbContext<GMSUser>
    {
        public GMSAppContext()
        {

        }

        public GMSAppContext(DbContextOptions<GMSAppContext> options)
            : base(options)
        {
        }
        
        public DbSet<City> Cities {get; set;}
        public DbSet<Country> Countries {get; set;}
        public DbSet<Location> Locations {get; set;}
        public DbSet<Gym> Gyms {get ;set;}
        public DbSet<Manager> Managers {get; set;}
        public DbSet<ManagerType> ManagerTypes { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<MembershipType> MembershipTypes { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<CoachType> CoachTypes { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Session> Sessions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Linux connection(Azure data studio)
            optionsBuilder.UseSqlServer("Server=tcp:localhost;Database=GMSDb;User Id=hbuser;Password=hbuser1029");
            //Windows connection(SSMS)
            //optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GMSDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // UNIQUE additions
            builder.Entity<Country>().HasIndex(x=>x.CountryName).IsUnique(true);
            builder.Entity<City>().HasIndex(x=>x.CityName).IsUnique(true);
            builder.Entity<Location>().HasIndex(x=>x.CityId).IsUnique(true);
            builder.Entity<Gym>().HasIndex(x=>x.GymName).IsUnique(true);
            builder.Entity<ManagerType>().HasIndex(x=>x.ManagerTypeName).IsUnique(true);
            builder.Entity<MembershipType>().HasIndex(x=>x.MembershipTypeName).IsUnique(true);
            builder.Entity<CoachType>().HasIndex(x=>x.CoachTypeName).IsUnique(true);
            // builder.Entity<Faculty>().HasIndex(x=>x.FacultyName).IsUnique(true);
            // builder.Entity<Course>().HasIndex(x=>x.CourseName).IsUnique(true);
            
            // //Relationship configuring 
            builder.Entity<Schedule>().HasOne(x=>x.Coach);
            builder.Entity<Schedule>().HasMany(x=>x.Sessions);
            builder.Entity<Coach>().HasMany(x=>x.Sessions).WithOne(x=>x.Coach);
            builder.Entity<Member>().HasMany(x=>x.Sessions).WithOne(x=>x.Member);
            // builder.Entity<School>().HasMany(x=>x.Candidates).WithOne(x=>x.School);
            builder.Entity<Member>().HasOne(x=>x.Gym).WithMany(x=>x.Members);
            builder.Entity<Member>().HasOne(x=>x.MembershipType);
            builder.Entity<Coach>().HasOne(x=>x.CoachType);
            builder.Entity<Manager>().HasOne(x=>x.ManagerType);
            builder.Entity<Coach>().HasOne(x=>x.Gym).WithMany(x=>x.Coaches);
            builder.Entity<Manager>().HasOne(x=>x.Gym).WithMany(x=>x.Managers);
            builder.Entity<Location>().HasOne(x=>x.City);
            builder.Entity<Location>().HasOne(x=>x.Country);
            // builder.Entity<Portfolio>().HasMany(x=>x.Achievements).WithOne(x=>x.Portfolio);
            // builder.Entity<Portfolio>().HasOne(x=>x.Candidate).WithOne(x=>x.Portfolio).IsRequired();
            // builder.Entity<Achievement>().HasOne(x=>x.AchievementType);
        }      
    }
}
