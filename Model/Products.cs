namespace ManagementCoffee.Model
{
    public class Products
    {
        public string productCode { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public int categoryID { get; set; }
        public string? image { get; set; }
        public DateTime? create_at { get; set; }
        public DateTime? update_at { get; set;}
        public int? create_by { get; set; }
    }
}
