
using PAP.DataAccess;
using PAP.Repositories;
using System.Linq;

namespace PAP.Generators
{
    public class SuppliersGenerator
    {
        private readonly SupplierRepository repository;

        public SuppliersGenerator()
        {
            repository = new SupplierRepository();
        }

        public string Generate()
        {
            if (repository.GetAll().Any())
            {
                return "Error: Ya existen proveedores registrados.";
            }

            for (int i = 1; i <= 10; i++)
            {
                Supplier supplier = new Supplier();

                supplier.Name = "Proveedor " + i;
                supplier.ContactName = "Contacto " + i;
                supplier.Email = "proveedor" + i + "@correo.com";
                supplier.Phone = "888800" + i.ToString("00");
                supplier.Address = "San José, Costa Rica";
                supplier.IsActive = true;

                repository.Add(supplier);
            }

            return "Se generaron 10 proveedores correctamente.";
        }

        public bool CanGenerate()
        {
            return !repository.GetAll().Any();
        }
    }
}
