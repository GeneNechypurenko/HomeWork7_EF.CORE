using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HomeWork7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var context = new BookStoreContext())
            {
                if (!context.Database.CanConnect())
                {
                    context.Database.EnsureCreated();

                    var authors = new List<Author>
                    {
                        new Author { Name = "Author 1" },
                        new Author { Name = "Author 2" },
                        new Author { Name = "Author 3" }
                    };
                    context.Authors.AddRange(authors);

                    var books = new List<Book>
                    {
                        new Book { Title = "Book 1", Price = 10.0m },
                        new Book { Title = "Book 2", Price = 15.0m },
                        new Book { Title = "Book 3", Price = 20.0m },
                        new Book { Title = "Book 4", Price = 25.0m },
                        new Book { Title = "Book 5", Price = 30.0m }
                    };
                    context.Books.AddRange(books);

                    context.SaveChanges();

                    context.Database.ExecuteSqlRaw($@"
                        CREATE PROCEDURE UpdateBookPricesForAuthor
                        @AuthorId int
                        AS
                        BEGIN
                            UPDATE Books
                            SET Price = Price * 1.1
                            WHERE BookId IN (SELECT BookId FROM BookAuthor WHERE AuthorId = @AuthorId);
                        END
                    ");

                    if (context.Authors.Any() && context.Books.Any())
                    {
                        var bookAuthors = new List<BookAuthor>
                    {
                        new BookAuthor { BookId = 1, AuthorId = 1 },
                        new BookAuthor { BookId = 2, AuthorId = 3 },
                        new BookAuthor { BookId = 3, AuthorId = 1 },
                        new BookAuthor { BookId = 4, AuthorId = 2 },
                        new BookAuthor { BookId = 5, AuthorId = 1 }
                    };
                        context.AddRange(bookAuthors);
                        context.SaveChanges();
                    }
                }
                else
                {
                    var books = context.Books;

                    foreach(var book in books)
                    {
                        Console.WriteLine(book);
                    }

                    Console.WriteLine();
                }
            }

            using (var context = new BookStoreContext())
            {
                context.UpdateBookPricesForAuthor(1);

                var books = context.Books;

                foreach (var book in books)
                {
                    Console.WriteLine(book);
                }
            }
        }
    }
}
