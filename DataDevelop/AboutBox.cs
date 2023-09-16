using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace DataDevelop
{
	partial class AboutBox : Form
	{
		public AboutBox()
		{
			InitializeComponent();

			//  Initialize the AboutBox to display the product information from the assembly information.
			//  Change assembly information settings for your application through either:
			//  - Project->Properties->Application->Assembly Information
			//  - AssemblyInfo.cs
			Text = $"About {AssemblyTitle}";
			labelProductName.Text = AssemblyProduct;
			labelVersion.Text = $"Version {AssemblyVersion}";
			labelCopyright.Text = AssemblyCopyright;
			homepageLinkLabel.Text = Program.Homepage;

			componentsListView.Items.Add(new ListViewItem(new[] { WindowsVersion.Name, WindowsVersion.Version.ToString() }));
			componentsListView.Items.Add(new ListViewItem(new[] { ".NET Framework", Environment.Version.ToString() }));

			var list = new SortedDictionary<string, string>();
			foreach (var assembly in new[] { Assembly.GetExecutingAssembly(), typeof(Data.DbProvider).Assembly }) {
				foreach (var assemblyName in assembly.GetReferencedAssemblies()) {
					if (!list.ContainsKey(assemblyName.Name)) {
						list.Add(assemblyName.Name, assemblyName.Version.ToString());
					}
				}
			}

			foreach (var entry in list) {
				var item = new ListViewItem(new[] { entry.Key, entry.Value });
				componentsListView.Items.Add(item);
			}
		}

		#region Assembly Attribute Accessors

		public static string AssemblyTitle
		{
			get
			{
				// Get all Title attributes on this assembly
				var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
				// If there is at least one Title attribute
				if (attributes.Length > 0) {
					// Select the first one
					var titleAttribute = (AssemblyTitleAttribute)attributes[0];
					// If it is not an empty string, return it
					if (titleAttribute.Title.Length > 0) {
						return titleAttribute.Title;
					}
				}
				// If there was no Title attribute, or if the Title attribute was the empty string, return the .exe name
				return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
			}
		}

		public static string AssemblyVersion
		{
			get
			{
				if (ApplicationDeployment.IsNetworkDeployed) {
					return ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
				}
				return Assembly.GetExecutingAssembly().GetName().Version.ToString();
			}
		}

		public static string AssemblyDescription
		{
			get
			{
				// Get all Description attributes on this assembly
				var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
				// If there aren't any Description attributes, return an empty string
				if (attributes.Length == 0)
					return "";
				// If there is a Description attribute, return its value
				return ((AssemblyDescriptionAttribute)attributes[0]).Description;
			}
		}

		public static string AssemblyProduct
		{
			get
			{
				// Get all Product attributes on this assembly
				var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
				// If there aren't any Product attributes, return an empty string
				if (attributes.Length == 0)
					return "";
				// If there is a Product attribute, return its value
				return ((AssemblyProductAttribute)attributes[0]).Product;
			}
		}

		public static string AssemblyCopyright
		{
			get
			{
				// Get all Copyright attributes on this assembly
				var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
				// If there aren't any Copyright attributes, return an empty string
				if (attributes.Length == 0)
					return "";
				// If there is a Copyright attribute, return its value
				return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
			}
		}

		public static string AssemblyCompany
		{
			get
			{
				// Get all Company attributes on this assembly
				var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
				// If there aren't any Company attributes, return an empty string
				if (attributes.Length == 0)
					return "";
				// If there is a Company attribute, return its value
				return ((AssemblyCompanyAttribute)attributes[0]).Company;
			}
		}
		#endregion

		private void HomepageLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start(Program.Homepage);
		}
	}
}
