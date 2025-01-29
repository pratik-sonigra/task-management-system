using Microsoft.EntityFrameworkCore;
using TaskManagement.Contracts.DTOs;
using TaskManagement.Domain.Models;
using TaskStatus = TaskManagement.Domain.Models.TaskStatus;

namespace TaskManagement.Infrastructure.Persistence;

public partial class TaskManagementContext : DbContext
{
    public TaskManagementContext(DbContextOptions<TaskManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ActivityLog> ActivityLogs { get; set; }

    public virtual DbSet<ChangeType> ChangeTypes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<TaskEntity> Tasks { get; set; }

    public virtual DbSet<TaskStatus> TaskStatuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public DbSet<DashboardSummary> DashboardSummaries { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActivityLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__Activity__5E548648F1C078C3");

            entity.ToTable("ActivityLog");

            entity.Property(e => e.ChangeDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.ChangeType).WithMany(p => p.ActivityLogs)
                .HasForeignKey(d => d.ChangeTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ActivityL__Chang__3B75D760");

            entity.HasOne(d => d.Task).WithMany(p => p.ActivityLogs)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ActivityL__TaskI__398D8EEE");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.ActivityLogs)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ActivityL__Updat__3A81B327");
        });

        modelBuilder.Entity<ChangeType>(entity =>
        {
            entity.HasKey(e => e.ChangeTypeId).HasName("PK__ChangeTy__A4EF71B1EE8531A6");

            entity.ToTable("ChangeType");

            entity.HasIndex(e => e.ChangeTypeName, "UQ__ChangeTy__0A67A461446C46C6").IsUnique();

            entity.Property(e => e.ChangeTypeName)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1A8873D1DD");

            entity.HasIndex(e => e.RoleName, "UQ__Roles__8A2B6160DB22024F").IsUnique();

            entity.Property(e => e.RoleName)
                .IsRequired()
                .HasMaxLength(20);
        });

        modelBuilder.Entity<TaskEntity>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("PK__Tasks__7C6949B159667EB7");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DueDate).HasColumnType("datetime");
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasOne(d => d.AssignedUser).WithMany(p => p.TaskAssignedUsers)
                .HasForeignKey(d => d.AssignedUserId)
                .HasConstraintName("FK__Tasks__AssignedU__33D4B598");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.TaskCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tasks__CreatedBy__35BCFE0A");

            entity.HasOne(d => d.Status).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tasks__StatusId__34C8D9D1");
        });

        modelBuilder.Entity<TaskStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__TaskStat__C8EE2063C076A182");

            entity.ToTable("TaskStatus");

            entity.HasIndex(e => e.StatusName, "UQ__TaskStat__05E7698A2DE51949").IsUnique();

            entity.Property(e => e.StatusName)
                .IsRequired()
                .HasMaxLength(20);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C423F540E");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E43850D6F6").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D1053435602DD2").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.PasswordHash).IsRequired();
            entity.Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__RoleId__300424B4");
        });

        modelBuilder.Entity<DashboardSummary>().HasNoKey();

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}