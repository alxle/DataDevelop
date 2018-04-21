using System.Windows.Forms;

namespace DataDevelop
{
	public class PropertyGrid : System.Windows.Forms.PropertyGrid
	{
		public PropertyGrid()
		{
			base.HelpVisible = false;
			base.PropertySort = PropertySort.Alphabetical;
		}
	}
}
