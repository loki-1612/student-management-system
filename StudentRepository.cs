using System;
using System.Collections.Generic;
using StudentManagementSystem.Models;


namespace StudentManagementSystem.Repository
{
    public class StudentRepository
    {
        private List<Student> students = new List<Student>();

        public void AddStudent(Student student)
        {
            students.Add(student);
        }

        public List<Student> GetAllStudents()
        {
            return students;
        }

        public Student GetStudentById(int id)
        {
            foreach (var student in students)
            {
                if (student.Id == id)
                {
                    return student;
                }
            }
            return null;
        }

        public bool UpdateStudent(int id, string name, int age, string email, string grade)
        {
            var student = GetStudentById(id);
            if (student != null)
            {
                student.Name = name;
                student.Age = age;
                student.Email = email;
                student.Grade = grade;
                return true;
            }
            return false;
        }

        public bool DeleteStudent(int id)
        {
            var student = GetStudentById(id);
            if (student != null)
            {
                students.Remove(student);
                return true;
            }
            return false;
        }
    }


}

