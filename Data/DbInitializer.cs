using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using HogwartsPotions.Models.Enums;

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

            var courses = new Room[]
            {
            new Room{Capacity= 3, Residents = new HashSet<Student>()},

            new Room{Capacity= 2, Residents = new HashSet<Student>()}

            };
            foreach (Room c in courses)
            {
                context.Rooms.Add(c);
            }
            context.SaveChanges();
            
        }
    }
}
