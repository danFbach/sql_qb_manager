using System;
using System.Collections.Generic;
using System.IO;

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
	}
}
