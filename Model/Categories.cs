namespace ManagementCoffee.Model
{
    public class Categories
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime create_at { get; set; }
        public DateTime update_at { get; set;}
        public int create_by { get; set; }
    }
}
