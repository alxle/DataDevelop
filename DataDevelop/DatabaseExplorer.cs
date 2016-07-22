using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Windows.Forms;
using DataDevelop.Data;
using DataDevelop.Dialogs;
using DataDevelop.Properties;
using DataDevelop.UIComponents;

namespace DataDevelop
{
	public partial class DatabaseExplorer : Toolbox
	{
		public DatabaseExplorer()
		{
			InitializeComponent();
		}

		private void DatabaseExplorer_Load(object sender, EventArgs e)
		{
			LoadDatabases();
		}

		private TreeNode CreateFolderNode(string folderName)
		{
			TreeNode node = CreateTreeNode(folderName, "folder", null);
			return node;
		}

		private TreeNode CreateStoredProceduresFolderNode(Database db)
		{
			TreeNode node = CreateFolderNode("Stored Procedures");
			node.Nodes.Add(String.Empty);

			TreeNodeController controller = new TreeNodeController();
			controller.Populate += delegate
			{
				foreach (StoredProcedure sp in db.StoredProcedures) {
					node.Nodes.Add(CreateStoredProcedureNode(sp));
				}
			};
			controller.Refresh += delegate { db.RefreshStoredProcedures(); Unpopulate(node); };
			node.Tag = controller;
			return node;
		}

		private TreeNode CreateStoredProcedureNode(StoredProcedure storedProdecure)
		{
			TreeNode node = CreateTreeNode(storedProdecure.Name, "storedProcedure", procedureContextMenu);
			node.Nodes.Add(String.Empty);

			TreeNodeController controller = new TreeNodeController();
			controller.Populate += delegate { AddParameters(node, storedProdecure); };
			controller.Refresh += delegate { storedProdecure.RefreshParameters(); };
			controller.Tag = storedProdecure;
			node.Tag = controller;

			return node;
		}

		private TreeNode CreateUserDefinedFunctionsFolderNode(Database db)
		{
			TreeNode node = CreateFolderNode("User Defined Functions");
			node.Nodes.Add(String.Empty);

			TreeNodeController controller = new TreeNodeController();
			controller.Populate += delegate
			{
				foreach (UserDefinedFunction fn in db.UserDefinedFunctions) {
					node.Nodes.Add(CreateUserDefinedFunctionNode(fn));
				}
			};
			controller.Refresh += delegate { db.RefreshUserDefinedFunctions(); Unpopulate(node); };
			node.Tag = controller;
			return node;
		}

		private TreeNode CreateUserDefinedFunctionNode(UserDefinedFunction userDefinedFunction)
		{
			TreeNode node = CreateTreeNode(userDefinedFunction.Name, "function", procedureContextMenu);
			node.Nodes.Add(String.Empty);

			TreeNodeController controller = new TreeNodeController();
			controller.Populate += delegate { AddParameters(node, userDefinedFunction); };
			controller.Refresh += delegate { userDefinedFunction.RefreshParameters(); };
			controller.Tag = userDefinedFunction;
			node.Tag = controller;

			return node;
		}

		private void Unpopulate(TreeNode node)
		{
			node.Nodes.Clear();
			node.Nodes.Add(String.Empty);
			node.Collapse();
		}

		private DatabaseNode CreateDatabaseNode(Database db)
		{
			DatabaseNode node = new DatabaseNode(db);
			node.AsyncException += DatabaseConnect_AsyncException;
			node.ContextMenuStrip = databaseContextMenu;
			node.Nodes.Add(String.Empty);

			TreeNodeController controller = new TreeNodeController();
			controller.Populate += delegate { AddTables(node); };
			controller.Refresh += delegate { db.RefreshTables(); Unpopulate(node); };
			controller.DoubleClick += delegate { if (!node.IsExpanded) { node.Expand(); } };
			controller.Tag = new ConnectionSettings(db);
			node.Tag = controller;

			return node;
		}

		private TableNode CreateTableNode(Table table)
		{
			TableNode node = new TableNode(table);
			node.ContextMenuStrip = tableContextMenu;

			TreeNodeController controller = new TreeNodeController();
			controller.Populate += delegate { AddColumns(node); };
			controller.Refresh += delegate { table.RefreshColumns(); Unpopulate(node); };
			controller.Tag = table;
			node.Tag = controller;

			return node;
		}

		private TreeNode CreateColumnNode(Column column)
		{
			string imageKey;
			if (column.InPrimaryKey) {
				imageKey = "primaryKey";
			} else {
				imageKey = "column";
			}
			TreeNode node = CreateTreeNode(column.Name, imageKey, null);
			node.Tag = new TreeNodeController(column);
			return node;
		}

		private TreeNode CreateTriggerNode(Trigger trigger)
		{
			TreeNode node = CreateTreeNode(trigger.Name, "trigger", null);

			TreeNodeController controller = new TreeNodeController();
			controller.DoubleClick += delegate { OpenQuery(trigger.Database, trigger.GenerateAlterStatement()); };
			controller.Tag = trigger;
			node.Tag = controller;

			return node;
		}

		private TreeNode CreateKeyNode(ForeignKey key)
		{
			TreeNode node = CreateTreeNode(key.Name, "foreingKey", null);
			node.Tag = new TreeNodeController(key);
			return node;
		}

		private TreeNode CreateTreeNode(string text, string imageKey, ContextMenuStrip contextMenu)
		{
			TreeNode node = new TreeNode();
			node.ImageKey = node.SelectedImageKey = imageKey;
			node.Text = text;
			node.ContextMenuStrip = contextMenu;
			return node;
		}

		private void AddNewFolder(string folderName)
		{
			treeView.Nodes.Insert(0, CreateFolderNode(folderName));
		}

		private void AddNewDatabase(Database database)
		{
			treeView.Nodes.Add(CreateDatabaseNode(database));
			DatabasesManager.Add(database);
		}

		private void AddDatabase(Database database)
		{
			treeView.Nodes.Add(CreateDatabaseNode(database));
		}

		private void AddTables(DatabaseNode node)
		{
			node.Nodes.Clear();
			node.RunAsyncOperation("expanding",
			delegate
			{
				if (!node.Database.IsConnected) {
					node.Database.Connect();
				}
				foreach (var table in node.Database.Tables) {
					break;
				}
			},
			delegate
			{
				if (!node.Database.IsConnected) return;
				node.SetImage(true);
				Cursor normal = treeView.Cursor;
				treeView.Cursor = Cursors.WaitCursor;

				try {
					treeView.BeginUpdate();
					TreeNode views = null;
					foreach (Table table in node.Database.Tables) {
						TableNode tableNode = CreateTableNode(table);
						tableNode.Nodes.Add(String.Empty);
						if (table.IsView) {
							if (views == null) {
								views = CreateFolderNode("Views");
							}
							views.Nodes.Add(tableNode);
						} else {
							node.Nodes.Add(tableNode);
						}
					}
					if (views != null) {
						node.Nodes.Add(views);
					}
					if (node.Database.SupportStoredProcedures) {
						node.Nodes.Add(CreateStoredProceduresFolderNode(node.Database));
					}
					if (node.Database.SupportUserDefinedFunctions) {
						node.Nodes.Add(CreateUserDefinedFunctionsFolderNode(node.Database));
					}
				} catch (Exception ex) {
					MessageBox.Show(this, ex.Message, this.ProductName);
					node.Nodes.Clear();
				} finally {
					treeView.EndUpdate();
					treeView.Cursor = normal;
					node.Expand();
				}
			}, null);
		}

		private void AddColumns(TableNode node)
		{
			try {
				treeView.BeginUpdate();

				foreach (Column column in node.Table.Columns) {
					TreeNode columnNode = CreateColumnNode(column);
					node.Nodes.Add(columnNode);
				}
				if (node.Table.Triggers.Count > 0) {
					TreeNode triggers = node.Nodes.Add("Triggers");
					triggers.SelectedImageKey = triggers.ImageKey = "folder";
					foreach (Trigger trigger in node.Table.Triggers) {
						TreeNode triggerNode = CreateTriggerNode(trigger);
						triggers.Nodes.Add(triggerNode);
					}
				}
				if (node.Table.ForeignKeys.Count > 0) {
					TreeNode keys = node.Nodes.Add("Foreign Keys");
					keys.SelectedImageKey = keys.ImageKey = "folder";
					foreach (ForeignKey key in node.Table.ForeignKeys) {
						TreeNode keyNode = CreateKeyNode(key);
						keyNode.ContextMenuStrip = this.foreignKeyMenuStrip;
						keys.Nodes.Add(keyNode);
					}
				}
			} catch (DbException ex) {
				MessageBox.Show(ex.Message);
			} finally {
				treeView.EndUpdate();
			}
		}

		public DbProvider SelectProvider()
		{
			DbProvider provider = null;
			using (SelectProviderDialog providerDialog = new SelectProviderDialog()) {
				FormExtensions.DownPositionate(providerDialog, this.toolStrip);
				if (providerDialog.ShowDialog(this) == DialogResult.OK) {
					provider = providerDialog.Provider;
				}
			}
			return provider;
		}

		public void NewDatabase()
		{
			DbProvider provider = SelectProvider();
			if (provider == null) return;
			if (!provider.IsFileBased) {
				MessageBox.Show(this, "Selected Database is not File based, No creation supported.", "Information");
				return;
			}

			if (saveFileDialog.ShowDialog(this) == DialogResult.OK) {
				string fileName = saveFileDialog.FileName;
				try {
					string connectionString = provider.CreateDatabaseFile(fileName);
					using (ConnectionStringForm box = new ConnectionStringForm()) {
						box.ConnectionStringBuilder = provider.CreateConnectionStringBuilder();
						box.ConnectionStringBuilder.ConnectionString = connectionString;
						if (box.ShowDialog(this) == DialogResult.OK) {
							Database db = provider.CreateDatabase(box.DatabaseName, box.ConnectionString);
							AddNewDatabase(db);
						}
					}
				} catch (Exception ex) {
					MessageBox.Show(this, ex.Message, "Error on creating database");
				}
			}
		}

		public string GetDatabaseName(string fileName)
		{
			int i = 0;
			string name = System.IO.Path.GetFileNameWithoutExtension(fileName);
			string dbname = name;
			while (DatabasesManager.Contains(dbname)) {
				dbname = name + i;
			}
			return dbname;
		}

		public void OpenDatabase()
		{
			DbProvider provider = SelectProvider();
			if (provider == null) {
				return;
			}

			using (ConnectionStringForm box = new ConnectionStringForm()) {
				box.ConnectionStringBuilder = provider.CreateConnectionStringBuilder();
				if (box.ShowDialog(this) == DialogResult.OK) {
					Database db = provider.CreateDatabase(box.DatabaseName, box.ConnectionString);
					AddNewDatabase(db);
				}
			}
		}

		#region Events

		private void treeView_TreeNodePopulate(object sender, TreeViewEventArgs e)
		{
			TreeNodeController controller = e.Node.Tag as TreeNodeController;
			if (controller != null) {
				controller.PerformPopulate(e.Node);
			}
		}

		private void AddParameters(TreeNode treeNode, StoredProcedure sp)
		{
			try {
				treeNode.TreeView.BeginUpdate();
				foreach (Parameter p in sp.Parameters) {
					TreeNode node = CreateTreeNode(p.Name, "column", null);
					node.Tag = new TreeNodeController(p);
					treeNode.Nodes.Add(node);
				}
			} finally {
				treeNode.TreeView.EndUpdate();
			}
		}

		private void treeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			TreeNodeController controller = treeView.SelectedNode.Tag as TreeNodeController;
			if (controller != null) {
				controller.PerformDoubleClick(e.Node);
			}
		}

		private void removeDatabaseMenuItem_Click(object sender, EventArgs e)
		{
			DatabaseNode node = treeView.SelectedNode as DatabaseNode;
			if (node != null) {
				while (node.Database.IsConnected) {
					node.Database.Disconnect();
				}
				DatabasesManager.Remove(node.Database);
				if (node.Parent == null) {
					treeView.Nodes.Remove(node);
				} else {
					node.Parent.Nodes.Remove(node);
				}
			}
		}

		private void DatabaseConnect_AsyncException(object sender, AsyncCompletedEventArgs e)
		{
			MessageBox.Show(this, e.Error.Message, "Error Connecting", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private bool ConnectDatabase(DatabaseNode node, bool reconnect)
		{
			try {
				if (reconnect && node.Database.IsConnected) {
					node.Database.Reconnect();
				} else {
					node.Connect();
				}
			} catch (Exception e) {
				MessageBox.Show(this, e.Message, "Error Connecting", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			return true;
		}

		////private bool Disconnect(DatabaseNode node)
		////{
		////    try {
		////        node.Disconnect();
		////        return true;
		////    } catch (DbException e) {
		////        MessageBox.Show(this, e.Message, "Error Disconnecting", MessageBoxButtons.OK, MessageBoxIcon.Error);
		////    }
		////    return false;
		////}

		private void openTableDataMenuItem_Click(object sender, EventArgs e)
		{
			var tableNode = (TableNode)treeView.SelectedNode;
			if (tableNode != null) {
				var table = tableNode.Table;
				var key = table.Database.Name + '.' + table.Name;
				Document document = GetDocument(key);

				if (document != null) {
					document.Activate();
					if (MessageBox.Show(this, "A copy of this table is already open.\r\nDo you want to open another copy?",
						"Table: " + key, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) {
						return;
					}
				}

				document = new TableDocument(table);
				document.Text = key;
				document.Show(this.DockPanel);
			}
		}

		private void openDataWithFilterToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var tableNode = (TableNode)treeView.SelectedNode;
			if (tableNode != null) {
				var table = tableNode.Table;
				var key = table.Database.Name + '.' + table.Name;

				using (FilterDialog filterDialog = new FilterDialog(new TableFilter(table))) {
					filterDialog.Text = "Open with filter: " + key;
					filterDialog.ControlBox = true;
					filterDialog.ShowIcon = false;
					filterDialog.StartPosition = FormStartPosition.CenterParent;
					if (filterDialog.ShowDialog(this) == DialogResult.OK) {
						var filter = filterDialog.Filter;
						var document = new TableDocument(table, filter);
						document.Text = key;
						document.Show(this.DockPanel);
					}
				}
			}
		}

		private Document GetDocument(string text)
		{
			foreach (WeifenLuo.WinFormsUI.Docking.IDockContent doc in DockPanel.Documents) {
				Document document = doc as Document;
				if (document != null && String.Equals(document.Text, text, StringComparison.OrdinalIgnoreCase)) {
					return document;
				}
			}
			return null;
		}

		private void newQueryToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DatabaseNode dbNode = (DatabaseNode)treeView.SelectedNode;
			string key = dbNode.Text;

			if (dbNode.Database.IsConnected) {
				OpenQuery(dbNode.Database, String.Empty);
			} else {
				dbNode.ConnectAsync(false, delegate { OpenQuery(dbNode.Database, String.Empty); });
			}
		}

		private IEnumerable<Document> GetDocumentsFrom(Database database)
		{
			foreach (object obj in DockPanel.Documents) {
				IDbObject db = obj as IDbObject;
				if (db != null && Object.ReferenceEquals(db.Database, database)) {
					yield return (Document)db;
				}
			}
		}

		private TreeNode GetTreeNode(IDataObject data)
		{
			string type = typeof(TreeNode).ToString();
			if (data.GetDataPresent(type, true)) {
				return data.GetData(type, true) as TreeNode;
			}
			return null;
		}

		private DatabaseNode GetDatabaseNode(IDataObject data)
		{
			return data.GetData(typeof(DatabaseNode)) as DatabaseNode;
		}

		private bool IsFolderNode(TreeNode node)
		{
			if (node != null && node.ImageKey == "folder")
				return true;
			return false;
		}

		private void renameToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var tableNode = treeView.SelectedNode as TableNode;
			if (tableNode != null) {
				tableNode.Text = tableNode.Table.Name;
				treeView.LabelEdit = true;
				treeView.SelectedNode.BeginEdit();
			}
		}

		private void treeView_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
		{

		}

		private void treeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
		{
			e.CancelEdit = true; // Cancel assigning e.Label to e.Node.Text
			treeView.LabelEdit = false;

			var table = SelectedTable;
			if (e.Label == null) {
				e.Node.Text = table.DisplayName;
				return;
			}

			try {
				if (table.Rename(e.Label)) {
					e.Node.Text = table.DisplayName;
				} else {
					MessageBox.Show(this, "Couldn't rename table", "Error");
				}
			} catch (Exception ex) {
				MessageBox.Show(this, ex.Message, "Exception throwed!");
			}
		}

		#endregion

		#region Settings

		public void LoadDatabases()
		{
			this.LoadDatabases(true);
		}

		public void LoadDatabases(bool reloadSettings)
		{
			try {
				treeView.BeginUpdate();
				if (treeView.Nodes.Count > 0) {
					treeView.Nodes.Clear();
				}
				if (reloadSettings) {
					SettingsManager.LoadDatabases();
				}
				foreach (Database db in DatabasesManager.Databases.Values) {
					AddDatabase(db);
				}
			} finally {
				treeView.EndUpdate();
			}
		}

		public void SaveDatabases()
		{
			SettingsManager.SaveDatabases();
		}

		#endregion

		public event EventHandler<OpenPropertiesEventArgs> ShowProperties;

		private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			ShowNodeProperties(e.Node);
		}

		private void ShowNodeProperties(TreeNode node)
		{
			object obj = null;
			TreeNodeController controller = node.Tag as TreeNodeController;
			if (controller != null) {
				refreshToolStripButton.Enabled = controller.SupportsRefresh;
				obj = controller.Tag;
			} else {
				refreshToolStripButton.Enabled = false;
			}
			if (ShowProperties != null) {
				ShowProperties(this, new OpenPropertiesEventArgs(obj));
			}
		}

		private void createDatabaseToolStripButton_Click(object sender, EventArgs e)
		{
			NewDatabase();
		}

		private void addDatabaseToolStripButton_Click(object sender, EventArgs e)
		{
			OpenDatabase();
		}

		private void RefreshSelected()
		{
			TreeNodeController controller = treeView.SelectedNode.Tag as TreeNodeController;
			if (controller != null) {
				controller.PerformRefresh(treeView.SelectedNode);
			}
		}

		private void refreshToolStripButton_Click(object sender, EventArgs e)
		{
			RefreshSelected();
		}

		private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ShowNodeProperties(treeView.SelectedNode);
		}

		private void databaseContextMenu_Opened(object sender, EventArgs e)
		{
			Database db = this.SelectedDatabase;
			connectToolStripMenuItem.Enabled = !db.IsConnected;
			reconnectToolStripMenuItem.Enabled = db.IsConnected;
			disconnectToolStripMenuItem.Enabled = db.IsConnected;
		}

		private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Disconnect(SelectedDatabaseNode);
		}

		private bool Disconnect(DatabaseNode dbNode)
		{
			List<Document> documents = new List<Document>(GetDocumentsFrom(dbNode.Database));
			if (documents.Count > 0 && MessageBox.Show(this, Properties.Resources.DisconnectDatabase, Text,
				MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) {
				return false;
			}

			foreach (Document doc in documents) {
				if (!FormExtensions.Close(doc)) {
					return false;
				}
			}
			dbNode.Disconnect();
			if (dbNode.Database.IsConnected) {
				return false;
			}
			Unpopulate(dbNode);
			return true;
		}

		private void selectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Table table = this.SelectedTable;
			OpenQuery(table.Database, table.GenerateSelectStatement());
		}

		private void joinToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Table table = this.SelectedTable;
			if (table.ForeignKeys.Count > 0) {
				OpenQuery(table.Database, ForeignKey.GenerateSelectStatement(table));
			} else {
				MessageBox.Show(this, "This table does not contains Foreign Keys", this.ProductName);
			}
		}

		private void insertToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Table table = this.SelectedTable;
			OpenQuery(table.Database, table.GenerateInsertStatement());
		}

		private void updateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Table table = this.SelectedTable;
			OpenQuery(table.Database, table.GenerateUpdateStatement());
		}

		private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Table table = this.SelectedTable;
			OpenQuery(table.Database, table.GenerateDeleteStatement());
		}

		private void createToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Table table = this.SelectedTable;
			OpenQuery(table.Database, table.GenerateCreateStatement());
		}

		private void alterToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Table table = this.SelectedTable;
			OpenQuery(table.Database, table.GenerateAlterStatement());
		}

		private void dropToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Table table = this.SelectedTable;
			OpenQuery(table.Database, table.GenerateDropStatement());
		}

		private void OpenQuery(Database database, string script)
		{
			if (script != null) {
				CommandDocument doc = new CommandDocument(database);
				doc.Text = String.Format(Resources.QueryDocumentTitle, database.Name);
				doc.CommandText = script;
				doc.Show(this.DockPanel);
			}
		}

		public Database SelectedDatabase
		{
			get { return ((DatabaseNode)treeView.SelectedNode).Database; }
		}

		public Table SelectedTable
		{
			get { return ((TableNode)treeView.SelectedNode).Table; }
		}

		private void generateClassesToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}

		private DatabaseNode SelectedDatabaseNode
		{
			get { return (DatabaseNode)treeView.SelectedNode; }
		}

		private void modifyConnectionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (Disconnect(SelectedDatabaseNode)) {
				using (ConnectionStringForm box = new ConnectionStringForm()) {
					Database db = this.SelectedDatabase;
					box.DatabaseName = db.Name;
					box.IsUpdate = true;
					box.ConnectionStringBuilder = db.Provider.CreateConnectionStringBuilder();
					box.ConnectionStringBuilder.ConnectionString = db.ConnectionString;
					if (box.ShowDialog(this) == DialogResult.OK) {
						db.ChangeConnectionString(box.ConnectionString);
						DatabasesManager.IsCollectionDirty = true;
						ShowNodeProperties(treeView.SelectedNode);
					}
				}
			}
		}

		private StoredProcedure SelectedStoredProcedure
		{
			get { return (StoredProcedure)((TreeNodeController)treeView.SelectedNode.Tag).Tag; }
		}

		private T GetSelectedNode<T>()
		{
			return (T)((TreeNodeController)treeView.SelectedNode.Tag).Tag;
		}

		private void scriptAsExecuteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			StoredProcedure storedProdecure = SelectedStoredProcedure;
			OpenQuery(storedProdecure.Database, storedProdecure.GenerateExecuteStatement());
		}

		private void scriptAsCreateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			StoredProcedure storedProdecure = SelectedStoredProcedure;
			OpenQuery(storedProdecure.Database, storedProdecure.GenerateCreateStatement());
		}

		private void scriptAsAlterToolStripMenuItem_Click(object sender, EventArgs e)
		{
			StoredProcedure storedProdecure = SelectedStoredProcedure;
			OpenQuery(storedProdecure.Database, storedProdecure.GenerateAlterStatement());
		}

		private void scriptAsDropToolStripMenuItem_Click(object sender, EventArgs e)
		{
			StoredProcedure storedProdecure = SelectedStoredProcedure;
			OpenQuery(storedProdecure.Database, storedProdecure.GenerateDropStatement());
		}

		private void disconnectAllToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.DisconnectAll();
		}

		private void saveConnectionsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.SaveDatabases();
		}

		private void loadConnectionsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DialogResult result = MessageBox.Show(this, Resources.LoadConnections, "Confirm",
				MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
			if (result == DialogResult.Yes) {
				this.LoadDatabases();
			}
		}

		public bool DisconnectAll()
		{
			foreach (DatabaseNode dbNode in treeView.Nodes) {
				if (!this.Disconnect(dbNode)) {
					return false;
				}
			}
			return true;
		}

		private void SortDatabases(object sender, EventArgs e)
		{
			DatabasesManager.Sort();
			this.LoadDatabases(false);
		}

		private void joinQueryMenuItem_Click(object sender, EventArgs e)
		{
			var foreignKey = this.GetSelectedNode<ForeignKey>();
			OpenQuery(foreignKey.Database, foreignKey.GenerateSelectStatement());
		}

		private void connectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.SelectedDatabaseNode.ConnectAsync(false, null);
		}

		private void reconnectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.SelectedDatabaseNode.ConnectAsync(true, null);
		}

		private void databaseContextMenu_Opening(object sender, CancelEventArgs e)
		{
			e.Cancel = this.SelectedDatabaseNode.IsBusy;
		}
	}
}
