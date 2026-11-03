
using PAP.DataAccess;
using PAP.Repositories;
using System.Collections.Generic;

namespace PAP.Business
{
    public class ShoppingCartManager
    {
        private readonly ShoppingCartRepository _repository;

        public ShoppingCartManager()
        {
            _repository = new ShoppingCartRepository();
        }

        public IEnumerable<ShoppingCart> GetAllShoppingCarts()
        {
            return _repository.GetAll();
        }

        public ShoppingCart GetShoppingCartById(int id)
        {
            return _repository.GetById(id);
        }

        public void AddShoppingCart(ShoppingCart shoppingCart)
        {
            _repository.Add(shoppingCart);
        }

        public void UpdateShoppingCart(ShoppingCart shoppingCart)
        {
            _repository.Update(shoppingCart);
        }

        public void DeleteShoppingCart(int id)
        {
            _repository.Delete(id);
        }
    }
}

