using FastColoredTextBoxNS;
using System;
using System.Drawing;

namespace DataDevelop.UIComponents
{
	public partial class LogTextBox : FastColoredTextBox
	{
		TextStyle textStyle = new TextStyle(Brushes.Black, null, FontStyle.Regular);
		TextStyle infoStyle = new TextStyle(Brushes.DarkBlue, null, FontStyle.Regular);
		TextStyle errorStyle = new TextStyle(Brushes.Red, null, FontStyle.Regular);

		public LogTextBox()
		{
			InitializeComponent();
			this.Font = new Font("Consolas", 8.0F);
			this.IsReplaceMode = false;
			this.ReadOnly = true;
			this.ShowLineNumbers = false;
		}

		private void Log(string text, Style style)
		{
			this.BeginUpdate();
			this.Selection.BeginUpdate();
			this.TextSource.CurrentTB = this;
			this.AppendText(text, style);
			this.AppendText(Environment.NewLine);
			this.GoEnd();
			this.Selection.EndUpdate();
			this.EndUpdate();
		}

		public void LogInfo(string text)
		{
			this.Log(text, infoStyle);
		}

		public void LogError(string text)
		{
			this.Log(text, errorStyle);
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) components.Dispose();
				if (textStyle != null) textStyle.Dispose();
				if (infoStyle != null) infoStyle.Dispose();
				if (errorStyle != null) errorStyle.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
