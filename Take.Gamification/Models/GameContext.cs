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

        public DbSet<UserAccount> Users { get; set; }

        public DbSet<Merit> Merits { get; set; }

        public DbSet<UserMerit> UserMerits { get; set; }

        public DbSet<UserMedal> UserMedals { get; set; }

        public DbSet<Medal> Medals { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserMerit>().HasOptional(x => x.OwnerUser).WithMany(x => x.Owners).HasForeignKey(x => x.OwnerUserId);
            modelBuilder.Entity<UserMerit>().HasRequired(x => x.TargetUser).WithMany(x => x.Targets).HasForeignKey(x => x.TargetUserId);
            modelBuilder.Entity<UserMerit>().HasRequired(x => x.Merit).WithMany(x => x.UserMerits).HasForeignKey(x => x.MeritId);

            modelBuilder.Entity<UserMedal>().HasRequired(x => x.User).WithMany(x => x.Medals).HasForeignKey(x => x.UserId);
            modelBuilder.Entity<UserMedal>().HasRequired(x => x.Medal).WithMany(x => x.UserMedals).HasForeignKey(x => x.MedalId);
        }

    }
}