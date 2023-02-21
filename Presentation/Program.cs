using System.Globalization;
using Core.Constants;
using Core.Entities;
using Core.Helpers;
using Data.Repositories.Concrete;
using Presentation.Services;

namespace Presentation
{
    public static class Program
    {
        readonly static GroupService _groupService;
        readonly static StudentService _studentService;

        static Program()
        {
            _groupService = new GroupService();
            _studentService = new StudentService();
        }

        static void Main()
        {
        MainMenuDescription: ConsoleHelper.WriteWithColor("--- Welcome! ---", ConsoleColor.DarkCyan);

            while (true)
            {
                ConsoleHelper.WriteWithColor("(1) - Groups", ConsoleColor.DarkYellow);
                ConsoleHelper.WriteWithColor("(2) - Students", ConsoleColor.DarkYellow);
                ConsoleHelper.WriteWithColor("(0) - Exit", ConsoleColor.DarkYellow);

                int number;

                bool isSucceeded = int.TryParse(Console.ReadLine(), out number);
                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("Invalid Input :(", ConsoleColor.Red);
                    goto MainMenuDescription;
                }
                else
                {
                    switch (number)
                    {
                        case (int)MainMenuOptions.Students:
                            while (true)
                            {
                                StudentMenuDescription: ConsoleHelper.WriteWithColor("(1) - Create Student", ConsoleColor.DarkYellow);
                                ConsoleHelper.WriteWithColor("(2) - Update Student", ConsoleColor.DarkYellow);
                                ConsoleHelper.WriteWithColor("(3) - Delete Student", ConsoleColor.DarkYellow);
                                ConsoleHelper.WriteWithColor("(4) - Get All Students", ConsoleColor.DarkYellow);
                                ConsoleHelper.WriteWithColor("(5) - Get All Students By Group", ConsoleColor.DarkYellow);
                                ConsoleHelper.WriteWithColor("(0) - Back To Main Menu", ConsoleColor.DarkYellow);

                                isSucceeded = int.TryParse(Console.ReadLine(), out number);
                                if (!isSucceeded)
                                {
                                    ConsoleHelper.WriteWithColor("Invalid Input :(", ConsoleColor.Red);
                                }
                                else
                                {
                                    switch (number)
                                    {
                                        case (int)StudentOptions.CreateStudent:
                                            _studentService.Create();
                                            break;
                                        case (int)StudentOptions.UpdateStudent:
                                            break;
                                        case (int)StudentOptions.DeleteStudent:
                                            _studentService.Delete();
                                            break;
                                        case (int)StudentOptions.GetAllStudents:
                                            _studentService.GetAll();
                                            break;
                                        case (int)StudentOptions.GetAllStudentsByGroup:
                                            _studentService.GetAllByGroup();
                                            break;
                                        case (int)StudentOptions.BackToMainMenu:
                                            goto StudentMenuDescription;
                                        default:
                                            ConsoleHelper.WriteWithColor("Invalid Input :(", ConsoleColor.Red);
                                            goto StudentMenuDescription;
                                    }
                                }
                            }
                        case (int)MainMenuOptions.Groups:
                            while (true)
                            {
                            GroupDescription: ConsoleHelper.WriteWithColor("(1) - Create Group", ConsoleColor.DarkYellow);
                                ConsoleHelper.WriteWithColor("(2) - Update Group", ConsoleColor.DarkYellow);
                                ConsoleHelper.WriteWithColor("(3) - Delete Group", ConsoleColor.DarkYellow);
                                ConsoleHelper.WriteWithColor("(4) - Get All Groups", ConsoleColor.DarkYellow);
                                ConsoleHelper.WriteWithColor("(5) - Get Group By ID", ConsoleColor.DarkYellow);
                                ConsoleHelper.WriteWithColor("(6) - Get Group By Name", ConsoleColor.DarkYellow);
                                ConsoleHelper.WriteWithColor("(0) - Back To Main Menu", ConsoleColor.DarkYellow);
                                ConsoleHelper.WriteWithColor("--- Select Option ---", ConsoleColor.DarkCyan);

                                isSucceeded = int.TryParse(Console.ReadLine(), out number);
                                if (!isSucceeded)
                                {
                                    ConsoleHelper.WriteWithColor("Invalid Input :(", ConsoleColor.Red);
                                }
                                else
                                {
                                    switch (number)
                                    {
                                        case (int)GroupOptions.CreateGroup:
                                            _groupService.Create();
                                            break;
                                        case (int)GroupOptions.UpdateGroup:
                                            _groupService.Update();
                                            break;
                                        case (int)GroupOptions.DeleteGroup:
                                            _groupService.Delete();
                                            break;
                                        case (int)GroupOptions.GetAllGroups:
                                            _groupService.GetAll();
                                            break;
                                        case (int)GroupOptions.GetGroupByID:
                                            _groupService.GetGroupById();
                                            break;
                                        case (int)GroupOptions.GetGroupByName:
                                            _groupService.GetGroupByName();
                                            break;
                                        case (int)GroupOptions.BackToMainMenu:
                                            goto MainMenuDescription;
                                        default:
                                            ConsoleHelper.WriteWithColor("Invalid Input :(", ConsoleColor.Red);
                                            goto GroupDescription;
                                    }
                                }
                            }
                        case (int)MainMenuOptions.Exit:
                            return;
                        default:
                            ConsoleHelper.WriteWithColor("Invalid Input :(", ConsoleColor.Red);
                            goto MainMenuDescription;
                    }
                }
            }
        }
    }
}