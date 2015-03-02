using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
			this.toolStrip.Renderer = SystemToolStripRenderers.ToolStripSquaredEdgesRenderer;
		}

		private DataGridViewBinaryCell cell;

		public DataGridViewBinaryCell BinaryCell
		{
			get { return cell; }
			set { cell = value; }
		}

		public bool ReadOnly
		{
			get { return this.readOnly; }
			set
			{
				this.readOnly = value;
				this.newToolStripButton.Enabled = !value;
				this.openToolStripButton.Enabled = !value;
				this.pasteToolStripButton.Enabled = !value;
			}
		}

		public static void Show(IWin32Window owner, DataGridViewBinaryCell cell)
		{
			using (BinaryVisualizer bv = new BinaryVisualizer()) {
				bv.BinaryCell = cell;
				bv.ReadOnly = cell.ReadOnly || cell.DataGridView.ReadOnly;
				bv.ShowDialog(owner);
			}
		}

		private void BinaryVisualizer_Load(object sender, EventArgs e)
		{
			ShowValue();
		}

		private void ShowValue()
		{
			if (cell.BinaryData == null) {
				picturePanel.Hide();
				hexTextBox.Hide();
				statusLabel.Text = "Null Data";
			} else {
				try {
					ShowImage();
					picturePanel.Show();
					hexTextBox.Hide();
				} catch {
					picturePanel.Hide();
					ShowHexadecimal();
					hexTextBox.Show();
				}
				statusLabel.Text = String.Format("Data Length: {0} bytes.", cell.BinaryData.Length);
			}
		}

		private void ShowImage()
		{
			Image picture = Image.FromStream(new System.IO.MemoryStream(cell.BinaryData));
			pictureBox.Image = picture;
			if (picture.Width > pictureBox.Width || pictureBox.Height > pictureBox.Height) {
				stretchToolStripMenuItem.PerformClick();
			} else {
				normalToolStripMenuItem.PerformClick();
			}
		}

		private void ShowHexadecimal()
		{
			StringBuilder builder = new StringBuilder(cell.BinaryData.Length * 3);
			foreach (byte b in cell.BinaryData) {
				string f = b.ToString("X");
				if (f.Length == 1) {
					builder.Append('0');
				}
				builder.Append(f);
				builder.Append(' ');
			}
			hexTextBox.Text = builder.ToString();
		}

		private void BinaryVisualizer_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape) {
				e.Handled = true;
				this.Close();
			}
		}

		private void newToolStripButton_Click(object sender, EventArgs e)
		{
			cell.Value = null;
			ShowValue();
		}

		private void openToolStripButton_Click(object sender, EventArgs e)
		{
			const long MaxLength = (2L * 1024 * 1024); // 2MB
			if (openFileDialog.ShowDialog(this) == DialogResult.OK) {
				var info = new FileInfo(openFileDialog.FileName);
				if (info.Length <= MaxLength) {
					cell.Value = File.ReadAllBytes(info.FullName);
					ShowValue();
				} else {
					MessageBox.Show(this, "File Exceeds the Max Length allowed (2MB)");
				}
			}
		}

		private void saveToolStripButton_Click(object sender, EventArgs e)
		{
			if (saveFileDialog.ShowDialog(this) == DialogResult.OK) {
				File.WriteAllBytes(saveFileDialog.FileName, cell.BinaryData);
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
				cell.Value = Clipboard.GetImage();
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
			Point location = pictureBox.Location;
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

	}
}