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
                Console.WriteLine("3. Find Student By ID");
                Console.WriteLine("4. Update Student");
                Console.WriteLine("5. Delete Student");
                Console.WriteLine("6. Search by Name");
                Console.WriteLine("7. Sort by Name");
                Console.WriteLine("8. Sort by Age");
                Console.WriteLine("9. Filter by Grade");
                Console.WriteLine("10. Export to CSV");
                Console.WriteLine("11. Exit");
                Console.WriteLine("Enter your choice: ");

                if (! int.TryParse(Console.ReadLine(), out int choice))
                {
                    PrintError("Invalid input! Please enter a number.");
                    Pause();
                    continue;
                }

                try
                {
                    switch (choice)
                    {
                        case 1:

                            int id = GetValidStudentId(repo);
                            if (id == -1) return;

                            string name =GetValidName("Enter Name: ");

                            int age = GetValidAge();

                            string grade = GetValidGrade("Enter Grade (A/B/C): ");

                            string email = GetValidEmail();

                            repo.AddStudent(new Student(id, name, age, grade, email));

                            PrintSuccess("Student added Successfully!");

                            break;

                        case 2:
                            var students = repo.GetAllStudents();
                            DisplayStudents(students);

                            break;

                        case 3:

                            int searchId = ReadInt("Enter Student ID: ");
                            var found = repo.GetStudentById(searchId);

                            PrintError(found != null ? found.ToString() : "Student not found");
                            break;

                        case 4:
                            int updateId = GetExistingStudentId(repo);

                            string newName = GetValidName("Enter New Name: ");

                            int newAge = GetValidAge();

                            string newGrade = GetValidGrade("Enter Grade (A/B/C): ");

                            string newEmail = GetValidEmail();

                            repo.UpdateStudent(updateId, newName, newAge, newGrade, newEmail);

                            PrintSuccess("Student updated successfully!");
   
                            break;

                        case 5:

                            int deleteId = ReadInt("Enter ID to delete: ");

                            var student = repo.GetStudentById(deleteId);
                            if(student != null)
                            {
                                PrintWarning("Are you sure you want to delete? (Y/N): ");
                                string ?confirm = Console.ReadLine();

                                if (confirm?.ToUpper() == "Y")
                                {
                                    repo.DeleteStudent(deleteId);
                                    PrintSuccess("Student deleted successfully.");
                                }
                                else
                                {
                                    PrintWarning("Delete cancelled.");
                                }
                            }
                            else
                            {
                                
                                PrintError("Student not found");
                         

                            }

                            break;

                        case 6:
                            string searchName = GetValidName("Enter name to search: ");
                            var results = repo.SearchByName(searchName);

                            if (results.Count == 0)
                            {
                               
                               PrintError("Student not found");
                               
                            }
                            else
                            {
                                DisplayStudents(results); 
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
                           
                            string Grade = GetValidGrade("Enter Grade (A/B/C): ");

                            var filtered = repo.GetStudentByGrade(Grade);
                            if(filtered.Count == 0)
                            {
                                PrintError("No students found.");
                            }
                            else
                            {
                                DisplayStudents(filtered);
                            }
                            break;

                        case 10:
                            string path = "students.csv";
                            repo.ExportToCsv(path);
                            PrintSuccess("Data exported to students.csv");
                            break;

                        case 11:
                            PrintWarning("Exiting...");
                            return;


                        default:
                            PrintError("Invalid Choice");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    PrintError("Something went wrong: " + ex.Message);
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
                PrintError("Invalid number, try agian");
            }
        }

        //Helper method : Read String
        public static string ReadString(string message)
        {
            while(true)
            {
                Console.Write(message);
                string? input = Console.ReadLine();

                if(! string.IsNullOrWhiteSpace(input) )
                {
                    return input;
                }
                PrintWarning("Input cannot be empty.");
            }
        }

        //Display students
        public static void DisplayStudents(List<Student> students)
        {
            if (students.Count == 0)
            {
                PrintError("No students found.");
                return;
            }

            // Header
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("| ID   | Name       | Age | Grade | Email              |");
            Console.WriteLine("--------------------------------------------------------");
            Console.ResetColor();

            foreach (var s in students)
            {
                // Color based on Grade
                switch (s.Grade.ToUpper())
                {
                    case "A":
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                    case "B":
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    case "C":
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    default:
                        Console.ResetColor();
                        break;
                }

                Console.WriteLine(
                    $"| {s.Id,-4} | {s.Name,-10} | {s.Age,-3} | {s.Grade,-5} | {s.Email,-18} |"
                );

                Console.ResetColor();
            }

            Console.WriteLine("--------------------------------------------------------");

            // Student Count
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"Total Students: {students.Count}");
            Console.ResetColor();
        }

        //Valid Grade
        public static string GetValidGrade(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                string grade = Console.ReadLine().ToUpper().Trim();

                if (grade == "A" || grade == "B" || grade == "C")
                    return grade;
                PrintError("Invalid grade! Please enter A, B, or C only.");
            }
        }

        //Validation Id
        public static int GetValidStudentId(StudentRepository repo)
        {
            while (true)
            {
                Console.Write("Enter Id: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    PrintError("Invalid number. Try again.");
                    continue;
                }
                if (repo.StudentExists(id))
                {
                    PrintWarning("Id already exists. Try different ID.");
                }
                return id;
       
            }
        }

        //valid name
        public static string GetValidName(string message)
        {
            while (true)
            {
                Console.Write(message);
                string? name = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(name))
                {
                    PrintError("Name cannot be empty.");
                    continue;
                }

                // Check if name contains only letters and spaces
                if (!name.All(c => char.IsLetter(c) || c == ' '))
                {
                    PrintError("Name must contain only letters.");
                    continue;
                }

                return name.Trim();
            }
        }


        //validation Age
        public static int GetValidAge()
        {
            while (true)
            {
                Console.Write("Enter Age: ");

                if (!int.TryParse(Console.ReadLine(), out int age))
                {
                    PrintError("Invalid number. Try again.");
                    continue;
                }

                // validation 
                if (age >= 12 && age <= 25)
                {
                    return age;
                }

                PrintError("Age must be between 12 and 25.");
            }
        }

        //validation Email
        public static string GetValidEmail()
        {
            while (true)
            {
                Console.Write("Enter Email: ");
                string email = Console.ReadLine();

                // regex validation 
                string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

                if (System.Text.RegularExpressions.Regex.IsMatch(email, pattern))
                {
                    return email;
                }

                PrintError("Invalid email format. Try again.");
            }
        }

        //Update by checking existing Id
        public static int GetExistingStudentId(StudentRepository repo)
        {
            while (true)
            {
                Console.Write("Enter Student ID to update: ");

                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    PrintError("Invalid ID. Try again.");
                    continue;
                }

                var student = repo.GetStudentById(id);

                if (student == null)
                {
                    PrintError(" Student not found. Enter valid ID.");
                    continue;
                }

                return id;
            }
        }


        //Pause Screen
        static void Pause()
        {
            PrintWarning("\nPress any key to continue...");
            Console.ReadKey();
        }
        //success color
        public static void PrintSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan; // not green
            Console.WriteLine(message);
            Console.ResetColor();
        }

        //Error color
        public static void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        //warning color
        public static void PrintWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}