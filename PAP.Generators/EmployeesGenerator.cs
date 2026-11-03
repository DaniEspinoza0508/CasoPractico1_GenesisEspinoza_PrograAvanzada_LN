
using PAP.DataAccess;
using PAP.Repositories;
using System;
using System.Linq;

namespace PAP.Generators
{
    public class EmployeesGenerator
    {
        private readonly EmployeeRepository repository;
        private readonly RoleRepository roleRepository;

        public EmployeesGenerator()
        {
            repository = new EmployeeRepository();
            roleRepository = new RoleRepository();
        }

        public string Generate()
        {
            if (repository.GetAll().Any())
            {
                return "Error: Ya existen empleados registrados.";
            }

            var roles = roleRepository.GetAll().ToList();

            if (!roles.Any())
            {
                return "Error: Primero deben existir roles registrados.";
            }

            for (int i = 1; i <= 10; i++)
            {
                Employee employee = new Employee();

                employee.RoleId = roles[(i - 1) % roles.Count].Id;
                employee.FirstName = "Empleado" + i;
                employee.LastName = "Apellido" + i;
                employee.Email = "empleado" + i + "@correo.com";
                employee.PasswordHash = "HASH_PRUEBA_" + i;
                employee.IsActive = true;
                employee.CreatedAt = DateTime.Now;

                repository.Add(employee);
            }

            return "Se generaron 10 empleados correctamente.";
        }

        public bool CanGenerate()
        {
            return !repository.GetAll().Any()
                && roleRepository.GetAll().Any();
        }
    }
}
