using PAP.Business;
using PAP.DataAccess;
using System;
using System.Linq;
using System.Web.Mvc;

namespace PAP.Mvc.Controllers
{
    public class PurchaseOrdersController : Controller
    {
        private readonly PurchaseOrderManager manager = new PurchaseOrderManager();
        public ActionResult Index() => View(manager.GetAllPurchaseOrders());
        public ActionResult Details(int id)
        {
            var item = manager.GetPurchaseOrderById(id);
            return item == null ? (ActionResult)HttpNotFound() : View(item);
        }
        public ActionResult Create()
        {
            LoadLists();
            return View(new PurchaseOrder());
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SupplierId,OrderDate,ExpectedDate,Status,TotalCost")] PurchaseOrder item)
        {
            if (SupplierIdInvalid(item.SupplierId)) ModelState.AddModelError("SupplierId", "Seleccione un registro válido.");
            if (ModelState.IsValid)
            {
            item.OrderDate = DateTime.Now;
                manager.AddPurchaseOrder(item);
                return RedirectToAction("Index");
            }
            LoadLists(item);
            return View(item);
        }
        public ActionResult Edit(int id)
        {
            var item = manager.GetPurchaseOrderById(id);
            if (item == null) return HttpNotFound();
            LoadLists(item);
            return View(item);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SupplierId,OrderDate,ExpectedDate,Status,TotalCost")] PurchaseOrder item)
        {
            if (SupplierIdInvalid(item.SupplierId)) ModelState.AddModelError("SupplierId", "Seleccione un registro válido.");
            if (ModelState.IsValid)
            {
                var existing = manager.GetPurchaseOrderById(item.Id);
                if (existing == null) return HttpNotFound();
                existing.SupplierId = item.SupplierId;
                existing.ExpectedDate = item.ExpectedDate;
                existing.Status = item.Status;
                existing.TotalCost = item.TotalCost;

                manager.UpdatePurchaseOrder(existing);
                return RedirectToAction("Index");
            }
            LoadLists(item);
            return View(item);
        }
        public ActionResult Delete(int id)
        {
            var item = manager.GetPurchaseOrderById(id);
            return item == null ? (ActionResult)HttpNotFound() : View(item);
        }
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (manager.GetPurchaseOrderById(id) == null) return HttpNotFound();
            manager.DeletePurchaseOrder(id);
            return RedirectToAction("Index");
        }
        private void LoadLists(PurchaseOrder selected = null)
        {
            ViewBag.SupplierId = new SelectList(new SupplierManager().GetAllSuppliers(), "Id", "Name", selected?.SupplierId);
        }
        private bool SupplierIdInvalid(int id) => !new SupplierManager().GetAllSuppliers().Any(x => x.Id == id);
    }
}
