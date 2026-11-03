
using PAP.DataAccess;
using PAP.Repositories;
using System.Collections.Generic;

namespace PAP.Business
{
    public class AddressManager
    {
        private readonly AddressRepository _repository;

        public AddressManager()
        {
            _repository = new AddressRepository();
        }

        public IEnumerable<Address> GetAllAddresses()
        {
            return _repository.GetAll();
        }

        public Address GetAddressById(int id)
        {
            return _repository.GetById(id);
        }

        public void AddAddress(Address address)
        {
            _repository.Add(address);
        }

        public void UpdateAddress(Address address)
        {
            _repository.Update(address);
        }

        public void DeleteAddress(int id)
        {
            _repository.Delete(id);
        }
    }
}

