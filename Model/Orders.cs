namespace ManagementCoffee.Model
{
    public class Orders
    {
        public int OrderID { get; set; }
        public DateTime date { get; set; }
        public double amount { get; set; }
        public DateTime create_at { get; set; }
        public DateTime update_at { get; set;}
        public int create_by { get; set; }

    }
}
