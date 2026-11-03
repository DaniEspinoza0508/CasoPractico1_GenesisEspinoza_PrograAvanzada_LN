using PAP.Business;
using PAP.DataAccess;
using System;
using System.Linq;
using System.Web.Mvc;

namespace PAP.Mvc.Controllers
{
    public class CartItemsController : Controller
    {
        private readonly CartItemManager manager = new CartItemManager();
        public ActionResult Index() => View(manager.GetAllCartItems());
        public ActionResult Details(int id)
        {
            var item = manager.GetCartItemById(id);
            return item == null ? (ActionResult)HttpNotFound() : View(item);
        }
        public ActionResult Create()
        {
            LoadLists();
            return View(new CartItem());
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CartId,BookId,Quantity,AddedAt")] CartItem item)
        {
            if (CartIdInvalid(item.CartId)) ModelState.AddModelError("CartId", "Seleccione un registro válido.");
            if (BookIdInvalid(item.BookId)) ModelState.AddModelError("BookId", "Seleccione un registro válido.");
            if (ModelState.IsValid)
            {
            item.AddedAt = DateTime.Now;
                manager.AddCartItem(item);
                return RedirectToAction("Index");
            }
            LoadLists(item);
            return View(item);
        }
        public ActionResult Edit(int id)
        {
            var item = manager.GetCartItemById(id);
            if (item == null) return HttpNotFound();
            LoadLists(item);
            return View(item);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CartId,BookId,Quantity,AddedAt")] CartItem item)
        {
            if (CartIdInvalid(item.CartId)) ModelState.AddModelError("CartId", "Seleccione un registro válido.");
            if (BookIdInvalid(item.BookId)) ModelState.AddModelError("BookId", "Seleccione un registro válido.");
            if (ModelState.IsValid)
            {
                var existing = manager.GetCartItemById(item.Id);
                if (existing == null) return HttpNotFound();
                existing.CartId = item.CartId;
                existing.BookId = item.BookId;
                existing.Quantity = item.Quantity;

                manager.UpdateCartItem(existing);
                return RedirectToAction("Index");
            }
            LoadLists(item);
            return View(item);
        }
        public ActionResult Delete(int id)
        {
            var item = manager.GetCartItemById(id);
            return item == null ? (ActionResult)HttpNotFound() : View(item);
        }
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (manager.GetCartItemById(id) == null) return HttpNotFound();
            manager.DeleteCartItem(id);
            return RedirectToAction("Index");
        }
        private void LoadLists(CartItem selected = null)
        {
            ViewBag.CartId = new SelectList(new ShoppingCartManager().GetAllShoppingCarts(), "Id", "Id", selected?.CartId);
            ViewBag.BookId = new SelectList(new BookManager().GetAllBooks(), "Id", "Title", selected?.BookId);
        }
        private bool CartIdInvalid(int id) => !new ShoppingCartManager().GetAllShoppingCarts().Any(x => x.Id == id);
        private bool BookIdInvalid(int id) => !new BookManager().GetAllBooks().Any(x => x.Id == id);
    }
}
