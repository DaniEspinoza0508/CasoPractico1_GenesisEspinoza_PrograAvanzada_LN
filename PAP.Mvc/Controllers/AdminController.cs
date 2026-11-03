
using PAP.Generators;
using System.Web.Mvc;

namespace PAP.Mvc.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GenerateAuthors()
        {
            AuthorsGenerator generator = new AuthorsGenerator();
            TempData["Message"] = generator.Generate();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GenerateCategories()
        {
            CategoriesGenerator generator = new CategoriesGenerator();
            TempData["Message"] = generator.Generate();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GenerateGenres()
        {
            GenresGenerator generator = new GenresGenerator();
            TempData["Message"] = generator.Generate();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GeneratePublishers()
        {
            PublishersGenerator generator = new PublishersGenerator();
            TempData["Message"] = generator.Generate();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GenerateSuppliers()
        {
            SuppliersGenerator generator = new SuppliersGenerator();
            TempData["Message"] = generator.Generate();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GenerateCustomers()
        {
            CustomersGenerator generator = new CustomersGenerator();
            TempData["Message"] = generator.Generate();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GenerateAddresses()
        {
            AddressesGenerator generator = new AddressesGenerator();
            TempData["Message"] = generator.Generate();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GenerateRoles()
        {
            RolesGenerator generator = new RolesGenerator();
            TempData["Message"] = generator.Generate();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GenerateEmployees()
        {
            EmployeesGenerator generator = new EmployeesGenerator();
            TempData["Message"] = generator.Generate();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GenerateBooks()
        {
            BooksGenerator generator = new BooksGenerator();
            TempData["Message"] = generator.Generate();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GenerateBookAuthors()
        {
            //BookAuthorsGenerator generator = new BookAuthorsGenerator();
            //TempData["Message"] = generator.Generate();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GenerateInventory()
        {
            InventoryGenerator generator = new InventoryGenerator();
            TempData["Message"] = generator.Generate();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GenerateOrders()
        {
            OrdersGenerator generator = new OrdersGenerator();
            TempData["Message"] = generator.Generate();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GenerateOrderItems()
        {
            OrderItemsGenerator generator = new OrderItemsGenerator();
            TempData["Message"] = generator.Generate();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GeneratePayments()
        {
            PaymentsGenerator generator = new PaymentsGenerator();
            TempData["Message"] = generator.Generate();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GenerateShoppingCarts()
        {
            ShoppingCartsGenerator generator = new ShoppingCartsGenerator();
            TempData["Message"] = generator.Generate();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GenerateCartItems()
        {
            CartItemsGenerator generator = new CartItemsGenerator();
            TempData["Message"] = generator.Generate();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GenerateReviews()
        {
            ReviewsGenerator generator = new ReviewsGenerator();
            TempData["Message"] = generator.Generate();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GeneratePurchaseOrders()
        {
            PurchaseOrdersGenerator generator = new PurchaseOrdersGenerator();
            TempData["Message"] = generator.Generate();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GeneratePurchaseOrderItems()
        {
            PurchaseOrderItemsGenerator generator =
                new PurchaseOrderItemsGenerator();

            TempData["Message"] = generator.Generate();
            return RedirectToAction("Index");
        }
    }
}
