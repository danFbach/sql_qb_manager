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
			mainSwitch ms = new mainSwitch( );
			fileSpec spec = new fileSpec( );
			Write w = new Write( );
			try
			{
				if(args.Length > 0)
				{
					ms.main(args);
				}
				else
				{
					string[] _args = { "-a", "-q", "-pr", "255" };
					//string[] _args = { "-a", "-i", "-pt" };
					//string[] _args = { "-a", "-u", "-v", "25" };
					//string[] _args = { "-a", "-d", "-pr", "20" };
					ms.main(_args);
				}
			}
			catch(Exception e)
			{
				w.lineWrite(DateTime.Now.ToString( ) + Environment.NewLine + "Message: " + e.Message + Environment.NewLine + "Inner Ex: " + e.InnerException + Environment.NewLine + "Stack: " + e.StackTrace, spec.errorpath);
			}
			//ms.importManager( );
		}
	}
}
