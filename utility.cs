using System.IO;
using System.Collections.Generic;
using System.Data.SqlClient;
using static RAF_to_SQL.datasets;

namespace RAF_to_SQL
{
	class utility
	{
		public void stitchAddress()
		{
			List<string> addresses = new List<string>( );
			using(StreamReader sr = new StreamReader(@"C:\Users\Dan\Downloads\xcart_orders(1).csv"))
			{
				string address;
				string halfAddress = "";
				bool half = false;
				while((address = sr.ReadLine( )) != null)
				{
					int count = 0;
					if(!half)
					{
						foreach(char letter in address.ToCharArray( ))
						{

							if(letter.Equals(';')) { count++; }

						}
						if(count == 7) { addresses.Add(address); continue; }
						else
						{
							halfAddress = address;
							half = true;
							continue;
						}
					}
					else
					{
						halfAddress += " " + address;
						addresses.Add(halfAddress);
						halfAddress = "";
						half = false;
						continue;
					}
				}
			}
			using(StreamWriter sw = new StreamWriter(@"C:\Users\Dan\Downloads\xcart_orders_fixed.csv"))
			{
				foreach(string address in addresses)
				{
					sw.WriteLine(address);
				}
			}
		}
		public sqlParameters getSSP(string searchKey, string searchVal, string SQLcmd, string tableName, SqlConnection db_connector)
		{
			dbConfig db = new dbConfig( );
			sqlParameters ssp = new sqlParameters( );
			ssp.db_connector = new SqlConnection(db.inven_SQL_admin);
			ssp.tableName = tableName;
			ssp.SQLcmd = SQLcmd;
			ssp.searchKey = searchKey;
			ssp.searchVal = searchVal;
			return ssp;
		}
	}
}
