using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using HogwartsPotions.Models.Enums;
using System.Xml.Linq;

namespace HogwartsPotions.Data
{
    public class DbInitializer
    {
        public static void Initialize(HogwartsContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Students.Any())
            {
                return;   // DB has been seeded
            }

            /*var ingredients = new Ingredient[]
            {
                new Ingredient { Name = "MandragoraRoot" },
                new Ingredient { Name = "OldMansBeard" },
                new Ingredient { Name = "TearDrop" },
                new Ingredient { Name = "Salt" },
                new Ingredient { Name = "ChickenEgg" },
                new Ingredient { Name = "SunFlower" },
                new Ingredient { Name = "Mud" },
                new Ingredient { Name = "Sugar" }
            };

            foreach (Ingredient c in ingredients)
            {
                context.Ingredients.Add(c);
            }
            context.SaveChanges();




            var students = new Student[]
            {
            new Student{Name= "Carson",HouseType= HouseType.Gryffindor,PetType= PetType.Owl, Room = new Room()},
            new Student{Name= "Connor",HouseType= HouseType.Hufflepuff,PetType= PetType.Cat, Room = new Room()}

            };
            foreach (Student s in students)
            {
                context.Students.Add(s);
            }
            context.SaveChanges();

            var rooms = new Room[]
            {
            new Room{Capacity= 3, Residents = new HashSet<Student>()},

            new Room{Capacity= 2, Residents = new HashSet<Student>()}

            };
            foreach (Room c in rooms)
            {
                context.Rooms.Add(c);
            }
            context.SaveChanges();*/


            var recipes = new Recipe[]
            {
                new Recipe{ Name = "BigBumm", Student = new Student{Name= "Donald",HouseType= HouseType.Gryffindor,PetType= PetType.Owl, Room = new Room()}, Ingredients = new List<Ingredient>()
                    {new Ingredient{Name = "Bean"}, new Ingredient{Name = "Pepper"}, new Ingredient{Name = "Cocoa"}}}

            };

            foreach (Recipe c in recipes)
            {
                context.Recipes.Add(c);
            }
            context.SaveChanges();


            /*
            var potions = new Potion[]
            {
                new Potion
                {
                    Name = "BigBumm", Ingredients = new List<Ingredient>
                    {
                        new Ingredient { Name = "MandragoraRoot" },
                        new Ingredient { Name = "OldMansBeard" },
                        new Ingredient { Name = "TearDrop" }
                    },
                    Recipe = new Recipe
                    {
                        Name = "BadaBumm",
                        Student = new Student
                        {
                            Name = "Kelvin", HouseType = HouseType.Slytherin, PetType = PetType.Rat,
                            Room = new Room { Capacity = 4 }
                        },
                        Ingredients = new List<Ingredient>
                        {
                            new Ingredient { Name = "MandragoraRoot" },
                            new Ingredient { Name = "OldMansBeard" },
                            new Ingredient { Name = "TearDrop" },
                            new Ingredient { Name = "Salt" }
                        }
                    }, Student = new Student
                    {
                        Name = "John", HouseType = HouseType.Slytherin, PetType = PetType.Rat,
                        Room = new Room { Capacity = 3 }
                    }
                }
            };
            foreach (Potion c in potions)
            {
                context.Potions.Add(c);
            }
            context.SaveChanges();

            */



        }
    }
}
