using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Sql;
using static RAFtest.datasets;

namespace RAFtest
{

	class db_manager
	{
		#region global vars
		SqlConnection sql_conn = new SqlConnection();
		dbConfig dbconfig = new dbConfig();
		dataSwitches _switch = new dataSwitches();
		#endregion global vars
		public void load_Into_SQL(List<vendorFields> vendors, sqlSearchParameters ssp)
		{
			int added = 0;
			List<SqlCommand> sqlCommands = new List<SqlCommand>();
			openDBcnxtn();
			SqlCommandBuilder scb = createCommandBuilder(dbconfig.vendorTable);
			SqlCommand insert = scb.GetInsertCommand();
			SqlCommand holder = new SqlCommand();
			foreach (vendorFields _v in vendors)
			{
				holder = insert.Clone();
				foreach (SqlParameter param in holder.Parameters)
				{
					if (param.SourceColumn == "id")
					{
						param.Value = _v.id;
					}
					else
					{
						param.Value = _switch.vendorSqlSwitch(param.SourceColumn, _v);
					}
				}
				holder.Connection = sql_conn;
				holder.CommandType = System.Data.CommandType.Text;

				sqlCommands.Add(holder);
			}

			foreach (SqlCommand s in sqlCommands)
			{
				DataSet ds = new DataSet();
				SqlDataAdapter sda1 = new SqlDataAdapter(ssp.sqlCMD + " * FROM " + ssp.tableName + " WHERE " + ssp.searchKey + " = " + s.Parameters[0].Value.ToString(), sql_conn);
				sda1.Fill(ds, "vendor");
				if (ds.Tables["vendor"].Rows.Count == 0)
				{
					added += s.ExecuteNonQuery();
				}
			}
			sql_conn.Close();
			Console.WriteLine(added + " vendors added to database.");
			Console.ReadKey();
		}
		#region db_utils
		public void openDBcnxtn()
		{
			sql_conn.ConnectionString = dbconfig.inven_general_conn;
			sql_conn.Open();
		}
		public SqlCommandBuilder createCommandBuilder(string table)
		{
			DataSet dataset = new DataSet();
			SqlCommandBuilder scb = new SqlCommandBuilder();
			SqlDataAdapter sda;
			sda = new SqlDataAdapter("SELECT TOP(1) * FROM " + table, sql_conn);
			sda.Fill(dataset, table);
			scb = new SqlCommandBuilder(sda);
			return scb;
		}
		public SqlCommandBuilder createCommandBuilder(string sqlQuery, string table)
		{
			DataSet dataset = new DataSet();
			SqlCommandBuilder scb = new SqlCommandBuilder();
			SqlDataAdapter sda;
			sda = new SqlDataAdapter(sqlQuery, sql_conn);
			sda.Fill(dataset, table);
			scb = new SqlCommandBuilder(sda);
			return scb;
		}
		public SqlDataReader selectQuery(string command = "WHERE Id = 0", string table = "Products")
		{
			SqlCommand sql = new SqlCommand();
			sql.Connection = sql_conn;
			sql.CommandType = CommandType.Text;
			sql.CommandText = "SELECT * FROM " + table + " " + command;
			return sql.ExecuteReader();

		}
		public rawDataAndType dataReader(int i, SqlDataReader sdr, rawDataAndType rawData)
		{
			rawData = new rawDataAndType();
			rawData.raw = sdr[i].ToString();
			rawData.type = sdr[i].GetType();
			return rawData;
		}
		#endregion db_utils
		#region string_along
		public void create_string_along(vendorFields vendor)
		{

		}

		public void update_string_along(sqlSearchParameters ssp, vendorFields vendor)
		{
			List<SqlCommand> sqlCommands = new List<SqlCommand>();
			SqlCommandBuilder scb = new SqlCommandBuilder();
			SqlDataAdapter sda;
			DataSet vendorDataSet = new DataSet();
			sql_conn.ConnectionString = dbconfig.inven_general_conn;
			sql_conn.Open();
			sda = new SqlDataAdapter("SELECT TOP(1) * FROM " + ssp.tableName, sql_conn);
			sda.Fill(vendorDataSet, "vendor");
			scb = new SqlCommandBuilder(sda);
			SqlCommand update = scb.GetUpdateCommand(true);
			ssp.searchVal = vendor.id.ToString();
			sample_data sampledata = new sample_data();

			//select the vendor being used
			sql_conn.ConnectionString = dbconfig.inven_general_conn;
			DataSet ds = new DataSet();
			sql_conn.Open();
			SqlDataAdapter sda1 = new SqlDataAdapter(ssp.sqlCMD + " * FROM " + ssp.tableName + " WHERE " + ssp.searchKey + " = " + ssp.searchVal, sql_conn);
			sda1.Fill(ds, "vendor");
			scb = new SqlCommandBuilder(sda1);
			//((System.Data.DataTable)(new System.Collections.ArrayList.ArrayListDebugView(ds.Tables.List).Items[0])).Columns.List
			if (ds.Tables["vendor"].Rows.Count == 1)
			{
				object[] drd = ds.Tables["vendor"].Rows[0].ItemArray;
				for (int i = 1; i <= drd.Count() - 1; i++)
				{
					vendor = _switch.vendorSwitch(i, drd[i].ToString(), vendor);
				}
				foreach (SqlParameter param in update.Parameters)
				{
					if (param.SourceColumn.ToString() == "id")
					{
						continue;
					}
					else
					{
						param.Value = _switch.vendorSqlSwitch(param.SourceColumn, vendor);
					}
				}
				update.ExecuteNonQuery();
			}
			sql_conn.Close();
		}

		public vendorFields delete_string_along(vendorFields vendor, sqlSearchParameters ssp)
		{
			ssp.searchVal = vendor.id.ToString();
			SqlCommandBuilder scb = new SqlCommandBuilder();
			sample_data sampledata = new sample_data();

			//select the vendor being used
			sql_conn.ConnectionString = dbconfig.inven_general_conn;
			DataSet ds = new DataSet();
			sql_conn.Open();
			SqlDataAdapter sda1 = new SqlDataAdapter(ssp.sqlCMD + " * FROM " + ssp.tableName + " WHERE " + ssp.searchKey + " = " + ssp.searchVal, sql_conn);
			sda1.Fill(ds, "vendor");
			scb = new SqlCommandBuilder(sda1);
			object[] drd = ds.Tables["vendor"].Rows[0].ItemArray;
			for (int i = 1; i <= drd.Count() - 1; i++)
			{
				vendor = _switch.vendorSwitch(i, drd[i].ToString(), vendor);
			}
			return vendor;
		}

		public void query_string_along(vendorFields vendor)
		{

		}
		#endregion string_along
		#region product
		public void importProducts(List<productFields> products)
		{
			openDBcnxtn();
			SqlCommandBuilder scb = createCommandBuilder(dbconfig.productTable);
			SqlCommand insert = scb.GetInsertCommand(true);
			List<SqlCommand> insertCommands = new List<SqlCommand>();
			int updated = 0;
			foreach (productFields product in products)
			{
				SqlCommand insertTemp = insert.Clone();
				insertTemp.CommandType = CommandType.Text;
				insertTemp.Connection = sql_conn;
				_switch.productSqlSwitch(insertTemp, product, updated);
				updated += insertTemp.ExecuteNonQuery();
			}
			Console.WriteLine(updated + " Products added to Database.");
			Console.ReadKey();
		}
		public void importProductParts(List<productFields> products)
		{
			openDBcnxtn();
			SqlCommandBuilder scb = createCommandBuilder(dbconfig.prodReqdPartTable);
			SqlCommand insert = scb.GetInsertCommand(true);
			List<SqlCommand> insertCommands = new List<SqlCommand>();
			int updated = 0;
			foreach (productFields product in products)
			{
				foreach (KeyValuePair<string, int> part in product.parts_reqd)
				{
					SqlCommand insertTemp = insert.Clone();
					insertTemp.CommandType = CommandType.Text;
					insertTemp.Connection = sql_conn;
					foreach (SqlParameter param in insertTemp.Parameters)
					{
						switch (param.SourceColumn)
						{
							case "Id":
								param.Value = updated;
								continue;
							case "product_Id_FK":
								param.Value = product.Product_Number;
								continue;
							case "part_id_FK":
								param.Value = part.Key;
								continue;
							case "quantity_reqd":
								param.Value = part.Value;
								continue;
						}
					}
					updated += insertTemp.ExecuteNonQuery();
				}
			}
			Console.WriteLine(updated + " Products added to Database.");
			Console.ReadKey();
		}
		public void create_product(vendorFields vendor)
		{

		}
		public void update_product(productFields product)
		{
			openDBcnxtn();
			SqlDataReader sdr = selectQuery("WHERE id = 0", dbconfig.productTable);
			sdr.Read();
			for(int i = 0; i < dbconfig.productColCount; i++)
			{
				dataReader
			}
			List<rawDataAndType> rawData = dataReader(dbconfig.productColCount, sdr);
			SqlCommandBuilder scb = createCommandBuilder("SELECT TOP(1) * FROM " + dbconfig.productTable, dbconfig.productTable);

			SqlCommand update = scb.GetUpdateCommand();

			sql_conn.Close();
		}
		public vendorFields delete_product(vendorFields vendor, sqlSearchParameters ssp)
		{
			ssp.searchVal = vendor.id.ToString();
			SqlCommandBuilder scb = new SqlCommandBuilder();
			sample_data sampledata = new sample_data();

			//select the vendor being used
			sql_conn.ConnectionString = dbconfig.inven_general_conn;
			DataSet ds = new DataSet();
			sql_conn.Open();
			SqlDataAdapter sda1 = new SqlDataAdapter(ssp.sqlCMD + " * FROM " + ssp.tableName + " WHERE " + ssp.searchKey + " = " + ssp.searchVal, sql_conn);
			sda1.Fill(ds, "vendor");
			scb = new SqlCommandBuilder(sda1);
			object[] drd = ds.Tables["vendor"].Rows[0].ItemArray;
			for (int i = 1; i <= drd.Count() - 1; i++)
			{
				vendor = _switch.vendorSwitch(i, drd[i].ToString(), vendor);
			}
			return vendor;
		}
		public void query_product(vendorFields vendor)
		{

		}
		#endregion product
		#region part

		public void importParts(List<partField> parts)
		{
			openDBcnxtn();
			SqlCommandBuilder scb = createCommandBuilder(dbconfig.partTable);
			SqlCommand insert = scb.GetInsertCommand(true);
			List<SqlCommand> insertCommands = new List<SqlCommand>();
			int updated = 0;
			foreach (partField part in parts)
			{
				SqlCommand insertTemp = insert.Clone();
				insertTemp.CommandType = CommandType.Text;
				insertTemp.Connection = sql_conn;
				_switch.partSqlParamSwitch(insertTemp, part, updated);
				updated += insertTemp.ExecuteNonQuery();
				//insertCommands.Add(insertTemp);
			}
			//foreach(SqlCommand sqlCmd in insertCommands)
			//{
			//	updated += sqlCmd.ExecuteNonQuery();
			//}
			Console.WriteLine(updated + " Parts added to Database.");
			Console.ReadKey();
		}
		public void create_part(vendorFields vendor)
		{

		}

		public void update_part(sqlSearchParameters ssp, vendorFields vendor)
		{
			List<SqlCommand> sqlCommands = new List<SqlCommand>();
			SqlCommandBuilder scb = new SqlCommandBuilder();
			SqlDataAdapter sda;
			DataSet vendorDataSet = new DataSet();
			sql_conn.ConnectionString = dbconfig.inven_general_conn;
			sql_conn.Open();
			sda = new SqlDataAdapter("SELECT TOP(1) * FROM " + ssp.tableName, sql_conn);
			sda.Fill(vendorDataSet, "vendor");
			scb = new SqlCommandBuilder(sda);
			SqlCommand update = scb.GetUpdateCommand(true);
			ssp.searchVal = vendor.id.ToString();
			sample_data sampledata = new sample_data();

			//select the vendor being used
			sql_conn.ConnectionString = dbconfig.inven_general_conn;
			DataSet ds = new DataSet();
			sql_conn.Open();
			SqlDataAdapter sda1 = new SqlDataAdapter(ssp.sqlCMD + " * FROM " + ssp.tableName + " WHERE " + ssp.searchKey + " = " + ssp.searchVal, sql_conn);
			sda1.Fill(ds, "vendor");
			scb = new SqlCommandBuilder(sda1);
			//((System.Data.DataTable)(new System.Collections.ArrayList.ArrayListDebugView(ds.Tables.List).Items[0])).Columns.List
			if (ds.Tables["vendor"].Rows.Count == 1)
			{
				object[] drd = ds.Tables["vendor"].Rows[0].ItemArray;
				for (int i = 1; i <= drd.Count() - 1; i++)
				{
					vendor = _switch.vendorSwitch(i, drd[i].ToString(), vendor);
				}
				foreach (SqlParameter param in update.Parameters)
				{
					if (param.SourceColumn.ToString() == "id")
					{
						continue;
					}
					else
					{
						param.Value = _switch.vendorSqlSwitch(param.SourceColumn, vendor);
					}
				}
				update.ExecuteNonQuery();
			}
			sql_conn.Close();
		}

		public vendorFields delete_part(vendorFields vendor, sqlSearchParameters ssp)
		{
			ssp.searchVal = vendor.id.ToString();
			SqlCommandBuilder scb = new SqlCommandBuilder();
			sample_data sampledata = new sample_data();

			//select the vendor being used
			sql_conn.ConnectionString = dbconfig.inven_general_conn;
			DataSet ds = new DataSet();
			sql_conn.Open();
			SqlDataAdapter sda1 = new SqlDataAdapter(ssp.sqlCMD + " * FROM " + ssp.tableName + " WHERE " + ssp.searchKey + " = " + ssp.searchVal, sql_conn);
			sda1.Fill(ds, "vendor");
			scb = new SqlCommandBuilder(sda1);
			object[] drd = ds.Tables["vendor"].Rows[0].ItemArray;
			for (int i = 1; i <= drd.Count() - 1; i++)
			{
				vendor = _switch.vendorSwitch(i, drd[i].ToString(), vendor);
			}
			return vendor;
		}

		public void query_part(vendorFields vendor)
		{

		}
		#endregion part
		#region vendor
		public void create_vendor(vendorFields vendor)
		{

		}

		public void update_vendor(sqlSearchParameters ssp, vendorFields vendor)
		{
			List<SqlCommand> sqlCommands = new List<SqlCommand>();
			SqlCommandBuilder scb = new SqlCommandBuilder();
			SqlDataAdapter sda;
			DataSet vendorDataSet = new DataSet();
			sql_conn.ConnectionString = dbconfig.inven_general_conn;
			sql_conn.Open();
			sda = new SqlDataAdapter("SELECT TOP(1) * FROM " + ssp.tableName, sql_conn);
			sda.Fill(vendorDataSet, "vendor");
			scb = new SqlCommandBuilder(sda);
			SqlCommand update = scb.GetUpdateCommand(true);
			ssp.searchVal = vendor.id.ToString();
			sample_data sampledata = new sample_data();

			//select the vendor being used
			sql_conn.ConnectionString = dbconfig.inven_general_conn;
			DataSet ds = new DataSet();
			sql_conn.Open();
			SqlDataAdapter sda1 = new SqlDataAdapter(ssp.sqlCMD + " * FROM " + ssp.tableName + " WHERE " + ssp.searchKey + " = " + ssp.searchVal, sql_conn);
			sda1.Fill(ds, "vendor");
			scb = new SqlCommandBuilder(sda1);
			//((System.Data.DataTable)(new System.Collections.ArrayList.ArrayListDebugView(ds.Tables.List).Items[0])).Columns.List
			if (ds.Tables["vendor"].Rows.Count == 1)
			{
				object[] drd = ds.Tables["vendor"].Rows[0].ItemArray;
				for (int i = 1; i <= drd.Count() - 1; i++)
				{
					vendor = _switch.vendorSwitch(i, drd[i].ToString(), vendor);
				}
				foreach (SqlParameter param in update.Parameters)
				{
					if (param.SourceColumn.ToString() == "id")
					{
						continue;
					}
					else
					{
						param.Value = _switch.vendorSqlSwitch(param.SourceColumn, vendor);
					}
				}
				update.ExecuteNonQuery();
			}
			sql_conn.Close();
		}

		public vendorFields delete_vendor(vendorFields vendor, sqlSearchParameters ssp)
		{
			ssp.searchVal = vendor.id.ToString();
			SqlCommandBuilder scb = new SqlCommandBuilder();
			sample_data sampledata = new sample_data();

			//select the vendor being used
			sql_conn.ConnectionString = dbconfig.inven_general_conn;
			DataSet ds = new DataSet();
			sql_conn.Open();
			SqlDataAdapter sda1 = new SqlDataAdapter(ssp.sqlCMD + " * FROM " + ssp.tableName + " WHERE " + ssp.searchKey + " = " + ssp.searchVal, sql_conn);
			sda1.Fill(ds, "vendor");
			scb = new SqlCommandBuilder(sda1);
			object[] drd = ds.Tables["vendor"].Rows[0].ItemArray;
			for (int i = 1; i <= drd.Count() - 1; i++)
			{
				vendor = _switch.vendorSwitch(i, drd[i].ToString(), vendor);
			}
			return vendor;
		}

		public void query_vendor(vendorFields vendor)
		{

		}
		#endregion vendor
	}
}
