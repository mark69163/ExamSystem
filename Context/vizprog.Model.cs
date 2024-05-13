﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using EF Core template.
// Code is generated on: 2024. 04. 14. 10:53:13
//
// Changes to this file may cause incorrect behavior and will be lost if
// the code is regenerated.
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Model
{

    public partial class Model : DbContext
    {

        public Model() :
            base()
        {
            OnCreated();
        }

        public Model(DbContextOptions<Model> options) :
            base(options)
        {
            OnCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured ||
                (!optionsBuilder.Options.Extensions.OfType<RelationalOptionsExtension>().Any(ext => !string.IsNullOrEmpty(ext.ConnectionString) || ext.Connection != null) &&
                 !optionsBuilder.Options.Extensions.Any(ext => !(ext is RelationalOptionsExtension) && !(ext is CoreOptionsExtension))))
            {
                optionsBuilder.UseSqlServer(@"Server=localhost;Database=examSystem;Trusted_Connection=True; TrustServerCertificate=True");
            }
            CustomizeConfiguration(ref optionsBuilder);
            base.OnConfiguring(optionsBuilder);
        }

        partial void CustomizeConfiguration(ref DbContextOptionsBuilder optionsBuilder);

        public virtual DbSet<STUDENT> STUDENTs
        {
            get;
            set;
        }

        public virtual DbSet<EXAM> EXAMs
        {
            get;
            set;
        }

        public virtual DbSet<INSTRUCTOR> INSTRUCTORs
        {
            get;
            set;
        }

        public virtual DbSet<QUESTION> QUESTIONs
        {
            get;
            set;
        }

       
    
        public virtual DbSet<STUDENT_EXAM> STUDENTs_EXAMs { get; set; }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            this.STUDENTMapping(modelBuilder);
            this.CustomizeSTUDENTMapping(modelBuilder);

            this.EXAMMapping(modelBuilder);
            this.CustomizeEXAMMapping(modelBuilder);

            this.INSTRUCTORMapping(modelBuilder);
            this.CustomizeINSTRUCTORMapping(modelBuilder);

            this.QUESTIONMapping(modelBuilder);
            this.CustomizeQUESTIONMapping(modelBuilder);

            RelationshipsMapping(modelBuilder);
            CustomizeMapping(ref modelBuilder);

            modelBuilder.Entity<STUDENT_EXAM>().HasKey(se => new { se.neptun_id, se.course_id });
        }

        #region STUDENT Mapping

        private void STUDENTMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<STUDENT>().ToTable(@"STUDENTs");
            modelBuilder.Entity<STUDENT>().Property(x => x.neptun_id).HasColumnName(@"neptun_id").IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<STUDENT>().Property(x => x.hash_password).HasColumnName(@"hash_password").IsRequired().ValueGeneratedNever().HasMaxLength(256);
            modelBuilder.Entity<STUDENT>().Property(x => x.first_name).HasColumnName(@"first_name").IsRequired().ValueGeneratedNever().HasMaxLength(50);
            modelBuilder.Entity<STUDENT>().Property(x => x.last_name).HasColumnName(@"last_name").IsRequired().ValueGeneratedNever().HasMaxLength(50);
            modelBuilder.Entity<STUDENT>().Property(x => x.user_status).HasColumnName(@"user_status").IsRequired().ValueGeneratedNever().HasMaxLength(25);
            modelBuilder.Entity<STUDENT>().HasKey(@"neptun_id");
        }

        partial void CustomizeSTUDENTMapping(ModelBuilder modelBuilder);

        #endregion

        #region EXAM Mapping

        private void EXAMMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EXAM>().ToTable(@"EXAMs");
            modelBuilder.Entity<EXAM>().Property(x => x.course_id).HasColumnName(@"course_id").IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<EXAM>().Property(x => x.title).HasColumnName(@"title").IsRequired().ValueGeneratedNever().HasMaxLength(120);
            modelBuilder.Entity<EXAM>().Property(x => x.level).HasColumnName(@"level").IsRequired().ValueGeneratedNever().HasMaxLength(10);
            modelBuilder.Entity<EXAM>().Property(x => x.kredit_value).HasColumnName(@"kredit_value").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<EXAM>().Property(x => x.start_time).HasColumnName(@"start_time").ValueGeneratedNever();
            modelBuilder.Entity<EXAM>().Property(x => x.end_time).HasColumnName(@"end_time").ValueGeneratedNever();
            modelBuilder.Entity<EXAM>().Property(x => x.time_limit).HasColumnName(@"time_limit").ValueGeneratedNever();
            modelBuilder.Entity<EXAM>().HasKey(@"course_id");
        }

        partial void CustomizeEXAMMapping(ModelBuilder modelBuilder);

        #endregion

        #region INSTRUCTOR Mapping

        private void INSTRUCTORMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<INSTRUCTOR>().ToTable(@"INSTRUCTORs");
            modelBuilder.Entity<INSTRUCTOR>().Property(x => x.profid).HasColumnName(@"profid").IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<INSTRUCTOR>().Property(x => x.hash_password).HasColumnName(@"hash_password").IsRequired().ValueGeneratedNever().HasMaxLength(256);
            modelBuilder.Entity<INSTRUCTOR>().Property(x => x.first_name).HasColumnName(@"first_name").IsRequired().ValueGeneratedNever().HasMaxLength(50);
            modelBuilder.Entity<INSTRUCTOR>().Property(x => x.last_name).HasColumnName(@"last_name").IsRequired().ValueGeneratedNever().HasMaxLength(50);
            modelBuilder.Entity<INSTRUCTOR>().Property(x => x.department).HasColumnName(@"department").IsRequired().ValueGeneratedNever().HasMaxLength(50);
            modelBuilder.Entity<INSTRUCTOR>().Property(x => x.office).HasColumnName(@"office").IsRequired().ValueGeneratedNever().HasMaxLength(100);
            modelBuilder.Entity<INSTRUCTOR>().Property(x => x.user_status).HasColumnName(@"user_status").IsRequired().ValueGeneratedNever().HasMaxLength(25);
            modelBuilder.Entity<INSTRUCTOR>().HasKey(@"profid");
        }

        partial void CustomizeINSTRUCTORMapping(ModelBuilder modelBuilder);

        #endregion

        #region QUESTION Mapping

        private void QUESTIONMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QUESTION>().ToTable(@"QUESTIONs");
            modelBuilder.Entity<QUESTION>().Property(x => x.id).HasColumnName(@"id").IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<QUESTION>().Property(x => x.question).HasColumnName(@"question").IsRequired().ValueGeneratedNever().HasMaxLength(500);
            modelBuilder.Entity<QUESTION>().Property(x => x.answers).HasColumnName(@"answers").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<QUESTION>().Property(x => x.solution).HasColumnName(@"solution").IsRequired().ValueGeneratedNever().HasMaxLength(120);
            modelBuilder.Entity<QUESTION>().Property(x => x.point_value).HasColumnName(@"point_value").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<QUESTION>().Property(x => x.course_id).HasColumnName(@"course_id").ValueGeneratedNever();
            modelBuilder.Entity<QUESTION>().HasKey(@"id");
        }

        partial void CustomizeQUESTIONMapping(ModelBuilder modelBuilder);

        #endregion

        private void RelationshipsMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<STUDENT>().HasMany(x => x.EXAMs).WithMany(op => op.STUDENTs);
            modelBuilder.Entity<EXAM>().HasMany(x => x.INSTRUCTORs).WithMany(op => op.EXAMs);
            modelBuilder.Entity<EXAM>().HasMany(x => x.QUESTIONs).WithOne(op => op.EXAM).HasForeignKey(@"course_id").IsRequired(true);

            modelBuilder.Entity<QUESTION>().HasOne(x => x.EXAM).WithMany(op => op.QUESTIONs).HasForeignKey(@"course_id").IsRequired(true);
        }

        partial void CustomizeMapping(ref ModelBuilder modelBuilder);

        public bool HasChanges()
        {
            return ChangeTracker.Entries().Any(e => e.State == Microsoft.EntityFrameworkCore.EntityState.Added || e.State == Microsoft.EntityFrameworkCore.EntityState.Modified || e.State == Microsoft.EntityFrameworkCore.EntityState.Deleted);
        }

        partial void OnCreated();
    }
}
