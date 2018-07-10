namespace RAFtest
{
	class dbConfig
	{
		public string inven_general_conn = @"Server=DANDELL\MSSQLSERVER01;Database=Inven_SQL;UID=PublicSQLLogin;PWD=LPI-1958;";
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
