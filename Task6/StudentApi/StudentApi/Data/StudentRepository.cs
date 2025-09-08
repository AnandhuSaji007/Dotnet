using StudentApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace StudentApi.Data
{
    public static class StudentRepository
    {
        private static List<Student> students = new List<Student>
        {
            new Student { Id = 1, Name = "John", Age = 20 },
            new Student { Id = 2, Name = "Alice", Age = 22 }
        };

        public static List<Student> GetAll() => students;
        public static Student? GetById(int id) => students.FirstOrDefault(s => s.Id == id);
        public static void Add(Student student) => students.Add(student);
        public static void Update(Student student)
        {
            var existing = GetById(student.Id);
            if (existing != null)
            {
                existing.Name = student.Name;
                existing.Age = student.Age;
            }
        }
        public static void Delete(int id) => students.RemoveAll(s => s.Id == id);
    }
}
