using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork7
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public ICollection<BookAuthor> BooksAuthors { get; set; }
        public override string ToString()
        {
            return $"AuthorId: {AuthorId}, Name: {Name}";
        }
    }
}
