using PAP.Business;
using PAP.DataAccess;
using System;
using System.Linq;
using System.Web.Mvc;

namespace PAP.Mvc.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly ReviewManager manager = new ReviewManager();
        public ActionResult Index() => View(manager.GetAllReviews());
        public ActionResult Details(int id)
        {
            var item = manager.GetReviewById(id);
            return item == null ? (ActionResult)HttpNotFound() : View(item);
        }
        public ActionResult Create()
        {
            LoadLists();
            return View(new Review());
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookId,CustomerId,Comment,CreatedAt")] Review item)
        {
            if (BookIdInvalid(item.BookId)) ModelState.AddModelError("BookId", "Seleccione un registro válido.");
            if (CustomerIdInvalid(item.CustomerId)) ModelState.AddModelError("CustomerId", "Seleccione un registro válido.");
            if (ModelState.IsValid)
            {
            item.CreatedAt = DateTime.Now;
                manager.AddReview(item);
                return RedirectToAction("Index");
            }
            LoadLists(item);
            return View(item);
        }
        public ActionResult Edit(int id)
        {
            var item = manager.GetReviewById(id);
            if (item == null) return HttpNotFound();
            LoadLists(item);
            return View(item);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BookId,CustomerId,Comment,CreatedAt")] Review item)
        {
            if (BookIdInvalid(item.BookId)) ModelState.AddModelError("BookId", "Seleccione un registro válido.");
            if (CustomerIdInvalid(item.CustomerId)) ModelState.AddModelError("CustomerId", "Seleccione un registro válido.");
            if (ModelState.IsValid)
            {
                var existing = manager.GetReviewById(item.Id);
                if (existing == null) return HttpNotFound();
                existing.BookId = item.BookId;
                existing.CustomerId = item.CustomerId;
                existing.Comment = item.Comment;

                manager.UpdateReview(existing);
                return RedirectToAction("Index");
            }
            LoadLists(item);
            return View(item);
        }
        public ActionResult Delete(int id)
        {
            var item = manager.GetReviewById(id);
            return item == null ? (ActionResult)HttpNotFound() : View(item);
        }
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (manager.GetReviewById(id) == null) return HttpNotFound();
            manager.DeleteReview(id);
            return RedirectToAction("Index");
        }
        private void LoadLists(Review selected = null)
        {
            ViewBag.BookId = new SelectList(new BookManager().GetAllBooks(), "Id", "Title", selected?.BookId);
            ViewBag.CustomerId = new SelectList(new CustomerManager().GetAllCustomers(), "Id", "Email", selected?.CustomerId);
        }
        private bool BookIdInvalid(int id) => !new BookManager().GetAllBooks().Any(x => x.Id == id);
        private bool CustomerIdInvalid(int id) => !new CustomerManager().GetAllCustomers().Any(x => x.Id == id);
    }
}
