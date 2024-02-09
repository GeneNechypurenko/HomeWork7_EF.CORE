using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork7
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; internal set; }
        public ICollection<BookAuthor> BooksAuthors { get; set; }
        public override string ToString()
        {
            return $"BookId: {BookId}, Title: {Title}, Price: {Price:C}";
        }
    }
}
