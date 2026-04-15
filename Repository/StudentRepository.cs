using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using StudentManagementSystem.Models;


namespace StudentManagementSystem.Repository
{
    public class StudentRepository
    {
        private List<Student> students = new List<Student>();

        private string FilePath = "students.tsx";

        public StudentRepository()
        {
            LoadFromFile();
        }

        //Add Student
        public void AddStudent(Student student)
        {
            students.Add(student);
            SaveToFile();
        }

        //Get All
        public List<Student> GetAllStudents()
        {
            return students;
        }

        //Find by Id
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
                SaveToFile();
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
                SaveToFile();
                return true;
            }
            return false;
        }

        //Save to file
        private void SaveToFile()
        {
            List<string> Lines = new List<string>();

            foreach (var s in students)
            {
                string Line = $"{s.Id},{s.Name},{s.Age},{s.Grade},{s.Email}";
                Lines.Add(Line);
            }
            File.WriteAllLines(FilePath, Lines);
        }

        //Load from file
        private void LoadFromFile()
        {
            if (!File.Exists(FilePath))
                return;
            var Lines = File.ReadAllLines(FilePath);

            foreach (var Line in Lines)
            {
                var data = Line.Split(',');
                int id = int.Parse(data[0]);
                string name = data[1];
                int age = int.Parse(data[2]);
                string grade = data[3];
                string email = data[4];

                students.Add(new Student(id, name, age, grade, email));

            }
        }

        //Search by Name
        public List<Student> SearchByName(string name)
        {
            List<Student> result = new List<Student>();

            foreach (var s in students)
            {
                if (s.Name.ToLower().Contains(name.ToLower()))
                {
                    result.Add(s);
                }
            }
            return result;
        }

        //sort by name
        public List<Student> SortByName()
        {
            return students.OrderBy(s => s.Name).ToList();
        }

        //sort by age
        public List<Student> SortByAge()
        {
            return students.OrderBy(s => s.Age).ToList();
        }

        //sort by grade
        public List <Student> GetStudentByGrade(string grade)
        {
            return students.Where(s => s.Grade.ToUpper() == grade.ToUpper()).ToList();
        }

        //Duplicate Id Check
        public bool StudentExists(int id)
        {
            foreach (var student in students)
            {
                if (student.Id == id)
                    return true;
            }
            return false;
        }

        //Export to csv
        public void ExportToCsv(string filePath)
        {
            using(StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("Id, Name, Age, Grade, Email");
                foreach(var s in students)
                {
                    writer.WriteLine($"{s.Id},{s.Name},{s.Age},{s.Grade},{s.Email}");
                }
            }
        }

        
    }
}

