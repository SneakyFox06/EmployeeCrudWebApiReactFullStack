using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EmployeeCrudAsp.Data;

public partial class EmsDbContext : DbContext
{
    public EmsDbContext()
    {
    }

    public EmsDbContext(DbContextOptions<EmsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DId).HasName("PK__Departme__3214EC07D6EE9839");

            entity.ToTable("Department");

            entity.Property(e => e.DepartmentName).HasMaxLength(50);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EId).HasName("PK__Employee__3214EC07A92C1F9F");

            entity.ToTable("Employee");

            entity.Property(e => e.Age).HasComputedColumnSql("((CONVERT([int],CONVERT([char](8),getdate(),(112)))-CONVERT([int],CONVERT([char](8),[dob],(112))))/(10000))", false);
            entity.Property(e => e.Dob)
                .HasColumnType("datetime")
                .HasColumnName("DOB");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);

            entity.HasOne(d => d.Department).WithMany(p => p.Employees)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK__Employee__Depart__2A4B4B5E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
