
using System;
using System.Linq;
using System.Web.Mvc;
using PAP.DataAccess;

namespace PAP.Mvc.Controllers
{
    public class BookAuthorsController : Controller
    {
        private readonly BookStoreDBEntities db =
            new BookStoreDBEntities();

        public ActionResult Index()
        {
            var authors = db.Authors
                .Include("Books")
                .ToList();

            return View(authors);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.BookId = new SelectList(
                db.Books.ToList(), "Id", "Id");

            ViewBag.AuthorId = new SelectList(
                db.Authors.ToList(), "Id", "FirstName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int bookId, int authorId)
        {
            var book = db.Books.Find(bookId);
            var author = db.Authors.Find(authorId);

            if (book == null || author == null)
            {
                ModelState.AddModelError("",
                    "Debes seleccionar un libro y un autor.");
            }
            else
            {
                if (!author.Books.Any(b => b.Id == bookId))
                {
                    author.Books.Add(book);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            ViewBag.BookId = new SelectList(
                db.Books.ToList(), "Id", "Id", bookId);

            ViewBag.AuthorId = new SelectList(
                db.Authors.ToList(), "Id", "FirstName", authorId);

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
