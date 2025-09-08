using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Enter your name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter your age:");
            int age = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"Hii {name},Your age is {age}");
        }
    }
}