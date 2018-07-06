using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RAFtest
{
	class datasets
	{
		public class sqlCMD
		{
			public string searchKey { get; set; }
			public string searchVal { get; set; }
			public string tableName { get; set; }
		}
		public class sqlSearchParameters
		{
			public string searchKey { get; set; }
			public string searchVal { get; set; }
			public string sqlCMD { get; set; }
			public string tableName { get; set; }
		}
		public class multitool
		{
			public List<universalDataPack> data { get; set; }
		}
		public class universalDataPack
		{
			public partField partFields { get; set; }
			public productFields productFields { get; set; }
			public vendorFields vendorFields { get; set; }
		}
		public class stringAlongCollection
		{
			public int masterID { get; set; }
			public List<stringAlongField> stringAlongPack { get; set; }
		}
		public class stringAlongField
		{
			public int ID { get; set; }
			public string name { get; set; }
			public List<partPack> reqd_part { get; set; }
		}
		public class partPack
		{
			public int part_ID { get; set; }
			public int qty_reqd { get; set; }
		}
		public class productParts
		{
			public string productNumber { get; set; }
			public string partNumber { get; set; }
			public int quantity_reqd { get; set; }
		}
		public class partField
		{
			
			public string part_number { get; set; }
			public string description { get; set; }
			public int specification { get; set; }
			public int special_instruction { get; set; }
			public int years_use { get; set; }
			public int lead_time_in_weeks { get; set; }
			public string listed_vendor_id { get; set; }
			public int best_quantity_to_order { get; set; }
			public int finished_weight { get; set; }
			public decimal price { get; set; }
			public int quantity_to_order { get; set; }
			public int listed_PO_num { get; set; }
			public int delivery_date_1 { get; set; }
			public int delivery_date_2 { get; set; }
			public int delivery_date_3 { get; set; }
			public int delivery_date_4 { get; set; }
			public decimal added_cost { get; set; }
			public int cycle_time_secs_second_machine { get; set; }
			public decimal added_cost_machine_2 { get; set; }
			public int quantity_on_hand { get; set; }
			public int raw_material_number { get; set; }
			public decimal material_weight { get; set; }
			public int ytd_sales { get; set; }
			public decimal latest_quote { get; set; }
			public int quantity_assembled { get; set; }
			public int cycle_time { get; set; }
			public int machine_num { get; set; }
			public decimal machine_rate { get; set; }
			public int last_years_use { get; set; }
			public int weeks_cushion { get; set; }
			public int allocated { get; set; }
			public int setup_time { get; set; }
			public int raw_material_2 { get; set; }
			public decimal list_price { get; set; }
			public string memo { get; set; }
			public int picture_path { get; set; }
			public int drawing_path { get; set; }
		}
		public class productFields
		{
			public string Product_Number { get; set; }
			public string Description { get; set; }
			public decimal Price { get; set; }
			public decimal Weight { get; set; }
			public decimal Master_Units { get; set; }	
			public decimal Cubic_Feet { get; set; }
			public int quantity_on_hand { get; set; }
			public int annual_use { get; set; }
			public int sales_last_period { get; set; }
			public int ytd_sales { get; set; }
			public int gross { get; set; }
			public decimal assembly_time_secs { get; set; }
			public int product_code { get; set; }
			public Dictionary<string, int> parts_reqd { get; set; }
			public int string_along { get; set; }
		}
		public class vendorFields
		{
			public int id { get; set; }
			public string v_code { get; set; }
			public string business_name { get; set; }
			public string address_1 { get; set; }
			public string address_2 { get; set; }
			public string city_state_zip { get; set; }
			public string fax_number { get; set; }
			public string terms { get; set; }
			public string order_contact { get; set; }
			public string order_email { get; set; }
			public string order_email_cc { get; set; }
			public string order_phone { get; set; }
			public string account_contact { get; set; }
			public string account_email { get; set; }
			public string account_phone { get; set; }
			public string quality_contact { get; set; }
			public string quality_email { get; set; }
			public string quality_phone { get; set; }
			public string shipping_contact { get; set; }
			public string shipping_email { get; set; }
			public string shipping_phone { get; set; }
		}
		public class sample_data
		{
			public vendorFields get_sample_vendor()
			{
				vendorFields vendor_tester = new vendorFields();
				vendor_tester.id = 0;
				vendor_tester.v_code = "QUCAST";
				vendor_tester.business_name = "QUALITY CASTINGS";
				vendor_tester.address_1 = "1908 MACARTHUR RD";
				vendor_tester.address_2 = "";
				vendor_tester.city_state_zip = "WAUKESHA WI 53188";
				vendor_tester.fax_number = "414-475-6539";
				vendor_tester.terms = "";
				vendor_tester.order_contact = "RANDY HARRINGTON";
				vendor_tester.order_email = "willf@fsasales.com";
				vendor_tester.order_email_cc = "262-442-7565";
				vendor_tester.order_phone = "";
				vendor_tester.account_contact = "";
				vendor_tester.account_email = "";
				vendor_tester.account_phone = "";
				vendor_tester.quality_contact = "test quality contact";
				vendor_tester.quality_email = "";
				vendor_tester.quality_phone = "";
				vendor_tester.shipping_contact = "";
				vendor_tester.shipping_email = "";
				vendor_tester.shipping_phone = "test shipping phone";
				return vendor_tester;
			}
		}

		public class rawDataAndType
		{
			public Type type { get; set; }
			public string raw { get; set; }
		}

		public class fileType
		{
			public int len { get; set; }
			public List<int> fields { get; set; }
			public string path { get; set; }
		}
		public class fileSpecs
		{
			public int vendorLen = 574;
			public List<int> vendor = new List<int> { 6, 35, 24, 24, 30, 20, 4, 30, 45, 45, 20, 30, 45, 20, 30, 45, 20, 30, 45, 20 };
			public string vendata = @"C:\Users\Dan\Documents\Visual Studio 2017\Projects\RAFtest\RAF_to_SQL\data\NVEND.DAT";

			public int partLen = 137; //roughly...
			public List<int> part = new List<int> { 6, 20, 24, 2, 4, 2, 6, 4, 2, 4, 4, 2, 2, 2, 2, 2, 2, 4, 2, 2, 2, 4, 4, 2, 2, 2, 4, 2, 4, 2, 2, 2, 1, 2, 2, 0 };
			public string partdata = @"C:\Users\Dan\Documents\Visual Studio 2017\Projects\RAFtest\RAF_to_SQL\data\INVEN.DAT";

			public int prodLen = 82; //roughly..
			public List<int> product = new List<int> { 6, 24, 6, 5, 4, 4, 4, 4, 4, 4, 4, 4, 2, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2 };
			public string prodata = @"C:\Users\Dan\Documents\Visual Studio 2017\Projects\RAFtest\RAF_to_SQL\data\inven.dat";
		}
	}
}