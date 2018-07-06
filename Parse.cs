
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

		public List<stringAlongCollection> stringAlongParser(List<string> raw)
		{
			List<stringAlongCollection> collection = new List<stringAlongCollection>();
			stringAlongCollection stringAlongMaster = new stringAlongCollection();
			stringAlongMaster.stringAlongPack = new List<stringAlongField>();
			stringAlongField stringAlong = new stringAlongField();
			partPack part = new partPack();

			foreach(string sa in raw)
			{
				stringAlongMaster = new stringAlongCollection();
				stringAlongMaster.stringAlongPack = new List<stringAlongField>();
				string[] stringAlongPack = sa.Split('|');
				foreach(string saa in stringAlongPack)
				{
					stringAlong = new stringAlongField();
					string[] newSA = saa.Split(',');
					string[] idAndDescription = newSA.First().Split('ƒ');
					stringAlong.ID = int.Parse(idAndDescription.First());
					stringAlong.name = idAndDescription.Last();
					if (newSA.Count() > 1)
					{
						stringAlong.reqd_part = new List<partPack>();
						for (int i = 1; i < newSA.Count(); i++)
						{
							part = new partPack();
							string[] a_part = newSA[i].Trim().Split(' ');
							part.part_ID = int.Parse(a_part.First());
							part.qty_reqd = int.Parse(a_part.Last());
							stringAlong.reqd_part.Add(part);
						}
					}
					stringAlongMaster.stringAlongPack.Add(stringAlong);
				}
				stringAlongMaster.masterID = stringAlongMaster.stringAlongPack[0].ID;
				collection.Add(stringAlongMaster);
			}
			return collection;
		}

		public List<productFields> productParser(List<string> raw)
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

		public List<partField> part_parser(List<string> raw)
		{
			List<partField> parts = new List<partField>();
			partField part = new partField();
			foreach(string s in raw)
			{
				part = new partField();
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
