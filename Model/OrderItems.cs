using MessagePack;
using System.ComponentModel.DataAnnotations;

namespace ManagementCoffee.Model
{
    public class OrderItems
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int OrderID { get; set; }
        [System.ComponentModel.DataAnnotations.Key]
        public string productCode { get; set; }
        public int quantity { get; set; }
        public double sellPrice { get; set; }
        public DateTime create_at { get; set; }
        public DateTime update_at { get; set;}
        public int create_by{ get; set;}
    }
}
