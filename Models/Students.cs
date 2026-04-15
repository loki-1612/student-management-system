using System;

namespace StudentManagementSystem.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Grade { get; set; }
        public string Email { get; set; }

        public Student(int Id, string Name, int Age, string Grade, string Email)
        {
            this.Id = Id;
            this.Name = Name;
            this.Age = Age;
            this.Grade = Grade;
            this.Email = Email;
        }

        public override string ToString()
        {
            return $"| {Id,-4} | {Name,-20} | {Age,-4} | {Grade,-6} | {Email,-25} |";
        }

    }
}