using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.Common;
using DataDevelop.Data;


namespace DataDevelop
{
	class DatabaseNode : TreeNode
	{
		const string dbOnImageKey = "db";
		const string dbOffImageKey = "db2";
		Database database;

		public DatabaseNode(Database database)
		{
			this.database = database;
			this.Name = this.Text = database.Name;
			this.SetImage(database.IsConnected);
		}

		public Database Database
		{
			get { return database; }
		}

		public virtual void Connect()
		{
			database.Connect();
			SetImage(true);
		}

		public void Disconnect()
		{
			database.Disconnect();
			if (!database.IsConnected) {
				SetImage(false);
			}
		}

		protected void SetImage(bool isConnected)
		{
			if (isConnected) {
				ImageKey = SelectedImageKey = dbOnImageKey;
			} else {
				ImageKey = SelectedImageKey = dbOffImageKey;
			}
		}
		
	}
}
