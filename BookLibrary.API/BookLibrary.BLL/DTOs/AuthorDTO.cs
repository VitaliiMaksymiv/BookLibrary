using System.Collections.Generic;

namespace BookLibrary.BLL.DTOs
{
    public class AuthorDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public ICollection<BookDTO> Books {get;set;}
    }
}