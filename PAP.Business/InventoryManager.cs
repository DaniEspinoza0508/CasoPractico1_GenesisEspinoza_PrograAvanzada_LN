
using PAP.DataAccess;
using PAP.Repositories;
using System.Collections.Generic;

namespace PAP.Business
{
    public class InventoryManager
    {
        private readonly InventoryRepository _repository;

        public InventoryManager()
        {
            _repository = new InventoryRepository();
        }

        public IEnumerable<Inventory> GetAllInventories()
        {
            return _repository.GetAll();
        }

        public Inventory GetInventoryById(int id)
        {
            return _repository.GetById(id);
        }

        public void AddInventory(Inventory inventory)
        {
            _repository.Add(inventory);
        }

        public void UpdateInventory(Inventory inventory)
        {
            _repository.Update(inventory);
        }

        public void DeleteInventory(int id)
        {
            _repository.Delete(id);
        }
    }
}
