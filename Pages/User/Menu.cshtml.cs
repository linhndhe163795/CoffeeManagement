using ManagementCoffee.Data;
using ManagementCoffee.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ManagementCoffee.Pages.User
{
    public class MenuModel : PageModel
    {
        private readonly CoffeeManagementContext _context;
        private readonly IConfiguration Configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        [BindProperty]
        public string ProductCode { get; set; }
        public int TotalItemInCart { get; set; }
        public List<Products> Products { get; set; }
        public MenuModel(CoffeeManagementContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _context=context;
            Configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public PaginatedList<Products> products { get; set; }
        public async Task OnGetAsync(int? pageIndex)
        {
            IQueryable<Products> productIQ = from s in _context.Products
                                             select s;

            var pageSize = Configuration.GetValue("PageSize", 6);
            products = await PaginatedList<Products>.CreateAsync(
                productIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
            CaculatorTotalItemsCart();

        }

        public IActionResult OnPost()
        {
            Products = _context.Products.ToList();
            if (ProductCode == null)
            {
                CaculatorTotalItemsCart();
                return RedirectToPage("menu");
            }
            else
            {
                var cart = _httpContextAccessor.HttpContext.Session.GetString("cart");
                if (cart == null)
                {
                    Dictionary<string, int> dict = new Dictionary<string, int>();
                    dict.Add(ProductCode, 1);
                    var dictJson = JsonConvert.SerializeObject(dict);
                    _httpContextAccessor.HttpContext.Session.SetString("cart", dictJson);

                }
                else
                {
                    var cartDict = JsonConvert.DeserializeObject<Dictionary<string, int>>(cart);
                    if (cartDict.TryGetValue(ProductCode, out int quanity))
                    {
                        quanity += 1;
                        cartDict[ProductCode] = quanity;
                    }
                    else
                    {
                        cartDict[ProductCode] = 1;
                    }
                    var cartDictJson = JsonConvert.SerializeObject(cartDict);
                    _httpContextAccessor.HttpContext.Session.SetString("cart", cartDictJson);
                }
                CaculatorTotalItemsCart();
                return RedirectToPage("menu");
            }


        }

        private void CaculatorTotalItemsCart()
        {
            var cart = _httpContextAccessor.HttpContext.Session.GetString("cart");
            if (cart == null)
            {
                TotalItemInCart = 0;
            }
            else
            {
                var carDict = JsonConvert.DeserializeObject<Dictionary<string, int>>(cart);
                TotalItemInCart = carDict.Count();
            }
        }
    }
}
