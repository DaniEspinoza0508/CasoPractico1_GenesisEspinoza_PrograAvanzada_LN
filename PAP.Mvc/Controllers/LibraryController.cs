
using PAP.Business;
using PAP.DataAccess;
using PAP.Mvc.ModelBinders;
using System;
using System.Linq;
using System.Web.Mvc;

namespace PAP.Mvc.Controllers
{
    [RoutePrefix("biblioteca")]
    public class LibraryController : Controller
    {
        private readonly BookManager _bookManager;

        public LibraryController()
        {
            _bookManager = new BookManager();
        }

        // Mostrar todos los libros del catálogo
        public ActionResult Index()
        {
            var books = _bookManager.GetAllBooks();
            return View(books);
        }

        // Mostrar libros en tabla o tarjetas
        public ActionResult Catalog(string vista)
        {
            var books = _bookManager.GetAllBooks();

            if (string.Equals(vista, "cards",
                StringComparison.OrdinalIgnoreCase))
            {
                return View("Cards", books);
            }

            return View("Index", books);
        }

        // Mostrar detalles de un libro
        [HttpGet]
        [Route("detalle/{id:int}")]
        public ActionResult Details(int id)
        {
            var book = _bookManager.GetBookById(id);

            if (book == null)
            {
                return HttpNotFound();
            }

            return View(book);
        }

        // Mostrar formulario para crear un libro
        [HttpGet]
        [Route("crear")]
        public ActionResult Create()
        {
            return View(new Book
            {
                PublicationDate = DateTime.Today,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsInStock = true
            });
        }

        // Guardar un libro nuevo en SQL Server
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("crear")]
        public ActionResult Create(Book book)
        {
            if (string.IsNullOrWhiteSpace(book.Name))
            {
                book.Name = book.Title;
            }

            if (string.IsNullOrWhiteSpace(book.Title))
            {
                ModelState.AddModelError(
                    "Title",
                    "El título es obligatorio.");
            }

            if (book.Price < 0)
            {
                ModelState.AddModelError(
                    "Price",
                    "El precio no puede ser negativo.");
            }

            if (!ModelState.IsValid)
            {
                return View(book);
            }

            book.CreatedAt = DateTime.Now;
            book.UpdatedAt = DateTime.Now;

            _bookManager.AddBook(book);

            TempData["Mensaje"] =
                "El libro se guardó correctamente.";

            return RedirectToAction("Index");
        }

        // Mostrar formulario para editar un libro
        [HttpGet]
        [Route("editar/{id:int}")]
        public ActionResult Edit(int id)
        {
            var book = _bookManager.GetBookById(id);

            if (book == null)
            {
                return HttpNotFound();
            }

            return View(book);
        }

        // Guardar los cambios del libro
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("editar/{id:int}")]
        public ActionResult Edit(int id, Book book)
        {
            if (id != book.Id)
            {
                return new HttpStatusCodeResult(400);
            }

            var existingBook = _bookManager.GetBookById(id);

            if (existingBook == null)
            {
                return HttpNotFound();
            }

            if (string.IsNullOrWhiteSpace(book.Title))
            {
                ModelState.AddModelError(
                    "Title",
                    "El título es obligatorio.");
            }

            if (book.Price < 0)
            {
                ModelState.AddModelError(
                    "Price",
                    "El precio no puede ser negativo.");
            }

            if (!ModelState.IsValid)
            {
                return View(book);
            }

            // Actualizar los datos del formulario
            existingBook.Title = book.Title;
            existingBook.Name = book.Title;
            existingBook.Author = book.Author;
            existingBook.Genre = book.Genre;
            existingBook.Category = book.Category;
            existingBook.Publisher = book.Publisher;
            existingBook.Price = book.Price;
            existingBook.Description = book.Description;
            existingBook.IsBestSeller = book.IsBestSeller;
            existingBook.IsInStock = book.IsInStock;
            existingBook.UpdatedAt = DateTime.Now;

            // Guardar cambios en SQL Server
            _bookManager.UpdateBook(existingBook);

            TempData["Mensaje"] =
                "El libro se actualizó correctamente.";

            return RedirectToAction("Index");
        }

        // Mostrar confirmación para eliminar un libro
        [HttpGet]
        [Route("eliminar/{id:int}")]
        public ActionResult Delete(int id)
        {
            var book = _bookManager.GetBookById(id);

            if (book == null)
            {
                return HttpNotFound();
            }

            return View(book);
        }

        // Confirmar eliminación del libro
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("eliminar/{id:int}")]
        public ActionResult DeleteConfirmed(int id)
        {
            var book = _bookManager.GetBookById(id);

            if (book == null)
            {
                return HttpNotFound();
            }

            _bookManager.DeleteBook(id);

            TempData["Mensaje"] =
                "El libro se eliminó correctamente.";

            return RedirectToAction("Index");
        }

        // Ejemplo del profesor: Model Binder personalizado
        [HttpGet]
        [Route("precio")]
        public ActionResult PrecioDemo()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("precio")]
        public ActionResult PrecioDemo(
            [ModelBinder(typeof(CurrencyModelBinder))] decimal precio)
        {
            ViewBag.PrecioConvertido = precio;

            ModelState.Values.ToList().ForEach(
                x => ViewBag.Errors +=
                    string.Join(",", x.Errors));

            return View();
        }
    }
}

