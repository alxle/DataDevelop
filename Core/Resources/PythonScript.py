class Row:
	def __init__(self, dataRow):
		self.__dict__["_dataRow"] = dataRow

	def __getattr__(self, name):
		try:
			return self._dataRow[name]
		except KeyError:
			raise AttributeError(f"'Row' object has no attribute '{name}'")

	def __setattr__(self, name, value):
		self._dataRow[name] = value

	def __repr__(self):
		return f"<Row({self})>"

	def __str__(self):
		return ', '.join(f"{c.ColumnName}: {self._dataRow[c]}" for c in self._dataRow.Table.Columns)


class Table:
	DetachedState = 1
	UnchangedState = 2

	def __init__(self, table):
		self.base = table
		self._autoSave = True
		db = self.base.Database
		with DbConnection(db):
			self._adapter = db.CreateAdapter(self.base)
			self._dataTable = table.GetData(0, 0)

	def _GetAutoSaveChanges(self):
		return self._autoSave

	def _SetAutoSaveChanges(self, value):
		self._autoSave = value

	AutoSaveChanges = property(_GetAutoSaveChanges, _SetAutoSaveChanges)

	def __iter__(self):
		return self.Select()

	def Select(self, where=None):
		isLambda = callable(where)
		isString = isinstance(where, str)
		db = self.base.Database
		with DbConnection(db):
			select = self._adapter.SelectCommand.Clone()
			if isString:
				select.CommandText += " WHERE " + where
			with select.ExecuteReader() as reader:
				while reader.Read():
					dataRow = self._dataTable.NewRow()
					for i in range(reader.FieldCount):
						dataRow[reader.GetName(i)] = reader[i]
					self._dataTable.Rows.Add(dataRow)
					dataRow.AcceptChanges()
					row = Row(dataRow)
					if isLambda:
						if where(row):
							yield row
					else:
						yield row
					if dataRow.RowState == self.UnchangedState:
						self._dataTable.Rows.Remove(dataRow)
					elif self.AutoSaveChanges:
						self.SaveChanges()
			select.Dispose()

	def Where(self, where=None):
		return self.Select(where)

	def NewRow(self):
		return Row(self._dataTable.NewRow())

	def Insert(self, row):
		dataRow = row._dataRow
		if dataRow.RowState == self.DetachedState:
			self._dataTable.Rows.Add(dataRow)
		else:
			newRow = self._dataTable.NewRow()
			newRow.ItemArray = dataRow.ItemArray
			self._dataTable.Rows.Add(newRow)
		if self.AutoSaveChanges:
			self.SaveChanges()

	def Update(self, row):
		dataRow = row._dataRow
		if dataRow.RowState == self.UnchangedState:
			dataRow.SetModified()
		if self.AutoSaveChanges:
			self.SaveChanges()

	def Delete(self, row):
		row._dataRow.Delete()
		if self.AutoSaveChanges:
			self.SaveChanges()

	def Import(self, row):
		newRow = self.NewRow()
		newRow._dataRow.ItemArray = row._dataRow.ItemArray
		self.Insert(newRow)

	def ImportAll(self, fromTable):
		for row in fromTable:
			self.Import(row)

	def SaveChanges(self):
		self._adapter.Update(self._dataTable)
		self._dataTable.Rows.Clear()

	def ClearChanges(self):
		self._dataTable.Rows.Clear()

	def __repr__(self):
		return f"<Table(Name: {self.base.Name}, AutoSaveChanges: {self.AutoSaveChanges})>"

	def __str__(self):
		return f"Table: {self.base.Name}"


class Database:
	def __init__(self, database):
		if isinstance(database, str):
			if not _dbs.ContainsKey(database):
				raise Exception(f"Database '{database}' not found.")
			db = _dbs[database]
			self.base = db
		else:
			self.base = database
		self.base.Connect()
		self.base.Disconnect()

	@staticmethod
	def Create(provider, connectionString):
		if not _providers.ContainsKey(provider):
			raise Exception(f"DbProvider '{provider}' not found.")
		db = _providers[provider].CreateDatabase("<None>", connectionString)
		return Database(db)
	
	def __getattr__(self, name):
		table = self.base.GetTable(name)
		if table is None:
			table = self.base.GetTable(name.replace('_', ' '))
		if table is None:
			raise AttributeError(f"'{self.__class__.__name__}' object has no attribute '{name}'")
		t = Table(table)
		self.__dict__[name] = t
		return t

	def Query(self, command, *values):
		result = self.base.Query(command, *values) if values else self.base.Query(command)
		for dataRow in result.Rows:
			yield Row(dataRow)

	def Execute(self, command, *values):
		return self.base.NonQuery(command, *values)

	def NonQuery(self, command, *values):
		self.Execute(command, *values)

	def Scalar(self, command, *values):
		result = self.base.Query(command, *values)
		return result.Rows[0][0]

	def QueryFirst(self, command, *values):
		result = self.base.Query(command, *values)
		return Row(result.Rows[0])

	def GetData(self, command, *values):
		return self.base.Query(command, *values)

	def __repr__(self):
		return f"<Database(Name: {self.base.Name}, Provider: {self.base.Provider.Name})>"

	def __str__(self):
		return f"Database: {self.base.Name}"

	def __enter__(self):
		self.base.Connect()
		return self

	def __exit__(self, exc_type, exc_value, traceback):
		self.base.Disconnect()
		return False


class DbConnection:
	def __init__(self, db):
		self.db = db.base if isinstance(db, Database) else db

	def __enter__(self):
		self.db.Connect()
		return self.db

	def __exit__(self, exc_type, exc_value, traceback):
		self.db.Disconnect()
		return False
