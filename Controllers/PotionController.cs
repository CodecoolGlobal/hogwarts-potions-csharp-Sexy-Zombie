using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HogwartsPotions.Controllers
{
    [ApiController, Route("/potions")]
    public class PotionController : ControllerBase
    {
        private readonly HogwartsContext _context;

        public PotionController(HogwartsContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<List<Potion>> GetAllPotions()
        {
            return await _context.GetAllPotions();
        }

        [HttpPost]
        public async Task<Potion> BrewingPotion(Potion newPotion)
        {
            return await _context.BrewingPotion(newPotion);
        }

        [HttpGet("{studentId}")]
        public async Task<List<Potion>> GetPotionByStudentId(int studentId)
        {
            return await _context.GetPotionByStudentId(studentId);
        }

        [HttpPost("brew")]
        public async Task<Potion> BrewRandomPotion(Student student)
        {
            return await _context.BrewRandomPotion(student);
        }

        [HttpPut("{potionId}/add")]
        public async Task<Potion> UpdatePotionById([FromRoute] long potionId, [FromBody] Ingredient ingredient)
        {
            return await _context.UpdatePotionById(potionId, ingredient);
        }
    }
}
