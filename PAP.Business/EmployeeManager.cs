
using PAP.DataAccess;
using PAP.Repositories;
using System.Collections.Generic;

namespace PAP.Business
{
    public class EmployeeManager
    {
        private readonly EmployeeRepository _repository;

        public EmployeeManager()
        {
            _repository = new EmployeeRepository();
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _repository.GetAll();
        }

        public Employee GetEmployeeById(int id)
        {
            return _repository.GetById(id);
        }

        public void AddEmployee(Employee employee)
        {
            _repository.Add(employee);
        }

        public void UpdateEmployee(Employee employee)
        {
            _repository.Update(employee);
        }

        public void DeleteEmployee(int id)
        {
            _repository.Delete(id);
        }
    }
}

