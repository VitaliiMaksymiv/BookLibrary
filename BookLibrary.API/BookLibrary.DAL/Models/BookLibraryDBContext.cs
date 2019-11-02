using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using BookLibrary.DAL.Models.Entities;
using BookLibrary.DAL.Models.Entities.Abstraction;
using Microsoft.Data.Sqlite;

namespace BookLibrary.DAL.Models
{
    public class BookLibraryDbContext : DbContext
    {
        public BookLibraryDbContext(DbContextOptions<BookLibraryDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            IEnumerable<EntityEntry> unsavedItems = ChangeTracker.Entries()
                .Where(entity => entity.Entity is IAuditableEntity &&
                                 (entity.State == EntityState.Added ||
                                  entity.State == EntityState.Modified));

            foreach (EntityEntry item in unsavedItems)
            {
                IAuditableEntity entity = (IAuditableEntity)item.Entity;
                DateTime now = DateTime.Now;
                entity.UpdatedDate = now;
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

}
    