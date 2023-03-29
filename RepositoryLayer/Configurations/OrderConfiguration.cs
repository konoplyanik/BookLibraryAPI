using BookLibrary.Domain.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Infrastructure.Data.Configurations
{
    public class OrderConfiguration
    {
        public static void SetOrderConfiguration(ModelBuilder modelBuilder)
        {
            // Fluent API Configurations

            // Entity Configuration
            modelBuilder.Entity<Order>()
                .HasOne<ApplicationUser>(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(u => u.ApplicationUserId);

            modelBuilder.Entity<Order>().HasKey(b => b.OrderId);

            // Property Configuration
            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(o => o.Description)
                    .HasColumnName("DescriptionOrder")
                    .HasDefaultValue("Default Value")
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
