using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;


namespace HogwartsPotions.Models
{
    public class HogwartsContext : DbContext
    {
        public const int MaxIngredientsForPotions = 5;

        public DbSet<Student> Students { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Potion> Potions { get; set; }

        /*protected override void OnModelCreating(ModelBuilder constructor)
        {
            constructor.Entity<TeamPlayer>().HasKey(uc => new { uc.PlayerId, uc.TeamId });
            base.OnModelCreating(constructor);
        } */


        public HogwartsContext(DbContextOptions<HogwartsContext> options) : base(options)
        {
        }


        public async Task AddRoom(Room room)
        {
            await Rooms.AddAsync(room);
        }

        public Task<Room> GetRoom(long roomId)
        {
            var room = Rooms.Where(x => x.ID == roomId).FirstOrDefaultAsync();
            return room;
        }

        public Task<List<Room>> GetAllRooms()
        {
            var result = Rooms.ToListAsync();
            return result;

            //  .ToListAsync();
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

        public Task<List<Student>> GetAllStudents()
        {
            var students = Students.ToListAsync();
            return students;
        }

        public Task<List<Potion>> GetAllPotions()
        {
            var potions = Potions.Include(p => p.Student)
                .Include(p => p.Recipe)
                .Include(p => p.Ingredients)
                .ToListAsync();
            return potions;
        }


        public async Task AddAReciepe()
        {
            var reciepe = new Recipe
            {
                Name = "BadaBumm",
                Student = new Student
                    { Name = "Shepherd", HouseType = HouseType.Ravenclaw, PetType = PetType.Owl, Room = new Room() },
            };

            await Recipes.AddAsync(reciepe);

        }

        public async Task<Potion> BrewingPotion(Potion potion)
        {

            long studentId = potion.Student.ID;

            Student chefStudent = await Students
                .Where(d => d.ID == studentId)
                .FirstOrDefaultAsync();


            Random random = new Random();

            var recipesWithIngredients = await Recipes
                .Include(r => r.Ingredients)
                .ToListAsync();

            var potionIngredientsNames = potion.Ingredients.Select(x => x.Name).ToList();

            foreach (var recipe in recipesWithIngredients)
            {
                var recipeIngredientsNames = recipe.Ingredients.Select(x => x.Name).ToList();

                if ((potionIngredientsNames.Count() == recipeIngredientsNames.Count()) &&
                    !potionIngredientsNames.Except(recipeIngredientsNames).Any())
                {
                    var newPotion = new Potion
                    {
                        Name =
                            $"{chefStudent.Name} {potion.BrewingStatus = BrewingStatus.Replica} #{random.Next(0, 99)}",
                        Ingredients = recipe.Ingredients.ToList(),
                        Recipe = recipe,
                        BrewingStatus = BrewingStatus.Replica,
                        Student = chefStudent
                    };

                    await Potions.AddAsync(newPotion);

                    SaveChanges();
                    return newPotion;
                }
            }

            Recipe newRecipe = new Recipe
            {
                Ingredients = potion.Ingredients,
                Name = $"{chefStudent.Name} {BrewingStatus.Discovery} #{random.Next(0, 99)}",
                Student = chefStudent

            };

            potion.Student = chefStudent;

            potion.Recipe = newRecipe;

            await Potions.AddAsync(potion);

            SaveChanges();

            return potion;
        }

        public async Task<List<Potion>> GetPotionByStudentId(long studentId)
        {
            var filteredPotions = await Potions
                .Where(p => p.Student.ID == studentId)
                .Include(p => p.Student)
                .Include(p => p.Recipe)
                .Include(p => p.Ingredients)
                .ToListAsync();

            return filteredPotions;
        }

        public async Task<Potion> BrewRandomPotion(Student student)
        {
            Random random = new Random();
            var studentId = student.ID;


            var ingredientList = await Ingredients.Select(i => i.Name).ToListAsync();

            Student chefStudent = await Students
                .Where(d => d.ID == studentId)
                .FirstOrDefaultAsync();


            Recipe newRecipe = new Recipe
            {
                Name = $"{chefStudent.Name} {BrewingStatus.Brew} #{random.Next(0, 99)}",
                Student = chefStudent,
                Ingredients = new List<Ingredient>()
            };

            Potion newPotion = new Potion
            {
                Ingredients = new List<Ingredient>
                {
                    new Ingredient { Name = ingredientList[random.Next(0, ingredientList.Count)] },
                    new Ingredient { Name = ingredientList[random.Next(0, ingredientList.Count)] },
                    new Ingredient { Name = ingredientList[random.Next(0, ingredientList.Count)] },
                    new Ingredient { Name = ingredientList[random.Next(0, ingredientList.Count)] }
                },
                Student = chefStudent,
                Name = $"{chefStudent.Name} {BrewingStatus.Brew} #{random.Next(0, 99)}",
                Recipe = newRecipe
            };

            newPotion.Recipe.Ingredients = newPotion.Ingredients;
            newPotion.Recipe.Name = newPotion.Name;

            Potions.Add(newPotion);

            SaveChanges();

            return newPotion;

        }

        public async Task<Potion> UpdatePotionById(long potionId, Ingredient ingredient)
        {
            var potionToUpdate = await Potions
                .Where(p => p.ID == potionId)
                .Include(p => p.Ingredients)
                .Include(p => p.Student)
                .Include(p => p.Recipe)
                .FirstOrDefaultAsync();

            Ingredient newIngredient = new Ingredient
            {
                Name = ingredient.Name
            };

            potionToUpdate.Ingredients.Add(newIngredient);
            potionToUpdate.Recipe.Ingredients.Add(newIngredient);

            SaveChanges();

            return potionToUpdate;
        }

        public async Task<List<Recipe>> GetRecipesWithLessIngredients(long potionId)
        {
            var selectedPotion = await Potions.Where(p => p.ID == potionId)
                .Include(p =>p.Ingredients)
                .Include(p => p.Recipe)
                .FirstOrDefaultAsync();

            var equalRecipes = new List<Recipe>();

            var recipesWithIngredients = await Recipes
                .Include(r => r.Ingredients)
                .ToListAsync();

            var selectedPotionIngredientsNames = selectedPotion.Ingredients.Select(x => x.Name).ToList();

            foreach (var recipe in recipesWithIngredients)
            {
                var recipeIngredientsNames = recipe.Ingredients.Select(x => x.Name).ToList();

                if ((selectedPotionIngredientsNames.Count() == recipeIngredientsNames.Count()) &&
                    !selectedPotionIngredientsNames.Except(recipeIngredientsNames).Any())
                {
                    if (selectedPotion.Recipe != null && recipe.ID != selectedPotion.Recipe.ID)
                    {
                        equalRecipes.Add(recipe);
                    }
                }
            }

            return equalRecipes;
        }
    }
}
