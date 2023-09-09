using FastColoredTextBoxNS;
using System;
using System.Drawing;

namespace DataDevelop.UIComponents
{
	public partial class LogTextBox : FastColoredTextBox
	{
		Color messageTextColor = Color.Black;
		Color infoTextColor = Color.DarkBlue;
		Color errorTextColor = Color.Red;

		TextStyle messageStyle;
		TextStyle infoStyle;
		TextStyle errorStyle;

		public LogTextBox()
		{
			InitializeComponent();
			Font = new Font("Consolas", 8.0F);
			IsReplaceMode = false;
			ReadOnly = true;
			ShowLineNumbers = false;

			messageStyle = new TextStyle(new SolidBrush(messageTextColor), null, FontStyle.Regular);
			infoStyle = new TextStyle(new SolidBrush(infoTextColor), null, FontStyle.Regular);
			errorStyle = new TextStyle(new SolidBrush(errorTextColor), null, FontStyle.Regular);
		}

		public Color MessageTextColor
		{
			get => messageTextColor;
			set
			{
				messageTextColor = value;
				messageStyle.Dispose();
				messageStyle = new TextStyle(new SolidBrush(messageTextColor), null, FontStyle.Regular);
			}
		}

		public Color InfoTextColor
		{
			get => infoTextColor;
			set
			{
				infoTextColor = value;
				infoStyle.Dispose();
				infoStyle = new TextStyle(new SolidBrush(infoTextColor), null, FontStyle.Regular);
			}
		}

		public Color ErrorTextColor
		{
			get => errorTextColor;
			set
			{
				errorTextColor = value;
				errorStyle.Dispose();
				errorStyle = new TextStyle(new SolidBrush(errorTextColor), null, FontStyle.Regular);
			}
		}

		private void Log(string text, Style style)
		{
			BeginUpdate();
			Selection.BeginUpdate();
			TextSource.CurrentTB = this;
			AppendText(text, style);
			AppendText(Environment.NewLine);
			GoEnd();
			Selection.EndUpdate();
			EndUpdate();
		}

		public void LogInfo(string text)
		{
			Log(text, infoStyle);
		}

		public void LogError(string text)
		{
			Log(text, errorStyle);
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				components?.Dispose();
				messageStyle?.Dispose();
				infoStyle?.Dispose();
				errorStyle?.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
