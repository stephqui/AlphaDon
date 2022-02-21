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
        public DbSet<Comment> Comment { get; set; }

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

            //modelBuilder.Entity<NonCashDonation>().HasIndex(a => a.CollectId).IsUnique();

        }

        public void InitializeDb()
        {
            this.Database.EnsureDeleted();
            this.Database.EnsureCreated();

            //*********************************************  Début   CREATIONS DES PROFILS *****************************************
            Adress addr = new Adress { City = "Paris", Country = "FR", Street = "2, avenue de l'Opéra", Zip = 75016 };
            Profile profile = new Profile
            {
                profilePersonality = ProfilePersonality.Single,
                FirstName = "Alain",
                LastName = "Prevost",
                Birthday = 1991 - 06 - 25,
                PayMethod = "CB",
                Nick = "toto",
                Nationality = "FR",
                Phone = "0123456789",
                Adress = addr
            };
            this.UserAccounts.Add(new UserAccount
            {
                Mail = "alain@gmail.com",
                Password = "9C-D4-E5-8F-17-09-F7-F9-0A-A4-75-FA-F1-89-62-A7",
                Status = AccountStatus.IsProjectCreator,
                Profil = profile,
                Role = "Basic"
            });
            Adress addr2 = new Adress { City = "Reims", Country = "FR", Street = "12, rue Carnot", Zip = 51000 };
            Profile profile2 = new Profile
            {
                profilePersonality = ProfilePersonality.Ngo,
                FirstName = "Jean-Louis",
                LastName = "AUBIN",
                Birthday = 2000 - 02 - 08,
                PayMethod = "CB",
                Nick = "tata",
                Nationality = "FR",
                Phone = "0987654321",
                Siret = "178569248",
                Adress = addr2
            };
            this.UserAccounts.Add(new UserAccount
            {
                Mail = "joe@yahoo.com",
                Password = "C6-01-63-F5-D6-84-99-D3-07-EA-E7-1A-BE-31-9D-ED",
                Status = AccountStatus.Valid,
                Profil = profile2,
                Role = "Basic"
            });
            Adress addr3 = new Adress { City = "Montévrin", Country = "FR", Street = "51, rue de l'Eglise", Zip = 77140 };
            Profile profile3 = new Profile
            {
                profilePersonality = ProfilePersonality.Single,
                FirstName = "Jeremy",
                LastName = "ENGELMANN",
                Birthday = 1987 - 03 - 12,
                PayMethod = "CB",
                Nick = "jeje",
                Nationality = "FR",
                Phone = "0612010304",
                Picture = "crash.jpg",
                Adress = addr3
            };
            this.UserAccounts.Add(new UserAccount
            {
                Mail = "jeremy@gmail.com",
                Password = "C6-01-63-F5-D6-84-99-D3-07-EA-E7-1A-BE-31-9D-ED",
                Status = AccountStatus.Valid,
                Profil = profile3,
                Role = "Admin"
            });
            Adress addr4 = new Adress { City = "Caen", Country = "FR", Street = "253, Boulevard Leroy", Zip = 14000 };
            Profile profile4 = new Profile
            {
                profilePersonality = ProfilePersonality.Single,
                FirstName = "David",
                LastName = "LAUNEY",
                Birthday = 1990-08-07,
                PayMethod = "CB",
                Nick = "dav",
                Nationality = "FR",
                Phone = "0712210304",
                Adress = addr4
            };
            this.UserAccounts.Add(new UserAccount
            {
                Mail = "david@gmail.com",
                Password = "1D-F1-37-E0-56-CD-51-99-14-A4-87-52-D3-66-77-04",
                Status = AccountStatus.Valid,
                Profil = profile4,
                Role = "Admin"
            });
            Adress addr5 = new Adress { City = "Toulouse", Country = "FR", Street = "58, Route de Narbonne", Zip = 31500 };
            Profile profile5 = new Profile
            {
                profilePersonality = ProfilePersonality.Single,
                FirstName = "Nacer",
                LastName = "BENCHEHIDA ",
                Birthday = 1989-08-07,
                PayMethod = "CB",
                Nick = "nass",
                Nationality = "FR",
                Phone = "0689213604",
                Adress = addr5
            };
            this.UserAccounts.Add(new UserAccount
            {
                Mail = "nacer@gmail.com",
                Password = "93-EE-3B-3C-46-68-92-1D-BF-4B-44-8D-20-B7-C6-63",
                Status = AccountStatus.Valid,
                Profil = profile5,
                Role = "Admin"
            });
            Adress addr6 = new Adress { City = "Annecy", Country = "FR", Street = "156, Avenue de Geneve", Zip = 74000 };
            Profile profile6 = new Profile
            {
                profilePersonality = ProfilePersonality.Single,
                FirstName = "Stephane",
                LastName = "PROST-TOURNIER ",
                Birthday = 1968-04-25,
                PayMethod = "CB",
                Nick = "steph",
                Nationality = "FR",
                Phone = "0702413302",
                Adress = addr6
            };
            this.UserAccounts.Add(new UserAccount
            {
                Mail = "stephane@gmail.com",
                Password = "A8-B7-6B-BF-C1-F3-39-16-81-44-28-3D-F3-A8-09-2F",
                Status = AccountStatus.Valid,
                Profil = profile6,
                Role = "Admin"
            });
            Adress addr7 = new Adress { City = "Lyon", Country = "FR", Street = "16, Boulevard Gambetta", Zip = 69000 };
            Profile profile7 = new Profile
            {
                profilePersonality = ProfilePersonality.Single,
                FirstName = "Blaise",
                LastName = "Wolff ",
                Birthday = 2003-02-03,
                PayMethod = "CB",
                Nick = "bunedik",
                Nationality = "FR",
                Phone = "0677851420",
                Adress = addr7
            };
            this.UserAccounts.Add(new UserAccount
            {
                Mail = "blaise@gmail.com",
                Password = "60-DA-C8-94-81-3A-D2-98-AE-61-F6-36-21-36-9B-20",
                Status = AccountStatus.Valid,
                Profil = profile7,
                Role = "basic"
            });
            Adress addr8 = new Adress { City = "Gissac", Country = "FR", Street = "36 Rue de l'Aire aux Grains", Zip = 12360 };
            Profile profile8 = new Profile
            {
                profilePersonality = ProfilePersonality.Single,
                FirstName = "Didier",
                LastName = "Allain ",
                Birthday = 1978-02-05,
                PayMethod = "CB",
                Nick = "lepan",
                Nationality = "FR",
                Phone = "0655418766",
                Adress = addr8
            };
            this.UserAccounts.Add(new UserAccount
            {
                Mail = "alaind@gmail.com",
                Password = "9C-D4-E5-8F-17-09-F7-F9-0A-A4-75-FA-F1-89-62-A7",
                Status = AccountStatus.Valid,
                Profil = profile8,
                Role = "basic"
            });
            Adress addr9 = new Adress { City = "Hussigny-Godbrange", Country = "FR", Street = "22 Rue Henri Lasne", Zip = 54590 };
            Profile profile9 = new Profile
            {
                profilePersonality = ProfilePersonality.Single,
                FirstName = "Marielle",
                LastName = "Arnaud ",
                Birthday = 1969-06-11,
                PayMethod = "CB",
                Nick = "soxoha",
                Nationality = "FR",
                Phone = "0655418766",
                Adress = addr9
            };
            this.UserAccounts.Add(new UserAccount
            {
                Mail = "marielle@wanadoo.fr",
                Password = "F8-FE-6D-0B-C2-1F-20-69-D2-DF-BE-74-BD-72-13-DE",
                Status = AccountStatus.Valid,
                Profil = profile9,
                Role = "basic"
            });
            Adress addr10 = new Adress { City = "Lanet ", Country = "FR", Street = "24 Rue de la Bretinais", Zip = 11330 };
            Profile profile10 = new Profile
            {
                profilePersonality = ProfilePersonality.Single,
                FirstName = "Marcelle",
                LastName = "Godard ",
                Birthday = 1993 - 02 - 21,
                PayMethod = "CB",
                Nick = "rusum",
                Nationality = "FR",
                Phone = "0769411766",
                Adress = addr10
            };
            this.UserAccounts.Add(new UserAccount
            {
                Mail = "marcelle@yahoo.fr",
                Password = "F8-FE-6D-0B-C2-1F-20-69-D2-DF-BE-74-BD-72-13-DE",
                Status = AccountStatus.Valid,
                Profil = profile10,
                Role = "basic"
            });

            //*********************************************   FIN de  CREATIONS DES PROFILS *****************************************

            this.Collects.AddRange(
             new Collect { CurrentAmount = 20, Id = 1 },
             new Collect { CurrentAmount = 224, Id = 2 },
             new Collect { CurrentAmount = 2500, Id = 3 },
             new Collect { CurrentAmount = 560, Id = 4 },
             new Collect { CurrentAmount = 3600, Id = 5 },
             new Collect { CurrentAmount = 4150, Id = 6 }
             );
            this.SaveChanges();

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
                    Limit = 5000,
                    Picture = "chat.jpg",
                    CollectId = 1,
                    ProfileId = 1,
                    Status = ProjectStatus.created
                    
                },
                 new Project
                 {
                     ProjectName = "Traverse",
                     StartDate = new DateTime(2021, 12, 10),
                     EndDate = new DateTime(2022, 06, 25),
                     Description = "Aider la rénovation du pont de Morette",
                     Place = "Thones",
                     Rib = "FR76 8002 0001 0150",
                     Category = ProjectCategory.infrastructural,
                     Limit = 2000,
                     CollectId = 2,
                     ProfileId = 2,
                     Picture= "pontthones.jpg"
                 },
                 new Project
                 {
                     ProjectName = "Rénovation d'une école",
                     StartDate = new DateTime(2021, 12, 25),
                     EndDate = new DateTime(2023, 01, 12),
                     Description = "Aider la rénovation d'une école au Cambodge",
                     Place = "Phnom Pen",
                     Rib = "FR76 8002 0004 0220",
                     Category = ProjectCategory.infrastructural,
                     Limit = 50000,
                     CollectId = 3,
                     ProfileId = 7,
                     Picture = "pnompen4.jpg",
                     Status = ProjectStatus.collectStarted
                 },
                 new Project
                 {
                     ProjectName = "Tour du monde en sac à dos",
                     StartDate = new DateTime(2022, 01, 12),
                     EndDate = new DateTime(2022, 07, 31),
                     Description = "M'aider à réaliser mon rêve de faire le tour du monde",
                     Place = "France",
                     Rib = "FR76 8002 0008 0880",
                     Category = ProjectCategory.sport,
                     Limit = 25000,
                     CollectId = 4,
                     ProfileId = 8,
                     Picture = "tourmonde.jpg",
                     Status = ProjectStatus.collectStarted
                 },
                   new Project
                   {
                       ProjectName = "Du matériel médical pour le Cameroun",
                       StartDate = new DateTime(2021, 12, 31),
                       EndDate = new DateTime(2022, 12, 31),
                       Description = "Envoi d'un container à l'hôpital de M'Bouo",
                       Place = "Cameroun",
                       Rib = "FR76 8002 0009 0990",
                       Category = ProjectCategory.medical,
                       Limit = 10000,
                       CollectId = 5,
                       ProfileId = 9,
                       Picture = "cameroun.jpg",
                       Status = ProjectStatus.beingControled
                   },
                   new Project
                   {
                       ProjectName = "Opération humanitaire en Bulgarie",
                       StartDate = new DateTime(2022, 01, 01),
                       EndDate = new DateTime(2022, 10, 10),
                       Description = "3 étudiants pour aider les orphelins bulgares",
                       Place = "Bulgarie",
                       Rib = "FR76 8002 0010 1010",
                       Category = ProjectCategory.humanitarian,
                       Limit = 5000,
                       CollectId = 6,
                       ProfileId = 10,
                       Picture= "bulgarie.jpg",
                       Status = ProjectStatus.toControl
                   }
                 //  new Project
                 //  {
                 //      ProjectName = "ENSEMBLE, POUR DES FORÊTS LIBRES",
                 //      StartDate = new DateTime(2021, 11, 01),
                 //      EndDate = new DateTime(2022, 11, 01),
                 //      Description = "DÉCOUVRIR ET DÉFENDRE LE VIVANT, ET SA BIODIVERSITÉ",
                 //      Place = "France",
                 //      Rib = "FR76 8002 0011 1111",
                 //      Category = ProjectCategory.infrastructural,
                 //      Limit = 4000,
                 //      CollectId = 7,
                 //      ProfileId = 7,
                 //      Status = ProjectStatus.collectOver
                 //  }

                 );
            this.SaveChanges();

            this.UnitDonations.AddRange(
             new UnitDonation { PayMethod = "CB", Amount = 20, Date = new DateTime(2022, 02, 10), CollectId = 1 },
             new UnitDonation { PayMethod = "CB", Amount = 100, Date = new DateTime(2022, 01, 08), CollectId = 2 },
             new UnitDonation { PayMethod = "PayPa", Amount = 48, Date = new DateTime(2022, 01, 09), CollectId = 2 },
             new UnitDonation { PayMethod = "PayPa", Amount = 76, Date = new DateTime(2022, 01, 09), CollectId = 2 },
             new UnitDonation { PayMethod = "CB", Amount = 2500, Date = new DateTime(2022, 02, 10), CollectId = 3 },
             new UnitDonation { PayMethod = "CB", Amount = 560, Date = new DateTime(2022, 01, 08), CollectId = 4 },
             new UnitDonation { PayMethod = "PayPa", Amount = 3600, Date = new DateTime(2022, 01, 09), CollectId = 5 },
             new UnitDonation { PayMethod = "PayPa", Amount = 4150, Date = new DateTime(2022, 01, 09), CollectId = 6 }
            );

            this.SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;user id=root;password=rrrrr;database=Alpha");
        }
    }
}
