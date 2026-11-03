using PAP.Business;
using PAP.DataAccess;
using System;
using System.Linq;
using System.Web.Mvc;

namespace PAP.Mvc.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly PaymentManager manager = new PaymentManager();
        public ActionResult Index() => View(manager.GetAllPayments());
        public ActionResult Details(int id)
        {
            var item = manager.GetPaymentById(id);
            return item == null ? (ActionResult)HttpNotFound() : View(item);
        }
        public ActionResult Create()
        {
            LoadLists();
            return View(new Payment());
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderId,PaymentMethod,Amount,PaymentDate,Status,TransactionRef")] Payment item)
        {
            if (OrderIdInvalid(item.OrderId)) ModelState.AddModelError("OrderId", "Seleccione un registro válido.");
            if (ModelState.IsValid)
            {
            item.PaymentDate = DateTime.Now;
                manager.AddPayment(item);
                return RedirectToAction("Index");
            }
            LoadLists(item);
            return View(item);
        }
        public ActionResult Edit(int id)
        {
            var item = manager.GetPaymentById(id);
            if (item == null) return HttpNotFound();
            LoadLists(item);
            return View(item);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OrderId,PaymentMethod,Amount,PaymentDate,Status,TransactionRef")] Payment item)
        {
            if (OrderIdInvalid(item.OrderId)) ModelState.AddModelError("OrderId", "Seleccione un registro válido.");
            if (ModelState.IsValid)
            {
                var existing = manager.GetPaymentById(item.Id);
                if (existing == null) return HttpNotFound();
                existing.OrderId = item.OrderId;
                existing.PaymentMethod = item.PaymentMethod;
                existing.Amount = item.Amount;
                existing.Status = item.Status;
                existing.TransactionRef = item.TransactionRef;

                manager.UpdatePayment(existing);
                return RedirectToAction("Index");
            }
            LoadLists(item);
            return View(item);
        }
        public ActionResult Delete(int id)
        {
            var item = manager.GetPaymentById(id);
            return item == null ? (ActionResult)HttpNotFound() : View(item);
        }
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (manager.GetPaymentById(id) == null) return HttpNotFound();
            manager.DeletePayment(id);
            return RedirectToAction("Index");
        }
        private void LoadLists(Payment selected = null)
        {
            ViewBag.OrderId = new SelectList(new OrderManager().GetAllOrders(), "Id", "Id", selected?.OrderId);
        }
        private bool OrderIdInvalid(int id) => !new OrderManager().GetAllOrders().Any(x => x.Id == id);
    }
}
