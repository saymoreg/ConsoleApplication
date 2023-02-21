using System;
namespace Core.Entities
{
	public class Student: BaseEntitiy
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Email { get; set; }
		public DateTime BirthDate { get; set; }

		public Group Group { get; set; }
		public int GroupId { get; set; }  // this line of code is not used in this project at all
	}
}

