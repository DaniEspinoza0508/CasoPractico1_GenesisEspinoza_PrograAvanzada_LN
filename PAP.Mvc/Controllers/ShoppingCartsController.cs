using PAP.Business;
using PAP.DataAccess;
using System;
using System.Linq;
using System.Web.Mvc;

namespace PAP.Mvc.Controllers
{
    public class ShoppingCartsController : Controller
    {
        private readonly ShoppingCartManager manager = new ShoppingCartManager();
        public ActionResult Index() => View(manager.GetAllShoppingCarts());
        public ActionResult Details(int id)
        {
            var item = manager.GetShoppingCartById(id);
            return item == null ? (ActionResult)HttpNotFound() : View(item);
        }
        public ActionResult Create()
        {
            LoadLists();
            return View(new ShoppingCart());
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerId,CreatedAt,UpdatedAt")] ShoppingCart item)
        {
            if (CustomerIdInvalid(item.CustomerId)) ModelState.AddModelError("CustomerId", "Seleccione un registro válido.");
            if (ModelState.IsValid)
            {
            item.CreatedAt = DateTime.Now;
            item.UpdatedAt = DateTime.Now;
                manager.AddShoppingCart(item);
                return RedirectToAction("Index");
            }
            LoadLists(item);
            return View(item);
        }
        public ActionResult Edit(int id)
        {
            var item = manager.GetShoppingCartById(id);
            if (item == null) return HttpNotFound();
            LoadLists(item);
            return View(item);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CustomerId,CreatedAt,UpdatedAt")] ShoppingCart item)
        {
            if (CustomerIdInvalid(item.CustomerId)) ModelState.AddModelError("CustomerId", "Seleccione un registro válido.");
            if (ModelState.IsValid)
            {
                var existing = manager.GetShoppingCartById(item.Id);
                if (existing == null) return HttpNotFound();
                existing.CustomerId = item.CustomerId;
                existing.UpdatedAt = item.UpdatedAt;
                existing.UpdatedAt = DateTime.Now;
                manager.UpdateShoppingCart(existing);
                return RedirectToAction("Index");
            }
            LoadLists(item);
            return View(item);
        }
        public ActionResult Delete(int id)
        {
            var item = manager.GetShoppingCartById(id);
            return item == null ? (ActionResult)HttpNotFound() : View(item);
        }
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (manager.GetShoppingCartById(id) == null) return HttpNotFound();
            manager.DeleteShoppingCart(id);
            return RedirectToAction("Index");
        }
        private void LoadLists(ShoppingCart selected = null)
        {
            ViewBag.CustomerId = new SelectList(new CustomerManager().GetAllCustomers(), "Id", "Email", selected?.CustomerId);
        }
        private bool CustomerIdInvalid(int id) => !new CustomerManager().GetAllCustomers().Any(x => x.Id == id);
    }
}
