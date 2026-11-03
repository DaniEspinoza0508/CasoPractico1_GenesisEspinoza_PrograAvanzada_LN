
using PAP.Business;
using PAP.DataAccess;
using System.Linq;
using System.Web.Mvc;

namespace PAP.Mvc.Controllers
{
    public class AddressesController : Controller
    {
        private readonly AddressManager manager = new AddressManager();
        private readonly CustomerManager customerManager = new CustomerManager();

        public ActionResult Index()
        {
            return View(manager.GetAllAddresses());
        }

        public ActionResult Details(int id)
        {
            Address address = manager.GetAddressById(id);

            if (address == null)
                return HttpNotFound();

            return View(address);
        }

        public ActionResult Create()
        {
            LoadCustomers();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "CustomerId,AddressType,Street,City,State,PostalCode,Country,IsDefault")]
            Address address)
        {
            if (!customerManager.GetAllCustomers()
                .Any(c => c.Id == address.CustomerId))
            {
                ModelState.AddModelError("CustomerId",
                    "Debe seleccionar un cliente válido.");
            }

            if (ModelState.IsValid)
            {
                manager.AddAddress(address);
                return RedirectToAction("Index");
            }

            LoadCustomers(address.CustomerId);
            return View(address);
        }

        public ActionResult Edit(int id)
        {
            Address address = manager.GetAddressById(id);

            if (address == null)
                return HttpNotFound();

            LoadCustomers(address.CustomerId);
            return View(address);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "Id,CustomerId,AddressType,Street,City,State,PostalCode,Country,IsDefault")]
            Address address)
        {
            if (!customerManager.GetAllCustomers()
                .Any(c => c.Id == address.CustomerId))
            {
                ModelState.AddModelError("CustomerId",
                    "Debe seleccionar un cliente válido.");
            }

            if (ModelState.IsValid)
            {
                manager.UpdateAddress(address);
                return RedirectToAction("Index");
            }

            LoadCustomers(address.CustomerId);
            return View(address);
        }

        public ActionResult Delete(int id)
        {
            Address address = manager.GetAddressById(id);

            if (address == null)
                return HttpNotFound();

            return View(address);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Address address = manager.GetAddressById(id);

            if (address == null)
                return HttpNotFound();

            manager.DeleteAddress(id);
            return RedirectToAction("Index");
        }

        private void LoadCustomers(int? selectedId = null)
        {
            var customers = customerManager.GetAllCustomers()
                .Select(c => new
                {
                    c.Id,
                    Name = c.FirstName + " " + c.LastName
                })
                .ToList();

            ViewBag.CustomerId = new SelectList(
                customers, "Id", "Name", selectedId);
        }
    }
}
