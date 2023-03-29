using BookLibrary.Domain.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Infrastructure.Data.Configurations
{
    public class BookConfiguration
    {
        public static void SetBookConfiguration(ModelBuilder modelBuilder)
        {
            // Fluent API Configurations

            // Entity Configuration
            modelBuilder.Entity<Book>().HasKey(b => b.BookId);

            // Property Configuration
            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(b => b.Author)
                    .HasColumnName("BookAuthor")
                    .HasDefaultValue("Default Value")
                    .IsRequired();

                entity.Property(b => b.Title)
                    .HasColumnName("BookTitle")
                    .HasDefaultValue("Default Value")
                    .IsRequired();

                entity.Property(b => b.Price)
                    .HasColumnName("BookPrice")
                    .IsRequired();
            });
        }
    }
}
