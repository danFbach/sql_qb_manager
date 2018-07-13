using System.Data.SqlClient;
using System.Collections.Generic;
using static RAF_to_SQL.datasets;

namespace RAF_to_SQL
{
	public class mainSwitch
	{
		#region global vars
		Write w = new Write( );
		fileSpec spec = new fileSpec( );
		db_manager dbm = new db_manager( );
		sqlParameters ssp = new sqlParameters( );
		RAF_Read r = new RAF_Read( );
		dbConfig db = new dbConfig( );
		List<string> toDB = new List<string>( );
		#endregion global vars
		public void main(string[] _args)
		{
			ssp.db_connector = new SqlConnection( );
			ssp.db_connector.ConnectionString = db.inven_general_conn;
			string send;
			switch(_args[0])
			{
				case "-q":
					switch(_args[1])
					{
						case "-pr":
							if(int.TryParse(_args[2], out int prodQ))
							{
								if(prodQ > 0 && prodQ < 1000)
								{
									prodQ += 90000;
									ssp.tableName = "Inven_SQL.dbo.Products";
									ssp.searchVal = prodQ.ToString( );
									ssp.searchKey = "Product_Number";
									ssp.SQLcmd = "SELECT * FROM " + ssp.tableName + " WHERE " + ssp.searchKey + " = " + ssp.searchVal;
									send = dbm.selectQuery(ssp);
									if(send != null)
									{
										w.lineWrite(send, spec.sendbackpath);
									}
								}
							}
							break;
						case "-pt":
							if(int.TryParse(_args[2], out int partQ))
							{
								if(partQ > 0 && partQ < 4500)
								{
									partQ += 1000;
									ssp.tableName = "Inven_SQL.dbo.Parts";
									ssp.searchVal = partQ.ToString( );
									ssp.searchKey = "part_number";
									ssp.SQLcmd = "SELECT * FROM " + ssp.tableName + " WHERE " + ssp.searchKey + " = " + ssp.searchVal;
									send = dbm.selectQuery(ssp);
									if(send != null)
									{
										w.lineWrite(send, spec.sendbackpath);
									}
								}
							}
							break;
						case "-v":
							ssp.tableName = "vendor";
							if(spec.abc.Contains(_args[2]))
							{
								//for search by first letter of vcode
								ssp.searchKey = "v_code";
								ssp.searchVal = _args[2] + "%";
								ssp.SQLcmd = "SELECT * FROM " + ssp.tableName + " WHERE " + ssp.searchKey + " LIKE '" + ssp.searchVal + "'";
								spec.response = dbm.query_vendor(ssp);
							}
							else if(int.TryParse(_args[2], out int idQuery))
							{
								ssp.tableName = "Inven_SQL.dbo.vendor";
								ssp.searchVal = idQuery.ToString( );
								ssp.searchKey = "Id";
								ssp.SQLcmd = "SELECT * FROM " + ssp.tableName + " WHERE " + ssp.searchKey + " = " + ssp.searchVal;
								send = dbm.selectQuery(ssp);
							}
							else if(_args[2].Length > 1)
							{
								//searching by vendor code
								ssp.searchKey = "v_code";
								ssp.searchVal = _args[2];
								ssp.SQLcmd = "SELECT * FROM " + ssp.tableName + " WHERE " + ssp.searchKey + " = '" + ssp.searchVal + "'";
								spec.response = dbm.query_vendor(ssp);
							}
							if(spec.response.Count == 1)
							{
								w.lineWrite(spec.response[0], spec.sendbackpath);
							}
							else if(spec.response.Count > 1) { w.listWrite(spec.response, spec.sendbackpath); }
							break;
					}
					break;
				case "-i":
					//toDB = r.readFromFile(@"\\SOURCE\INVEN\TEMPDATA\toDB.txt");
					toDB = r.readFromFile(@"C:\Users\Dan\Documents\Visual Studio 2017\Projects\RAFtest\RAF_to_SQL\data\toDB.txt");
					if(toDB.Count > 0)
					{
						switch(_args[1])
						{
							case "-pr":
								ssp.searchKey = "Product_Number"; ssp.tableName = "Inven_SQL.dbo.Products"; ssp.SQLcmd = "SELECT TOP(1) * FROM " + ssp.tableName;
								dbm.insert_product(ssp, toDB[0]);
								break;
							case "-pt":
								ssp.searchKey = "part_number"; ssp.tableName = "Inven_SQL.dbo.Parts"; ssp.SQLcmd = "SELECT TOP(1) * FROM " + ssp.tableName;
								dbm.insert_part(ssp, toDB[0]);
								break;
							case "-v":
								ssp.searchKey = "1"; ssp.tableName = "Inven_SQL.dbo.vendor"; ssp.SQLcmd = "SELECT TOP(1) * FROM " + ssp.tableName;
								dbm.insert_vendor(ssp, toDB[0]);
								break;
						}
					}
					break;
				case "-u":
					//toDB = r.readFromFile(@"\\SOURCE\INVEN\TEMPDATA\toDB.txt");
					toDB = r.readFromFile(@"C:\Users\Dan\Documents\Visual Studio 2017\Projects\RAFtest\RAF_to_SQL\data\toDB.txt");
					if(toDB.Count > 0)
					{
						switch(_args[1])
						{
							case "-pr":
								//if(int.TryParse(_args[2], out int Uprod))
								//{
								//	ssp.searchVal = (Uprod += 90000).ToString( );
								//	ssp.searchKey = "Product_Number";
								//	ssp.tableName = "Inven_SQL.dbo.Products";
								//	dbm.update_product(ssp, toDB[0]);
								//}
								break;
							case "-pt":
								if(int.TryParse(_args[2], out int Upart))
								{
									ssp.searchVal = (Upart += 1000).ToString( );
									ssp.searchKey = "part_number";
									ssp.tableName = "Inven_SQL.dbo.Parts";
									dbm.update_part(ssp, toDB[0]);
								}
								break;
							case "-v":
								if(int.TryParse(_args[2], out int Uvend))
								{
									ssp.searchVal = (Uvend).ToString( ); ssp.searchKey = "Id"; ssp.tableName = "Inven_SQL.dbo.vendor";
									dbm.update_vendor(ssp, toDB[0]);
								}
								break;
						}
					}
					break;
				case "-d":
					switch(_args[1])
					{
						case "-pr":
							if(int.TryParse(_args[2], out int Dprod))
							{
								ssp.searchVal = (Dprod += 90000).ToString( );
								ssp.tableName = "Inven_SQL.dbo.Products";
								ssp.searchKey = "product_number";
								ssp.SQLcmd = "DELETE FROM " + ssp.tableName + " WHERE " + ssp.searchKey + " = " + ssp.searchVal;
								dbm.delete_row(ssp);
							}
							break;
						case "-pt":
							if(int.TryParse(_args[2], out int Dpart))
							{
								ssp.searchVal = (Dpart += 1000).ToString( );
								ssp.tableName = "Inven_SQL.dbo.Parts";
								ssp.searchKey = "part_number";
								ssp.SQLcmd = "DELETE FROM " + ssp.tableName + " WHERE " + ssp.searchKey + " = " + ssp.searchVal;
								dbm.delete_row(ssp);
							}
							break;
						case "-v":
							if(int.TryParse(_args[2], out int Dvend))
							{
								ssp.searchVal = (Dvend).ToString( );
								ssp.tableName = "Inven_SQL.dbo.vendor";
								ssp.searchKey = "Id";
								ssp.SQLcmd = "DELETE FROM " + ssp.tableName + " WHERE " + ssp.searchKey + " = " + ssp.searchVal;
								dbm.delete_row(ssp);
							}
							break;
					}
					break;
			}

		}
		public void importManager()
		{
			Parse parse = new Parse( );
			//List<string> raw = r.readFromFile(@"C:\Users\Dan\Documents\Visual Studio 2017\Projects\RAFtest\RAF_to_SQL\data\PRODDATA.txt");
			//List<productField> products = parse.productParser(raw);
			//dbm.importProducts(products);
			//dbm.importProductParts(products);

			//List<string> raw = r.readFromFile(@"C:\Users\Dan\Documents\Visual Studio 2017\Projects\RAFtest\RAF_to_SQL\data\PARTDATA.txt");
			//List<partFieldImport> parts = parse.part_parser(raw);
			//dbm.importParts(parts);

			//List<string> raw = r.readFromFile(@"C:\Users\Dan\Documents\Visual Studio 2017\Projects\RAFtest\RAF_to_SQL\data\STRLDATA.TXT");
			//parse.stringAlongParser(raw);

			//multitool rawData = r.rafRead("pt");
			//dbm.load_Into_SQL(rawData, ssp);

			//ssp.sqlCMD = "SELECT TOP(1)";
			//dbm.update_vendor(ssp, sample.get_sample_vendor());

			//parse.parser(rawData);
		}
	}
}
