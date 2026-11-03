
using PAP.Business;
using PAP.DataAccess;
using System.Web.Mvc;

namespace PAP.Mvc.Controllers
{
    public class RolesController : Controller
    {
        private readonly RoleManager manager = new RoleManager();

        public ActionResult Index()
        {
            return View(manager.GetAllRoles());
        }

        public ActionResult Details(int id)
        {
            Role role = manager.GetRoleById(id);

            if (role == null)
                return HttpNotFound();

            return View(role);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Role role)
        {
            if (ModelState.IsValid)
            {
                manager.AddRole(role);
                return RedirectToAction("Index");
            }

            return View(role);
        }

        public ActionResult Edit(int id)
        {
            Role role = manager.GetRoleById(id);

            if (role == null)
                return HttpNotFound();

            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Role role)
        {
            if (ModelState.IsValid)
            {
                manager.UpdateRole(role);
                return RedirectToAction("Index");
            }

            return View(role);
        }

        public ActionResult Delete(int id)
        {
            Role role = manager.GetRoleById(id);

            if (role == null)
                return HttpNotFound();

            return View(role);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            manager.DeleteRole(id);
            return RedirectToAction("Index");
        }
    }
}
