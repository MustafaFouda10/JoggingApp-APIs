using JoggingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace JoggingApp.Data
{
    public class JoggingDbContext : DbContext
    {
        public JoggingDbContext()
        {
        }
        public JoggingDbContext(DbContextOptions<JoggingDbContext> options)
           : base(options)
        {
        }

        DbSet<User> Users { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<Jogging> Joggings { get; set; }
        DbSet<Permission> Permissions { get; set; }
        DbSet<RolePermission> RolePermissions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RolePermission>().HasKey(scs => new { scs.RoleId, scs.PermissionId });
            
            //================= data seeding =============
            modelBuilder.Entity<Permission>().HasData(new Permission[] {
                new Permission{Id = 1, Name = "CRUD User" },
                new Permission{Id = 2, Name = "CRUD Jogging" }
            });

            modelBuilder.Entity<Role>().HasData(new Role[] {
                new Role{Id = 1, Name = "Admin" },
                new Role{Id = 2, Name = "User Manager" },
                new Role{Id = 3, Name = "Regular User" }
            });

            modelBuilder.Entity<RolePermission>().HasData(new RolePermission[] {
               
                // Admin 
                new RolePermission{RoleId = 1, PermissionId = 1 },
                new RolePermission{RoleId = 1, PermissionId = 2 },

                // User Manager
                new RolePermission{RoleId = 2, PermissionId = 1 },

                // Regular User
                new RolePermission{RoleId = 3, PermissionId = 2 },


            });


            modelBuilder.Entity<User>().HasData(new User[] {
                new User{Id = 1, UserName = "Mustafa" , RoleId= 1, Password="123"},
                new User{Id = 2, UserName = "Amr" , RoleId = 2 , Password="456"}
            });
        }
    }
}
