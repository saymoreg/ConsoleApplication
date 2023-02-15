using System;
using Core.Entities;

namespace Data.Repositories.Abstract
{
	public interface IGroupRepositories
	{
		List<Group> GetAll();
		Group Get(int id);
		void Add(Group group);
		void Update(Group group);
		void Delete(Group group);
	}
}

