using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RAFtest
{
	class dataSwitches
	{
		public string vendorSqlSwitch(string sqlCol, datasets.vendorFields v)
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

		public datasets.productFields prodSwitch(int i, string rawData, datasets.productFields p)
		{
			if (rawData.Trim() == "") { return p; }
			switch (i)
			{
				case 0:
					p.productNumber = rawData;
					break;
				case 1:
					p.description = rawData;
					break;
				case 2:
					bool rx0 = decimal.TryParse(rawData, out decimal dx0);
					if (rx0) { p.price = dx0; }
					break;
				case 3:
					bool rx1 = decimal.TryParse(rawData, out decimal dx1);
					if (rx1) { p.weight = dx1; }
					break;
				case 4:
					bool r0 = decimal.TryParse(rawData, out decimal i0);
					if (r0) { p.master_units = i0; }
					break;
				case 5:
					bool r1 = decimal.TryParse(rawData, out decimal i1);
					if (r1) { p.cubic_feet = i1; }
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
					bool r7 = int.TryParse(rawData, out int i7);
					if (r7) { p.assembly_time_secs = i7; }
					break;
				case 12:
					bool r8 = int.TryParse(rawData, out int i8);
					if (r8) { p.product_code = i8; }
					break;
				case 13:
					bool r9 = int.TryParse(rawData, out int i9);
					if (r9) { p.product_code = i9; }
					break;
				case 14: case 15: case 16: case 17: case 18: case 19: case 20: case 21: case 22: case 23: case 24:
				case 25: case 26: case 27: case 28: case 29: case 30: case 31: case 32: case 33: case 34: case 35:
					string[] rawArray = rawData.TrimStart().Split(' ');
					bool part = int.TryParse(rawArray[0], out int xx);
					bool qty = int.TryParse(rawArray[1], out int yy);
					if ((part && qty) && (xx != 0))
					{
						if (!p.parts_reqd.ContainsKey(xx.ToString())){ p.parts_reqd.Add(xx.ToString(), yy); }
						else { p.parts_reqd[xx.ToString()] += yy; }
					}
					break;
			}
			return p;
		}

		public datasets.partFields partSwitch(int i, string rawData, datasets.partFields p)
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
				case 7:
					bool r4 = int.TryParse(rawData, out int x4);
					if (r4) { p.best_quantity_to_order = x4; }
					return p;
				case 9:
					bool r5 = int.TryParse(rawData, out int x5);
					if (r5) { p.finished_weight = x5; }
					return p;
				case 10:
					bool r6 = decimal.TryParse(rawData, out decimal x6);
					if (r6) { p.price = x6; }
					return p;
				case 11:
					bool r7 = int.TryParse(rawData, out int x7);
					if (r7) { p.quantity_to_order = x7; }
					return p;
				case 12:
					bool r28 = int.TryParse(rawData, out int x28);
					if (r28) { p.listed_PO_num = x28; }
					return p;
				case 13:
					bool dr0 = DateTime.TryParse(rawData, out DateTime dt0);
					if (dr0) { p.delivery_date_1 = dt0; }
					return p;
				case 14:
					bool dr1 = DateTime.TryParse(rawData, out DateTime dt1);
					if (dr1) { p.delivery_date_1 = dt1; }
					return p;
				case 15:
					bool dr2 = DateTime.TryParse(rawData, out DateTime dt2);
					if (dr2) { p.delivery_date_1 = dt2; }
					return p;
				case 16:
					bool dr3 = DateTime.TryParse(rawData, out DateTime dt3);
					if (dr3) { p.delivery_date_1 = dt3; }
					return p;
				case 17:
					bool r8 = decimal.TryParse(rawData, out decimal x8);
					if (r8) { p.added_cost = x8; }
					return p;
				case 18:
					bool r9 = int.TryParse(rawData, out int x9);
					if (r9) { p.cycle_time_secs_second_machine = x9; }
					return p;
				case 19:
					bool r10 = decimal.TryParse(rawData, out decimal x10);
					if (r10) { p.added_cost_machine_2 = x10; }
					return p;
				case 20:
					bool r11 = int.TryParse(rawData, out int x11);
					if (r11) { p.quantity_on_hand = x11; }
					return p;
				case 21:
					bool r27 = int.TryParse(rawData, out int x27);
					if (r27) { p.raw_material_number = x27; }
					return p;
				case 22:
					bool r12 = decimal.TryParse(rawData, out decimal x12);
					if (r12) { p.material_weight = x12; }
					return p;
				case 23:
					bool r13 = int.TryParse(rawData, out int x13);
					if (r13) { p.ytd_sales = x13; }
					return p;
				case 24:
					bool r14 = decimal.TryParse(rawData, out decimal x14);
					if (r14) { p.latest_quote = x14; }
					return p;
				case 25:
					bool r15 = int.TryParse(rawData, out int x15);
					if (r15) { p.quantity_assembled = x15; }
					return p;
				case 26:
					bool r21 = int.TryParse(rawData, out int x21);
					if (r21) { p.cycle_time = x21; }
					return p;
				case 27:
					bool r22 = int.TryParse(rawData, out int x22);
					if (r22) { p.machine_num = x22; }
					return p;
				case 28:
					bool r16 = decimal.TryParse(rawData, out decimal x16);
					if (r16) { p.machine_rate = x16; }
					return p;
				case 29:
					bool r17 = int.TryParse(rawData, out int x17);
					if (r17) { p.last_years_use = x17; }
					return p;
				case 30:
					bool r18 = int.TryParse(rawData, out int x18);
					if (r18) { p.weeks_cushion = x18; }
					return p;
				case 31:
					bool r19 = int.TryParse(rawData, out int x19);
					if (r19) { p.allocated = x19; }
					return p;
				case 32:
					bool r20 = int.TryParse(rawData, out int x20);
					if (r20) { p.setup_time = x20; }
					return p;
				case 33:
					bool r23 = int.TryParse(rawData, out int x23);
					if (r23) { p.raw_material_2 = x23; }
					return p;
				case 34:
					bool r24 = decimal.TryParse(rawData, out decimal x24);
					if (r24) { p.list_price = x24; }
					return p;
				case 35:
					p.memo = rawData;
					return p;
				case 36:
					bool r25 = int.TryParse(rawData, out int x25);
					if (r25) { p.picture_path = x25; }
					return p;
				case 37:
					bool r26 = int.TryParse(rawData, out int x26);
					if (r26) { p.drawing_path = x26; }
					return p;
				default:
					return p;
			}
		}

		public datasets.vendorFields vendorSwitch(int i, string rawData, datasets.vendorFields v)
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
	}
}