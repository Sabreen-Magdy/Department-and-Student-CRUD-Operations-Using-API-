using Microsoft.AspNetCore.Mvc;
using Task01API.Entities;
using Task01API.Models;

namespace Task01API.Repositories
{
    public interface IStudentRepository
    {
        public List<Student> GetAll();
        public Student GetById(int id);
        public Student GetByName(string name);
        public void Add(Student std);
        public void Edit(Student std);
        public void Delete(int id);

    }
}
