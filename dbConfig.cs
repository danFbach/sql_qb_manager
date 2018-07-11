namespace RAF_to_SQL
{
	class dbConfig
	{
		public string inven_general_conn = @"Data Source=DANDELL\MSSQLSERVER01;Initial Catalog=Inven_SQL;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
		public string vendorTable = "vendor";
		public int vendorColCount = 21;
		public string partVendorRelatTable = "part_vendor_relational";
		public int partVendorColCount = 3;
		public string productTable = "Products";
		public int productColCount = 14;
		public string prodReqdPartTable = "reqd_part";
		public int prodReqdPartColCount = 4;
		public string partTable = "Parts";
		public int partColCount = 38;
		public string stringAlongPartTable = "string_along_part";
		public int stringAlongPartColCount = 4;
		public string stringAlongTable = "string_along";
		public int stringAlongColCount = 3;
	}
}
