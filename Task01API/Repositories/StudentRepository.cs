using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task01API.Entities;
using Task01API.Models;

namespace Task01API.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly TaskAPIDbContext _db;
        public StudentRepository(TaskAPIDbContext db)
        {
            _db = db;
        }
        public List<Student> GetAll()
        {
            return _db.Students.Include(s => s.Department).ToList();
        }
        public Student GetById(int id)
        {
            return _db.Students.Include(s => s.Department).FirstOrDefault(s=>s.Id==id);
        }
        public Student GetByName(string name)
        {
            return _db.Students.Include(s => s.Department).FirstOrDefault(s => s.Name == name);
        }
        public void Add(Student std)
        {
                _db.Students.Add(std);
                _db.SaveChanges();
        }
        public void Edit(Student std)
        {
                _db.Students.Update(std);
                _db.SaveChanges();
        }
        public void Delete(int id)
        {
            var std = _db.Students.Find(id);
            _db.Students.Remove(std);
            _db.SaveChanges();
        }
    }
}
