using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task01API.DTOs;
using Task01API.Entities;
using Task01API.Models;
using Task01API.Repositories;

namespace Task01API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
       
        [HttpGet]
        public IActionResult GetAll()
        {
            var depts = _departmentRepository.GetAll();
            if (depts is null)
            {
                return NotFound();
            }
            return Ok(depts);
        }
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            var dept = _departmentRepository.GetById(id);
            if (dept is null)
            {
                return NotFound();
            }
            DeptsWithStdsName deptsWithEmpsName=new DeptsWithStdsName();
            deptsWithEmpsName.Department_Number = dept.Id;
            deptsWithEmpsName.Department_Name = dept.Name;
            deptsWithEmpsName.Department_Manager = dept.MgrName;
            foreach(var e in dept.Students)
            {
                deptsWithEmpsName.Students_Name.Add(e.Name);
            }

            return Ok(deptsWithEmpsName);
        }
        [HttpGet]
        [Route("{name:alpha}")]
        public IActionResult GetByName(string name)
        {
            var dept = _departmentRepository.GetByName(name);
            if (dept is null)
            {
                return NotFound();
            }
            DeptsWithStdsName deptsWithEmpsName = new DeptsWithStdsName();
            deptsWithEmpsName.Department_Number = dept.Id;
            deptsWithEmpsName.Department_Name = dept.Name;
            deptsWithEmpsName.Department_Manager = dept.MgrName;
            foreach (var e in dept.Students)
            {
                deptsWithEmpsName.Students_Name.Add(e.Name);
            }

            return Ok(deptsWithEmpsName);
        }
        [HttpPost]
        public IActionResult Add(Department dept)
        {
            if (ModelState.IsValid)
            {
                _departmentRepository.Add(dept);
                //return Created($"http://localhost:5103/api/Student/{std.Id}",std);
                //return Ok(std);
                return CreatedAtAction(actionName: "GetById", routeValues: new { id = dept.Id }, "");
            }
            return BadRequest();
        }
        [HttpPut]
        public IActionResult Edit(Department dept)
        {
            if (ModelState.IsValid)
            {
                _departmentRepository.Edit(dept);
                return NoContent();
            }
            return BadRequest();
        }
        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            var dept = _departmentRepository.GetById(id);
            if (dept is null)
                return NotFound();
            _departmentRepository.Delete(id);
            return Ok(dept);
        }
    }
}
