using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BookStore2.Models.Repositories
{
    public class BookRepository : IBookstoreRepository<Book>
    {
        List<Book> books;

        public BookRepository()
        {
            books = new List<Book>()
            {
                new Book
                {
                    id=1,Title="C# PROGRAMMING ",Description="No Description",ImageUrl="1.jpg",Author=new Author()
                },
                new Book
                {
                    id=2,Title="JAVA PROGRAMMING ",Description="Nothing",ImageUrl="2.jpg",Author=new Author()
                },
                new Book
                {
                    id=3,Title="Python PROGRAMMING ",Description="No Data",ImageUrl = "103.jpg",Author=new Author()
                },
            };
        }
        public void Add(Book entity)
        {
            entity.id=books.Max(x => x.id)+1; 
            books.Add(entity);
        }

        public void Delete(int id)
        {
            var book = Find(id);
             books.Remove(book);
        }

        public Book Find(int id)
        {
            var book = books.SingleOrDefault(b => b.id == id);
            return book;
        }

        public IList<Book> List()
        {
            return books;
        }

        public List<Book> Search(string term)
        {
            return books.Where(a => a.Title.Contains(term)).ToList();

        }

        public void Update(int id,Book newBook)
        {
            var book = Find(id);
            book.Title = newBook.Title;
            book.Description = newBook.Description;
            book.Author = newBook.Author;
            book.ImageUrl = newBook.ImageUrl;
        }
    }
}
