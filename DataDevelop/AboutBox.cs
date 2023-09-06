using System;
using System.Collections.Generic;
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
			this.Text = String.Format("About {0}", AssemblyTitle);
			this.labelProductName.Text = AssemblyProduct;
			this.labelVersion.Text = String.Format("Version {0}", AssemblyVersion);
			this.labelCopyright.Text = AssemblyCopyright;
			this.homepageLinkLabel.Text = Program.Homepage;

			this.componentsListView.Items.Add(new ListViewItem(new string[] { "Operating System: " + Environment.OSVersion.Platform.ToString(), Environment.OSVersion.Version.ToString() }));
			this.componentsListView.Items.Add(new ListViewItem(new string[] { ".NET Framework", Environment.Version.ToString() }));

			var list = new List<AssemblyName>();
			foreach (var assembly in new Assembly[] { Assembly.GetExecutingAssembly(), typeof(Data.DbProvider).Assembly }) {
				foreach (var assemblyName in assembly.GetReferencedAssemblies()) {
					if (!list.Exists(i => i.Name == assemblyName.Name)) {
						list.Add(assemblyName);
					}
				}
			}

			list.Sort(new Comparison<AssemblyName>((x, y) => String.Compare(x.Name, y.Name)));

			foreach (var assemblyName in list) {
				var item = new ListViewItem(new string[] { assemblyName.Name, assemblyName.Version.ToString() });
				this.componentsListView.Items.Add(item);
			}
		}

		#region Assembly Attribute Accessors

		public static string AssemblyTitle
		{
			get
			{
				// Get all Title attributes on this assembly
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
				// If there is at least one Title attribute
				if (attributes.Length > 0) {
					// Select the first one
					AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
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
				if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed) {
					return System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
				}
				return Assembly.GetExecutingAssembly().GetName().Version.ToString();
			}
		}

		public static string AssemblyDescription
		{
			get
			{
				// Get all Description attributes on this assembly
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
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
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
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
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
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
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
				// If there aren't any Company attributes, return an empty string
				if (attributes.Length == 0)
					return "";
				// If there is a Company attribute, return its value
				return ((AssemblyCompanyAttribute)attributes[0]).Company;
			}
		}
		#endregion

		private void homepageLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start(Program.Homepage);
		}
	}
}
