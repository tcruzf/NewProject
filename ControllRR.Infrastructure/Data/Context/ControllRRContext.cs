using System;
using Microsoft.EntityFrameworkCore;
using ControllRR.Domain.Entities;

namespace ControllRR.Infrastructure.Data.Context;

    public partial class ControllRRContext : DbContext
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
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Altere a string de conexão de acordo com seu projeto
                // Substitua pela sua string de conexão para MySQL
                optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;password=mypass;database=NEWGEN"); 
                
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }