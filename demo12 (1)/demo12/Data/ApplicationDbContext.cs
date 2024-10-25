﻿using demo12.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace demo12.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,
                                        ApplicationRole, string, IdentityUserClaim<string>,
                                        UserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>,
                                        IdentityUserToken<string>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<ApplicationJob> ApplcationJobs { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<UserRole> UserRoless { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>(a =>
            {
                a.HasMany(b => b.Claims)
                    .WithOne()
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                a.HasMany(b => b.Logins)
                    .WithOne()
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                a.HasMany(b => b.Tokens)
                    .WithOne()
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                a.HasMany(b => b.UserRoles)
                    .WithOne(b => b.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });
            modelBuilder.Entity<ApplicationRole>(a =>
            {
                a.HasMany(b => b.UserRoles)
                .WithOne(b => b.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();
            });
        }

    }
}
