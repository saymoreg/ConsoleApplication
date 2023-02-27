using System;
namespace Core.Entities
{
	public class Teacher : BaseEntitiy
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public DateTime BirthDate { get; set; }
		public string Speciality { get; set; }
		public List<Group> Groups { get; set; }
	}
}

