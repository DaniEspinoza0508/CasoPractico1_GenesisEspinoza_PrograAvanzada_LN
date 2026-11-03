
using PAP.Business;
using PAP.DataAccess;
using System.Web.Mvc;

namespace PAP.Mvc.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly CategoryManager manager = new CategoryManager();

        public ActionResult Index()
        {
            return View(manager.GetAllCategories());
        }

        public ActionResult Details(int id)
        {
            Category category = manager.GetCategoryById(id);

            if (category == null)
                return HttpNotFound();

            return View(category);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                manager.AddCategory(category);
                return RedirectToAction("Index");
            }

            return View(category);
        }

        public ActionResult Edit(int id)
        {
            Category category = manager.GetCategoryById(id);

            if (category == null)
                return HttpNotFound();

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                manager.UpdateCategory(category);
                return RedirectToAction("Index");
            }

            return View(category);
        }

        public ActionResult Delete(int id)
        {
            Category category = manager.GetCategoryById(id);

            if (category == null)
                return HttpNotFound();

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            manager.DeleteCategory(id);
            return RedirectToAction("Index");
        }
    }
}
