
using PAP.Business;
using PAP.DataAccess;
using System;
using System.Web.Helpers;
using System.Web.Mvc;

namespace PAP.Mvc.Controllers
{
    public class CustomersController : Controller
    {
        private readonly CustomerManager manager = new CustomerManager();

        public ActionResult Index()
        {
            return View(manager.GetAllCustomers());
        }

        public ActionResult Details(int id)
        {
            Customer customer = manager.GetCustomerById(id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "FirstName,LastName,Email,Phone,BirthDate,IsActive")]
            Customer customer,
            string Password)
        {
            if (string.IsNullOrWhiteSpace(Password))
            {
                ModelState.AddModelError("Password",
                    "Debe ingresar una contraseña.");
            }

            if (ModelState.IsValid)
            {
                customer.PasswordHash = Crypto.HashPassword(Password);

                customer.CreatedAt = DateTime.Now;
                customer.UpdatedAt = DateTime.Now;

                manager.AddCustomer(customer);

                return RedirectToAction("Index");
            }

            return View(customer);
        }

        public ActionResult Edit(int id)
        {
            Customer customer = manager.GetCustomerById(id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "Id,FirstName,LastName,Email,Phone,BirthDate,IsActive")]
            Customer customer)
        {
            if (ModelState.IsValid)
            {
                Customer original = manager.GetCustomerById(customer.Id);

                if (original == null)
                    return HttpNotFound();

                original.FirstName = customer.FirstName;
                original.LastName = customer.LastName;
                original.Email = customer.Email;
                original.Phone = customer.Phone;
                original.BirthDate = customer.BirthDate;
                original.IsActive = customer.IsActive;
                original.UpdatedAt = DateTime.Now;

                manager.UpdateCustomer(original);

                return RedirectToAction("Index");
            }

            return View(customer);
        }

        public ActionResult Delete(int id)
        {
            Customer customer = manager.GetCustomerById(id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = manager.GetCustomerById(id);

            if (customer == null)
                return HttpNotFound();

            manager.DeleteCustomer(id);

            return RedirectToAction("Index");
        }
    }
}
