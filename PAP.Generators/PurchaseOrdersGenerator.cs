
using PAP.DataAccess;
using PAP.Repositories;
using System;
using System.Linq;

namespace PAP.Generators
{
    public class PurchaseOrdersGenerator
    {
        private readonly PurchaseOrderRepository repository;
        private readonly SupplierRepository supplierRepository;

        public PurchaseOrdersGenerator()
        {
            repository = new PurchaseOrderRepository();
            supplierRepository = new SupplierRepository();
        }

        public string Generate()
        {
            if (repository.GetAll().Any())
            {
                return "Error: Ya existen órdenes de compra registradas.";
            }

            var suppliers = supplierRepository.GetAll().ToList();

            if (!suppliers.Any())
            {
                return "Error: Primero deben existir proveedores registrados.";
            }

            for (int i = 1; i <= 10; i++)
            {
                Supplier supplier = suppliers[(i - 1) % suppliers.Count];

                PurchaseOrder purchaseOrder = new PurchaseOrder();

                purchaseOrder.SupplierId = supplier.Id;
                purchaseOrder.OrderDate = DateTime.Now.AddDays(-i);
                purchaseOrder.ExpectedDate = DateTime.Now.AddDays(i + 5);
                purchaseOrder.Status = "Pending";
                purchaseOrder.TotalCost = 15000m + (i * 2500m);

                repository.Add(purchaseOrder);
            }

            return "Se generaron 10 órdenes de compra correctamente.";
        }

        public bool CanGenerate()
        {
            return !repository.GetAll().Any()
                && supplierRepository.GetAll().Any();
        }
    }
}
