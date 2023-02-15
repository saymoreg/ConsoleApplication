using System;
using System.Collections.Generic;
using Core.Entities;

namespace Data.Contexts
{
	public static class DbContext
	{
		static DbContext()
		{
			Groups = new List<Group>();
		}
		public static List<Group> Groups { get; set; }
	}
}

