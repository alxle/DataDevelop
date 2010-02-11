using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DataDevelop.UIComponents
{
	public partial class TextVisualizer : Form
	{
		private bool textChanged;

		public TextVisualizer()
		{
			InitializeComponent();
			this.textBox.Font = new Font(FontFamily.GenericMonospace, 9F);
		}

		public bool TextValueChanged
		{
			get { return this.textChanged; }
		}

		public bool ReadOnly
		{
			get { return this.textBox.ReadOnly; }
			set { this.textBox.ReadOnly = value; }
		}

		public string TextValue
		{
			get { return this.textBox.Text; }
			set
			{
				this.textBox.Text = value;
				this.textChanged = false;
			}
		}

		private void textBox_TextChanged(object sender, EventArgs e)
		{
			this.textChanged = true;
		}

		private void TextVisualizer_Load(object sender, EventArgs e)
		{
			this.textBox.SelectionStart = 0;
			this.textBox.SelectionLength = 0;
		}
	}
}