
using PAP.DataAccess;
using PAP.Repositories;
using System.Collections.Generic;

namespace PAP.Business
{
    public class CustomerManager
    {
        private readonly CustomerRepository _repository;

        public CustomerManager()
        {
            _repository = new CustomerRepository();
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _repository.GetAll();
        }

        public Customer GetCustomerById(int id)
        {
            return _repository.GetById(id);
        }

        public void AddCustomer(Customer customer)
        {
            _repository.Add(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            _repository.Update(customer);
        }

        public void DeleteCustomer(int id)
        {
            _repository.Delete(id);
        }
    }
}

