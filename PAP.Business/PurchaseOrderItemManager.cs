
using PAP.DataAccess;
using PAP.Repositories;
using System.Collections.Generic;

namespace PAP.Business
{
    public class PurchaseOrderItemManager
    {
        private readonly PurchaseOrderItemRepository _repository;

        public PurchaseOrderItemManager()
        {
            _repository = new PurchaseOrderItemRepository();
        }

        public IEnumerable<PurchaseOrderItem> GetAllPurchaseOrderItems()
        {
            return _repository.GetAll();
        }

        public PurchaseOrderItem GetPurchaseOrderItemById(int id)
        {
            return _repository.GetById(id);
        }

        public void AddPurchaseOrderItem(PurchaseOrderItem purchaseOrderItem)
        {
            _repository.Add(purchaseOrderItem);
        }

        public void UpdatePurchaseOrderItem(PurchaseOrderItem purchaseOrderItem)
        {
            _repository.Update(purchaseOrderItem);
        }

        public void DeletePurchaseOrderItem(int id)
        {
            _repository.Delete(id);
        }
    }
}

