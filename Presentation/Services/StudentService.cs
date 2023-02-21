using System;
using System.Globalization;
using Core.Entities;
using Core.Extentions;
using Core.Helpers;
using Data.Repositories.Concrete;

namespace Presentation.Services
{
    public class StudentService
    {
        private readonly GroupService _groupService;
        private readonly GroupRepository _groupRepository;
        private readonly StudentRepository _studentRepository;

        public StudentService()
        {
            _groupService = new GroupService();
            _groupRepository = new GroupRepository();
            _studentRepository = new StudentRepository();
        }
        public void GetAll()
        {
            var students = _studentRepository.GetAll();

            ConsoleHelper.WriteWithColor("--- All Students ---", ConsoleColor.DarkCyan);

            foreach (var student in students)
            {
                ConsoleHelper.WriteWithColor($"ID = {student.Id}\n Fullname: {student.Name} {student.Surname}\n Email: {student.Email}\n Group: {student.Group?.Name} ");  // student.Group?.Name will give us empty space if there is no existed group
            }
        }
        public void GetAllByGroup()
        {
            _groupService.GetAll();

        GroupDescription: ConsoleHelper.WriteWithColor("--- Enter Group ID ---", ConsoleColor.DarkCyan);

            int groupId;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out groupId);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Invalid Group ID Format :(", ConsoleColor.Red);
                goto GroupDescription;
            }

            var group = _groupRepository.Get(groupId);
            if (group is null)
            {
                ConsoleHelper.WriteWithColor("We Cant Find Any Group By This ID :/", ConsoleColor.Red);
            }

            if (group.Students.Count == 0)
            {
                ConsoleHelper.WriteWithColor("Cant Find Any Group In The Created List :(");
            }
            else
            {
                foreach (var student in group.Students)
                {
                    ConsoleHelper.WriteWithColor($"ID = {student.Id}\n Fullname: {student.Name} {student.Surname}\n Email: {student.Email}");  // student.Group?.Name will give us empty space if there is no existed group
                }
            }
        }
        public void Create()
        {
            if (_groupRepository.GetAll().Count == 0)
            {
                ConsoleHelper.WriteWithColor("Firstly, You Need To Create Group!", ConsoleColor.Red);
                return;
            }

            ConsoleHelper.WriteWithColor("--- Enter Student Name ---", ConsoleColor.DarkCyan);
            string name = Console.ReadLine();
            ConsoleHelper.WriteWithColor("--- Enter Student Surname ---", ConsoleColor.DarkCyan);
            string surname = Console.ReadLine();
        EmailDescription: ConsoleHelper.WriteWithColor("--- Enter Student Email ---", ConsoleColor.DarkCyan);
            string email = Console.ReadLine();

            if (email.IsEmail())  // checking if it is in a correct format or not
            {
                ConsoleHelper.WriteWithColor("Invalid E-Mail Format :/", ConsoleColor.Red);
                goto EmailDescription;
            }

            if (_studentRepository.IsDublicateEmail(email)) // it shows us weather it is dublicate or not
            {
                ConsoleHelper.WriteWithColor("--- This E-Mail Is Already Used ---");
                goto EmailDescription;
            }

        BirthDateDescription: ConsoleHelper.WriteWithColor("--- Enter Birth Date ---", ConsoleColor.DarkCyan);
            DateTime birthDate;
            bool isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthDate);

            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("--- Invalid Birth Date Format ---", ConsoleColor.Red);
                goto BirthDateDescription;
            }

            _groupService.GetAll();

        GroupDescription: ConsoleHelper.WriteWithColor("--- Enter Group ID ---", ConsoleColor.DarkCyan);
            int groupId;
            isSucceeded = int.TryParse(Console.ReadLine(), out groupId);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Invalid ID Format :(", ConsoleColor.Red);
                goto GroupDescription;
            }

            var group = _groupRepository.Get(groupId);
            if (group is null)
            {
                ConsoleHelper.WriteWithColor("We Could Not Find Any Group By This ID :( ", ConsoleColor.Red);
                goto GroupDescription;
            }

            if (group.MaxSize <= group.Students.Count) // it checks if there is any available place in the group
            {
                ConsoleHelper.WriteWithColor("This Group Is Already Full", ConsoleColor.Red);
            }

            var student = new Student  // we creating it to not get a NULL
            {
                Name = name,
                Surname = surname,
                Email = email,
                BirthDate = birthDate,
                Group = group,
                GroupId = group.Id
            };

            group.Students.Add(student);  // to tell the group which student will be added to the list

            _studentRepository.Add(student); // adding student information to data
            ConsoleHelper.WriteWithColor($"{student.Name} {student.Surname} is succesfuly created :# ", ConsoleColor.Green);
        }
        public void Delete()
        {
            GetAll();

        EnterIdDescription: ConsoleHelper.WriteWithColor("--- Enter ID ---", ConsoleColor.DarkCyan);
            int id;
            bool isSecceeded = int.TryParse(Console.ReadLine(), out id);
            if (!isSecceeded)
            {
                ConsoleHelper.WriteWithColor("Invalid ID Format:( ", ConsoleColor.Red);
                goto EnterIdDescription;
            }

            var student = _studentRepository.Get(id);
            if (student is null)
            {
                ConsoleHelper.WriteWithColor("We Cant Find Any Student By This ID :(", ConsoleColor.Red);
            }
            else
            {
                _studentRepository.Delete(student);
                ConsoleHelper.WriteWithColor($"{student.Name} {student.Surname} is succesfuly deleted :)", ConsoleColor.Green);
            }
        }
    }
}

