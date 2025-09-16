using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using TemplateApp.Domain.Entities;
using TemplateApp.Persistence.PostgreSQL.DbConfigurations;

namespace TemplateApp.Persistence.PostgreSQL.Database;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Template> Templates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TemplateConfiguration());

        base.OnModelCreating(modelBuilder);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseExceptionProcessor();
    }
}
