using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class GenericMasterEntity
    {
        public int GenericMasterID { get; set; }
        public DataProvider DataProvider { get; set; }
        public string address { get; set; }
        public string body_style { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public int days_on_lot { get; set; }
        public string dealer_name { get; set; }
        public string drivetrain { get; set; }
        public string exterior_color { get; set; }
        public string final_url { get; set; }
        public string fuel_type { get; set; }
        public string hs_company_id { get; set; }
        public DateTime in_stock_date { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string make { get; set; }
        public decimal mileage_value { get; set; }
        public string mileage_unit { get; set; }
        public string model { get; set; }
        public string msrp { get; set; }
        public string phone_number { get; set; }
        public DateTime platform_sold_date { get; set; }
        public int platform_sold_day_counter { get; set; }
        public double price { get; set; }
        public double sale_price { get; set; }
        public string state { get; set; }
        public string state_of_vehicle { get; set; }
        public int stock_number { get; set; }
        public string title { get; set; }
        public string transmission { get; set; }
        public string trim { get; set; }
        public string vin { get; set; }
        public int year { get; set; }
        public string zip_code { get; set; }
    }
}
