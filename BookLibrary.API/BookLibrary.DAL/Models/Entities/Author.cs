using System;
using System.Collections.Generic;
using BookLibrary.DAL.Models.Entities.Abstraction;

namespace BookLibrary.DAL.Models.Entities
{
    public class Author : IBaseEntity, IAuditableEntity
    {
        public Author()
        {
            AuthorBooks = new HashSet<AuthorBook>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<AuthorBook> AuthorBooks { get; set; }
    }
}