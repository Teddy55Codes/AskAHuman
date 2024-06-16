using DatabaseLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DatabaseLayer.Context;

public partial class AskAHumanDbContext(IConfiguration configuration) : DbContext
{
    public virtual DbSet<Chat> Chats { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => 
        optionsBuilder.UseMySql(
            configuration?.GetConnectionString("MariaDB"), 
            ServerVersion.AutoDetect(configuration?.GetConnectionString("MariaDB")),
            options => options.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(15),
                errorNumbersToAdd: null
                ));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb3_general_ci")
            .HasCharSet("utf8mb3");

        modelBuilder.Entity<Chat>(entity =>
        {
            entity.HasKey(e => new { e.Id })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0 });

            entity.ToTable("chats");

            entity.HasIndex(e => e.UsersAnswererId, "fk_Chats_Users1_idx");

            entity.HasIndex(e => e.UsersQuestioningId, "fk_Chats_Users2_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("bigint(20)");
            entity.Property(e => e.UsersAnswererId).HasColumnType("bigint(20)");
            entity.Property(e => e.UsersQuestioningId).HasColumnType("bigint(20)");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime");
            entity.Property(e => e.AnswererJoinedAt).HasColumnType("datetime");
            entity.Property(e => e.Completed).HasColumnType("tinyint(4)");
            entity.Property(e => e.Title).HasColumnType("text");
            entity.Property(e => e.Question).HasColumnType("text");

            entity.HasOne(d => d.UsersAnswerer).WithMany(p => p.ChatUsersAnswerers)
                .HasForeignKey(d => d.UsersAnswererId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Chats_Users1");

            entity.HasOne(d => d.UsersQuestioning).WithMany(p => p.ChatUsersQuestionings)
                .HasForeignKey(d => d.UsersQuestioningId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Chats_Users2");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.ChatId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("messages");

            entity.HasIndex(e => e.ChatId, "fk_Messages_Chats1_idx");
            
            entity.HasIndex(e => e.AuthorId, "fk_messages_users1_idx");
            
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("bigint(20)");
            entity.Property(e => e.ChatId).HasColumnType("bigint(20)");
            entity.Property(e => e.Content).HasColumnType("text");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime");
            
            entity.HasOne(d => d.Author).WithMany(p => p.Messagess)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_messages_users1_idx");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("permissions");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Name).HasColumnType("text");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("roles");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Name).HasColumnType("text");

            entity.HasMany(d => d.Permissions).WithMany(p => p.Roles)
                .UsingEntity<Dictionary<string, object>>(
                    "RolesHasPermission",
                    r => r.HasOne<Permission>().WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_Roles_has_Permissions_Permissions1"),
                    l => l.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_Roles_has_Permissions_Roles1"),
                    j =>
                    {
                        j.HasKey("RoleId", "PermissionId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("roles_has_permissions");
                        j.HasIndex(new[] { "PermissionId" }, "fk_Roles_has_Permissions_Permissions1_idx");
                        j.HasIndex(new[] { "RoleId" }, "fk_Roles_has_Permissions_Roles1_idx");
                        j.IndexerProperty<int>("RoleId").HasColumnType("int(11)");
                        j.IndexerProperty<int>("PermissionId").HasColumnType("int(11)");
                    });

            entity.HasMany(d => d.Users).WithMany(p => p.Roles)
                .UsingEntity<Dictionary<string, object>>(
                    "RolesHasUser",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_Roles_has_Users_Users1"),
                    l => l.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_Roles_has_Users_Roles"),
                    j =>
                    {
                        j.HasKey("RoleId", "UserId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("roles_has_users");
                        j.HasIndex(new[] { "RoleId" }, "fk_Roles_has_Users_Roles_idx");
                        j.HasIndex(new[] { "UserId" }, "fk_Roles_has_Users_Users1_idx");
                        j.IndexerProperty<int>("RoleId").HasColumnType("int(11)");
                        j.IndexerProperty<long>("UserId").HasColumnType("bigint(20)");
                    });
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("users");

            entity.Property(e => e.Id).HasColumnType("bigint(20)");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime");
            entity.Property(e => e.PasswordHash).HasColumnType("text");
            entity.Property(e => e.PasswordSalt).HasColumnType("text");
            entity.Property(e => e.Reputation)
                .HasComputedColumnSql("0", false)
                .HasColumnType("bigint(20)");
            entity.Property(e => e.Username).HasColumnType("text");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
