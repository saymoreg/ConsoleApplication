using System;
using Core.Helpers;
using Data.Repositories.Concrete;
using System.Globalization;
using Core.Entities;
using System.Xml.Linq;

namespace Presentation.Services
{
    public class GroupService
    {
        private readonly GroupRepository _groupRepository;
        private readonly StudentRepository _studentRepository;
        private readonly TeacherRepository _teacherRepository;
        public GroupService()
        {
            _groupRepository = new GroupRepository();
            _studentRepository = new StudentRepository();
            _teacherRepository = new TeacherRepository();
        }

        public void GetAll()
        {
            var groups = _groupRepository.GetAll();

            ConsoleHelper.WriteWithColor("--- All existing groups ---", ConsoleColor.DarkCyan);

            foreach (var group in groups)
            {
                ConsoleHelper.WriteWithColor($"Group ID: {group.Id} \n Name: {group.Name} \n Max size: {group.MaxSize} \n Start Date: {group.StartDate.ToShortDateString()} \n End Date: {group.EndDate}", ConsoleColor.Magenta);
            }
        }
        public void GetAllGroupsByTeacher()
        {
            var teachers = _teacherRepository.GetAll();
            foreach (var teacher in teachers)
            {
                ConsoleHelper.WriteWithColor($"Id: {teacher.Id} Name: {teacher.Name}", ConsoleColor.DarkCyan);

            }
        TeacherIdDescription: ConsoleHelper.WriteWithColor("Enter teacher id", ConsoleColor.DarkCyan);

            int id;
            bool isSucceded = int.TryParse(Console.ReadLine(), out id);
            if (!isSucceded)
            {
                ConsoleHelper.WriteWithColor("Invalid input", ConsoleColor.Red);
                goto TeacherIdDescription;
            }

            var dbTeacher = _teacherRepository.Get(id);
            if (dbTeacher is null)
            {
                ConsoleHelper.WriteWithColor("There is no teacher in this id", ConsoleColor.Red);
            }

            else
            {
                foreach (var group in dbTeacher.Groups)
                {
                    ConsoleHelper.WriteWithColor($"Id: {group.Id} Name: {group.Name}", ConsoleColor.DarkCyan);

                }
            }
        }
        public void GetGroupById()
        {
            var groups = _groupRepository.GetAll();

            if (groups.Count == 0)  // group count can be 0 so that we need to give the option to create the new group
            {
            AreYouSureDescription: ConsoleHelper.WriteWithColor("We cant find any group in base :(\n Do you want to create new group?\n --- (y) or (n) ---", ConsoleColor.Red);
                char decision;
                bool isSucceededResult = char.TryParse(Console.ReadLine(), out decision);
                if (!isSucceededResult)   // logic to clarify if this is correct FORMAT or not
                {
                    ConsoleHelper.WriteWithColor("Invalid format :/", ConsoleColor.Red);
                }

                if (!(decision == 'y' || decision == 'n'))  // logic to clarify if this is correct INPUT or wrong
                {
                    ConsoleHelper.WriteWithColor("Invalid choise");
                    goto AreYouSureDescription;
                }

                if (decision == 'y')
                {
                    Create();
                }
            }
            else
            {
                GetAll();
            EnterIdDescription: ConsoleHelper.WriteWithColor("--- Enter ID ---", ConsoleColor.DarkCyan);
                int id;
                bool isSucceeded = int.TryParse(Console.ReadLine(), out id);
                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("Invalid ID format :/");
                    goto EnterIdDescription;
                }

                var group = _groupRepository.Get(id);
                if (group is null)
                {
                    ConsoleHelper.WriteWithColor("I cant find any group by this ID :(", ConsoleColor.Red);
                }
                ConsoleHelper.WriteWithColor($"Group ID: {group.Id} \n Name: {group.Name} \n Max size: {group.MaxSize} \n Start Date: {group.StartDate.ToShortDateString()} \n End Date: {group.EndDate}", ConsoleColor.Magenta);

            }

        }
        public void GetGroupByName()
        {
            GetAll();

            ConsoleHelper.WriteWithColor("Enter group name", ConsoleColor.DarkCyan);
            string name = Console.ReadLine();

            var group = _groupRepository.GetByName(name.ToLower());

            if (group is null)
            {
            AreYouSureDescription: ConsoleHelper.WriteWithColor("We cant find any group in base by this name :(\n Do you want to create new group?\n--- (y) or (n) ---", ConsoleColor.Red);
                char decision;
                bool isSucceededResult = char.TryParse(Console.ReadLine(), out decision);
                if (!isSucceededResult)   // logic to clarify if this is correct FORMAT or not
                {
                    ConsoleHelper.WriteWithColor("Invalid format :/", ConsoleColor.Red);
                }

                if (!(decision == 'y' || decision == 'n'))  // logic to clarify if this is correct INPUT or wrong
                {
                    ConsoleHelper.WriteWithColor("--- Invalid choise ---");
                    goto AreYouSureDescription;
                }

                if (decision == 'y')
                {
                    Create();
                }
                ConsoleHelper.WriteWithColor($"Group ID: {group.Id} \n Name: {group.Name} \n Max size: {group.MaxSize} \n Start Date: {group.StartDate.ToShortDateString()} \n End Date: {group.EndDate}", ConsoleColor.Magenta);
            }
        }
        public void Create()
        {
            if (_teacherRepository.GetAll().Count == 0)
            {
                ConsoleHelper.WriteWithColor("You should create a teacher first!", ConsoleColor.Red);
            }
            else
            {
            NameDescription: ConsoleHelper.WriteWithColor(" --- Enter Group Name ---", ConsoleColor.DarkCyan);
                string name = Console.ReadLine();

                var group = _groupRepository.GetByName(name);

                if (group is not null)
                {
                    ConsoleHelper.WriteWithColor("This Group Is Already Added", ConsoleColor.Red);
                    goto NameDescription;
                }

                int maxSize;

            MaxSizeDescription: ConsoleHelper.WriteWithColor("--- Enter Group Max Size ---", ConsoleColor.DarkCyan);
                bool isSucceeded = int.TryParse(Console.ReadLine(), out maxSize);

                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("--- Invalid Max Size Input ---", ConsoleColor.Red);
                    goto MaxSizeDescription;
                }
                if (maxSize > 18)
                {
                    ConsoleHelper.WriteWithColor("--- Entered group max size is out of range ---", ConsoleColor.DarkCyan);
                    goto MaxSizeDescription;
                }

            StartDateDescription: ConsoleHelper.WriteWithColor("--- Enter start date ---", ConsoleColor.DarkCyan);
                DateTime startDate;
                isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);

                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("--- Invalid date input ---", ConsoleColor.Red);
                    goto StartDateDescription;
                }

                DateTime boundaryDate = new DateTime(2015, 1, 1);
                if (startDate < boundaryDate)
                {
                    ConsoleHelper.WriteWithColor("Entered date is not chosen right :( ", ConsoleColor.Red);
                    goto StartDateDescription;
                }

            EndDateDescription: ConsoleHelper.WriteWithColor("--- Enter end date ---", ConsoleColor.DarkCyan);
                DateTime endDate;
                isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate);

                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("--- Invalid date input ---", ConsoleColor.Red);
                    goto EndDateDescription;
                }

                if (startDate > endDate)
                {
                    ConsoleHelper.WriteWithColor("--- End date have to be bigger than boundary date ---", ConsoleColor.Red);
                }

                var teachers = _groupRepository.GetAll();

                foreach (var teacher in teachers)
                {
                    ConsoleHelper.WriteWithColor($"ID: {teacher.Id}, Name: {teacher.Name}");
                }

            TeacherIdDescription: ConsoleHelper.WriteWithColor("enter teacher id", ConsoleColor.Cyan);
                int teacherId;
                isSucceeded = int.TryParse(Console.ReadLine(), out teacherId);
                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("Invalid input", ConsoleColor.Red);
                    goto TeacherIdDescription;
                }
                var dbTeacher = _teacherRepository.Get(teacherId);
                if (dbTeacher is null)
                {
                    ConsoleHelper.WriteWithColor("no any teacher in this id", ConsoleColor.Red);
                    goto TeacherIdDescription;
                }

                group = new Group

                {
                    Name = name,
                    MaxSize = maxSize,
                    StartDate = startDate,
                    EndDate = endDate,
                    Teacher = dbTeacher
                };


                group = new Group
                {
                    Name = name,
                    MaxSize = maxSize,
                    StartDate = startDate,
                    EndDate = endDate,

                };

                dbTeacher.Groups.Add(group);
                _groupRepository.Add(group);
                ConsoleHelper.WriteWithColor($"Group Succesfully created!\n Name: {group.Name} \n Max size: {group.MaxSize} \n Start Date: {group.StartDate.ToShortDateString()} \n End Date: {group.EndDate}");
            }
        }
        public void Update()
        {
            GetAll();

        EnterGroupDescription: ConsoleHelper.WriteWithColor("--- Enter the (1)- ID or (2)- Name ---", ConsoleColor.DarkCyan);

            int number;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out number);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Invalid format :/", ConsoleColor.Red);
                goto EnterGroupDescription;
            }

            if (!(number == 1 || number == 2))
            {
                ConsoleHelper.WriteWithColor("Invalid choise ;(", ConsoleColor.Red);
                goto EnterGroupDescription;
            }

            if (number == 1)  // getting group and updating it by id
            {
                ConsoleHelper.WriteWithColor("--- Enter group ID ---", ConsoleColor.DarkCyan);
                int id;
                isSucceeded = int.TryParse(Console.ReadLine(), out id);
                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("Invalid format", ConsoleColor.Red);
                    goto EnterGroupDescription;
                }

                var group = _groupRepository.Get(id);
                if (group is null)
                {
                    ConsoleHelper.WriteWithColor("I cant find any group by this ID", ConsoleColor.Red);
                }
                else
                {
                    ConsoleHelper.WriteWithColor("Enter new name:");
                    string name = Console.ReadLine();

                MaxSizeDescription: ConsoleHelper.WriteWithColor("--- Enter New Start Date ---", ConsoleColor.DarkCyan);
                    int maxSize;
                    isSucceeded = int.TryParse(Console.ReadLine(), out maxSize);
                    if (!isSucceeded)  // checking right format of inputed value
                    {
                        ConsoleHelper.WriteWithColor("Invalid Max Size format :(", ConsoleColor.Red);
                        goto MaxSizeDescription;
                    }

                    if (maxSize > 18)  // checking if new range is out of range or not
                    {
                        ConsoleHelper.WriteWithColor("Entered New Date Is Out Of Range :/", ConsoleColor.Red);
                        goto MaxSizeDescription;
                    }

                StartDateDescription: ConsoleHelper.WriteWithColor("Enter new Start Date");
                    DateTime startDate;
                    isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);
                    if (!isSucceeded)
                    {
                        ConsoleHelper.WriteWithColor("Invalid start date format", ConsoleColor.Red);
                        goto StartDateDescription;
                    }

                EndDateDescription: ConsoleHelper.WriteWithColor("Enter new Start Date");
                    DateTime endDate;
                    isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate);
                    if (!isSucceeded)
                    {
                        ConsoleHelper.WriteWithColor("Invalid start date format", ConsoleColor.Red);
                        goto EndDateDescription;
                    }

                    group.Name = name;
                    group.MaxSize = maxSize;
                    group.StartDate = startDate;
                    group.EndDate = endDate;

                    _groupRepository.Update(group);
                }
            }
            else
            {
            EnterGroupNameDescription: ConsoleHelper.WriteWithColor("--- Enter Group Name ---", ConsoleColor.DarkCyan);

                string name = Console.ReadLine();

                var group = _groupRepository.GetByName(name);
                if (group is null)
                {
                    ConsoleHelper.WriteWithColor("I Cant Find Any Group By This Name", ConsoleColor.Red);
                }
                else
                {
                    ConsoleHelper.WriteWithColor("--- Enter New Name ---", ConsoleColor.DarkCyan);
                    string newName = Console.ReadLine();

                MaxSizeDescription: ConsoleHelper.WriteWithColor("--- Enter New Start Date ---", ConsoleColor.DarkCyan);
                    int maxSize;
                    isSucceeded = int.TryParse(Console.ReadLine(), out maxSize);
                    if (!isSucceeded)  // checking right format of inputed value
                    {
                        ConsoleHelper.WriteWithColor("Invalid Max Size format :(", ConsoleColor.Red);
                        goto MaxSizeDescription;
                    }

                    if (maxSize > 18)  // checking if new range is out of range or not
                    {
                        ConsoleHelper.WriteWithColor("Entered New Date Is Out Of Range :/", ConsoleColor.Red);
                        goto MaxSizeDescription;
                    }

                StartDateDescription: ConsoleHelper.WriteWithColor("--- Enter New Start Date ---");
                    DateTime startDate;
                    isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);
                    if (!isSucceeded)
                    {
                        ConsoleHelper.WriteWithColor("--- Invalid Start Date Format ---", ConsoleColor.Red);
                        goto StartDateDescription;
                    }

                EndDateDescription: ConsoleHelper.WriteWithColor("--- Enter new Start Date ---");
                    DateTime endDate;
                    isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate);
                    if (!isSucceeded)
                    {
                        ConsoleHelper.WriteWithColor("--- Invalid Start Date Format ---", ConsoleColor.Red);
                        goto EndDateDescription;
                    }

                    group.Name = name;
                    group.MaxSize = maxSize;
                    group.StartDate = startDate;
                    group.EndDate = endDate;

                    _groupRepository.Update(group);
                }
            }

        }
        private void InternalUpdate()
        {
            string name = Console.ReadLine();
            var group = _groupRepository.GetByName(name);
            if (group is null)
            {
                ConsoleHelper.WriteWithColor("I cant find any group by this ID", ConsoleColor.Red);
            }
            else
            {
                ConsoleHelper.WriteWithColor("Enter new name:");
                name = Console.ReadLine();

            MaxSizeDescription: int maxSize;
                bool isSucceeded = int.TryParse(Console.ReadLine(), out maxSize);
                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("Invalid Max Size format :(", ConsoleColor.Red);
                    goto MaxSizeDescription;
                }

                if (maxSize > 18)
                {
                    ConsoleHelper.WriteWithColor("Inputed Group Size is out of range :/");
                    goto MaxSizeDescription;
                }

            StartDateDescription: ConsoleHelper.WriteWithColor("Enter new Start Date");
                DateTime startDate;
                isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);
                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("Invalid start date format", ConsoleColor.Red);
                    goto StartDateDescription;
                }

            EndDateDescription: ConsoleHelper.WriteWithColor("Enter new Start Date");
                DateTime endDate;
                isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate);
                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("Invalid start date format", ConsoleColor.Red);
                    goto EndDateDescription;
                }

                group.Name = name;
                group.MaxSize = maxSize;
                group.StartDate = startDate;
                group.EndDate = endDate;

                _groupRepository.Update(group);
            }
        }
        public void Delete()
        {
            GetAll();

        IdDescription: ConsoleHelper.WriteWithColor("--- Enter ID ---", ConsoleColor.DarkCyan);

            int id;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out id);

            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Invalid ID :(", ConsoleColor.Red);
                goto IdDescription;
            }

            var dbgroup = _groupRepository.Get(id);
            if (dbgroup is null)
            {
                ConsoleHelper.WriteWithColor("There is no group with this ID :/ ", ConsoleColor.Red);
            }
            else
            {
                foreach (var student in dbgroup.Students)
                {
                    student.Group = null;
                    _studentRepository.Update(student);
                }
                _groupRepository.Delete(dbgroup);
                ConsoleHelper.WriteWithColor("Group Succesfully Deleted!", ConsoleColor.Green);
            }
        }
    }
}