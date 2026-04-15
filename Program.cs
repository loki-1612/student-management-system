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
                Console.Clear();
                Console.WriteLine("\n======= STUDENT MANAGEMENT SYSTEM =======");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. View Student");
                Console.WriteLine("3. Find Student");
                Console.WriteLine("4. Update Student");
                Console.WriteLine("5. Delete Student");
                Console.WriteLine("6. Exit");
                Console.WriteLine("Enter your choice: ");

                if (! int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input! Please enter a number.");
                    Pause();
                    continue;
                }


                switch (choice)
                {
                    case 1:

                        int id = ReadInt("Enter Id: ");

                        string name = ReadString("Enter Name: ");

                        int age = ReadInt("Enter Age: ");

                        string grade = ReadString("Enter Grade: ");

                        string email = ReadString("Enter Email: ");

                        repo.AddStudent(new Student(id, name, age, grade, email));

                        Console.WriteLine("Student added Successfully!");

                        break;

                    case 2:
                        var students = repo.GetAllStudents();

                        Console.WriteLine("\n| ID  | Name  | Age  | Grade | Email |");

                        Console.WriteLine("----------------------------------------");

                        foreach(var stu in students)
                        {
                            Console.WriteLine(stu);
                        }
                        break;

                    case 3:

                        int searchId = ReadInt("Enter Student ID: ");
                        var found = repo.GetStudentById(searchId);

                        Console.WriteLine(found != null ? found.ToString() : "Student not found");
                        
                        break;

                    case 4:

                        int updateId = ReadInt("Enter Id to update: ");

                        string newName = ReadString("New Name: ");

                        int newAge = ReadInt("New Age: ");

                        string newGrade = ReadString("New Grade: ");

                        string newEmail = ReadString("New Email: ");

                        bool updated = repo.UpdateStudent(updateId, newName, newAge, newEmail, newGrade);

                        Console.WriteLine(updated ? "Updated successfully!" : "Student not found");

                        break;

                    case 5:

                        int deleteId = ReadInt("Enter ID to delete: ");

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
                Pause();
            }
        }

        //Helper method : Read Integer

        static int ReadInt(string message)
        {
            int value;
            while(true)
            {
                Console.Write(message);
                if (int.TryParse(Console.ReadLine(), out value))
                {
                    return value;
                }
                Console.WriteLine("Invalid number, try agian");
            }
        }

        //Helper method : Read String

        static string ReadString(string message)
        {
            string input;
            while(true)
            {
                Console.Write(message);
                input = Console.ReadLine();

                if(! string.IsNullOrWhiteSpace(input) )
                {
                    return input;
                }
                Console.WriteLine("Input cannot be empty.");
            }
        }

        //Pause Screen
        static void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}