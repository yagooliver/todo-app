using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoList.Domain.Entities;

namespace TodoList.Infra.Data.PostgresSQL.Mappings
{
    public class ItemMapping : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .HasColumnName("name")
                .IsRequired();

            builder.Property(e => e.Description)
                .HasColumnName("description")
                .IsRequired();

            builder.Property(e => e.ShowAlerts)
                .HasColumnName("show_alert")
                .IsRequired();

            builder.Property(e => e.EndDate)
                .HasColumnName("end_date")
                .IsRequired();

            builder.Property(e => e.UserId)
                .HasColumnName("user_id");

            builder.HasOne(e => e.Status)
                .WithMany(e => e.Items)
                .HasForeignKey(e => e.StatusId)
                .IsRequired();

            builder.HasMany(e => e.ItemHistories)
                .WithOne(e => e.Item)
                .HasForeignKey(e => e.ItemId)
                .IsRequired();
        }
    }
}
