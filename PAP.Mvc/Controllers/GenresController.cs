
using PAP.Business;
using PAP.DataAccess;
using System.Web.Mvc;

namespace PAP.Mvc.Controllers
{
    public class GenresController : Controller
    {
        private readonly GenreManager manager = new GenreManager();

        public ActionResult Index()
        {
            return View(manager.GetAllGenres());
        }

        public ActionResult Details(int id)
        {
            Genre genre = manager.GetGenreById(id);

            if (genre == null)
                return HttpNotFound();

            return View(genre);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Genre genre)
        {
            if (ModelState.IsValid)
            {
                manager.AddGenre(genre);
                return RedirectToAction("Index");
            }

            return View(genre);
        }

        public ActionResult Edit(int id)
        {
            Genre genre = manager.GetGenreById(id);

            if (genre == null)
                return HttpNotFound();

            return View(genre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Genre genre)
        {
            if (ModelState.IsValid)
            {
                manager.UpdateGenre(genre);
                return RedirectToAction("Index");
            }

            return View(genre);
        }

        public ActionResult Delete(int id)
        {
            Genre genre = manager.GetGenreById(id);

            if (genre == null)
                return HttpNotFound();

            return View(genre);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            manager.DeleteGenre(id);
            return RedirectToAction("Index");
        }
    }
}
