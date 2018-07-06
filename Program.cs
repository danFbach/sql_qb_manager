using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static RAFtest.datasets;

namespace RAFtest
{
	class Program
	{
		static void Main(string[] args)
		{
			db_manager dbm = new db_manager();
			Parse parse = new Parse();
			RAF_Read r = new RAF_Read();
			sample_data sample = new sample_data();
			fileSpecs specs = new fileSpecs();

			List<string> raw = r.readFromFile(@"C:\Users\Dan\Documents\Visual Studio 2017\Projects\RAFtest\RAF_to_SQL\data\PRODDATA.txt");
			List<productFields> products = parse.productParser(raw);
			foreach(productFields product in products)
			{
				dbm.update_product(product);
			}
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
