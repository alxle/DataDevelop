using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ICSharpCode.TextEditor;

namespace DataDevelop.UIComponents
{
	public partial class TextEditor : TextEditorControl
	{
		private bool hasChanges;

		public TextEditor()
		{
			this.InitializeComponent();
			this.Document.DocumentChanged += delegate { this.HasChanges = true; };
			this.FileNameChanged += delegate { this.HasChanges = false; };
		}
		
		[Browsable(false)]
		public bool HasChanges
		{
			get { return this.hasChanges; }
			set
			{
				if (this.hasChanges != value) {
					this.hasChanges = value;
					if (HasChangesChanged != null) {
						HasChangesChanged(this, EventArgs.Empty);
					}
				}
			}
		}

		public event EventHandler HasChangesChanged;
		
		public new void LoadFile(string fileName)
		{
			base.LoadFile(fileName);
			this.HasChanges = false;
		}

		public new void SaveFile(string fileName)
		{
			base.SaveFile(fileName);
			this.HasChanges = false;
		}

		private void Undo_Click(object sender, EventArgs e)
		{
			this.Undo();
		}

		private void Redo_Click(object sender, EventArgs e)
		{
			this.Redo();
		}

		private void Cut_Click(object sender, EventArgs e)
		{
			this.ActiveTextAreaControl.TextArea.ClipboardHandler.Cut(sender, e);
		}

		private void Copy_Click(object sender, EventArgs e)
		{
			this.ActiveTextAreaControl.TextArea.ClipboardHandler.Copy(sender, e);
		}

		private void Paste_Click(object sender, EventArgs e)
		{
			this.ActiveTextAreaControl.TextArea.ClipboardHandler.Paste(sender, e);
		}

		private void SelectAll_Click(object sender, EventArgs e)
		{
			this.ActiveTextAreaControl.TextArea.ClipboardHandler.SelectAll(sender, e);
		}
	}
}
