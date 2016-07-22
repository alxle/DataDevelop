using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using DataDevelop.Reflection;

namespace DataDevelop
{
	partial class TypeDocument : Document
	{
		TypeNode typeNode;
		ListViewGroup fieldsGroup;
		ListViewGroup constructorsGroup;
		ListViewGroup methodsGroup;
		ListViewGroup propertiesGroup;
		ListViewGroup eventsGroup;

		public TypeDocument()
		{
			InitializeComponent();
			typeLabel.Font = new Font(typeLabel.Font.FontFamily, 14F);
			fieldsGroup = new ListViewGroup("Fields");
			constructorsGroup = new ListViewGroup("Contructors");
			methodsGroup = new ListViewGroup("Methods");
			propertiesGroup = new ListViewGroup("Properties");
			eventsGroup = new ListViewGroup("Events");
			mainListView.Groups.Add(fieldsGroup);
			mainListView.Groups.Add(constructorsGroup);
			mainListView.Groups.Add(methodsGroup);
			mainListView.Groups.Add(propertiesGroup);
			mainListView.Groups.Add(eventsGroup);
		}

		public TypeNode TypeNode
		{
			get { return typeNode; }
			set
			{
				if (typeNode != value) {
					typeNode = value;
					typeLabel.Text = typeNode.Type.FullName;
					Text = TabText = typeNode.Name;
					LoadData();
				}
			}
		}

		private void LoadData()
		{
			foreach (FieldInfo f in typeNode.Fields) {
				AddField(f);
			}
			foreach (ConstructorInfo c in typeNode.Contructors) {
				AddConstructor(c);
			}
			foreach (PropertyInfo p in typeNode.Properties) {
				AddProperty(p);
			}
			foreach (MethodInfo m in typeNode.Methods) {
				AddMethod(m);
			}
			foreach (EventInfo ev in typeNode.Events) {
				AddEvent(ev);
			}
		}

		private void AddField(FieldInfo field)
		{
			ListViewItem item = AddListItem(field.Name, field, "field", fieldsGroup);
			item.SubItems.Add(field.FieldType.Name);
			item.SubItems.Add(GetModifier(field));
		}

		private void AddConstructor(ConstructorInfo constructor)
		{
			ListViewItem item = AddListItem(GetName(constructor), constructor, "method", constructorsGroup);
			item.SubItems.Add(String.Empty);
			item.SubItems.Add(GetModifier(constructor));
		}

		private void AddProperty(PropertyInfo property)
		{
			ListViewItem item = AddListItem(property.Name, property, "property", propertiesGroup);
			item.SubItems.Add(property.PropertyType.Name);
			StringBuilder modifier = new StringBuilder();
			if (property.CanRead) {
				MethodInfo get = property.GetGetMethod(true);
				modifier.Append(GetModifier(get));
				modifier.Append(" Get");
			}
			if (property.CanWrite) {
				MethodInfo set = property.GetSetMethod(true);
				if (modifier.Length > 0) {
					modifier.Append("; ");
				}
				modifier.Append(GetModifier(set));
				modifier.Append(" Set");
			}
			item.SubItems.Add(modifier.ToString());
		}

		private void AddMethod(MethodInfo method)
		{
			ListViewItem item = AddListItem(GetName(method), method, "method", methodsGroup);
			item.SubItems.Add(method.ReturnType.Name);
			item.SubItems.Add(GetModifier(method));
		}

		private void AddEvent(EventInfo @event)
		{
			ListViewItem item = AddListItem(@event.Name, @event, "event", eventsGroup);
			item.SubItems.Add(@event.EventHandlerType.Name);
			item.SubItems.Add(GetModifier(@event.GetAddMethod()));
		}

		private ListViewItem AddListItem(string text, object tag, string imageKey, ListViewGroup group)
		{
			ListViewItem item = new ListViewItem();
			item.Text = text;
			item.Tag = tag;
			item.ImageKey = imageKey;
			item.Group = group;
			mainListView.Items.Add(item);
			return item;
		}

		private static string GetModifier(MethodBase method)
		{
			if (method.IsPublic)
				return "Public";
			if (method.IsFamilyAndAssembly)
				return "Protected Internal";
			if (method.IsFamily)
				return "Protected";
			if (method.IsAssembly)
				return "Internal";
			return "Private";
		}

		private static string GetModifier(FieldInfo field)
		{
			if (field.IsPublic)
				return "Public";
			if (field.IsFamilyAndAssembly)
				return "Protected Internal";
			if (field.IsFamily)
				return "Protected";
			if (field.IsAssembly)
				return "Internal";
			return "Private";
		}

		private static string GetName(MethodBase method)
		{
			StringBuilder builder = new StringBuilder();
			builder.Append(method.Name);
			builder.Append('(');
			ParameterInfo[] parameters = method.GetParameters();
			for (int i = 0; i < parameters.Length; i++) {
				if (i > 0) {
					builder.Append(',');
					builder.Append(' ');
				}
				builder.Append(parameters[i].ParameterType.Name);
				builder.Append(' ');
				builder.Append(parameters[i].Name);
			}
			builder.Append(')');
			return builder.ToString();
		}

		private void typeTreeView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			mainListView.BeginUpdate();
			mainListView.Items.Clear();
			IList<MethodInfo> list = e.Node.Tag as IList<MethodInfo>;
			if (list != null) {
				foreach (MethodInfo m in list) {
					ListViewItem item = new ListViewItem(mainListView.Groups[m.IsStatic ? 0 : 1]);
					item.Text = GetName(m);
					item.SubItems.Add(m.ReturnType.Name);
					item.SubItems.Add(GetModifier(m));
					mainListView.Items.Add(item);
				}
			}
			mainListView.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
			mainListView.EndUpdate();
		}
	}
}
