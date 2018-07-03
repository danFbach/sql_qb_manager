
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static RAFtest.datasets;

namespace RAFtest
{
	class Parse
	{
		dataSwitches _switch = new dataSwitches();
		public List<productFields> prod_parser(List<string> raw)
		{
			List<productFields> products = new List<productFields>();
			productFields product = new productFields();
			
			foreach (string line in raw)
			{
				product = new productFields();
				product.parts_reqd = new Dictionary<string, int>();
				string[] cracked = line.Split(',');
				for(int i = 0;i < cracked.Count() - 1; i++)
				{
					product = _switch.prodSwitch(i, cracked[i], product);
				}
				products.Add(product);
			}
			return products;
		}

		public List<partFields> part_parser(List<string> raw)
		{
			List<partFields> parts = new List<partFields>();
			partFields part = new partFields();
			foreach(string s in raw)
			{
				part = new partFields();
				string[] cracked = s.Split(',');
				for(int i = 0;i < (cracked.Count() - 1); i++)
				{
					part = _switch.partSwitch(i, cracked[i], part);
				}
				parts.Add(part);
			}
			return parts;
		}
	}
}
