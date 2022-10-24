using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookStore2.Models;
using BookStore2.Models.Repositories;

namespace BookStore2.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IBookstoreRepository<Author> authorRepository;

        // GET: eController1
        public AuthorController(IBookstoreRepository<Author> authorRepository)
        {
            this.authorRepository = authorRepository;
        }
        public ActionResult Index()
        {
            var authors = authorRepository.List();

            return View(authors);
        }

        // GET: eController1/Details/5
        public ActionResult Details(int id)
        {
            var authors = authorRepository.Find(id);
            return View(authors);
        }

        // GET: eController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: eController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Author author)
        {
            try
            {
                authorRepository.Add(author);   
                
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: eController1/Edit/5
        public ActionResult Edit(int id)
        {
            var authors =authorRepository.Find(id);
            return View(authors);
        }

        // POST: eController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Author author)
        {
            try
            {
                authorRepository.Update(id, author);    
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: eController1/Delete/5
        public ActionResult Delete(int id)
        {
            var author = authorRepository.Find(id);     
            return View(author);
        }

        // POST: eController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Author author)
        {
            try
            {
                authorRepository.Delete(id);    
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
