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
                Console.WriteLine("6. Search by Name");
                Console.WriteLine("7. Sort by Name");
                Console.WriteLine("8. Sort by Age");
                Console.WriteLine("9. Filter by Grade");
                Console.WriteLine("10. Exit");
                Console.WriteLine("Enter your choice: ");

                if (! int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input! Please enter a number.");
                    Pause();
                    continue;
                }

                try
                {
                    switch (choice)
                    {
                        case 1:

                            int id = ReadInt("Enter Id: ");

                            string name = ReadString("Enter Name: ");

                            int age = ReadInt("Enter Age: ");

                            string grade = ReadGrade("Enter Grade (A/B/C): ");
                          
                            string email = ReadString("Enter Email: ");

                            repo.AddStudent(new Student(id, name, age, grade, email));

                            Console.WriteLine("Student added Successfully!");

                            break;

                        case 2:
                            var students = repo.GetAllStudents();
                            DisplayStudents(students);

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

                            string newGrade = ReadGrade("Enter Grade (A/B/C): ");

                            string newEmail = ReadString("New Email: ");

                            bool updated = repo.UpdateStudent(updateId, newName, newAge, newEmail, newGrade);

                            Console.WriteLine(updated ? "Updated successfully!" : "Student not found");

                            break;

                        case 5:

                            int deleteId = ReadInt("Enter ID to delete: ");

                            var student = repo.GetStudentById(deleteId);
                            if(student != null)
                            {
                                Console.WriteLine("Are you sure you want to delete? (Y/N): ");
                                string confirm = Console.ReadLine();

                                if (confirm.ToUpper() == "Y")
                                {
                                    repo.DeleteStudent(deleteId);
                                    Console.WriteLine("Student deleted successfully.");
                                }
                                else
                                {
                                    Console.WriteLine("Delete cancelled.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Student not found");
                            }

                            break;

                        case 6:
                            string SearchName = ReadString("Enter name to search: ");
                            var results = repo.SearchByName(SearchName);

                            foreach (var s in results)
                            {
                                Console.WriteLine(s);
                            }
                            break;

                        case 7:
                            var SortedByName = repo.SortByName();
                            foreach (var s in SortedByName)
                            {
                                Console.WriteLine(s);
                            }
                            break;

                        case 8:
                            var SortedByAge = repo.SortByAge();
                            foreach (var s in SortedByAge)
                            {
                                Console.WriteLine(s);
                            }
                            break;

                        case 9:
                           
                            string Grade = ReadGrade("Enter Grade (A/B/C): ");

                            var filtered = repo.GetStudentByGrade(Grade);
                            if(filtered.Count == 0)
                            {
                                Console.WriteLine("No students found.");
                            }
                            else
                            {
                                DisplayStudents(filtered);
                            }
                            break;

                        case 10:
                            Console.WriteLine("Exiting...");
                            return;


                        default:
                            Console.WriteLine("Invalid Choice");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Something went wrong: " + ex.Message);
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

        //Helper method : Read Grade
        public static string ReadGrade(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                string grade = Console.ReadLine().ToUpper().Trim();
                if (grade == "A" || grade == "B" || grade == "C")
                    return grade;
                Console.WriteLine("Invalid grade! Please enter A, B, or C only.");
            }
        }

        //Display students
        public static void DisplayStudents(List<Student> students)
        {
            Console.WriteLine("\n| {0,-5}  | {1,-10}  | {2,-5}  | {3,-5} | {4,-20} |",
                                "ID", "Name", "Age", "Grade", "Email");

            Console.WriteLine(new string('-', 64));

            foreach (var stu in students)
            {
                Console.WriteLine("| {0,-5}  | {1,-10}  | {2,-5}  | {3,-5} | {4,-20} |",
                    stu.Id, stu.Name, stu.Age, stu.Grade, stu.Email);
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