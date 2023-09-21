using System;
using System.ComponentModel;
using ICSharpCode.TextEditor;

namespace DataDevelop.UIComponents
{
	public partial class TextEditor : TextEditorControl
	{
		private bool hasChanges;

		public TextEditor()
		{
			InitializeComponent();
			Document.DocumentChanged += delegate { HasChanges = true; };
			FileNameChanged += delegate { HasChanges = false; };
		}
		
		[Browsable(false)]
		public bool HasChanges
		{
			get => hasChanges;
			set
			{
				if (hasChanges != value) {
					hasChanges = value;
					HasChangesChanged?.Invoke(this, EventArgs.Empty);
				}
			}
		}

		public string SelectedText
			=> ActiveTextAreaControl.SelectionManager.HasSomethingSelected ?
				ActiveTextAreaControl.SelectionManager.SelectedText : Text;

		public event EventHandler HasChangesChanged;
		
		public new void LoadFile(string fileName)
		{
			base.LoadFile(fileName, false, true);
			HasChanges = false;
		}

		public new void SaveFile(string fileName)
		{
			base.SaveFile(fileName);
			HasChanges = false;
		}

		private void Undo_Click(object sender, EventArgs e)
		{
			Undo();
		}

		private void Redo_Click(object sender, EventArgs e)
		{
			Redo();
		}

		private void Cut_Click(object sender, EventArgs e)
		{
			ActiveTextAreaControl.TextArea.ClipboardHandler.Cut(sender, e);
		}

		private void Copy_Click(object sender, EventArgs e)
		{
			ActiveTextAreaControl.TextArea.ClipboardHandler.Copy(sender, e);
		}

		private void Paste_Click(object sender, EventArgs e)
		{
			ActiveTextAreaControl.TextArea.ClipboardHandler.Paste(sender, e);
		}

		private void SelectAll_Click(object sender, EventArgs e)
		{
			ActiveTextAreaControl.TextArea.ClipboardHandler.SelectAll(sender, e);
		}
	}
}
