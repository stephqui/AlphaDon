using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alpha.Models
{
    public class BddContext : DbContext
    {
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<Adress> Adresses { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<AnonymUser> AnonymUsers { get; set; }
        public DbSet<ProjectWallet> ProjectWallets { get; set; }
        public DbSet<ProjectHistory> ProjectHistories { get; set; }
        public DbSet<ProjectCreator> ProjectCreators { get; set; }
        public DbSet<Organization> Organisations { get; set; }
        public DbSet<SingleCreator> SingleCreators { get; set; }
        public DbSet<Collect> Collects { get; set; }
        public DbSet<UnitDonation> UnitDonations { get; set; }
        public DbSet<PeriodicDonation> PeriodicDonations { get; set; }
        public DbSet<NonCashDonation> NonCashDonations { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<ProjectManager> ProjectManagers { get; set; }
        public DbSet<AlphaPlatformAccount> AlphaPlatformAccounts { get; set; }
        public DbSet<Compensation> Compensations { get; set; }
        public DbSet<Newsletter> Newsletters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Adress>().Property(a => a.Street).HasMaxLength(27);
            modelBuilder.Entity<NonCashDonation>().HasIndex(a => a.CollectId).IsUnique();
            // A voir avec la classe AnonymUser
            // modelBuilder.Entity<NonCashDonation>().HasIndex(a => a.AnonymUserId).IsUnique();

            //Organization a plusieurs projet ?
            //modelBuilder.Entity<ProjectCreator>().HasIndex(a => a.OrganizationId).IsUnique();
            //modelBuilder.Entity<ProjectCreator>().HasIndex(a => a.ProjectWalletId).IsUnique();

            modelBuilder.Entity<UserAccount>().HasIndex(u => u.ProfilId).IsUnique();

            modelBuilder.Entity<Profile>().HasIndex(p => p.AdressId).IsUnique();

            modelBuilder.Entity<Project>().HasIndex(a => a.CollectId).IsUnique();
            //modelBuilder.Entity<Project>().HasIndex(a => a.ProjectWalletId).IsUnique();

            modelBuilder.Entity<Compensation>().HasIndex(a => a.ProjectId).IsUnique();

            modelBuilder.Entity<NonCashDonation>().HasIndex(a => a.CollectId).IsUnique();



        }

        public void InitializeDb()
        {
            this.Database.EnsureDeleted();
            this.Database.EnsureCreated();

            this.Profiles.AddRange(
                new Profile
                {
                    profilePersonality = ProfilePersonality.Single,
                    FirstName = "Alain",
                    LastName = "TERIEUR",
                    Birthday = 1991 - 06 - 25,
                    PayMethod = "CB",
                    Nick = "toto",
                    Nationality = "FR",
                    Phone = "0123456789",
                    AdressId = 1
                },
                new Profile
                {
                    profilePersonality = ProfilePersonality.Ngo,
                    FirstName = "Joe",
                    LastName = "DALTON",
                    Birthday = 2000 - 02 - 08,
                    PayMethod = "CB",
                    Nick = "tata",
                    Nationality = "FR",
                    Phone = "0987654321",
                    AdressId = 2,
                    Siret = "178569248"

                }
                );
            this.Adresses.AddRange(
                new Adress { City = "Paris", Country = "FR", Street = "2, avenue de l'Opéra", Zip = 75016 },
                new Adress { City = "Reims", Country = "FR", Street = "12, rue Carnot", Zip = 51000 }
                );
            this.UserAccounts.AddRange(
                new UserAccount { Mail = "alain@gmail.com", Password = "9C-D4-E5-8F-17-09-F7-F9-0A-A4-75-FA-F1-89-62-A7", Status = AccountStatus.IsProjectCreator, ProfilId = 1 },
                new UserAccount { Mail = "joe@yahoo.com", Password = "C6-01-63-F5-D6-84-99-D3-07-EA-E7-1A-BE-31-9D-ED", Status = AccountStatus.Valid, ProfilId = 2 }
                );

            this.Collects.AddRange(
                 new Collect { Limit = 5000, CurrentAmount = 2000, Id = 1 },
                 new Collect { Limit = 10000, CurrentAmount = 4000, Id = 2 }
                 );

            this.Projects.AddRange(
                new Project
                {
                    ProjectName = "Sauver les chats battus",
                    StartDate = new DateTime(2022, 02, 10),
                    EndDate = new DateTime(2022, 06, 25),
                    Description = "Aider les animaux à avoir une vie heureuse",
                    Place = "Marne-la-Vallée",
                    Rib = "FR76 8001 0032 1540",
                    Category = ProjectCategory.fairTrade,
                    CollectId = 1,
                    ProfileId =1
                },
                 new Project
                 {
                     ProjectName = "Lancez moi",
                     StartDate = new DateTime(2022, 02, 10),
                     EndDate = new DateTime(2022, 06, 25),
                     Description = "Aider la rénovation du pont de Morette",
                     Place = "Thones",
                     Rib = "FR76 8002 0001 0150",
                     Category = ProjectCategory.infrastructural,
                     CollectId = 2,
                     ProfileId = 2
                 });

            this.UnitDonations.AddRange(
             new UnitDonation { PayMethod = "CB", CurrentAmount = 20, Date = new DateTime(2022, 02, 10), CollectId = 1 },
             new UnitDonation { PayMethod = "CB", CurrentAmount = 100, Date = new DateTime(2022, 01, 08), CollectId = 2 },
             new UnitDonation { PayMethod = "PayPa", CurrentAmount = 48, Date = new DateTime(2022, 01, 09), CollectId = 2 }
            );

            this.SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;user id=root;password=rrrrr;database=Alpha");
        }
    }


}
