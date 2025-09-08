using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
    class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    class Program
    {
        static void Main()
        {
            List<Student> students = new List<Student>
            {
                new Student { Name = "Anandhu", Age = 24 },
                new Student { Name = "Arjun", Age = 23 },
                new Student { Name = "Madav", Age = 23 },
                
            };

            Console.WriteLine("Enter the age to filter:");
            int minAge = Convert.ToInt32(Console.ReadLine());

            var filteredStudents = students.Where(s => s.Age >= minAge);

            Console.WriteLine($"\nStudents with age >= {minAge}:");
            foreach (var student in filteredStudents)
            {
                Console.WriteLine($"{student.Name}, Age: {student.Age}");
            }
        }
    }
}
