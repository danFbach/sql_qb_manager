using System;
using System.IO;
using System.Data.SqlClient;
using static RAF_to_SQL.datasets;
using System.Collections.Generic;

namespace RAF_to_SQL
{
	class Program
	{
		static void Main(string[] args)
		{
			string sendbackpath = @"C:\Users\Dan\Documents\Visual Studio 2017\Projects\RAFtest\RAF_to_SQL\data\fromDB.txt";
			string abc = "abcdefghijklmnopqrstuvwxyz";
			db_manager dbm = new db_manager( );
			Parse parse = new Parse( );
			RAF_Read r = new RAF_Read( );
			sample_data sample = new sample_data( );
			fileSpec specs = new fileSpec( );
			Write w = new Write( );
			dbConfig db = new dbConfig( );
			sqlParameters ssp = new sqlParameters( );
			ssp.db_connector = new SqlConnection( );
			ssp.db_connector.ConnectionString = db.inven_general_conn;
			List<string> response = new List<string>( );
			//string[] _args = { "" };
			string[] _args = { "-q", "-pr", "255" };
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
										w.lineWrite(send, sendbackpath);
									}
								}
							}
							break;
						case "-pt":
							if(int.TryParse(_args[2], out int partQ))
							{
								if(partQ > 0 && partQ < 3500)
								{
									partQ += 1000;
									ssp.sqlCMD = "SELECT * FROM Parts WHERE part_number = " + partQ.ToString( );
									ssp.tableName = "Parts";
									send = dbm.query_part(ssp);
									if(send != null)
									{
										w.lineWrite(send, sendbackpath);
									}
								}
							}
							break;
						case "-v":
							ssp.tableName = "vendor";
							if(abc.Contains(_args[2]))
							{
								//for search by first letter of vcode
								ssp.searchKey = "v_code";
								ssp.searchVal = _args[2] + "%";
								ssp.sqlCMD = "SELECT * FROM " + ssp.tableName + " WHERE " + ssp.searchKey + " LIKE '" + ssp.searchVal + "'";
								response = dbm.query_vendor(ssp);
							}
							else if(int.TryParse(_args[2], out int idQuery))
							{
								ssp.searchKey = "Id";
								ssp.searchVal = idQuery.ToString( ).Trim( );
								ssp.sqlCMD = "SELECT * FROM " + ssp.tableName + " WHERE " + ssp.searchKey + " = " + ssp.searchVal;
								response = dbm.query_vendor(ssp);
							}
							else if(_args[2].Length > 1)
							{
								//searching by vendor code
								ssp.searchKey = "v_code";
								ssp.searchVal = _args[2];
								ssp.sqlCMD = "SELECT * FROM " + ssp.tableName + " WHERE " + ssp.searchKey + " = '" + ssp.searchVal + "'";
								response = dbm.query_vendor(ssp);
							}
							if(response.Count > 0)
							{
								w.listWrite(response, sendbackpath);
							}
							break;
					}
					break;
				case "-i":
					if(args[1] == "-pr")
					{
						dbm.insert_product( );
					}
					else if(args[1] == "-pt")
					{
						dbm.insert_part( );
					}
					else if(args[1] == "-v")
					{
						//dbm.insert_vendor();
					}
					break;
				case "-u":
					if(args[1] == "-pr")
					{
						//dbm.update_products( );
					}
					else if(args[1] == "-pt")
					{
						dbm.update_part( );
					}
					else if(args[1] == "-v")
					{
						//dbm.update_vendor( );
					}
					break;
				case "-d":
					if(args[1] == "-pr")
					{
						//dbm.delete_product( );
					}
					else if(args[1] == "-pt")
					{
						dbm.delete_part( );
					}
					else if(args[1] == "-v")
					{
						//dbm.delete_vendor( );
					}
					break;
			}

			//List<string> raw = r.readFromFile(@"C:\Users\Dan\Documents\Visual Studio 2017\Projects\RAFtest\RAF_to_SQL\data\PRODDATA.txt");
			//List<productField> products = parse.productParser(raw);
			//dbm.update_products(products);

			//dbm.importProducts(products);
			//dbm.importProductParts(products);

			//List<string> raw = r.readFromFile(@"C:\Users\Dan\Documents\Visual Studio 2017\Projects\RAFtest\RAF_to_SQL\data\PARTDATA.txt");
			//List<partFieldImport> parts = parse.part_parser(raw);
			//dbm.importParts(parts);

			//List<string> raw = r.readFromFile(@"C:\Users\Dan\Documents\Visual Studio 2017\Projects\RAFtest\RAF_to_SQL\data\STRLDATA.TXT");
			//parse.stringAlongParser(raw);

			//multitool rawData = r.rafRead("pt");
			//sqlSearchParameters ssp = new sqlSearchParameters();
			//ssp.searchKey = "id";
			//ssp.tableName = "vendor";
			//ssp.sqlCMD = "SELECT";

			//dbm.load_Into_SQL(rawData, ssp);

			//ssp.sqlCMD = "SELECT TOP(1)";
			//dbm.update_vendor(ssp, sample.get_sample_vendor());

			//parse.parser(rawData);

		}
	}
}
