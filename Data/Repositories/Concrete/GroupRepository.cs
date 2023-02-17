﻿using System;
using Core.Entities;
using Data.Contexts;
using Data.Repositories.Abstract;

namespace Data.Repositories.Concrete
{
    public class GroupRepository : IGroupRepositories
    {
        static int id;
        public List<Group> GetAll()
        {
            return DbContext.Groups;
        }

        public Group Get(int id)
        {
            return DbContext.Groups.FirstOrDefault(g => g.Id == id);
        }

        public Group GetByName(string name)
        {
            return DbContext.Groups.FirstOrDefault(g => g.Name.ToLower() == name.ToLower());
        }

        public void Add(Group group)
        {
            id++;
            group.Id = id;
            group.CreatedAt = DateTime.Now;
            DbContext.Groups.Add(group);
        }

        public void Update(Group group)
        {
            var dbGroup = DbContext.Groups.FirstOrDefault(g => group.Id == group.Id);
            if (dbGroup is not null)
            {
                dbGroup.Name = group.Name;
                dbGroup.MaxSize = group.MaxSize;
                dbGroup.StartDate = group.StartDate;
                dbGroup.EndDate = group.EndDate;
                dbGroup.ModifiedAt = DateTime.Now;
            }
        }

        public void Delete(Group group)
        {
            DbContext.Groups.Remove(group);
        }
    }
}

