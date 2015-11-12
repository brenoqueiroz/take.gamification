using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Take.Gamification.Models
{
    public class GameContext : DbContext
    {
        public GameContext() : base("DefaultConnection")
        {

        }

        public DbSet<UserAccount> UserSet { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserMerit>().HasOptional(x => x.OwnerUser).WithMany(x => x.Owners).HasForeignKey(x => x.OwnerUserId);
            modelBuilder.Entity<UserMerit>().HasRequired(x => x.TargetUser).WithMany(x => x.Targets).HasForeignKey(x => x.TargetUserId);
            modelBuilder.Entity<UserMerit>().HasRequired(x => x.Merit).WithMany(x => x.UserMerits).HasForeignKey(x => x.MeritId);
        }

    }
}