using System;
using StudentManagementSystem.Models;
using StudentManagementSystem.Repository;

namespace StudentManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            StudentRepository repo = new StudentRepository();

            while (true)
            {
                Console.WriteLine("\n======= STUDENT MANAGEMENT SYSTEM =======");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. View Student");
                Console.WriteLine("3. Find Student");
                Console.WriteLine("4. Update Student");
                Console.WriteLine("5. Delete Student");
                Console.WriteLine("6. Exit");
                Console.WriteLine("Enter your choice: ");

                int choice = int.Parse(Console.ReadLine());

                switch(choice)
                {
                    case 1:
                        Console.WriteLine("Enter Id: ");
                        int id = int.Parse(Console.ReadLine());

                        Console.WriteLine("Enter Name: ");
                        string name = Console.ReadLine();

                        Console.WriteLine("Enter Age: ");
                        int age= int.Parse(Console.ReadLine());

                        Console.WriteLine("Enter Grade: ");
                        string grade = Console.ReadLine();

                        Console.WriteLine("Enter Email: ");
                        string email = Console.ReadLine();

                        Student s = new Student(id,name, age, grade, email);
                        repo.AddStudent(s);

                        Console.WriteLine("Student added Successfully!");
                        break;

                    case 2:
                        var students = repo.GetAllStudents();
                        Console.WriteLine("\n| ID | Name | Age | Grade | Email |");
                        Console.WriteLine("-------------------------------------");
                        foreach(var stu in students)
                        {
                            Console.WriteLine(stu);
                        }
                        break;

                    case 3:
                        Console.WriteLine("Enter Student ID: ");
                        int searchId = int.Parse(Console.ReadLine());
                        var found = repo.GetStudentById(searchId);
                        if (found != null)
                        {
                            Console.WriteLine(found);
                        }
                        else
                        {
                            Console.WriteLine("Student not found");
                        }
                        break;

                    case 4:
                        Console.WriteLine("Enter Id to update: ");
                        int updateId = int.Parse(Console.ReadLine());

                        Console.WriteLine("New Name: ");
                        string newName = Console.ReadLine();

                        Console.WriteLine("New Age: ");
                        int newAge = int.Parse(Console.ReadLine());

                        Console.WriteLine("New Grade: ");
                        string newGrade = Console.ReadLine();

                        Console.WriteLine("New Email: ");
                        string newEmail = Console.ReadLine();

                        bool updated = repo.UpdateStudent(updateId, newName, newAge, newEmail, newGrade);
                        Console.WriteLine(updated ? "Updated successfully!" : "Student not found");

                        break;

                    case 5:
                        Console.WriteLine("Enter ID to delete: ");
                        int deleteId = int.Parse((Console.ReadLine()));
                        bool deleted = repo.DeleteStudent(deleteId);
                        Console.WriteLine(deleted ? "Deleted Sucessfully!" : "Student not found");
                        break;

                    case 6:
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid Choice");
                        break;
                }
            }
        }

    }
}