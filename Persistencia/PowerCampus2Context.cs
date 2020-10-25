using Dominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistencia
{
    public class PowerCampus2Context:DbContext
    {
        public PowerCampus2Context(DbContextOptions options): base(options)
        {

        }

        protected override void OnModelCreating (ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<T_role>().HasKey(tr => new { tr.id_role });
            modelbuilder.Entity<T_user>().HasKey(tr => new { tr.id_user });
            modelbuilder.Entity<T_career>().HasKey(tr => new { tr.id_career });
            modelbuilder.Entity<T_course>().HasKey(tr => new { tr.id_course });
            modelbuilder.Entity<T_group>().HasKey(tr => new { tr.id_group });
            modelbuilder.Entity<T_inscription>().HasKey(tr => new { tr.id_inscription });
            modelbuilder.Entity<T_det_inscription>().HasKey(tr => new { tr.id_det_inscription });
        }

        public DbSet<T_role> t_role { get; set; }
        public DbSet<T_user> t_user { get; set; }
        public DbSet<T_career> t_career { get; set; }
        public DbSet<T_course> t_course { get; set; }
        public DbSet<T_group> t_group { get; set; }
        public DbSet<T_inscription> t_inscriprion { get; set; }
        public DbSet<T_det_inscription> t_det_inscription { get; set; }
    }
}
