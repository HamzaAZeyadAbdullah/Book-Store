using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BookStore2.Models.Repositories
{
    public class AuterRepository : IBookstoreRepository<Author>
    {
        IList<Author> authors;
        public AuterRepository()
        {
            authors = new List<Author>()
            {
                new Author {id=1 ,fullname="Khaled" },
                new Author {id=2 ,fullname="Hamid" },
                new Author {id=3 ,fullname="Said" },

            };
        }

        public void Add(Author entity)
        {
            authors.Add(entity);
        }

        public void Delete(int id)
        {
            var author = Find(id);
            authors.Remove(author);
        }

        public Author Find(int id)
        {
            var author = authors.SingleOrDefault(a => a.id == id);
            return author;
        }

        public IList<Author> List()
        {
            return authors;
        }

        public List<Author> Search(string term)
        {
            return authors.Where(a => a.fullname.Contains(term)).ToList();
        }

        public void Update(int id, Author newAuthor)
        {
            var author = Find(id);
            author.fullname = newAuthor.fullname;
        }
    }
}
