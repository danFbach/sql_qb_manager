using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Sql;
using static RAF_to_SQL.datasets;
using System.Data.Common;

namespace RAF_to_SQL
{

	class db_manager
	{
		#region global vars
		DataSet ds = new DataSet( );
		fileSpec spec = new fileSpec( );
		partField part = new partField( );
		dbConfig dbconfig = new dbConfig( );
		vendorFields vendor = new vendorFields( );
		dataSwitches _switch = new dataSwitches( );
		productField product = new productField( );
		SqlConnection sql_conn = new SqlConnection( );
		string space = "                                                                         ";
		string fullString = "";
		int position = 0;
		int rowsChanged = 0;
		#endregion global vars
		#region db_utils
		public void openDBcnxtn()
		{
			sql_conn.ConnectionString = dbconfig.inven_general_conn;
			sql_conn.Open( );
		}
		public SqlCommandBuilder createCommandBuilder(string table)
		{
			DataSet dataset = new DataSet( );
			SqlCommandBuilder scb = new SqlCommandBuilder( );
			SqlDataAdapter sda;
			sda = new SqlDataAdapter("SELECT TOP(1) * FROM " + table, sql_conn);
			sda.Fill(dataset, table);

			scb = new SqlCommandBuilder(sda);
			return scb;
		}
		public SqlCommandBuilder createCommandBuilder(string sqlQuery, string table)
		{
			DataSet dataset = new DataSet( );
			SqlCommandBuilder scb = new SqlCommandBuilder( );
			SqlDataAdapter sda;
			sda = new SqlDataAdapter(sqlQuery, sql_conn);
			sda.Fill(dataset, table);
			scb = new SqlCommandBuilder(sda);
			return scb;
		}
		#endregion db_utils
		#region universal tools
		public string selectQuery(sqlParameters ssp)
		{
			SqlDataAdapter sda1 = new SqlDataAdapter(ssp.SQLcmd, ssp.db_connector);
			List<int> sendSpec = new List<int>( );
			switch(ssp.tableName)
			{
				case "Inven_SQL.dbo.Products":
					sendSpec = spec.productTxSpec;
					break;
				case "Inven_SQL.dbo.Parts":
					sendSpec = spec.partTxSpec;
					break;
				case "Inven_SQL.dbo.vendor":
					sendSpec = spec.vendorTxSpec;
					break;
			}
			ssp.db_connector.Open( );
			sda1.Fill(ds, ssp.tableName);
			if(ds.Tables.Count == 1)
			{
				if(ds.Tables[0].Rows.Count == 1)
				{
					int fieldCount = ds.Tables[0].Rows[0].ItemArray.Count( );
					for(int i = 0; i < fieldCount; i++)
					{
						fullString += (ds.Tables[0].Rows[0].ItemArray[i].ToString( ) + space).Remove(sendSpec[i]);
					}
				}
			}
			sql_conn.Close( );
			return fullString;
		}
		public int delete_row(sqlParameters ssp)
		{
			SqlCommand sqlCommand = new SqlCommand(ssp.SQLcmd, ssp.db_connector);
			ssp.db_connector.Open( );
			rowsChanged = sqlCommand.ExecuteNonQuery( );
			ssp.db_connector.Close( );
			return rowsChanged;
		}
		#endregion universal tools
		#region string_along
		public void create_string_along(vendorFields vendor)
		{

		}
		public void update_string_along(sqlParameters ssp, vendorFields vendor)
		{
		}
		public stringAlongField delete_string_along(stringAlongField stringAlong, sqlParameters ssp)
		{
			return stringAlong;
		}
		public void query_string_along(vendorFields vendor)
		{

		}
		#endregion string_along
		#region product
		public void insert_product(sqlParameters ssp, string insertData)
		{
			for(int i = 0; i < spec.partTxSpec.Count - 1; i++)
			{
				//product = _switch.productSwitchFromQB(i, insertData.Substring(position, spec.txPartSpec[i]), part);
				position += spec.partTxSpec[i];
			}
			DataSet ds = new DataSet( );
			SqlCommandBuilder scb;
			SqlDataAdapter sda = new SqlDataAdapter(ssp.SQLcmd, ssp.db_connector);
			sda.Fill(ds, ssp.tableName);
			scb = new SqlCommandBuilder(sda);
			SqlCommand insert = scb.GetInsertCommand(true);
			insert.Connection = ssp.db_connector;
			if(part.part_name.Trim( ) != null)
			{
				//insert = _switch.productSwitchToSQLInsert(insert, part);
				ssp.db_connector.Open( );
				insert.ExecuteNonQuery( );
				ssp.db_connector.Close( );
			}
		}
		public void update_products(sqlParameters ssp, string updateData)
		{
			List<string> parametersRAW = new List<string>( );
			for(int i = 0; i < spec.productTxSpec.Count - 1; i++)
			{
				product = _switch.prodSwitch(i, updateData.Substring(position, spec.productTxSpec[i]), product);
				position += spec.productTxSpec[i];
			}
			SqlCommandBuilder scb;
			DataSet ds = new DataSet( );
			ssp.SQLcmd = "SELECT * FROM " + ssp.tableName + " WHERE " + ssp.searchKey + " = " + ssp.searchVal;
			SqlDataAdapter sda = new SqlDataAdapter(ssp.SQLcmd, ssp.db_connector);
			sda.Fill(ds, ssp.tableName);
			scb = new SqlCommandBuilder(sda);
			SqlCommand update = scb.GetUpdateCommand(true);
			update.Connection = ssp.db_connector;
			if(int.TryParse(ds.Tables[0].Rows[0].ItemArray[0].ToString( ), out int Id))
			{
				update = _switch.productSqlSwitchUpdate(update, product, Id, ds.Tables[0].Rows[0].ItemArray);
			}
			ssp.db_connector.Open( );
			update.ExecuteNonQuery( );
			ssp.db_connector.Close( );
		}
		#endregion product
		#region part
		public void insert_part(sqlParameters ssp, string insertData)
		{
			for(int i = 0; i < spec.partTxSpec.Count - 1; i++)
			{
				part = _switch.partSwitchFromQB(i, insertData.Substring(position, spec.partTxSpec[i]), part);
				position += spec.partTxSpec[i];
			}
			DataSet ds = new DataSet( );
			SqlCommandBuilder scb;
			SqlDataAdapter sda = new SqlDataAdapter(ssp.SQLcmd, ssp.db_connector);
			sda.Fill(ds, ssp.tableName);
			scb = new SqlCommandBuilder(sda);
			SqlCommand insert = scb.GetInsertCommand(true);
			insert.Connection = ssp.db_connector;
			if(part.part_name.Trim( ) != null)
			{
				insert = _switch.partSwitchToSQLInsert(insert, part);
				ssp.db_connector.Open( );
				insert.ExecuteNonQuery( );
				ssp.db_connector.Close( );
			}
		}
		public void update_part(sqlParameters ssp, string updateData)
		{
			List<string> parametersRAW = new List<string>( );
			for(int i = 0; i < spec.partTxSpec.Count - 1; i++)
			{
				part = _switch.partSwitchFromQB(i, updateData.Substring(position, spec.partTxSpec[i]), part);
				position += spec.partTxSpec[i];
			}
			SqlCommandBuilder scb;
			DataSet ds = new DataSet( );
			ssp.SQLcmd = "SELECT * FROM " + ssp.tableName + " WHERE " + ssp.searchKey + " = " + ssp.searchVal;
			SqlDataAdapter sda = new SqlDataAdapter(ssp.SQLcmd, ssp.db_connector);
			sda.Fill(ds, ssp.tableName);
			scb = new SqlCommandBuilder(sda);
			SqlCommand update = scb.GetUpdateCommand(true);
			update.Connection = ssp.db_connector;
			if(int.TryParse(ds.Tables[0].Rows[0].ItemArray[0].ToString( ), out int Id))
			{
				update = _switch.partSwitchToSQLUpdate(update, part, Id, ds.Tables[0].Rows[0].ItemArray);
			}
			ssp.db_connector.Open( );
			update.ExecuteNonQuery( );
			ssp.db_connector.Close( );
		}
		#endregion part
		#region vendor
		public void insert_vendor(sqlParameters ssp, string insertData)
		{
			for(int i = 0; i < spec.vendorTxSpec.Count - 1; i++)
			{
				vendor = _switch.vendorSwitch(i, insertData.Substring(position, spec.vendorTxSpec[i]), vendor);
				position += spec.vendorTxSpec[i];
			}
			DataSet ds = new DataSet( );
			SqlCommandBuilder scb;
			SqlDataAdapter sda = new SqlDataAdapter(ssp.SQLcmd, ssp.db_connector);
			sda.Fill(ds, ssp.tableName);
			scb = new SqlCommandBuilder(sda);
			SqlCommand insert = scb.GetInsertCommand(true);
			insert.Connection = ssp.db_connector;
			if(vendor.v_code.Trim( ) != null)
			{
				insert = _switch.vendorSwitchToSQLInsert(insert, vendor);
				ssp.db_connector.Open( );
				insert.ExecuteNonQuery( );
				ssp.db_connector.Close( );
			}
		}
		public void update_vendor(sqlParameters ssp, string updateData)
		{
			int lent = updateData.Length;
			for(int i = 0; i < spec.vendorTxSpec.Count; i++)
			{
				vendor = _switch.vendorSwitch(i, updateData.Substring(position, spec.vendorTxSpec[i]), vendor);
				position += spec.vendorTxSpec[i];
			}
			SqlCommandBuilder scb;
			DataSet ds = new DataSet( );
			ssp.SQLcmd = "SELECT * FROM " + ssp.tableName + " WHERE " + ssp.searchKey + " = " + ssp.searchVal;
			SqlDataAdapter sda = new SqlDataAdapter(ssp.SQLcmd, ssp.db_connector);
			sda.Fill(ds, ssp.tableName);
			scb = new SqlCommandBuilder(sda);
			SqlCommand update = scb.GetUpdateCommand(true);
			update.Connection = ssp.db_connector;
			if(int.TryParse(ds.Tables[0].Rows[0].ItemArray[0].ToString( ), out int Id)) { update = _switch.vendorSwitchToSQLUpdate(update, vendor, ds.Tables[0].Rows[0].ItemArray); }
			ssp.db_connector.Open( );
			update.ExecuteNonQuery( );
			ssp.db_connector.Close( );
		}
		public List<string> query_vendor(sqlParameters ssp)
		{
			List<string> response = new List<string>( );
			string fullString = "";
			fileSpec spec = new fileSpec( );
			DataSet dataSet = new DataSet( );
			SqlDataAdapter sda1 = new SqlDataAdapter(ssp.SQLcmd, ssp.db_connector);
			ssp.db_connector.Open( );
			sda1.Fill(dataSet, ssp.tableName);
			if(dataSet.Tables.Count == 1)
			{
				if(dataSet.Tables[0].Rows.Count == 1)
				{
					int fieldCount = dataSet.Tables[0].Rows[0].ItemArray.Count( );
					for(int i = 0; i < fieldCount - 1; i++)
					{
						fullString += (dataSet.Tables[0].Rows[0].ItemArray[i].ToString( ) + space).Remove(spec.vendorTxSpec[i]);
					}
					response.Add(fullString);
				}
				if(dataSet.Tables[0].Rows.Count > 1)
				{
					foreach(DataRow row in dataSet.Tables[0].Rows)
					{
						fullString = "";
						for(int i = 0; i < row.ItemArray.Count( ); i++)
						{
							fullString += (row.ItemArray[i].ToString( ) + space).Remove(spec.vendorTxSpec[i]);
						}
						response.Add(fullString);
					}
				}
			}
			sql_conn.Close( );

			return response;
		}
		#endregion vendor
		#region import data
		public void importProducts(List<productField> products)
		{
			SqlCommandBuilder scb = createCommandBuilder(dbconfig.productTable);
			SqlCommand insert = scb.GetInsertCommand(true);
			List<SqlCommand> insertCommands = new List<SqlCommand>( );
			int updated = 0;
			openDBcnxtn( );
			foreach(productField product in products)
			{
				SqlCommand insertTemp = insert.Clone( );
				insertTemp.CommandType = CommandType.Text;
				insertTemp.Connection = sql_conn;
				_switch.productSqlSwitchInsert(insertTemp, product, updated);
				updated += insertTemp.ExecuteNonQuery( );
			}
			Console.WriteLine(updated + " Products added to Database.");
			Console.ReadKey( );
		}
		public void importProductParts(List<productField> products)
		{
			SqlCommandBuilder scb = createCommandBuilder(dbconfig.prodReqdPartTable);
			SqlCommand insert = scb.GetInsertCommand(true);
			List<SqlCommand> insertCommands = new List<SqlCommand>( );
			int updated = 0;
			openDBcnxtn( );
			foreach(productField product in products)
			{
				foreach(KeyValuePair<string, int> part in product.parts_reqd)
				{
					SqlCommand insertTemp = insert.Clone( );
					insertTemp.CommandType = CommandType.Text;
					insertTemp.Connection = sql_conn;
					foreach(SqlParameter param in insertTemp.Parameters)
					{
						switch(param.SourceColumn)
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
					updated += insertTemp.ExecuteNonQuery( );
				}
			}
			Console.WriteLine(updated + " Products added to Database.");
			Console.ReadKey( );
		}
		public void importParts(List<partFieldImport> parts)
		{
			SqlCommandBuilder scb = createCommandBuilder(dbconfig.partTable);
			SqlCommand insert = scb.GetInsertCommand(true);
			List<SqlCommand> insertCommands = new List<SqlCommand>( );
			int updated = 0;
			openDBcnxtn( );
			foreach(partFieldImport part in parts)
			{
				SqlCommand insertTemp = insert.Clone( );
				insertTemp.CommandType = CommandType.Text;
				insertTemp.Connection = sql_conn;
				_switch.partSqlImportSwitch(insertTemp, part, updated);
				updated += insertTemp.ExecuteNonQuery( );
			}
			Console.WriteLine(updated + " Parts added to Database.");
			Console.ReadKey( );
		}
		public void load_Into_SQL(List<vendorFields> vendors, sqlParameters ssp)
		{
			int added = 0;
			List<SqlCommand> sqlCommands = new List<SqlCommand>( );
			SqlCommandBuilder scb = createCommandBuilder(dbconfig.vendorTable);
			SqlCommand insert = scb.GetInsertCommand( );
			SqlCommand holder = new SqlCommand( );
			foreach(vendorFields _v in vendors)
			{
				holder = insert.Clone( );
				foreach(SqlParameter param in holder.Parameters)
				{
					if(param.SourceColumn == "id")
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

			openDBcnxtn( );
			foreach(SqlCommand s in sqlCommands)
			{
				DataSet ds = new DataSet( );
				SqlDataAdapter sda1 = new SqlDataAdapter(ssp.SQLcmd + " * FROM " + ssp.tableName + " WHERE " + ssp.searchKey + " = " + s.Parameters[0].Value.ToString( ), sql_conn);
				sda1.Fill(ds, "vendor");
				if(ds.Tables["vendor"].Rows.Count == 0)
				{
					added += s.ExecuteNonQuery( );
				}
			}
			sql_conn.Close( );
			Console.WriteLine(added + " vendors added to database.");
			Console.ReadKey( );
		}
		#endregion import data
	}
}
