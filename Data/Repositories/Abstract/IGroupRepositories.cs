using System;
using Core.Entities;

namespace Data.Repositories.Abstract
{
	public interface IGroupRepositories
	{
		List<Group> GetAll();
		Group Get(int id);
		Group GetByName(string name);
		void Add(Group group);
		void Update(Group group);
		void Delete(Group group);
	}
}

