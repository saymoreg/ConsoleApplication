using System.Globalization;
using Core.Constants;
using Core.Entities;
using Core.Helpers;
using Data.Repositories.Concrete;

namespace Presentation
{
    public static class Program
    {
        static void Main()
        {
            GroupRepository _groupRepository = new GroupRepository();

            ConsoleHelper.WriteWithColor("--- Welcome! ---", ConsoleColor.DarkCyan);

            while (true)
            {
                ConsoleHelper.WriteWithColor("(1) - Create Group", ConsoleColor.DarkYellow);
                ConsoleHelper.WriteWithColor("(2) - Update Group", ConsoleColor.DarkYellow);
                ConsoleHelper.WriteWithColor("(3) - Delete Group", ConsoleColor.DarkYellow);
                ConsoleHelper.WriteWithColor("(4) - Get All Groups", ConsoleColor.DarkYellow);
                ConsoleHelper.WriteWithColor("(5) - Get Group By ID", ConsoleColor.DarkYellow);
                ConsoleHelper.WriteWithColor("(6) - Get Group By Name", ConsoleColor.DarkYellow);
                ConsoleHelper.WriteWithColor("(0) - Exit", ConsoleColor.DarkYellow);

                ConsoleHelper.WriteWithColor("--- Select Option ---", ConsoleColor.DarkCyan);

                int number;

                bool isSucceeded = int.TryParse(Console.ReadLine(), out number);
                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("Invalid Input :(", ConsoleColor.Red);
                }
                else
                {
                    if (!(number >= 0 && number <= 6))
                    {
                        ConsoleHelper.WriteWithColor("Entered Option Is Not Shown In the Menu", ConsoleColor.Red);
                    }
                    else
                    {
                        switch (number)
                        {
                            case (int)GroupOptions.CreateGroup:

                                ConsoleHelper.WriteWithColor(" --- Enter Group Name: ", ConsoleColor.DarkCyan);
                                string name = Console.ReadLine();

                                int maxSize;

                            MaxSizeDescription: ConsoleHelper.WriteWithColor(" --- Enter Group Max Size: ", ConsoleColor.DarkCyan);
                                isSucceeded = int.TryParse(Console.ReadLine(), out maxSize);

                                if (!isSucceeded)
                                {
                                    ConsoleHelper.WriteWithColor("Invalid Max Size Input!", ConsoleColor.Red);
                                    goto MaxSizeDescription;
                                }
                                if (maxSize > 18)
                                {
                                    ConsoleHelper.WriteWithColor("Entered group max size is out of range");
                                    goto MaxSizeDescription;
                                }

                            StartDateDescription: ConsoleHelper.WriteWithColor("--- Enter start date: ", ConsoleColor.DarkCyan);
                                DateTime startDate;
                                isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);

                                if (!isSucceeded)
                                {
                                    ConsoleHelper.WriteWithColor("Invalid date input", ConsoleColor.Red);
                                    goto StartDateDescription;
                                }

                                DateTime boundaryDate = new DateTime(2015, 1, 1);
                                if (startDate > boundaryDate)
                                {
                                    ConsoleHelper.WriteWithColor("Entered date is not chosen right :( ", ConsoleColor.Red);
                                    goto StartDateDescription;
                                }

                            EndDateDescription: ConsoleHelper.WriteWithColor("--- Enter start date: ", ConsoleColor.DarkCyan);
                                DateTime endDate;
                                isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate);

                                if (!isSucceeded)
                                {
                                    ConsoleHelper.WriteWithColor("Invalid date input", ConsoleColor.Red);
                                    goto EndDateDescription;
                                }

                                if (startDate > endDate)
                                {
                                    ConsoleHelper.WriteWithColor("End date have to be bigger than boundary date", ConsoleColor.Red);
                                }

                                var group = new Group
                                {
                                    Name = name,
                                    MaxSize = maxSize,
                                    StartDate = startDate,
                                    EndDate = endDate,

                                };

                                _groupRepository.Add(group);
                                ConsoleHelper.WriteWithColor($"Group Succesfully created!\n Name: {group.Name} \n Max size: {group.MaxSize} \n Start Date: {group.StartDate.ToShortDateString()} \n End Date: {group.EndDate}");
                                break;
                            case (int)GroupOptions.UpdateGroup:
                                break;
                            case (int)GroupOptions.DeleteGroup:
                                var groupss = _groupRepository.GetAll();

                                ConsoleHelper.WriteWithColor("--- All existing groups ---", ConsoleColor.DarkCyan);

                                foreach (var group_ in groupss)
                                {
                                    ConsoleHelper.WriteWithColor($"Group ID: {group_.Id} \n Name: {group_.Name} \n Max size: {group_.MaxSize} \n Start Date: {group_.StartDate.ToShortDateString()} \n End Date: {group_.EndDate}", ConsoleColor.Magenta);
                                }

                            IdDescription: ConsoleHelper.WriteWithColor("--- Enter ID ---", ConsoleColor.DarkCyan);

                                int id;
                                isSucceeded = int.TryParse(Console.ReadLine(), out id);

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
                                    _groupRepository.Delete(dbgroup);
                                    ConsoleHelper.WriteWithColor("Group Succesfully Deleted!", ConsoleColor.Green);
                                }
                                break;
                            case (int)GroupOptions.GetAllGroups:
                                var groups = _groupRepository.GetAll();

                                ConsoleHelper.WriteWithColor("--- All existing groups ---", ConsoleColor.DarkCyan);

                                foreach (var group_ in groups)
                                {
                                    ConsoleHelper.WriteWithColor($"Group ID: {group_.Id} \n Name: {group_.Name} \n Max size: {group_.MaxSize} \n Start Date: {group_.StartDate.ToShortDateString()} \n End Date: {group_.EndDate}", ConsoleColor.Magenta);
                                }
                                break;
                            case (int)GroupOptions.GetGroupByID:
                                break;
                            case (int)GroupOptions.GetGroupByName:
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
    }
}