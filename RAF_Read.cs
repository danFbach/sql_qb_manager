using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using static RAF_to_SQL.datasets;

namespace RAF_to_SQL
{
	class RAF_Read
	{
		dataSwitches _switch = new dataSwitches();
		SqlConnection sql = new SqlConnection();
		dbConfig _db_config = new dbConfig();
		List<int> strings = new List<int> { 0, 1, 2, 3, 6, 11, 20, 32, 34, 35, 36 };

		public multitool rafRead(string dt)
		{
			fileType fileType = new fileType();
			fileType = datatypeSelect(dt);

			universalDataPack udp = new universalDataPack();
			udp.vendorFields = new vendorFields();
			udp.productFields = new productField();
			udp.partFields = new partField();
			multitool multitool = new multitool();
			multitool.data = new List<universalDataPack>();
			List<string> data = new List<string>();
			using (FileStream _fs = File.Open(fileType.path, FileMode.Open))
			{
				byte[] b = new byte[fileType.len];
				int position = 0;
				UTF8Encoding t = new UTF8Encoding(true);
				while (_fs.Read(b, 0, b.Length) > 0)
				{
					//reset container
					udp.vendorFields = new vendorFields(); udp.productFields = new productField(); udp.partFields = new partField();
					position = 0;
					string xRaw = b[0].ToString();
					string x = t.GetString(b.ToArray().Take(b.Length).ToArray());
					udp.vendorFields.id = int.Parse(xRaw);
					for (int i = 0; i < fileType.fields.Count; i++)
					{
						//vendorFields parser
						if (dt.Equals("v")) { if (i == (fileType.fields.Count - 1)) { udp.vendorFields = _switch.vendorSwitch(i + 1, x.Substring(position, (x.Length - position)).Trim(), udp.vendorFields); } else { udp.vendorFields = _switch.vendorSwitch(i + 1, x.TrimStart().Substring(position, fileType.fields[i]).Trim(), udp.vendorFields); } }
						else if (dt.Equals("pr"))
						{
							if (i == (fileType.fields.Count - 1)) { udp.productFields = _switch.prodSwitch(i + 1, x.Substring(position, (x.Length - position)).Trim(), udp.productFields); } else { udp.productFields = _switch.prodSwitch(i + 1, x.TrimStart().Substring(position, fileType.fields[i]).Trim(), udp.productFields); }
						}
						else if (dt.Equals("pt"))
						{
							if (strings.Contains(i))
							{
								if (i == (fileType.fields.Count - 1)) { udp.partFields = _switch.partSwitch(i + 1, x.Substring(position, (x.Length - position)).Trim(), udp.partFields); } else { udp.partFields = _switch.partSwitch(i + 1, x.TrimStart().Substring(position, fileType.fields[i]).Trim(), udp.partFields); }
							}
							else
							{
								string temp = t.GetString(b.Skip(position).ToArray().Take(fileType.fields[i]).ToArray());
								List<byte> lets_make_a_number = new List<byte> { };
								//for (int ii = (position - 1);ii < ((fileType.fields[i] + position) - 1);ii++)
								//{
								//	lets_make_a_number.Add(b[ii]);
								//}
								int a_number = 0;
								foreach (char s in temp)
								{
									a_number = Convert.ToInt32(s);
								}
								//int new_number = BitConverter.ToInt32(lets_make_a_number.ToArray(), 0);
								udp.partFields = _switch.partSwitch(i + 1, a_number.ToString(), udp.partFields);
							}
						}
						position += fileType.fields[i];
					}

					multitool.data.Add(udp);
				}
			}
			return multitool;
		}



		public List<string> readFromFile(string location)
		{
			List<string> rawdata = new List<string>();
			if (File.Exists(location))
			{
				using (StreamReader sr = new StreamReader(location))
				{
					string line;
					while((line = sr.ReadLine()) != null)
					{
						if (!string.IsNullOrEmpty(line))
						{
							rawdata.Add(line);
						}
					}
				}
			}
			return rawdata;
		}
		public fileType datatypeSelect(string _datatype)
		{
			fileType ft = new fileType();
			fileSpec fs = new fileSpec();
			switch (_datatype)
			{
				case "v":
					ft.fields = fs.vendor;
					ft.len = fs.vendorLen;
					ft.path = fs.vendata;
					return ft;
				case "pr":
					ft.fields = fs.product;
					ft.len = fs.prodLen;
					ft.path = fs.prodata;
					return ft;
				case "pt":
					ft.fields = fs.part;
					ft.len = fs.partLen;
					ft.path = fs.partdata;
					return ft;
				default:
					return ft;
			}
		}
	}
}
