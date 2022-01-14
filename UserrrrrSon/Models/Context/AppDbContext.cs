using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserrrrrSon.Models.Authentication;
using UserrrrrSon.Models.models_;

namespace UserrrrrSon.Models.Context
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, int>
    {

        public AppDbContext(DbContextOptions<AppDbContext> dbContext) : base(dbContext)
        {

        }


        public DbSet<Branch> Branches { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<Work> Works { get; set; }
        public DbSet<Service> Services { get; set; }
    }
}
