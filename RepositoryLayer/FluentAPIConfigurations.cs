using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public class FluentAPIConfigurations
    {
        public static void FluentAPIConfig(ModelBuilder modelBuilder)
        {
            // Fluent API Configurations

            // Entity Configuration
            modelBuilder.Entity<Book>().HasKey(b => b.BookId);
            modelBuilder.Entity<Order>().HasKey(b => b.OrderId);

            // Property Cofiguration
            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(b => b.Author)
                    .HasColumnName("BookAuthor")
                    .HasDefaultValue("Macro Code")
                    .IsRequired();

                entity.Property(b => b.Title)
                    .HasColumnName("BookTitle")
                    .HasDefaultValue("Macro Code")
                    .IsRequired();

                entity.Property(b => b.Price)
                    .HasColumnName("BookPrice")
                    .IsRequired();
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(o => o.Description)
                    .HasColumnName("DescriptionOrder")
                    .HasDefaultValue("Macro Code")
                    .IsRequired();

                entity.Property(o => o.DateOfOrder)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(o => o.BookId)
                    .IsRequired();
            });
        }
    }
}
