﻿using TPH_AllHierarchy.DataAccessLayer.Context;
using TPH_AllHierarchy.DataAccessLayer.Entities;

namespace TPH_AllHierarchy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var admin = new Admin
            {
                Fname = "Maher",
                Lname = "Ali",
                SSN = 123,
                Manager = "Ashraf"
            };

            var dev = new Developer
            {
                Fname = "Ibrahim",
                Lname = "Mohamed",
                Salary = 12000,
                JobTitle = "Junior"
            };


            using (var context = new ApplicationDbContext())
            {
                context.Employees.Add(admin);
                context.Employees.Add(dev);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext())
            {
                Console.WriteLine("Admins are :");
                foreach (var item in context.Set<Employee>().OfType<Admin>())
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine("Developers are :");
                foreach (var developer in context.Set<Employee>().OfType<Developer>())
                {
                    Console.WriteLine(developer);
                }
            }

        }
    }
}
