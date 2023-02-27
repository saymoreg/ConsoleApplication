using System;
using System.Globalization;
using Core.Entities;
using Core.Helpers;
using Data.Repositories.Concrete;

namespace Presentation.Services
{
    public class TeacherService
    {
        private readonly TeacherRepository _teacherRepository;
        private readonly GroupService _groupService;
        private readonly GroupRepository _groupRepository;

        public TeacherService()
        {
            _teacherRepository = new TeacherRepository();
            _groupService = new GroupService();
            _groupRepository = new GroupRepository();
        }

        public void GetAll()
        {
            var teachers = _teacherRepository.GetAll();
            if (teachers.Count == 0)
            {
                ConsoleHelper.WriteWithColor("We Cant Find Any Teacher By This ID :(", ConsoleColor.Red);

            }
            foreach (var teacher in teachers)
            {
                ConsoleHelper.WriteWithColor($"Id: {teacher.Id} FullName: {teacher.Name} {teacher.Surname} Speciality: {teacher.Speciality}", ConsoleColor.DarkCyan);
                if (teacher.Groups.Count == 0)
                {
                    ConsoleHelper.WriteWithColor("Teacher has no groups", ConsoleColor.DarkCyan);

                }
                foreach (var group in teacher.Groups)
                {
                    ConsoleHelper.WriteWithColor($"Id: {group.Id} Name: {group.Name}", ConsoleColor.DarkCyan);

                }
            }
        }

        public void GetTeacherByGroup()
        {
            if (_teacherRepository.GetAll().Count != 0)
            {
            GroupIdDescription: _groupService.GetAll();
                if (!(_groupRepository.GetAll().Count == 0))
                {
                    ConsoleHelper.WriteWithColor("--- Enter Group ID ---", ConsoleColor.Red);

                    int id;
                    bool issucceeded = int.TryParse(Console.ReadLine(), out id);

                    if (!issucceeded)
                    {
                        ConsoleHelper.WriteWithColor("Invalid input...", ConsoleColor.Red);
                        goto GroupIdDescription;
                    }

                    var group = _groupRepository.Get(id);

                    if (group is null)
                    {
                        ConsoleHelper.WriteWithColor("There is no group in this id", ConsoleColor.Red);
                        goto GroupIdDescription;

                    }

                    ConsoleHelper.WriteWithColor($"{group.Teacher.Name} {group.Teacher.Surname}", ConsoleColor.DarkCyan);
                }
            }
            else
            {
                ConsoleHelper.WriteWithColor("there is no any teacher", ConsoleColor.Red);

            }
        }

        public void Create()
        {
            ConsoleHelper.WriteWithColor("--- Enter Teacher's Name ---", ConsoleColor.DarkCyan);
            string name = Console.ReadLine();
            ConsoleHelper.WriteWithColor("--- Enter Teacher's Surname ---", ConsoleColor.DarkCyan);
            string surname = Console.ReadLine();
            ConsoleHelper.WriteWithColor("--- Enter Teacher's Birthdate ---", ConsoleColor.DarkCyan);

        TeacherBirthDateDescription: ConsoleHelper.WriteWithColor("--- Enter Birth Date ---", ConsoleColor.DarkCyan);
            DateTime birthDate;
            bool isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthDate);

            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("--- Invalid Birth Date Format ---", ConsoleColor.Red);
                goto TeacherBirthDateDescription;
            }

            ConsoleHelper.WriteWithColor("--- Enter Teacher's Speciality ---", ConsoleColor.DarkCyan);
            string speciality = Console.ReadLine();

            var teacher = new Teacher
            {
                Name = name,
                Surname = surname,
                BirthDate = birthDate,
                Speciality = speciality,

            };

            _teacherRepository.Add(teacher);
            string teacherBirthDate = teacher.BirthDate.ToString("dddd, dd MMMM yyyy");
            ConsoleHelper.WriteWithColor($"Fullname: {teacher.Name} {teacher.Surname}, with speciality: {teacher.Speciality} and birthdate: {teacher.BirthDate} has been created!", ConsoleColor.DarkGreen);
        }

        public void Update()
        {

        IdDescription: GetAll();
            if (!(_teacherRepository.GetAll().Count == 0))
            {
                ConsoleHelper.WriteWithColor("--- Enter ID ---", ConsoleColor.DarkCyan);
                int id;
                bool issucceeded = int.TryParse(Console.ReadLine(), out id);
                if (!issucceeded)
                {
                    ConsoleHelper.WriteWithColor("Invalid input....", ConsoleColor.Red);
                    goto IdDescription;
                }
                var teacher = _teacherRepository.Get(id);
                if (teacher is null)
                {
                    ConsoleHelper.WriteWithColor("We cant find any teacher by this ID", ConsoleColor.Red);
                    goto IdDescription;
                }

                ConsoleHelper.WriteWithColor("--- Enter new name ---", ConsoleColor.DarkCyan);
                string name = Console.ReadLine();

                ConsoleHelper.WriteWithColor("--- Enter new surname ---", ConsoleColor.DarkCyan);
                string surname = Console.ReadLine();

            BirthDateDescription: ConsoleHelper.WriteWithColor("Enter the birthDate of the teacher", ConsoleColor.DarkCyan);
                DateTime birthDate;
                bool issuccceed = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthDate);
                if (!issuccceed)
                {
                    ConsoleHelper.WriteWithColor("Invalid input...", ConsoleColor.Red);
                    goto BirthDateDescription;
                }

                ConsoleHelper.WriteWithColor("Enter new speciality", ConsoleColor.DarkCyan);
                string speciality = Console.ReadLine();

                teacher.Name = name;
                teacher.Surname = surname;
                teacher.BirthDate = birthDate;
                teacher.Speciality = speciality;

                _teacherRepository.Update(teacher);

                ConsoleHelper.WriteWithColor($"{teacher.Name} {teacher.Surname} is successfully updated", ConsoleColor.Green);
            }

        }

        public void Delete()
        {
        TeacherDescription: _teacherRepository.GetAll();

            if (_teacherRepository.GetAll().Count == 0)
            {
                ConsoleHelper.WriteWithColor("We cant find any teacher in base :(", ConsoleColor.Red);
            }
            else
            {
                ConsoleHelper.WriteWithColor("--- Enter teacher ID ---", ConsoleColor.DarkCyan);
                int id;
                bool issucceeded = int.TryParse(Console.ReadLine(), out id);
                if (!issucceeded)
                {
                    ConsoleHelper.WriteWithColor("Invalid input...", ConsoleColor.Red);
                    goto TeacherDescription;
                }

                var teacher = _teacherRepository.Get(id);
                if (teacher is null)
                {
                    ConsoleHelper.WriteWithColor("We cant find any teacher by this ID....", ConsoleColor.Red);
                    goto TeacherDescription;
                }
                _teacherRepository.Delete(teacher);
                ConsoleHelper.WriteWithColor($"{teacher.Name} {teacher.Surname} is deleted successfully", ConsoleColor.Green);

            }
        }
    }
}

