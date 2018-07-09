using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using static RAFtest.datasets;

namespace RAFtest
{
	class dataSwitches
	{
		dbConfig dbconfig = new dbConfig();
		public string vendorSqlSwitch(string sqlCol, vendorFields v)
		{
			switch (sqlCol)
			{
				case "id":
					return v.id.ToString();
				case "v_code":
					return v.v_code;
				case "business_name":
					return v.business_name;
				case "address_1":
					return v.address_1;
				case "address_2":
					return v.address_2;
				case "city_state_zip":
					return v.city_state_zip;
				case "fax_number":
					return v.fax_number;
				case "terms":
					return v.terms;
				case "order_contact":
					return v.order_contact;
				case "order_email":
					return v.order_email;
				case "order_email_cc":
					return v.order_email_cc;
				case "order_phone":
					return v.order_phone;
				case "account_contact":
					return v.account_contact;
				case "account_email":
					return v.account_email;
				case "account_phone":
					return v.account_phone;
				case "quality_contact":
					return v.quality_contact;
				case "quality_email":
					return v.quality_email;
				case "quality_phone":
					return v.quality_phone;
				case "shipping_contact":
					return v.shipping_contact;
				case "shipping_email":
					return v.shipping_email;
				case "shipping_phone":
					return v.shipping_phone;
				default:
					return "";
			}
		}
		public vendorFields vendorSwitch(int i, string rawData, vendorFields v)
		{
			if (rawData.Trim() == "") { return v; }
			switch (i)
			{
				case 1:
					v.v_code = rawData;
					return v;
				case 2:
					v.business_name = rawData;
					return v;
				case 3:
					v.address_1 = rawData;
					return v;
				case 4:
					v.address_2 = rawData;
					return v;
				case 5:
					v.city_state_zip = rawData;
					return v;
				case 6:
					v.fax_number = rawData;
					return v;
				case 7:
					v.terms = rawData;
					return v;
				case 8:
					v.order_contact = rawData;
					return v;
				case 9:
					v.order_email = rawData;
					return v;
				case 10:
					v.order_email_cc = rawData;
					return v;
				case 11:
					v.order_phone = rawData;
					return v;
				case 12:
					v.account_contact = rawData;
					return v;
				case 13:
					v.account_email = rawData;
					return v;
				case 14:
					v.account_phone = rawData;
					return v;
				case 15:
					v.quality_contact = rawData;
					return v;
				case 16:
					v.quality_email = rawData;
					return v;
				case 17:
					v.quality_phone = rawData;
					return v;
				case 18:
					v.shipping_contact = rawData;
					return v;
				case 19:
					v.shipping_email = rawData;
					return v;
				case 20:
					v.shipping_phone = rawData;
					return v;
				default:
					return v;
			}
		}
		public SqlCommand productSqlSwitch(SqlCommand command, productFields product, int id)
		{
			foreach (SqlParameter param in command.Parameters)
			{
				switch (param.SourceColumn)
				{
					case "Id":
						param.Value = id;
						continue;
					case "Product_Number":
						param.Value = product.Product_Number;
						continue;
					case "Description":
						param.Value = product.Description;
						continue;
					case "Price":
						param.Value = product.Price;
						continue;
					case "Weight":
						param.Value = product.Weight;
						continue;
					case "Master_Units":
						param.Value = product.Master_Units;
						continue;
					case "Cubic_Feet":
						param.Value = product.Cubic_Feet;
						continue;
					case "quantity_on_hand":
						param.Value = product.quantity_on_hand;
						continue;
					case "annual_use":
						param.Value = product.annual_use;
						continue;
					case "sales_last_period":
						param.Value = product.sales_last_period;
						continue;
					case "ytd_sales":
						param.Value = product.ytd_sales;
						continue;
					case "gross":
						param.Value = product.gross;
						continue;
					case "assembly_time_secs":
						param.Value = product.assembly_time_secs;
						continue;
					case "product_code":
						param.Value = product.product_code;
						continue;
				}
			}
			return command;
		}
		public List<SqlCommand> productCheckSwitch(List<List<rawDataAndType>> rawData, SqlCommand update)
		{
			List<SqlCommand> commandPack = new List<SqlCommand>();
			SqlCommand updateTemp;
			foreach (List<rawDataAndType> data in rawData)
			{
				updateTemp = update.Clone();
				for (int i = 0; i < dbconfig.productColCount; i++)
				{
					switch (i)
					{
						case 0:
							update.Parameters["Product_Number"].Value = data[i].value;
							break;
						case 1:
							update.Parameters["Description"].Value = data[i].value;
							break;
						case 2:
							bool rx0 = decimal.TryParse(data[i].value, out decimal dx0);
							if (rx0) { update.Parameters["Price"].Value = dx0; }
							break;
						case 3:
							bool rx1 = decimal.TryParse(data[i].value, out decimal dx1);
							if (rx1) { update.Parameters["Weight"].Value = dx1; }
							break;
						case 4:
							bool r0 = decimal.TryParse(data[i].value, out decimal i0);
							if (r0) { update.Parameters["Master_Units"].Value = i0; }
							break;
						case 5:
							bool r1 = decimal.TryParse(data[i].value, out decimal i1);
							if (r1) { update.Parameters["Cubic_Feet"].Value = i1; }
							break;
						case 6:
							bool r2 = int.TryParse(data[i].value, out int i2);
							if (r2) { update.Parameters["quantity_on_hand"].Value = i2; }
							break;
						case 7:
							bool r3 = int.TryParse(data[i].value, out int i3);
							if (r3) { update.Parameters["annual_use"].Value = i3; }
							break;
						case 8:
							bool r4 = int.TryParse(data[i].value, out int i4);
							if (r4) { update.Parameters["sales_last_period"].Value = i4; }
							break;
						case 9:
							bool r5 = int.TryParse(data[i].value, out int i5);
							if (r5) { update.Parameters["ytd_sales"].Value = i5; }
							break;
						case 10:
							bool r6 = int.TryParse(data[i].value, out int i6);
							if (r6) { update.Parameters["gross"].Value = i6; }
							break;
						case 11:
							bool r7 = decimal.TryParse(data[i].value, out decimal i7);
							if (r7) { update.Parameters["assembly_time_secs"].Value = i7; }
							break;
						case 12:
							bool r8 = int.TryParse(data[i].value, out int i8);
							if (r8) { update.Parameters["product_code"].Value = i8; }
							break;
						case 13:
							bool r9 = int.TryParse(data[i].value, out int i9);
							if (r9) { update.Parameters["string_along"].Value = i9; }
							break;
					}
				}
				commandPack.Add(updateTemp);
			}
			return commandPack;
		}
		public SqlCommand prodSwitch(List<rawDataAndType> dataFromSql, productFields newData, SqlCommand update)
		{
			
			for (int i = 1; i < dataFromSql.Count(); i++)
			{
				switch (i)
				{
					case 1:
						if (dataFromSql[i].value.Equals(newData.Product_Number)) { update.Parameters[i].Value = newData.Product_Number; update.Parameters[0].Value = dataFromSql[0].value; }
						else { return null; }
						continue;
					case 2:
						if (!dataFromSql[i].value.Equals(newData.Description)) { update.Parameters[i].Value = newData.Description; }
						else { update.Parameters[i].Value = dataFromSql[i].value; }
						continue;
					case 3:
						if (!dataFromSql[i].value.Equals(newData.Price)) { update.Parameters[i].Value = newData.Price; }
						else { update.Parameters[i].Value = dataFromSql[i].value; }
						continue;
					//bool rx0 = decimal.TryParse(dataFromSql[i].value, out decimal dx0);
					//if (rx0) { newData.Price = dx0; }
					//break;
					case 4:
						if (!dataFromSql[i].value.Equals(newData.Weight)) { update.Parameters[i].Value = newData.Weight; }
						else { update.Parameters[i].Value = dataFromSql[i].value; }
						continue;
					//bool rx1 = decimal.TryParse(dataFromSql[i].value, out decimal dx1);
					//if (rx1) { newData.Weight = dx1; }
					//break;
					case 5:
						if (!dataFromSql[i].value.Equals(newData.Master_Units)) { update.Parameters[i].Value = newData.Master_Units; }
						else { update.Parameters[i].Value = dataFromSql[i].value; }
						continue;
						//bool r0 = decimal.TryParse(dataFromSql[i].value, out decimal i0);
						//if (r0) { newData.Master_Units = i0; }
						//break;
					case 6:
						if (!dataFromSql[i].value.Equals(newData.Cubic_Feet)) { update.Parameters[i].Value = newData.Cubic_Feet; }
						else { update.Parameters[i].Value = dataFromSql[i].value; }
						continue;
						//bool r1 = decimal.TryParse(dataFromSql[i].value, out decimal i1);
						//if (r1) { newData.Cubic_Feet = i1; }
						//break;
					case 7:
						if (!dataFromSql[i].value.Equals(newData.quantity_on_hand)) { update.Parameters[i].Value = newData.quantity_on_hand; }
						else { update.Parameters[i].Value = dataFromSql[i].value; }
						continue;
						//bool r2 = int.TryParse(dataFromSql[i].value, out int i2);
						//if (r2) { newData.quantity_on_hand = i2; }
						//break;
					case 8:
						if (!dataFromSql[i].value.Equals(newData.annual_use)) { update.Parameters[i].Value = newData.annual_use; }
						else { update.Parameters[i].Value = dataFromSql[i].value; }
						continue;
						//bool r3 = int.TryParse(dataFromSql[i].value, out int i3);
						//if (r3) { newData.annual_use = i3; }
						//break;
					case 9:
						if (!dataFromSql[i].value.Equals(newData.sales_last_period)) { update.Parameters[i].Value = newData.sales_last_period; }
						else { update.Parameters[i].Value = dataFromSql[i].value; }
						continue;
					//bool r4 = int.TryParse(dataFromSql[i].value, out int i4);
					//if (r4) { newData.sales_last_period = i4; }
					//break;
					case 10:
						if (!dataFromSql[i].value.Equals(newData.ytd_sales)) { update.Parameters[i].Value = newData.ytd_sales; }
						else { update.Parameters[i].Value = dataFromSql[i].value; }
						continue;
						//bool r5 = int.TryParse(dataFromSql[i].value, out int i5);
						//if (r5) { newData.ytd_sales = i5; }
						//break;
					case 11:
						if (!dataFromSql[i].value.Equals(newData.gross)) { update.Parameters[i].Value = newData.gross; }
						else { update.Parameters[i].Value = dataFromSql[i].value; }
						continue;
					//bool r6 = int.TryParse(dataFromSql[i].value, out int i6);
					//if (r6) { newData.gross = i6; }
					//break;
					case 12:
						if (!dataFromSql[i].value.Equals(newData.assembly_time_secs)) { update.Parameters[i].Value = newData.assembly_time_secs; }
						else { update.Parameters[i].Value = dataFromSql[i].value; }
						continue;
						//bool r7 = decimal.TryParse(dataFromSql[i].value, out decimal i7);
						//if (r7) { newData.assembly_time_secs = i7; }
						//break;
					case 13:
						if (!dataFromSql[i].value.Equals(newData.product_code)) { update.Parameters[i].Value = newData.product_code; }
						else { update.Parameters[i].Value = dataFromSql[i].value; }
						continue;
						//bool r8 = int.TryParse(dataFromSql[i].value, out int i8);
						//if (r8) { newData.product_code = i8; }
						//break;
				}
			}
			return update;
		}
		public productFields prodSwitch(int i, string rawData, productFields p)
		{
			if (rawData.Trim() == "") { return p; }
			switch (i)
			{
				case 0:
					p.Product_Number = rawData;
					break;
				case 1:
					p.Description = rawData;
					break;
				case 2:
					bool rx0 = decimal.TryParse(rawData, out decimal dx0);
					if (rx0) { p.Price = dx0; }
					break;
				case 3:
					bool rx1 = decimal.TryParse(rawData, out decimal dx1);
					if (rx1) { p.Weight = dx1; }
					break;
				case 4:
					bool r0 = decimal.TryParse(rawData, out decimal i0);
					if (r0) { p.Master_Units = i0; }
					break;
				case 5:
					bool r1 = decimal.TryParse(rawData, out decimal i1);
					if (r1) { p.Cubic_Feet = i1; }
					break;
				case 6:
					bool r2 = int.TryParse(rawData, out int i2);
					if (r2) { p.quantity_on_hand = i2; }
					break;
				case 7:
					bool r3 = int.TryParse(rawData, out int i3);
					if (r3) { p.annual_use = i3; }
					break;
				case 8:
					bool r4 = int.TryParse(rawData, out int i4);
					if (r4) { p.sales_last_period = i4; }
					break;
				case 9:
					bool r5 = int.TryParse(rawData, out int i5);
					if (r5) { p.ytd_sales = i5; }
					break;
				case 10:
					bool r6 = int.TryParse(rawData, out int i6);
					if (r6) { p.gross = i6; }
					break;
				case 11:
					bool r7 = decimal.TryParse(rawData, out decimal i7);
					if (r7) { p.assembly_time_secs = i7; }
					break;
				case 12:
					bool r8 = int.TryParse(rawData, out int i8);
					if (r8) { p.product_code = i8; }
					break;
				case 13:
					bool r9 = int.TryParse(rawData, out int i9);
					if (r9) { p.string_along = i9; }
					break;
				case 14:
				case 15:
				case 16:
				case 17:
				case 18:
				case 19:
				case 20:
				case 21:
				case 22:
				case 23:
				case 24:
				case 25:
				case 26:
				case 27:
				case 28:
				case 29:
				case 30:
				case 31:
				case 32:
				case 33:
				case 34:
				case 35:
					string[] rawArray = rawData.TrimStart().Split(' ');
					bool part = int.TryParse(rawArray[0], out int xx);
					bool qty = int.TryParse(rawArray[1], out int yy);
					if ((part && qty) && (xx != 0))
					{
						if (!p.parts_reqd.ContainsKey(xx.ToString())) { p.parts_reqd.Add(xx.ToString(), yy); }
						else { p.parts_reqd[xx.ToString()] += yy; }
					}
					break;
			}
			return p;
		}
		public SqlCommand partSqlParamSwitch(SqlCommand command, partField part, int id)
		{
			DateTime _base = DateTime.Parse("1/1/1988");
			foreach (SqlParameter param in command.Parameters)
			{
				switch (param.SourceColumn)
				{
					case "Id":
						param.Value = id;
						continue;
					case "part_number":
						if (part.part_number == null)
						{
							param.Value = "";
						}
						else
						{
							param.Value = part.part_number;
						}
						continue;
					case "description":
						if (part.description == null)
						{
							param.Value = "";
						}
						else
						{
							param.Value = part.description;
						}
						continue;
					case "specification":
						param.Value = part.specification;
						continue;
					case "special_instruction":
						param.Value = part.special_instruction;
						continue;
					case "years_use":
						param.Value = part.years_use;
						continue;
					case "lead_time_in_weeks":
						param.Value = part.lead_time_in_weeks;
						continue;
					case "listed_vendor_id":
						if (part.listed_vendor_id == null)
						{
							param.Value = "";
						}
						else { param.Value = part.listed_vendor_id; }
						continue;
					case "best_quantity_to_order":
						param.Value = part.best_quantity_to_order;
						continue;
					case "finished_weight":
						param.Value = part.finished_weight;
						continue;
					case "price":
						param.Value = part.price;
						continue;
					case "quantity_to_order":
						param.Value = part.quantity_to_order;
						continue;
					case "listed_PO_num":
						param.Value = part.listed_PO_num;
						continue;
					case "delivery_date_1":
						if (part.delivery_date_1 > 0)
						{
							param.Value = _base.AddDays(part.delivery_date_1);
						}
						else
						{
							param.Value = DateTime.Parse("1/1/1754");
						}
						continue;
					case "delivery_date_2":
						if (part.delivery_date_2 > 0)
						{
							param.Value = _base.AddDays(part.delivery_date_2);
						}
						else
						{
							param.Value = DateTime.Parse("1/1/1754");
						}
						continue;
					case "delivery_date_3":
						if (part.delivery_date_3 > 0)
						{
							param.Value = _base.AddDays(part.delivery_date_3);
						}
						else
						{
							param.Value = DateTime.Parse("1/1/1754");
						}
						continue;
					case "delivery_date_4":
						if (part.delivery_date_4 > 0)
						{
							param.Value = _base.AddDays(part.delivery_date_4);
						}
						else
						{
							param.Value = DateTime.Parse("1/1/1754");
						}
						continue;
					case "added_cost":
						param.Value = part.added_cost;
						continue;
					case "cycle_time_secs_second_machine":
						param.Value = part.cycle_time_secs_second_machine;
						continue;
					case "added_cost_machine_2":
						param.Value = part.added_cost_machine_2;
						continue;
					case "quantity_on_hand":
						param.Value = part.quantity_on_hand;
						continue;
					case "raw_material_number":
						param.Value = part.raw_material_number;
						continue;
					case "material_weight":
						param.Value = part.material_weight;
						continue;
					case "ytd_sales":
						param.Value = part.ytd_sales;
						continue;
					case "latest_quote":
						param.Value = part.latest_quote;
						continue;
					case "quantity_assembled":
						param.Value = part.quantity_assembled;
						continue;
					case "cycle_time":
						param.Value = part.cycle_time;
						continue;
					case "machine_num":
						param.Value = part.machine_num;
						continue;
					case "machine_rate":
						param.Value = part.machine_rate;
						continue;
					case "last_years_use":
						param.Value = part.last_years_use;
						continue;
					case "weeks_cushion":
						param.Value = part.weeks_cushion;
						continue;
					case "allocated":
						param.Value = part.allocated;
						continue;
					case "setup_time":
						param.Value = part.setup_time;
						continue;
					case "raw_material_2":
						param.Value = part.raw_material_2;
						continue;
					case "list_price":
						param.Value = part.list_price;
						continue;
					case "memo":
						if (part.memo == null) { param.Value = ""; }
						else { param.Value = part.memo; }
						continue;
					case "picture_path":
						param.Value = part.picture_path;
						continue;
					case "drawing_path":
						param.Value = part.drawing_path;
						continue;
				}
			}
			return command;
		}
		public partField partSwitch(int i, string rawData, partField p)
		{
			if (rawData.Trim() == "") { return p; }
			switch (i)
			{
				case 0:
					p.part_number = rawData;
					return p;
				case 1:
					p.description = rawData;
					return p;
				case 2:
					bool r0 = int.TryParse(rawData, out int x0);
					if (r0) { p.specification = x0; }
					return p;
				case 3:
					bool r1 = int.TryParse(rawData, out int x1);
					if (r1) { p.special_instruction = x1; }
					return p;
				case 4:
					bool r2 = int.TryParse(rawData, out int x2);
					if (r2) { p.years_use = x2; }
					return p;
				case 5:
					bool r3 = int.TryParse(rawData, out int x3);
					if (r3) { p.lead_time_in_weeks = x3; }
					return p;
				case 6:
					p.listed_vendor_id = rawData;
					return p;
				case 7:
					bool r4 = int.TryParse(rawData, out int x4);
					if (r4) { p.best_quantity_to_order = x4; }
					return p;
				case 8:
					bool r5 = int.TryParse(rawData, out int x5);
					if (r5) { p.finished_weight = x5; }
					return p;
				case 9:
					bool r6 = decimal.TryParse(rawData, out decimal x6);
					if (r6) { p.price = x6; }
					return p;
				case 10:
					bool r7 = int.TryParse(rawData, out int x7);
					if (r7) { p.quantity_to_order = x7; }
					return p;
				case 11:
					bool r28 = int.TryParse(rawData, out int x28);
					if (r28) { p.listed_PO_num = x28; }
					return p;
				case 12:
					bool dr0 = int.TryParse(rawData, out int dt0);
					if (dr0) { p.delivery_date_1 = dt0; }
					return p;
				case 13:
					bool dr1 = int.TryParse(rawData, out int dt1);
					if (dr1) { p.delivery_date_2 = dt1; }
					return p;
				case 14:
					bool dr2 = int.TryParse(rawData, out int dt2);
					if (dr2) { p.delivery_date_3 = dt2; }
					return p;
				case 15:
					bool dr3 = int.TryParse(rawData, out int dt3);
					if (dr3) { p.delivery_date_4 = dt3; }
					return p;
				case 16:
					bool r8 = decimal.TryParse(rawData, out decimal x8);
					if (r8) { p.added_cost = x8; }
					return p;
				case 17:
					bool r9 = int.TryParse(rawData, out int x9);
					if (r9) { p.cycle_time_secs_second_machine = x9; }
					return p;
				case 18:
					bool r10 = decimal.TryParse(rawData, out decimal x10);
					if (r10) { p.added_cost_machine_2 = x10; }
					return p;
				case 19:
					bool r11 = int.TryParse(rawData, out int x11);
					if (r11) { p.quantity_on_hand = x11; }
					return p;
				case 20:
					bool r27 = int.TryParse(rawData, out int x27);
					if (r27) { p.raw_material_number = x27; }
					return p;
				case 21:
					bool r12 = decimal.TryParse(rawData, out decimal x12);
					if (r12) { p.material_weight = x12; }
					return p;
				case 22:
					bool r13 = int.TryParse(rawData, out int x13);
					if (r13) { p.ytd_sales = x13; }
					return p;
				case 23:
					bool r14 = decimal.TryParse(rawData, out decimal x14);
					if (r14) { p.latest_quote = x14; }
					return p;
				case 24:
					bool r15 = int.TryParse(rawData, out int x15);
					if (r15) { p.quantity_assembled = x15; }
					return p;
				case 25:
					bool r21 = int.TryParse(rawData, out int x21);
					if (r21) { p.cycle_time = x21; }
					return p;
				case 26:
					bool r22 = int.TryParse(rawData, out int x22);
					if (r22) { p.machine_num = x22; }
					return p;
				case 27:
					bool r17 = int.TryParse(rawData, out int x17);
					if (r17) { p.last_years_use = x17; }
					return p;
				case 28:
					bool r18 = int.TryParse(rawData, out int x18);
					if (r18) { p.weeks_cushion = x18; }
					return p;
				case 29:
					bool r19 = int.TryParse(rawData, out int x19);
					if (r19) { p.allocated = x19; }
					return p;
				case 30:
					bool r20 = int.TryParse(rawData, out int x20);
					if (r20) { p.setup_time = x20; }
					return p;
				case 31:
					bool r23 = int.TryParse(rawData, out int x23);
					if (r23) { p.raw_material_2 = x23; }
					return p;
				case 32:
					bool r24 = decimal.TryParse(rawData, out decimal x24);
					if (r24) { p.list_price = x24; }
					return p;
				case 33:
					p.memo = rawData;
					return p;
				case 34:
					bool r25 = int.TryParse(rawData, out int x25);
					if (r25) { p.picture_path = x25; }
					return p;
				case 35:
					bool r26 = int.TryParse(rawData, out int x26);
					if (r26) { p.drawing_path = x26; }
					return p;
				default:
					return p;
			}
		}
	}
}