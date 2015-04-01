using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Windows.Forms;
using DataDevelop.Data;
using DataDevelop.UIComponents;

namespace DataDevelop
{
	class DatabaseNode : AsyncTreeNode
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
			OnConnected(EventArgs.Empty);
		}

		public void Disconnect()
		{
			database.DisconnectAll();
			if (!database.IsConnected) {
				SetImage(false);
			}
		}

		internal void SetImage(bool isConnected)
		{
			if (isConnected) {
				ImageKey = SelectedImageKey = dbOnImageKey;
			} else {
				ImageKey = SelectedImageKey = dbOffImageKey;
			}
		}

		public void ConnectAsync(bool reconnect, Action onConnected)
		{
			if (!this.IsBusy) {
				this.RunAsyncOperation("Connecting",
					delegate
					{
						this.Database.Connect();
					},
					delegate
					{
						this.SetImage(true);
						if (onConnected != null) {
							onConnected();
						}
						this.OnConnected(EventArgs.Empty);
					},
					null);
			}
		}

		public event EventHandler Connected;

		public virtual void OnConnected(EventArgs e)
		{
			this.SetImage(this.database.IsConnected);
			if (Connected != null) {
				Connected(this, e);
			}
		}
	}
}
