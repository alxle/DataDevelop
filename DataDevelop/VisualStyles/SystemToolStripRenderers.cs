using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DataDevelop.VisualStyles
{
	class SystemToolStripRenderers
	{
		private static ToolStripProfessionalRenderer professionalRenderer;

		public static ToolStripProfessionalRenderer ToolStripProfessionalRenderer
		{
			get
			{
				if (professionalRenderer == null) {
					professionalRenderer = new ToolStripProfessionalRenderer(ProfessionalColorTable);
				}
				return professionalRenderer;
			}
		}

		private static ToolStripProfessionalRenderer squaredEdgesRenderer;

		public static ToolStripProfessionalRenderer ToolStripSquaredEdgesRenderer
		{
			get
			{
				if (squaredEdgesRenderer == null) {
					squaredEdgesRenderer = new ToolStripProfessionalRenderer(ProfessionalColorTable);
					squaredEdgesRenderer.RoundedEdges = false;
				}
				return squaredEdgesRenderer;
			}
		}

		private static ProfessionalColorTable professionalColorTable;

		private static ProfessionalColorTable ProfessionalColorTable
		{
			get
			{
				if (professionalColorTable == null) {
					professionalColorTable = new ProfessionalColorTable();
					professionalColorTable.UseSystemColors = true;
				}
				return professionalColorTable;
			}
		}

	}
}
