using System.Collections.Generic;

namespace BookLibrary.BLL.DTOs
{
    public class BookDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public int PagesCount { get; set; }
        public ICollection<AuthorDTO> Authors { get; set; }
    }
}