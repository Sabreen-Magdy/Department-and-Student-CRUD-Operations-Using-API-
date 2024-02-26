using Microsoft.EntityFrameworkCore;
using Task01API.Entities;
using Task01API.Models;

namespace Task01API.Repositories
{
    public class DepartmentRepository:IDepartmentRepository
    {
        private readonly TaskAPIDbContext _db;
        public DepartmentRepository(TaskAPIDbContext db)
        {
            _db = db;
        }
        public List<Department> GetAll()
        {
            return _db.Departments/*.Include(d=>d.Students)*/.ToList();
        }
        public Department GetById(int id)
        {
            return _db.Departments.Include(d=>d.Students).FirstOrDefault(d => d.Id == id);
        }
        public Department GetByName(string name)
        {
            return _db.Departments.Include(d => d.Students).FirstOrDefault(d => d.Name==name);
        }
        public void Add(Department dept)
        {
            _db.Departments.Add(dept);
            _db.SaveChanges();
        }
        public void Edit(Department dept)
        {
            _db.Departments.Update(dept);
            _db.SaveChanges();
        }
        public void Delete(int id)
        {
            var dept = _db.Departments.Find(id);
            _db.Departments.Remove(dept);
            _db.SaveChanges();
        }
    }
}
