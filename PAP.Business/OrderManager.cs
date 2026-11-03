
using PAP.DataAccess;
using PAP.Repositories;
using System.Collections.Generic;

namespace PAP.Business
{
    public class OrderManager
    {
        private readonly OrderRepository _repository;

        public OrderManager()
        {
            _repository = new OrderRepository();
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _repository.GetAll();
        }

        public Order GetOrderById(int id)
        {
            return _repository.GetById(id);
        }

        public void AddOrder(Order order)
        {
            _repository.Add(order);
        }

        public void UpdateOrder(Order order)
        {
            _repository.Update(order);
        }

        public void DeleteOrder(int id)
        {
            _repository.Delete(id);
        }
    }
}

