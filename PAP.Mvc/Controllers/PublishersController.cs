
using PAP.Business;
using PAP.DataAccess;
using System.Web.Mvc;

namespace PAP.Mvc.Controllers
{
    public class PublishersController : Controller
    {
        private readonly PublisherManager manager = new PublisherManager();

        public ActionResult Index()
        {
            return View(manager.GetAllPublishers());
        }

        public ActionResult Details(int id)
        {
            Publisher publisher = manager.GetPublisherById(id);

            if (publisher == null)
                return HttpNotFound();

            return View(publisher);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                manager.AddPublisher(publisher);
                return RedirectToAction("Index");
            }

            return View(publisher);
        }

        public ActionResult Edit(int id)
        {
            Publisher publisher = manager.GetPublisherById(id);

            if (publisher == null)
                return HttpNotFound();

            return View(publisher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                manager.UpdatePublisher(publisher);
                return RedirectToAction("Index");
            }

            return View(publisher);
        }

        public ActionResult Delete(int id)
        {
            Publisher publisher = manager.GetPublisherById(id);

            if (publisher == null)
                return HttpNotFound();

            return View(publisher);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            manager.DeletePublisher(id);
            return RedirectToAction("Index");
        }
    }
}
