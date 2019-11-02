using BookLibrary.DAL.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookLibrary.DAL.Models.EntityConfiguration
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("BOOK");

            builder.Property(e => e.Id).HasColumnName("ID");

            builder.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(date('now'))");

            builder.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(date('now'))");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);


            builder.Property(e => e.Year)
                .IsRequired();

            builder.Property(e => e.PagesCount)
                .IsRequired();
        }
    }
}