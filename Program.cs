﻿using System;
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

			mainSwitch ms = new mainSwitch( );
			string[] _args = { "-u", "-pt", "1"};
			//string[] _args = { "" };
			ms.main(_args);


			Parse parse = new Parse( );



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
