
using PAP.DataAccess;
using PAP.Repositories;
using System.Collections.Generic;

namespace PAP.Business
{
    public class SupplierManager
    {
        private readonly SupplierRepository _repository;

        public SupplierManager()
        {
            _repository = new SupplierRepository();
        }

        public IEnumerable<Supplier> GetAllSuppliers()
        {
            return _repository.GetAll();
        }

        public Supplier GetSupplierById(int id)
        {
            return _repository.GetById(id);
        }

        public void AddSupplier(Supplier supplier)
        {
            _repository.Add(supplier);
        }

        public void UpdateSupplier(Supplier supplier)
        {
            _repository.Update(supplier);
        }

        public void DeleteSupplier(int id)
        {
            _repository.Delete(id);
        }
    }
}

