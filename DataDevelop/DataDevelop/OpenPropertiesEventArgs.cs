using System;
using System.Collections.Generic;
using System.Text;

namespace DataDevelop
{
	public class OpenPropertiesEventArgs : EventArgs
	{
		private object _object;
		private bool _focus;

		public OpenPropertiesEventArgs(object @object)
		{
			_object = @object;
		}

		public OpenPropertiesEventArgs(object @object, bool focus)
		{
			_object = @object;
			_focus = focus;
		}

		public object Object
		{
			get { return _object; }
		}

		public bool Focus
		{
			get { return _focus; }
		}
	}
}
