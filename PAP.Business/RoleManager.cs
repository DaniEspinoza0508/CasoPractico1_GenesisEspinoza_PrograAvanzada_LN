
using PAP.DataAccess;
using PAP.Repositories;
using System.Collections.Generic;

namespace PAP.Business
{
    public class RoleManager
    {
        private readonly RoleRepository _repository;

        public RoleManager()
        {
            _repository = new RoleRepository();
        }

        public IEnumerable<Role> GetAllRoles()
        {
            return _repository.GetAll();
        }

        public Role GetRoleById(int id)
        {
            return _repository.GetById(id);
        }

        public void AddRole(Role role)
        {
            _repository.Add(role);
        }

        public void UpdateRole(Role role)
        {
            _repository.Update(role);
        }

        public void DeleteRole(int id)
        {
            _repository.Delete(id);
        }
    }
}

