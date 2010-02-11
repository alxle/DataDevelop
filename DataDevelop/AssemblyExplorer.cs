using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DataDevelop.Services;
using DataDevelop.Reflection;
using System.Reflection;

namespace DataDevelop
{
	public partial class AssemblyExplorer : Toolbox
	{
		public AssemblyExplorer()
		{
			InitializeComponent();
		}

		public event EventHandler<OpenPropertiesEventArgs> ShowProperties;

		private void AssemblyExplorer_Load(object sender, EventArgs e)
		{
			AddAssembly(new AssemblyNode(Assembly.LoadWithPartialName("mscorlib")));
			AddAssembly(new AssemblyNode(Assembly.LoadWithPartialName("System")));
			AddAssembly(new AssemblyNode(Assembly.LoadWithPartialName("System.Data")));
			AddAssembly(new AssemblyNode(Assembly.LoadWithPartialName("System.Drawing")));
			AddAssembly(new AssemblyNode(Assembly.LoadWithPartialName("System.Windows.Forms")));
			AddAssembly(new AssemblyNode(Assembly.LoadWithPartialName("System.Xml")));
			////AddAssembly(new AssemblyNode(Assembly.LoadWithPartialName("System.Web")));
			addAssemblyButton.Enabled = false;
		}

		private void addAssemblyButton_Click(object sender, EventArgs e)
		{
			
		}

		private void AddAssembly(AssemblyNode assemblyNode)
		{
			TreeNode node = new TreeNode();
			node.ImageKey = "assembly";
			node.SelectedImageKey = node.ImageKey;
			node.Text = assemblyNode.Name;
			node.Tag = assemblyNode;
			node.Nodes.Add(String.Empty);
			mainTreeView.Nodes.Add(node);
		}

		private void mainTreeView_TreeNodePopulate(object sender, TreeViewEventArgs e)
		{
			TreeNode node = e.Node;
			object obj = e.Node.Tag;
			if (obj is AssemblyNode) {
				AssemblyNode asm = (AssemblyNode)obj;
				foreach (NamespaceNode space in asm.Namespaces) {
					space.SortTypes();
					TreeNode spaceNode = new TreeNode(space.Name);
					spaceNode.ImageKey = "namespace";
					spaceNode.SelectedImageKey = spaceNode.ImageKey;
					spaceNode.Tag = space;
					spaceNode.Nodes.Add(String.Empty);
					node.Nodes.Add(spaceNode);
				}
			} else if (obj is NamespaceNode) {
				NamespaceNode space = (NamespaceNode)obj;
				foreach (TypeNode type in space.Types) {
					TreeNode typeNode = new TreeNode(type.Name);
					typeNode.ImageKey = GetImageKey(type);
					typeNode.SelectedImageKey = typeNode.ImageKey;
					typeNode.Tag = type;
					node.Nodes.Add(typeNode);
				}
			}
		}

		private static string GetImageKey(TypeNode type)
		{
			if (type.Type.IsEnum) return "enum";
			if (type.Type.IsValueType) return "struct";
			if (type.Type.IsInterface) return "interface";
			if (type.Type.IsSubclassOf(typeof(MulticastDelegate))) return "delegate";
			return "class";
		}

		private void mainTreeView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (ShowProperties != null) {
				ShowProperties(this, new OpenPropertiesEventArgs(e.Node.Tag));
			}
		}

		private void mainTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			TypeNode typeNode = e.Node.Tag as TypeNode;
			if (typeNode != null) {
				TypeDocument doc = new TypeDocument();
				doc.TypeNode = typeNode;
				doc.Show(this.DockPanel);
			}
		}
	}
}