using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using VanillaReact.Api.Models;

namespace VanillaReact.Api.Contexts
{
  public class ApplicationDbContext : DbContext
  {

    public DbSet<User> Users { get; set; }
    public DbSet<Snippet> Snippets { get; set; }
    public DbSet<Category> Categories { get; set; }

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    //You cannot create composite primary keys in EF currently with annotations.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Kudo>().HasKey(k => new { k.SnippetId, k.AuthorId });
      modelBuilder.Entity<Snippet>(s =>
      {
        s.HasIndex(e => new { e.AuthorId, e.Title }).IsUnique();
      });
    }
  }
}