using HogwartsPotions.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HogwartsPotions.Models.Enums;
using System;

namespace HogwartsPotions.Models
{
    public class Operations
    {
        private readonly HogwartsContext _context;

        public Operations(HogwartsContext context)
        {
            _context = context;
        }

        public Task<Room> GetRoom(long roomId)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateRoom(Room room)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteRoom(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Room>> GetRoomsForRatOwners()
        {
            throw new NotImplementedException();
        }




        public async Task AddRoom(Room room)
        {
            await _context.Rooms.AddAsync(room);
            _context.SaveChanges();
        }

        public Task<List<Student>> GetAllStudents()
        {
            var result = _context.Students.ToListAsync();
            return result;
        }

        public Task<List<Room>> GetAllRooms()
        {
            var result = _context.Rooms.ToListAsync();
            return result;
        }

        public Task<List<Potion>> GetAllPotions()
        {
            var result = _context.Potions.ToListAsync();
            return result;
        }

        public async Task<Potion> BrewingPotion(Potion potion)
        {

            Student chefStudent = new Student();
            long studentId = potion.Student.ID;

            foreach (var student in _context.Students)
            {
                if (student.ID == studentId)
                {
                    chefStudent = student;
                }
            }

            Random random = new Random();

            var recipesWithIngredients = await _context.Recipes
                .Include(r => r.Ingredients)
                .ToListAsync();

            foreach (var recipe in recipesWithIngredients)
            {
                if (recipe.Ingredients.Equals(potion.Ingredients))
                {
                    potion.Recipe = recipe;
                    potion.Name = $"{chefStudent.Name} {potion.BrewingStatus == BrewingStatus.Replica} #{random.Next(0, 99)}";
                    await _context.Potions.AddAsync(potion);
                    return potion;
                }
            }

            Recipe newRecipe = new Recipe
            {
                Ingredients = potion.Ingredients,
                Name = $"{chefStudent.Name} {BrewingStatus.Discovery} #{random.Next(0, 99)}",
                Student = chefStudent
            };

            potion.Recipe = newRecipe;

            await _context.Recipes.AddAsync(newRecipe);

            await _context.Potions.AddAsync(potion);

            _context.SaveChanges();

            return potion;
        }
    }
}
