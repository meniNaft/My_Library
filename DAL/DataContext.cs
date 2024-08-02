using Microsoft.EntityFrameworkCore;
using My_Library.Models;
using static System.Net.Mime.MediaTypeNames;

namespace My_Library.DAL
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<Shelf> Shelves { get; set; }
        public DbSet<Set> Sets { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Library>()
                .HasOne(l => l.Genre)
                .WithMany(g => g.Libraries)
                .HasForeignKey(l => l.GenreId)
                .OnDelete(DeleteBehavior.Restrict); // No cascade delete

            modelBuilder.Entity<Shelf>()
                .HasOne(s => s.Library)
                .WithMany(l => l.ShelfList) // Assuming Library can have multiple Shelves
                .HasForeignKey(s => s.LibraryId)
                .OnDelete(DeleteBehavior.Restrict); // No cascade delete

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Shelf)
                .WithMany(s => s.Books) // Assuming Shelf can have multiple Books
                .HasForeignKey(b => b.ShelfId)
                .OnDelete(DeleteBehavior.Restrict); // No cascade delete

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Set)
                .WithMany(s => s.Books) // Assuming Set can have multiple Books
                .HasForeignKey(b => b.SetId)
                .OnDelete(DeleteBehavior.SetNull); // Set to null if Set is deleted

            modelBuilder.Entity<Set>()
                .HasOne(s => s.Genre)
                .WithMany() // Assuming Genre can have multiple Sets
                .HasForeignKey(s => s.GenreId)
                .OnDelete(DeleteBehavior.Restrict); // No cascade delete
        }
    }
}
