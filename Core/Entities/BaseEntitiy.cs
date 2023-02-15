using System;
namespace Core.Entities
{
	public abstract class BaseEntitiy
	{
		public int Id { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime ModifiedAt { get; set; }
	}
}

