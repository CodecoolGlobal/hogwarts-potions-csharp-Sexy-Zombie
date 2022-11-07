using System.Collections.Generic;
using System.Threading.Tasks;
using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HogwartsPotions.Controllers
{
    [ApiController, Route("/student")]
    public class StudentController : ControllerBase
    {
        private readonly HogwartsContext _context;

        public StudentController(HogwartsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<List<Student>> GetAllStudents()
        {
            return await _context.GetAllStudents();
        }

    }
}
