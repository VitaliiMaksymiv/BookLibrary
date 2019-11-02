using BookLibrary.DAL.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookLibrary.DAL.Models.EntityConfiguration
{
    public class AuthorBookConfiguration : IEntityTypeConfiguration<AuthorBook>
    {
        public void Configure(EntityTypeBuilder<AuthorBook> builder)
        {
            builder.ToTable("AUTHOR_BOOK");

            builder.HasKey(e => new { e.AuthorId, e.BookId});

            builder.HasOne(e => e.Book)
                .WithMany(r => r.AuthorBooks)
                .HasForeignKey(e => e.BookId)
                .IsRequired();

            builder.HasOne(e => e.Author)
                .WithMany(r => r.AuthorBooks)
                .HasForeignKey(e => e.AuthorId)
                .IsRequired();
        }
    }
}