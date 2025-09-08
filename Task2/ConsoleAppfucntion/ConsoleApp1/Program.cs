using System;

namespace ConsoleApp
{
    class Student
    {
        public string Name;
        public int Age;

        public void PrintDetails()
        {
            Console.WriteLine($"Name: {Name}, Age: {Age}");
        }
    }



    class Program
    {
        static void Main()
        {
            Student student = new Student();

            Console.Write("Enter student's name: ");
            student.Name = Console.ReadLine();

            Console.Write("Enter student's age: ");
            student.Age = Convert.ToInt32(Console.ReadLine());

            student.PrintDetails();
        }
    }
}
