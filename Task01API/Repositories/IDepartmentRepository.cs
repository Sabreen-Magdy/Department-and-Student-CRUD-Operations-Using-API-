using Task01API.Models;

namespace Task01API.Repositories
{
    public interface IDepartmentRepository
    {
        public List<Department> GetAll();
        public Department GetById(int id);
        public Department GetByName(string name);
        public void Add(Department std);
        public void Edit(Department std);
        public void Delete(int id);
    }
}
