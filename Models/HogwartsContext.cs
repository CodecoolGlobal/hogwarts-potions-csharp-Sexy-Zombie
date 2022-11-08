using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.EntityFrameworkCore;

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


        public HogwartsContext(DbContextOptions<HogwartsContext> options) : base(options)
        {
        }
        

        public async Task AddRoom(Room room)
        {
            await Task.Run(() =>
                Rooms.Add(room));
        }

        public Task<Room> GetRoom(long roomId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Room>> GetAllRooms()
        {
            var result = Task.Run(() => Rooms.ToList());
            return result;
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
            var result = Task.Run(() => Students.ToList());
            return result;
        }

        public Task<List<Potion>> GetAllPotions()
        {
            var result = Task.Run(() => Potions.ToList());
            return result;
        }


        public async Task AddAReciepe()
        {
            var reciepe = new Recipe
            {
                Name = "BadaBumm",
                Student = new Student
                    { Name = "Shepherd", HouseType = HouseType.Ravenclaw, PetType = PetType.Owl, Room = new Room() },
            };

            await Task.Run(() =>
                Recipes.Add(reciepe));

        }

        public async Task BrewingPotion(Potion potion)
        {

        }


    }
}
