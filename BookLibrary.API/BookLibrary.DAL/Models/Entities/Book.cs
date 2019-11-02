using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BookLibrary.DAL.Models.Entities.Abstraction;

namespace BookLibrary.DAL.Models.Entities
{
    public class Book : IAuditableEntity
    {
        public Book()
        {
            AuthorBooks = new HashSet<AuthorBook>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        [Range(1, int.MaxValue)]
        public int PagesCount { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<AuthorBook> AuthorBooks { get; set; }
    }
}
