using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DataDevelop
{
	public partial class WebBrowser : DataDevelop.Document
	{
		public WebBrowser()
		{
			InitializeComponent();
			ResizeComboBox();
			browser.CanGoBackChanged += delegate { backButton.Enabled = browser.CanGoBack; };
			browser.CanGoForwardChanged += delegate { forwardButton.Enabled = browser.CanGoForward; };
			browser.Navigating += delegate { stopButton.Enabled = true; };
			browser.Navigated += delegate { stopButton.Enabled = false; };
			browser.DocumentTitleChanged += delegate { this.Text = this.TabText = browser.DocumentTitle; };
		}

		private void WebBrowser_Load(object sender, EventArgs e)
		{
		}

		private void mainToolStrip_SizeChanged(object sender, EventArgs e)
		{
			ResizeComboBox();
		}

		private void ResizeComboBox()
		{
			addressComboBox.Size = new Size(mainToolStrip.Width - 50, addressComboBox.Height);
		}

		public void Navigate(string url)
		{
			this.browser.Navigate(url);
		}

		private void addressComboBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter) {
				if (e.Control && e.Shift) {
					addressComboBox.Text += ".org";
				} else if (e.Control) {
					addressComboBox.Text += ".com";
				} else if (e.Shift) {
					addressComboBox.Text += ".net";
				}
				browser.Navigate(addressComboBox.Text);
				e.SuppressKeyPress = true;
				e.Handled = true;
			}
		}

		private void browser_LocationChanged(object sender, EventArgs e)
		{
			//addressComboBox.Text = (browser.Url != null) ? browser.Url.ToString() : String.Empty;
		}

		private void backButton_Click(object sender, EventArgs e)
		{
			browser.GoBack();
		}

		private void forwardButton_Click(object sender, EventArgs e)
		{
			browser.GoForward();
		}

		private void refreshButton_Click(object sender, EventArgs e)
		{
			browser.Refresh();
		}

		private void browser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
		{
			//loadingToolStripLabel.Visible = false;
			addressComboBox.Text = e.Url.ToString();
		}

		private void browser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
		{
			addressComboBox.Text = (e.Url != null) ? e.Url.ToString() : String.Empty;
			//loadingToolStripLabel.Visible = true;
		}

		private void browser_NewWindow(object sender, CancelEventArgs e)
		{
			
		}

		private void browser_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
		{
			////if (e.CurrentProgress == -1) {
			////    browserProgressBar.Style = ProgressBarStyle.Marquee;
			////} else {
			////    browserProgressBar.Style = ProgressBarStyle.Continuous;
			////}

			////if (e.CurrentProgress == e.MaximumProgress) {
			////    browserProgressBar.Value = browserProgressBar.Maximum;
			////    browserProgressBar.Visible = false;
			////} else {
			////    if (!browserProgressBar.Visible) {
			////        browserProgressBar.Visible = true;
			////    }
			////    int value = Convert.ToInt32(e.CurrentProgress / e.MaximumProgress);
			////    if (value != browserProgressBar.Value) {
			////        browserProgressBar.Value = value;
			////    }
			////}
		}

		private void goButton_Click(object sender, EventArgs e)
		{
			browser.Navigate(addressComboBox.Text);
		}

		private void stopButton_Click(object sender, EventArgs e)
		{
			browser.Stop();
		}

	}
}

