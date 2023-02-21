using System;
using Core.Entities;

namespace Data.Repositories.Abstract
{
	public interface IGroupRepositories:IRepository<Group>
	{
        Group GetByName(string name);
    }
}

