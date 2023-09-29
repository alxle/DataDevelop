using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace DataDevelop.UIComponents
{
	public partial class BinaryVisualizer : Form
	{
		private bool readOnly = false;

		private BinaryVisualizer()
		{
			InitializeComponent();
			if (MainForm.DarkMode) {
				this.UseImmersiveDarkMode();
				BackColor = VisualStyles.DarkThemeColors.Background;
				statusStrip.BackColor = VisualStyles.DarkThemeColors.BorderDark;
				strTextBox.BackColor = VisualStyles.DarkThemeColors.Control;
				strTextBox.ForeColor = VisualStyles.DarkThemeColors.TextColor;
				hexTextBox.BackColor = VisualStyles.DarkThemeColors.Control;
				hexTextBox.ForeColor = VisualStyles.DarkThemeColors.TextColor;
			}
		}

		public DataGridViewBinaryCell BinaryCell { get; set; }

		public bool ReadOnly
		{
			get { return readOnly; }
			set
			{
				readOnly = value;
				newToolStripButton.Enabled = !value;
				openToolStripButton.Enabled = !value;
				pasteToolStripButton.Enabled = !value;
			}
		}

		public static void Show(IWin32Window owner, DataGridViewBinaryCell cell)
		{
			var mouse = MousePosition;
			using (var bv = new BinaryVisualizer()) {
				bv.BinaryCell = cell;
				bv.ReadOnly = cell.ReadOnly || cell.DataGridView.ReadOnly;
				bv.PositionByMouse(mouse);
				bv.ShowDialog(owner);
			}
		}

		private void BinaryVisualizer_Load(object sender, EventArgs e)
		{
			ShowValue();
		}

		private void ShowValue()
		{
			hexLoaded = false;
			if (BinaryCell.BinaryData == null) {
				picturePanel.Hide();
				hexSplitContainer.Hide();
				statusLabel.Text = "Null Data";
			} else {
				try {
					ShowImage();
					picturePanel.Show();
					hexSplitContainer.Hide();
					viewHexButton.Visible = true;
					statusLabel.Text = $"Data Length: {BinaryCell.BinaryData.Length} bytes | Image Dimensions: {pictureBox.Image.Width} x {pictureBox.Image.Height}.";
				} catch {
					picturePanel.Hide();
					ShowHexadecimal();
					hexSplitContainer.Show();
					statusLabel.Text = $"Data Length: {BinaryCell.BinaryData.Length} bytes.";
				}
			}
		}

		private void ShowImage()
		{
			using (var mem = new MemoryStream(BinaryCell.BinaryData)) {
				var picture = Image.FromStream(mem);
				pictureBox.Image?.Dispose();
				pictureBox.Image = picture;

				if (picture.Width > pictureBox.Width || pictureBox.Height > pictureBox.Height) {
					stretchToolStripMenuItem.PerformClick();
				} else {
					normalToolStripMenuItem.PerformClick();
				}
			}
		}

		private void ShowHexadecimal()
		{
			const int MaxMB = 4;
			const int MaxBytes = MaxMB * 1024 * 1024;
			var hex = new StringBuilder(Math.Min(BinaryCell.BinaryData.Length * 3, MaxBytes * 3 + 50));
			var str = new StringBuilder(Math.Min(BinaryCell.BinaryData.Length * 2, MaxBytes * 2 + 50));
			var byteCount = 0;
			foreach (var b in BinaryCell.BinaryData) {
				if (byteCount++ > MaxBytes) {
					hex.AppendLine("...");
					hex.AppendLine($"[BLOB exceeds {MaxMB}MB. Byte preview truncated.]");
					str.AppendLine("...");
					str.AppendLine($"[BLOB exceeds {MaxMB}MB. Text preview truncated.]");
					break;
				}
				hex.Append(b.ToString("X2"));
				hex.Append(' ');
				var ch = (char)b;
				if (ch == '\\') str.Append("\\\\");
				else if (ch == '\0') str.Append("\\0");
				else if (ch == '\a') str.Append("\\a");
				else if (ch == '\b') str.Append("\\b");
				else if (ch == '\f') str.Append("\\f");
				else if (ch == '\n') str.Append("\\n");
				else if (ch == '\r') str.Append("\\r");
				else if (ch == '\t') str.Append("\\t");
				else if (ch == '\v') str.Append("\\v");
				else if (ch == ' ') str.Append(" ");
				else if (char.IsLetterOrDigit(ch) || char.IsPunctuation(ch) || char.IsSymbol(ch))
					str.Append((char)b);
				else
					str.AppendFormat("\\x{0:X2}", b);
			}
			hexTextBox.Text = hex.ToString();
			strTextBox.Text = str.ToString();
		}

		private void BinaryVisualizer_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape) {
				e.Handled = true;
				Close();
			}
		}

		private void newToolStripButton_Click(object sender, EventArgs e)
		{
			BinaryCell.Value = null;
			ShowValue();
		}

		private void openToolStripButton_Click(object sender, EventArgs e)
		{
			const long MaxLength = 20L * 1024 * 1024; // 20MB
			if (openFileDialog.ShowDialog(this) == DialogResult.OK) {
				var info = new FileInfo(openFileDialog.FileName);
				if (info.Length <= MaxLength) {
					BinaryCell.Value = File.ReadAllBytes(info.FullName);
					ShowValue();
				} else {
					MessageBox.Show(this, "File Exceeds the Max Length allowed (20MB)");
				}
			}
		}

		private void saveToolStripButton_Click(object sender, EventArgs e)
		{
			if (saveFileDialog.ShowDialog(this) == DialogResult.OK) {
				File.WriteAllBytes(saveFileDialog.FileName, BinaryCell.BinaryData);
			}
		}

		private void copyToolStripButton_Click(object sender, EventArgs e)
		{
			if (pictureBox.Visible) {
				Clipboard.SetImage(pictureBox.Image);
			}
		}

		private void pasteToolStripButton_Click(object sender, EventArgs e)
		{
			if (Clipboard.ContainsImage()) {
				BinaryCell.Value = Clipboard.GetImage();
				ShowValue();
			}
		}

		private void normalToolStripMenuItem_Click(object sender, EventArgs e)
		{
			normalToolStripMenuItem.Checked = true;
			stretchToolStripMenuItem.Checked = false;
			pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
			CenterPicture();
		}

		private void stretchToolStripMenuItem_Click(object sender, EventArgs e)
		{
			normalToolStripMenuItem.Checked = false;
			stretchToolStripMenuItem.Checked = true;
			pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
			FillPicture();
		}

		private void picturePanel_VisibleChanged(object sender, EventArgs e)
		{
			sizeModeToolStripDropDownButton.Visible = picturePanel.Visible;
			copyToolStripButton.Visible = picturePanel.Visible;
			//pasteToolStripButton.Visible = picturePanel.Visible;
			toolStripSeparator.Visible = picturePanel.Visible;
			toolStripSeparator1.Visible = picturePanel.Visible;
		}

		private void FillPicture()
		{
			pictureBox.Location = new Point(0, 0);
			pictureBox.Dock = DockStyle.Fill;
		}

		private void CenterPicture()
		{
			if (pictureBox.Dock != DockStyle.None) {
				pictureBox.Dock = DockStyle.None;
			}
			var location = pictureBox.Location;
			if (pictureBox.Width < picturePanel.Width) {
				location.X = (picturePanel.Width - pictureBox.Width) / 2;
			} else {
				location.X = picturePanel.AutoScrollPosition.X;
			}
			if (pictureBox.Height < picturePanel.Height) {
				location.Y = (picturePanel.Height - pictureBox.Height) / 2;
			} else {
				location.Y = picturePanel.AutoScrollPosition.Y;
			}
			pictureBox.Location = location;
		}

		private void picturePanel_Resize(object sender, EventArgs e)
		{
			if (pictureBox.Dock == DockStyle.None) {
				CenterPicture();
			}
		}

		bool hexLoaded = false;

		private void viewHexButton_Click(object sender, EventArgs e)
		{
			viewHexButton.Checked = !viewHexButton.Checked;
			picturePanel.Visible = !viewHexButton.Checked;
			if (!hexLoaded) {
				ShowHexadecimal();
				hexLoaded = true;
				hexSplitContainer.Show();
			}
		}
	}
}
