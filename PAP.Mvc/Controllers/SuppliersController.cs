
using PAP.Business;
using PAP.DataAccess;
using System.Web.Mvc;

namespace PAP.Mvc.Controllers
{
    public class SuppliersController : Controller
    {
        private readonly SupplierManager manager = new SupplierManager();

        public ActionResult Index()
        {
            return View(manager.GetAllSuppliers());
        }

        public ActionResult Details(int id)
        {
            Supplier supplier = manager.GetSupplierById(id);

            if (supplier == null)
                return HttpNotFound();

            return View(supplier);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                manager.AddSupplier(supplier);
                return RedirectToAction("Index");
            }

            return View(supplier);
        }

        public ActionResult Edit(int id)
        {
            Supplier supplier = manager.GetSupplierById(id);

            if (supplier == null)
                return HttpNotFound();

            return View(supplier);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                manager.UpdateSupplier(supplier);
                return RedirectToAction("Index");
            }

            return View(supplier);
        }

        public ActionResult Delete(int id)
        {
            Supplier supplier = manager.GetSupplierById(id);

            if (supplier == null)
                return HttpNotFound();

            return View(supplier);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            manager.DeleteSupplier(id);
            return RedirectToAction("Index");
        }
    }
}
