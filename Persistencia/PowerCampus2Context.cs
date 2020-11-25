using Dominio;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistencia
{
    public class PowerCampus2Context:IdentityDbContext<T_user>
    {
        public PowerCampus2Context(DbContextOptions options): base(options)
        {

        }

        protected override void OnModelCreating (ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);
            modelbuilder.Entity<T_user>().HasKey(tr => new { tr.Id });
            modelbuilder.Entity<T_career>().HasKey(tr => new { tr.id_career });
            modelbuilder.Entity<T_course>().HasKey(tr => new { tr.id_course });
            modelbuilder.Entity<T_group>().HasKey(tr => new { tr.id_group });
            modelbuilder.Entity<T_inscription>().HasKey(tr => new { tr.id_inscription });
            modelbuilder.Entity<T_det_inscription>().HasKey(tr => new { tr.id_det_inscription });
            /*modelbuilder.Entity<T_user>(entity =>
            {
                entity.HasKey(e => e.id_user);
                entity.HasAlternateKey(e => e.username);
            });*/
        }

        public DbSet<T_user> t_user { get; set; }
        public DbSet<T_career> t_career { get; set; }
        public DbSet<T_course> t_course { get; set; }
        public DbSet<T_group> t_group { get; set; }
        public DbSet<T_inscription> t_inscription { get; set; }
        public DbSet<T_det_inscription> t_det_inscription { get; set; }
    }
}
