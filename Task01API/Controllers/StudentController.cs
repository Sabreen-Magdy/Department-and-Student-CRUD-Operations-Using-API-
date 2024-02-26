using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Task01API.DTOs;
using Task01API.Entities;
using Task01API.Models;
using Task01API.Repositories;

namespace Task01API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentRepository _studentRepository;
        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = (StudentRepository?)studentRepository;    
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var Stds=_studentRepository.GetAll();
            if(Stds is null)
            {
                return NotFound();
            }
            return Ok(Stds);
        }
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            var Std = _studentRepository.GetById(id);
            if (Std is null)
            {
                return NotFound();
            }
            StdWithDept stdDTO=new StdWithDept();
            stdDTO.Student_Id = Std.Id;
            stdDTO.Student_Name = Std.Name;
            stdDTO.Student_Age=Std.Age;
            stdDTO.Student_Address=Std.Address;
            stdDTO.Student_Department=Std.Department.Name;
            return Ok(stdDTO);
        }
        [HttpGet]
        [Route("{name:alpha}")]
        public IActionResult GetByName(string name)
        {
            var Std = _studentRepository.GetByName(name);
            if (Std is null)
            {
                return NotFound();
            }
            StdWithDept stdDTO = new StdWithDept();
            stdDTO.Student_Id = Std.Id;
            stdDTO.Student_Name = Std.Name;
            stdDTO.Student_Age = Std.Age;
            stdDTO.Student_Address = Std.Address;
            stdDTO.Student_Department = Std.Department.Name;
            return Ok(stdDTO);
        }
        [HttpPost]
        public IActionResult Add(Student std)
        {
            if (ModelState.IsValid)
            {
                _studentRepository.Add(std);
                //return Created($"http://localhost:5103/api/Student/{std.Id}",std);
                //return Ok(std);
                return CreatedAtAction(actionName:"GetById",routeValues: new { id = std.Id },"");
            }
            return BadRequest();
        }
        [HttpPut]
        public IActionResult Edit(Student std)
        {
            if (ModelState.IsValid)
            {
                _studentRepository.Edit(std);
                return NoContent();
            }
            return BadRequest();
        }
        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            var std = _studentRepository.GetById(id);
            if (std is null)
                return NotFound();
            _studentRepository.Delete(id);
            return Ok(std);
        }
    }
}
