using PAP.Business;
using PAP.DataAccess;
using System;
using System.Linq;
using System.Web.Mvc;

namespace PAP.Mvc.Controllers
{
    public class OrderItemsController : Controller
    {
        private readonly OrderItemManager manager = new OrderItemManager();
        public ActionResult Index() => View(manager.GetAllOrderItems());
        public ActionResult Details(int id)
        {
            var item = manager.GetOrderItemById(id);
            return item == null ? (ActionResult)HttpNotFound() : View(item);
        }
        public ActionResult Create()
        {
            LoadLists();
            return View(new OrderItem());
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderId,BookId,Quantity,UnitPrice,Discount")] OrderItem item)
        {
            if (OrderIdInvalid(item.OrderId)) ModelState.AddModelError("OrderId", "Seleccione un registro válido.");
            if (BookIdInvalid(item.BookId)) ModelState.AddModelError("BookId", "Seleccione un registro válido.");
            if (ModelState.IsValid)
            {

                manager.AddOrderItem(item);
                return RedirectToAction("Index");
            }
            LoadLists(item);
            return View(item);
        }
        public ActionResult Edit(int id)
        {
            var item = manager.GetOrderItemById(id);
            if (item == null) return HttpNotFound();
            LoadLists(item);
            return View(item);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OrderId,BookId,Quantity,UnitPrice,Discount")] OrderItem item)
        {
            if (OrderIdInvalid(item.OrderId)) ModelState.AddModelError("OrderId", "Seleccione un registro válido.");
            if (BookIdInvalid(item.BookId)) ModelState.AddModelError("BookId", "Seleccione un registro válido.");
            if (ModelState.IsValid)
            {
                var existing = manager.GetOrderItemById(item.Id);
                if (existing == null) return HttpNotFound();
                existing.OrderId = item.OrderId;
                existing.BookId = item.BookId;
                existing.Quantity = item.Quantity;
                existing.UnitPrice = item.UnitPrice;
                existing.Discount = item.Discount;

                manager.UpdateOrderItem(existing);
                return RedirectToAction("Index");
            }
            LoadLists(item);
            return View(item);
        }
        public ActionResult Delete(int id)
        {
            var item = manager.GetOrderItemById(id);
            return item == null ? (ActionResult)HttpNotFound() : View(item);
        }
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (manager.GetOrderItemById(id) == null) return HttpNotFound();
            manager.DeleteOrderItem(id);
            return RedirectToAction("Index");
        }
        private void LoadLists(OrderItem selected = null)
        {
            ViewBag.OrderId = new SelectList(new OrderManager().GetAllOrders(), "Id", "Id", selected?.OrderId);
            ViewBag.BookId = new SelectList(new BookManager().GetAllBooks(), "Id", "Title", selected?.BookId);
        }
        private bool OrderIdInvalid(int id) => !new OrderManager().GetAllOrders().Any(x => x.Id == id);
        private bool BookIdInvalid(int id) => !new BookManager().GetAllBooks().Any(x => x.Id == id);
    }
}
