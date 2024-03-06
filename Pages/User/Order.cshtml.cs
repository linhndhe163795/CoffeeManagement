using ManagementCoffee.Data;
using ManagementCoffee.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace ManagementCoffee.Pages.User
{
    public class OrderModel : PageModel
    {
        private readonly CoffeeManagementContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public Dictionary<Products, int> Cart { get; set; }
        [BindProperty]
        public string ProductCode { get; set; }
        [BindProperty]
        public int Quantity { get; set; }
        [BindProperty]
        public decimal TotalPrice { get; set; }
        [BindProperty]
        public Orders Orders { get; set; }
        public OrderModel(CoffeeManagementContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context=context;
            _httpContextAccessor=httpContextAccessor;
            Cart = new Dictionary<Products, int>();

        }

        public void OnGet()
        {
            //Get cart
            var cart = _httpContextAccessor.HttpContext.Session.GetString("cart");
            if (cart != null)
            {
                var cartDict = JsonConvert.DeserializeObject<Dictionary<string, int>>(cart);
                foreach (var key in cartDict.Keys)
                {
                    Cart[_context.Products.FirstOrDefault(p => p.productCode == key)] = cartDict[key];

                }
            }
        }
        public IActionResult OnPostChangeQuantity()
        {
            if (ProductCode != null)
            {
                //get cart 
                var cart = _httpContextAccessor.HttpContext.Session.GetString("cart");
                var cartDict = JsonConvert.DeserializeObject<Dictionary<string, int>>(cart);
                
                    if (cartDict.TryGetValue(ProductCode, out int quantity))
                    {
                        if (Quantity == 0)
                        {
                            cartDict.Remove(ProductCode);
                        }
                        else
                        {
                            cartDict[ProductCode] = Quantity;
                        }
                    }
                var cartDictJson = JsonConvert.SerializeObject(cartDict);
                _httpContextAccessor.HttpContext.Session.SetString("cart", cartDictJson);
            }
            return RedirectToPage();
        }
        public IActionResult OnPostRemove()
        {
            if (ProductCode != null)
            {
                //get cart 
                var cart = _httpContextAccessor.HttpContext.Session.GetString("cart");
                var cartDict = JsonConvert.DeserializeObject<Dictionary<string, int>>(cart);
                cartDict.Remove(ProductCode);
                var cartDictJson = JsonConvert.SerializeObject(cartDict);
                _httpContextAccessor.HttpContext.Session.SetString("cart", cartDictJson);
            }
            return RedirectToPage();

        }
        public IActionResult OnPostPaymentOrder()
        {
            _httpContextAccessor.HttpContext.Session.Remove("cart");
            AddOrderItems();
            return RedirectToPage("menu");
        }
        public IActionResult AddOrderItems()
        {
            _context.Orders.Add(Orders);
            _context.SaveChanges();
            Console.WriteLine("Add hoa don thanh cong");
            return RedirectToPage("menu");
        }
    }
}
