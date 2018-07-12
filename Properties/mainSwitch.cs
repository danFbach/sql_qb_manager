using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RAF_to_SQL.datasets;
using System.Data.SqlClient;

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
									send = dbm.query_product(prodQ.ToString( ));
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
									ssp.sqlCMD = "SELECT * FROM Parts WHERE part_number = " + partQ.ToString( );
									ssp.tableName = "Parts";
									send = dbm.query_part(ssp);
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
								ssp.sqlCMD = "SELECT * FROM " + ssp.tableName + " WHERE " + ssp.searchKey + " LIKE '" + ssp.searchVal + "'";
								spec.response = dbm.query_vendor(ssp);
							}
							else if(int.TryParse(_args[2], out int idQuery))
							{
								ssp.searchKey = "Id";
								ssp.searchVal = idQuery.ToString( ).Trim( );
								ssp.sqlCMD = "SELECT * FROM " + ssp.tableName + " WHERE " + ssp.searchKey + " = " + ssp.searchVal;
								spec.response = dbm.query_vendor(ssp);
							}
							else if(_args[2].Length > 1)
							{
								//searching by vendor code
								ssp.searchKey = "v_code";
								ssp.searchVal = _args[2];
								ssp.sqlCMD = "SELECT * FROM " + ssp.tableName + " WHERE " + ssp.searchKey + " = '" + ssp.searchVal + "'";
								spec.response = dbm.query_vendor(ssp);
							}
							if(spec.response.Count > 0) { w.listWrite(spec.response, spec.sendbackpath); }
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
								ssp.searchKey = "Product_Number";
								ssp.tableName = "Inven_SQL.dbo.Products";
								ssp.sqlCMD = "SELECT TOP(1) * FROM " + ssp.tableName;
								dbm.insert_part(ssp, toDB[0]);
								break;
							case "-pt":
								ssp.searchKey = "part_number";
								ssp.tableName = "Inven_SQL.dbo.Parts";
								ssp.sqlCMD = "SELECT TOP(1) * FROM " + ssp.tableName;
								dbm.insert_part(ssp, toDB[0]);
								break;
							case "-v":
								ssp.searchKey = "1";
								ssp.tableName = "Inven_SQL.dbo.vendor";
								ssp.sqlCMD = "SELECT TOP(1) * FROM " + ssp.tableName;
								dbm.insert_part(ssp, toDB[0]);
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
								ssp.sqlCMD = "DELETE FROM " + ssp.tableName + " WHERE " + ssp.searchKey + " = " + ssp.searchVal;
								dbm.delete_row(ssp);
							}
							break;
						case "-pt":
							if(int.TryParse(_args[2], out int Dpart))
							{
								ssp.searchVal = (Dpart += 1000).ToString( );
								ssp.tableName = "Inven_SQL.dbo.Parts";
								ssp.searchKey = "part_number";
								ssp.sqlCMD = "DELETE FROM " + ssp.tableName + " WHERE " + ssp.searchKey + " = " + ssp.searchVal;
								dbm.delete_row(ssp);
							}
							break;
						case "-v":
							if(int.TryParse(_args[2], out int Dvend))
							{
								ssp.searchVal = (Dvend).ToString( );
								ssp.tableName = "Inven_SQL.dbo.vendor";
								ssp.searchKey = "Id";
								ssp.sqlCMD = "DELETE FROM " + ssp.tableName + " WHERE " + ssp.searchKey + " = " + ssp.searchVal;
								dbm.delete_row(ssp);
							}
							break;
					}
					break;
			}

		}
	}
}
