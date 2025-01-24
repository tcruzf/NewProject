using System;
using Microsoft.EntityFrameworkCore;
using ControllRR.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace ControllRR.Infrastructure.Data.Context;

    public partial class ControllRRContext : IdentityDbContext<IdentityUser>
    {
        public ControllRRContext()
        {
        }

        public ControllRRContext(DbContextOptions<ControllRRContext> options)
            : base(options)
        {
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Device> Devices{ get; set; }
        public virtual DbSet<Maintenance> Maintenances { get; set;}
        public virtual DbSet<Sector> Sectors { get; set;}
        public virtual DbSet<Document> Documents { get; set;}
        public virtual DbSet<MaintenanceNumberControl> MaintenanceNumberControls{ get; set; }
        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Altere a string de conexão de acordo com seu projeto
                // Substitua pela sua string de conexão para MySQL
               optionsBuilder.UseMySql("server=localhost;port=3306;user=root;password=mypass;database=NEWGEN", 
                    new MySqlServerVersion(new Version(8, 0, 32))); 
                
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        
    }