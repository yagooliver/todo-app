using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoList.Domain.Entities;

namespace TodoList.Infra.Data.PostgresSQL.Mappings
{
    public class ItemHistoryMapping : IEntityTypeConfiguration<ItemHistory>
    {
        public void Configure(EntityTypeBuilder<ItemHistory> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.NewStatus)
                .HasColumnName("new_status")
                .IsRequired();

            builder.Property(e => e.OldStatus)
                .HasColumnName("old_status");

            builder.Property(e => e.ItemId)
                .HasColumnName("item_id")
                .IsRequired();
                
        }

    }
}