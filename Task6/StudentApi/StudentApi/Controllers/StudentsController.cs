using Microsoft.AspNetCore.Mvc;
using StudentApi.Models;
using StudentApi.Data;

namespace StudentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll() => Ok(StudentRepository.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var student = StudentRepository.GetById(id);
            return student == null ? NotFound() : Ok(student);
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            StudentRepository.Add(student);
            return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Student student)
        {
            var existing = StudentRepository.GetById(id); if (existing == null)
            if (existing == null) return NotFound();
            student.Id = id;
            StudentRepository.Update(student);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = StudentRepository.GetById(id);
            if (existing == null) return NotFound();
            StudentRepository.Delete(id);
            return NoContent();
        }
    }
}
