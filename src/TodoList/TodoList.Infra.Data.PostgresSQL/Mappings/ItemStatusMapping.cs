using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoList.Domain.Entities;

namespace TodoList.Infra.Data.PostgresSQL.Mappings
{
    public class ItemStatusMapping : IEntityTypeConfiguration<ItemStatus>
    {
        public void Configure(EntityTypeBuilder<ItemStatus> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.Name)
                .HasColumnName("name")
                .IsRequired();

            builder.Property(e => e.Description)
                .HasColumnName("description")
                .IsRequired();

            builder.Property(e => e.Status)
                .IsRequired();
        }
    }
}
