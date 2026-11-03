
using PAP.DataAccess;
using PAP.Repositories;
using System.Collections.Generic;

namespace PAP.Business
{
    public class OrderItemManager
    {
        private readonly OrderItemRepository _repository;

        public OrderItemManager()
        {
            _repository = new OrderItemRepository();
        }

        public IEnumerable<OrderItem> GetAllOrderItems()
        {
            return _repository.GetAll();
        }

        public OrderItem GetOrderItemById(int id)
        {
            return _repository.GetById(id);
        }

        public void AddOrderItem(OrderItem orderItem)
        {
            _repository.Add(orderItem);
        }

        public void UpdateOrderItem(OrderItem orderItem)
        {
            _repository.Update(orderItem);
        }

        public void DeleteOrderItem(int id)
        {
            _repository.Delete(id);
        }
    }
}

