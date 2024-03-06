using ManagementCoffee.Data;
using ManagementCoffee.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ManagementCoffee.Pages.Home
{
    public class IndexModel : PageModel
    {
        private readonly CoffeeManagementContext _context;
        private readonly IConfiguration Configuration;
        public IndexModel(CoffeeManagementContext context, IConfiguration configuration)
        {
            _context=context;
            Configuration = configuration;
        }
        public PaginatedList<Products> products { get; set; }
        public async Task OnGetAsync(int? pageIndex)
        {
            IQueryable<Products> productIQ = from s in _context.Products
                                             select s;

            var pageSize = Configuration.GetValue("PageSize", 4);
            products = await PaginatedList<Products>.CreateAsync(
                productIQ.AsNoTracking(), pageIndex ?? 1, pageSize);


        }
    }
}
