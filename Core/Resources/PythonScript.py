class Row:
	def __init__(self, dataRow):
		self.__dict__["_dataRow"] = dataRow
	
	def __getattr__(self, name):
		return self._dataRow[name]
	
	def __setattr__(self, name, value):
		self._dataRow[name] = value

class Table:
	def __init__(self, table):
		self.base = table
		self._autoSave = True
		db = self.base.Database
		db.Connect()
		self._adapter = db.CreateAdapter(self.base)
		self._dataTable = table.GetData(0, 0)
		db.Disconnect()
	
	def _GetAutoSaveChanges(self):
		return self._autoSave
		
	def _SetAutoSaveChanges(self, value):
		self._autoSave = value
	
	AutoSaveChanges = property(_GetAutoSaveChanges, _SetAutoSaveChanges)
	
	def __iter__(self):
		return self.Select()
	
	def Select(self, where = None):
		isLambda = callable(where)
		isString = isinstance(where, str)
		db = self.base.Database
		db.Connect()
		select = self._adapter.SelectCommand.Clone()
		if isString:
			select.CommandText += " WHERE " + where
		reader = select.ExecuteReader()
		while reader.Read():
			dataRow = self._dataTable.NewRow()
			#reader.GetValues(dataRow.ItemArray)
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
			if dataRow.RowState.ToString() == "Unchanged":
				self._dataTable.Rows.Remove(dataRow)
			elif self.AutoSaveChanges:
				self.SaveChanges()
		reader.Close()
		reader.Dispose()
		select.Dispose()
		db.Disconnect()
	
	def NewRow(self):
		return Row(self._dataTable.NewRow())
	
	def Insert(self, row):
		dataRow = row._dataRow;
		if dataRow.RowState.ToString() == "Detached":
			self._dataTable.Rows.Add(dataRow)
		else:
			newRow = self._dataTable.NewRow()
			newRow.ItemArray = dataRow.ItemArray;
			self._dataTable.Rows.Add(newRow)
		if self.AutoSaveChanges:
			self.SaveChanges()
	
	def Update(self, row):
		dataRow = row._dataRow
		if dataRow.RowState.ToString() == "Unchanged":
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

class Database:
	def __init__(self, database):
		if isinstance(database, str):
			if _dbs.ContainsKey(database):
				db = _dbs[database]
			else:
				raise Exception("Database '" + database + "' not found.")
			self.base = db
		else:
			self.base = database
		#self.RefreshSchema()
		self.base.Connect()
		self.base.Disconnect()
	
	def __getattr__(self, name):
		for table in self.base.Tables:
			tableName = table.Name.replace(" ", "_")
			if tableName == name:
				t = Table(table)
				self.__dict__[tableName] = t
				return t
	
	def Query(self, command, *values):
		from System.Collections import ArrayList
		list = ArrayList()
		for value in values:
			list.Add(value)
		if list.Count == 0:
			for dataRow in self.base.Query(command).Rows:
				yield Row(dataRow)
		elif list.Count == 1:
			for dataRow in self.base.Query(command, list[0]).Rows:
				yield Row(dataRow)
		else:
			for dataRow in self.base.Query(command, list.ToArray()).Rows:
				yield Row(dataRow)
	
	def NonQuery(self, command, *values):
		from System.Collections import ArrayList
		list = ArrayList()
		for value in values:
			list.Add(value)
		if list.Count == 0:
			return self.base.NonQuery(command)
		elif list.Count == 1:
			return self.base.NonQuery(command, list[0])
		else:
			return self.base.NonQuery(command, list.ToArray())
