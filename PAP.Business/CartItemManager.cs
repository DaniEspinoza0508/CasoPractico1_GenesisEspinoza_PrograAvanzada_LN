
using PAP.DataAccess;
using PAP.Repositories;
using System.Collections.Generic;

namespace PAP.Business
{
    public class CartItemManager
    {
        private readonly CartItemRepository _repository;

        public CartItemManager()
        {
            _repository = new CartItemRepository();
        }

        public IEnumerable<CartItem> GetAllCartItems()
        {
            return _repository.GetAll();
        }

        public CartItem GetCartItemById(int id)
        {
            return _repository.GetById(id);
        }

        public void AddCartItem(CartItem cartItem)
        {
            _repository.Add(cartItem);
        }

        public void UpdateCartItem(CartItem cartItem)
        {
            _repository.Update(cartItem);
        }

        public void DeleteCartItem(int id)
        {
            _repository.Delete(id);
        }
    }
}

