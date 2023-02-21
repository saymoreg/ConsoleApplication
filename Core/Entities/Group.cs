using System;
namespace Core.Entities
{
	public class Group : BaseEntitiy
	{
        public Group()
        {
            Students = new List<Student>(); // for list to not be a NULL
        }
        public string Name { get; set; }
        public int MaxSize { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Student> Students { get; set; }
    }
}

