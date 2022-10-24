using BookStore2.Models;
using BookStore2.Models.Repositories;
using BookStore2.View_Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace BookStore2.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookstoreRepository<Book> bookrepository;
        [System.Obsolete]
        private readonly IHostingEnvironment hosting;

        private IBookstoreRepository<Author> authorrepository { get; }

        [System.Obsolete]
        public BookController(IBookstoreRepository<Book> bookrepository, IBookstoreRepository<Author> authorrepository, IHostingEnvironment hosting)
        {
            this.bookrepository = bookrepository;
            this.authorrepository = authorrepository;
            this.hosting = hosting;
        }
        // GET: BookController
        public ActionResult Index()
        {
            var books = bookrepository.List();
            return View(books);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            var books = bookrepository.Find(id);
            return View(books);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            var model = new BookAuthorViewModel
            {
                Authors = FillSelectList()
            };
            return View(model);
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [System.Obsolete]
        public ActionResult Create(BookAuthorViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string fileName = UploadFile(model.File) ?? string.Empty;


                    if (model.AuthorId == -1)
                    {
                        ViewBag.Massege = "Please Select Authors";

                        return View(GetAllAuthors());
                    }

                    var author = authorrepository.Find(model.AuthorId);
                    Book book = new Book()
                    {
                        id = model.BookId,
                        Title = model.Title,
                        Description = model.Discription,
                        Author = author,
                        ImageUrl = fileName,
                    };
                    bookrepository.Add(book);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }

            ModelState.AddModelError("", "you have to field required");
            return View(GetAllAuthors());


        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            var book = bookrepository.Find(id);
            var authorId = book.Author == null ? book.Author.id = 0 : book.Author.id;
            var viewmodel = new BookAuthorViewModel
            {
                BookId = book.id,
                Title = book.Title,
                Discription = book.Description,
                AuthorId = authorId,
                Authors = authorrepository.List().ToList(),
                imageurl = book.ImageUrl,
            };
            return View(viewmodel);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [System.Obsolete]
        public ActionResult Edit(BookAuthorViewModel viewmodel)
        {
            try
            {
                string fileName = UploadFile(viewmodel.File,viewmodel.imageurl);
                


                var author = authorrepository.Find(viewmodel.AuthorId);
                Book book = new Book()
                {
                    id = viewmodel.BookId,
                    Title = viewmodel.Title,
                    Description = viewmodel.Discription,
                    Author = author,
                    ImageUrl = fileName,
                };
                bookrepository.Update(viewmodel.BookId, book);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            var book = bookrepository.Find(id);
            return View(book);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                bookrepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        List<Author> FillSelectList()
        {
            var authors = authorrepository.List().ToList();
            authors.Insert(0, new Author { id = -1, fullname = "...please select an authors" });
            return authors;
        }
        BookAuthorViewModel GetAllAuthors()
        {
            var vmodel = new BookAuthorViewModel
            {
                Authors = FillSelectList()
            };
            return vmodel;
        }
        string UploadFile(IFormFile file)
        {
            if (file != null)
            {
                string uploads = Path.Combine(hosting.WebRootPath, "Uploads");
                string fullpath = Path.Combine(uploads, file.FileName);
                file.CopyTo(new FileStream(fullpath, FileMode.Create));

                return file.FileName;
            }
            return null;
        }


        string UploadFile(IFormFile file, string imgUrl)
        {
            if (file != null)
            {
                string uploads = Path.Combine(hosting.WebRootPath, "Uploads");
                string newpath = Path.Combine(uploads, file.FileName);


                string oldpath = Path.Combine(uploads, imgUrl);


                if (oldpath != newpath)
                {
                    System.IO.File.Delete(oldpath);

                    file.CopyTo(new FileStream(newpath, FileMode.Create));
                }
                return file.FileName;

            }
            return imgUrl;


        }
        public ActionResult Search(string term)
        {
            var resalt =bookrepository.Search(term);

            return View("Index",resalt);
        }
    }
}
