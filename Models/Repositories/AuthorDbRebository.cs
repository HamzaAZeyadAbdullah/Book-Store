﻿using System.Collections.Generic;
using System.Linq;

namespace BookStore2.Models.Repositories
{
    public class AuthorDbRebository : IBookstoreRepository<Author>
    {
        BookStoreDbContext db;
        public AuthorDbRebository(BookStoreDbContext _db)
        {
            db=_db; 
        }

        public void Add(Author entity)
        {
            db.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var author = Find(id);
            db.Authors.Remove(author);
            db.SaveChanges();
        }

        public Author Find(int id)
        {
            var author = db.Authors.SingleOrDefault(a => a.id == id);
            return author;
        }

        public IList<Author> List()
        {
            return db.Authors.ToList();
        }

        public List<Author> Search(string term)
        {
            return db.Authors.Where(a => a.fullname.Contains(term)).ToList();

        }

        public void Update(int id, Author newAuthor)
        {
            db.Update(newAuthor);
            db.SaveChanges();   
        }
    }
}

