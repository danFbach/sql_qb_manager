using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace RAF_to_SQL
{
	public class Write
	{
		public void lineWrite(string data, string writeLocation)
		{
			try
			{
				using(StreamWriter sw = new StreamWriter(writeLocation, false))
				{
					sw.WriteLine(data);
				}
			}
			catch(Exception e)
			{
				using(StreamWriter sw = new StreamWriter(@"C:\INVEN\csharpError.txt", false))
				{
					sw.WriteLine(e.Message + Environment.NewLine + e.InnerException + Environment.NewLine + e.StackTrace);
				}
			}
		}
		public void listWrite(List<string> data, string writeLocation)
		{
			try
			{
				using(StreamWriter sw = new StreamWriter(writeLocation, false))
				{
					foreach(string line in data)
					{
						sw.WriteLine(line);
					}
				}
			}
			catch(Exception e)
			{
				using(StreamWriter sw = new StreamWriter(@"C:\INVEN\csharpError.txt", false))
				{
					sw.WriteLine(e.Message + Environment.NewLine + e.InnerException + Environment.NewLine + e.StackTrace);
				}
			}

		}
	}
}
