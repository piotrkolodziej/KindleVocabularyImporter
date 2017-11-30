using KindleVocabularyImporter.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace KindleVocabularyImporter
{
    namespace Data
    {
        public class KindleContext : DbContext
        {
            public DbSet<Lookup> Lookups { get; set; }
            public DbSet<Word> Words { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                // Book Propertis
                modelBuilder.Entity<Book>().ToTable("book_info");
                modelBuilder.Entity<Book>().Property(x => x.Id).HasColumnName("id");
                modelBuilder.Entity<Book>().Property(x => x.Authors).HasColumnName("authors");
                modelBuilder.Entity<Book>().Property(x => x.Title).HasColumnName("title");

                // Lookups Properties
                modelBuilder.Entity<Lookup>().Property(x => x.Id).HasColumnName("id");
                modelBuilder.Entity<Lookup>().Property(x => x.Usage).HasColumnName("usage");
                modelBuilder.Entity<Lookup>().Property(x => x.WordId).HasColumnName("word_key");
                modelBuilder.Entity<Lookup>().Property(x => x.BookId).HasColumnName("book_key");

                // Words Properties
                modelBuilder.Entity<Word>().Property(x => x.Id).HasColumnName("id");
                modelBuilder.Entity<Word>().Property(x => x.Name).HasColumnName("word");

                // Mappings
                modelBuilder.Entity<Lookup>().HasOne(t => t.Book);
                modelBuilder.Entity<Lookup>().HasOne(t => t.Word).WithMany(t => t.Lookups).HasForeignKey(p => p.WordId);
            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlite("Data Source=vocab.db");
            }
        }
    }
}