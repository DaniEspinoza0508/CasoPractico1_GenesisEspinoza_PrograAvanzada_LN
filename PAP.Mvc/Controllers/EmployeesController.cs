
using PAP.Business;
using PAP.DataAccess;
using System;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;

namespace PAP.Mvc.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EmployeeManager manager = new EmployeeManager();
        private readonly RoleManager roleManager = new RoleManager();

        public ActionResult Index()
        {
            return View(manager.GetAllEmployees());
        }

        public ActionResult Details(int id)
        {
            Employee employee = manager.GetEmployeeById(id);

            if (employee == null)
                return HttpNotFound();

            return View(employee);
        }

        public ActionResult Create()
        {
            LoadRoles();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "RoleId,FirstName,LastName,Email,IsActive")]
            Employee employee,
            string Password)
        {
            if (string.IsNullOrWhiteSpace(Password))
            {
                ModelState.AddModelError("Password",
                    "Debe ingresar una contraseña.");
            }

            if (!roleManager.GetAllRoles()
                .Any(r => r.Id == employee.RoleId))
            {
                ModelState.AddModelError("RoleId",
                    "Debe seleccionar un rol válido.");
            }

            if (ModelState.IsValid)
            {
                employee.PasswordHash = Crypto.HashPassword(Password);
                employee.CreatedAt = DateTime.Now;

                manager.AddEmployee(employee);

                return RedirectToAction("Index");
            }

            LoadRoles(employee.RoleId);
            return View(employee);
        }

        public ActionResult Edit(int id)
        {
            Employee employee = manager.GetEmployeeById(id);

            if (employee == null)
                return HttpNotFound();

            LoadRoles(employee.RoleId);
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "Id,RoleId,FirstName,LastName,Email,IsActive")]
            Employee employee)
        {
            if (!roleManager.GetAllRoles()
                .Any(r => r.Id == employee.RoleId))
            {
                ModelState.AddModelError("RoleId",
                    "Debe seleccionar un rol válido.");
            }

            if (ModelState.IsValid)
            {
                Employee original = manager.GetEmployeeById(employee.Id);

                if (original == null)
                    return HttpNotFound();

                original.RoleId = employee.RoleId;
                original.FirstName = employee.FirstName;
                original.LastName = employee.LastName;
                original.Email = employee.Email;
                original.IsActive = employee.IsActive;

                manager.UpdateEmployee(original);

                return RedirectToAction("Index");
            }

            LoadRoles(employee.RoleId);
            return View(employee);
        }

        public ActionResult Delete(int id)
        {
            Employee employee = manager.GetEmployeeById(id);

            if (employee == null)
                return HttpNotFound();

            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = manager.GetEmployeeById(id);

            if (employee == null)
                return HttpNotFound();

            manager.DeleteEmployee(id);

            return RedirectToAction("Index");
        }

        private void LoadRoles(int? selectedId = null)
        {
            ViewBag.RoleId = new SelectList(
                roleManager.GetAllRoles(),
                "Id",
                "Name",
                selectedId);
        }
    }
}
