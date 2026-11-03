using PAP.Business;
using PAP.DataAccess;
using System;
using System.Linq;
using System.Web.Mvc;

namespace PAP.Mvc.Controllers
{
    public class PurchaseOrderItemsController : Controller
    {
        private readonly PurchaseOrderItemManager manager = new PurchaseOrderItemManager();
        public ActionResult Index() => View(manager.GetAllPurchaseOrderItems());
        public ActionResult Details(int id)
        {
            var item = manager.GetPurchaseOrderItemById(id);
            return item == null ? (ActionResult)HttpNotFound() : View(item);
        }
        public ActionResult Create()
        {
            LoadLists();
            return View(new PurchaseOrderItem());
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PurchaseOrderId,BookId,Quantity,UnitCost")] PurchaseOrderItem item)
        {
            if (PurchaseOrderIdInvalid(item.PurchaseOrderId)) ModelState.AddModelError("PurchaseOrderId", "Seleccione un registro válido.");
            if (BookIdInvalid(item.BookId)) ModelState.AddModelError("BookId", "Seleccione un registro válido.");
            if (ModelState.IsValid)
            {

                manager.AddPurchaseOrderItem(item);
                return RedirectToAction("Index");
            }
            LoadLists(item);
            return View(item);
        }
        public ActionResult Edit(int id)
        {
            var item = manager.GetPurchaseOrderItemById(id);
            if (item == null) return HttpNotFound();
            LoadLists(item);
            return View(item);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PurchaseOrderId,BookId,Quantity,UnitCost")] PurchaseOrderItem item)
        {
            if (PurchaseOrderIdInvalid(item.PurchaseOrderId)) ModelState.AddModelError("PurchaseOrderId", "Seleccione un registro válido.");
            if (BookIdInvalid(item.BookId)) ModelState.AddModelError("BookId", "Seleccione un registro válido.");
            if (ModelState.IsValid)
            {
                var existing = manager.GetPurchaseOrderItemById(item.Id);
                if (existing == null) return HttpNotFound();
                existing.PurchaseOrderId = item.PurchaseOrderId;
                existing.BookId = item.BookId;
                existing.Quantity = item.Quantity;
                existing.UnitCost = item.UnitCost;

                manager.UpdatePurchaseOrderItem(existing);
                return RedirectToAction("Index");
            }
            LoadLists(item);
            return View(item);
        }
        public ActionResult Delete(int id)
        {
            var item = manager.GetPurchaseOrderItemById(id);
            return item == null ? (ActionResult)HttpNotFound() : View(item);
        }
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (manager.GetPurchaseOrderItemById(id) == null) return HttpNotFound();
            manager.DeletePurchaseOrderItem(id);
            return RedirectToAction("Index");
        }
        private void LoadLists(PurchaseOrderItem selected = null)
        {
            ViewBag.PurchaseOrderId = new SelectList(new PurchaseOrderManager().GetAllPurchaseOrders(), "Id", "Id", selected?.PurchaseOrderId);
            ViewBag.BookId = new SelectList(new BookManager().GetAllBooks(), "Id", "Title", selected?.BookId);
        }
        private bool PurchaseOrderIdInvalid(int id) => !new PurchaseOrderManager().GetAllPurchaseOrders().Any(x => x.Id == id);
        private bool BookIdInvalid(int id) => !new BookManager().GetAllBooks().Any(x => x.Id == id);
    }
}
