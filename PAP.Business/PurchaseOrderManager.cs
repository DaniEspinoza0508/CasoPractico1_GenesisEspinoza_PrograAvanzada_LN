
using PAP.DataAccess;
using PAP.Repositories;
using System.Collections.Generic;

namespace PAP.Business
{
    public class PurchaseOrderManager
    {
        private readonly PurchaseOrderRepository _repository;

        public PurchaseOrderManager()
        {
            _repository = new PurchaseOrderRepository();
        }

        public IEnumerable<PurchaseOrder> GetAllPurchaseOrders()
        {
            return _repository.GetAll();
        }

        public PurchaseOrder GetPurchaseOrderById(int id)
        {
            return _repository.GetById(id);
        }

        public void AddPurchaseOrder(PurchaseOrder purchaseOrder)
        {
            _repository.Add(purchaseOrder);
        }

        public void UpdatePurchaseOrder(PurchaseOrder purchaseOrder)
        {
            _repository.Update(purchaseOrder);
        }

        public void DeletePurchaseOrder(int id)
        {
            _repository.Delete(id);
        }
    }
}

