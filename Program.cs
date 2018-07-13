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
			string[] _args = { "-u", "-v", "253"};
			//string[] _args = { "" };
			ms.main(_args);
		}
	}
}
