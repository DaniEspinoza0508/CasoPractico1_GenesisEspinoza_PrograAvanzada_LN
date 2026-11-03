
using PAP.DataAccess;
using PAP.Repositories;
using System.Linq;

namespace PAP.Generators
{
    public class RolesGenerator
    {
        private readonly RoleRepository repository;

        public RolesGenerator()
        {
            repository = new RoleRepository();
        }

        public string Generate()
        {
            if (repository.GetAll().Any())
            {
                return "Error: Ya existen roles registrados.";
            }

            string[] names = { "Admin", "Manager", "Cashier" };

            for (int i = 0; i < names.Length; i++)
            {
                Role role = new Role();

                role.Name = names[i];

                repository.Add(role);
            }

            return "Se generaron 3 roles correctamente.";
        }

        public bool CanGenerate()
        {
            return !repository.GetAll().Any();
        }
    }
}
