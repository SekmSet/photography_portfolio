using Microsoft.EntityFrameworkCore;

namespace server.Model;

public class DatabaseContext: DbContext
{
    public DbSet<UserModel> UserModels { get; set; }
    public DbSet<PolicyModel> PolicyModels { get; set; }
    public DbSet<TagModel> TagModel { get; set; }
    public DbSet<PhotographyModel> PhotographyModel { get; set; }
    public DbSet<TagOnPhotographyModel> TagOnPhotographyModel { get; set; }
    
    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configurer la clé composite pour TagOnPhotography
        modelBuilder.Entity<TagOnPhotographyModel>()
            .HasKey(t => new { t.PhotographyId, t.TagId });

        base.OnModelCreating(modelBuilder);
    }
}