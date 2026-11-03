
using PAP.Business;
using PAP.DataAccess;
using System.Web.Mvc;

namespace PAP.Mvc.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly AuthorManager manager = new AuthorManager();

        public ActionResult Index()
        {
            return View(manager.GetAllAuthors());
        }

        public ActionResult Details(int id)
        {
            Author author = manager.GetAuthorById(id);
            if (author == null)
                return HttpNotFound();

            return View(author);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Author author)
        {
            if (ModelState.IsValid)
            {
                manager.AddAuthor(author);
                return RedirectToAction("Index");
            }

            return View(author);
        }

        public ActionResult Edit(int id)
        {
            Author author = manager.GetAuthorById(id);
            if (author == null)
                return HttpNotFound();

            return View(author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Author author)
        {
            if (ModelState.IsValid)
            {
                manager.UpdateAuthor(author);
                return RedirectToAction("Index");
            }

            return View(author);
        }

        public ActionResult Delete(int id)
        {
            Author author = manager.GetAuthorById(id);
            if (author == null)
                return HttpNotFound();

            return View(author);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            manager.DeleteAuthor(id);
            return RedirectToAction("Index");
        }
    }
}
