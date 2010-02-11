using System;
using System.Collections.Generic;
using System.Text;

namespace DataDevelop.Data
{
	public interface IDbObject
	{
		Database Database
		{
			get;
		}
	}
}
