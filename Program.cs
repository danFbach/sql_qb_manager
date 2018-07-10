using static RAFtest.datasets;
using System.Collections.Generic;

namespace RAFtest
{
	class Program
	{
		static void Main(string[] args)
		{
			db_manager dbm = new db_manager( );
			Parse parse = new Parse( );
			RAF_Read r = new RAF_Read( );
			sample_data sample = new sample_data( );
			fileSpecs specs = new fileSpecs( );
			switch(args[0])
			{
				case "q":
					if(args[0] == "-pr")
					{
						dbm.query_product( );
					}
					else if(args[0] == "-pt")
					{
						dbm.query_part( );
					}
					else if(args[0] == "-v")
					{
						dbm.query_vendor( );
					}
					break;
				case "i":
					if(args[0] == "-pr")
					{
						dbm.insert_product( );
					}
					else if(args[0] == "-pt")
					{
						dbm.insert_part( );
					}
					else if(args[0] == "-v")
					{
						dbm.insert_vendor();
					}
					break;
				case "u":
					if(args[0] == "-pr")
					{
						dbm.update_products( );
					}
					else if(args[0] == "-pt")
					{
						dbm.update_part( );
					}
					else if(args[0] == "-v")
					{
						dbm.update_vendor( );
					}
					break;
				case "d":
					if(args[0] == "-pr")
					{
						dbm.delete_product( );
					}
					else if(args[0] == "-pt")
					{
						dbm.delete_part( );
					}
					else if(args[0] == "-v")
					{
						dbm.delete_vendor( );
					}
					break;
			}

			List<string> raw = r.readFromFile(@"C:\Users\Dan\Documents\Visual Studio 2017\Projects\RAFtest\RAF_to_SQL\data\PRODDATA.txt");
			List<productFields> products = parse.productParser(raw);
			dbm.update_products(products);

			//dbm.importProducts(products);
			//dbm.importProductParts(products);

			//List<string> raw = r.readFromFile(@"C:\Users\Dan\Documents\Visual Studio 2017\Projects\RAFtest\RAF_to_SQL\data\PARTDATA.txt");
			//List<partField> parts = parse.part_parser(raw);
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
