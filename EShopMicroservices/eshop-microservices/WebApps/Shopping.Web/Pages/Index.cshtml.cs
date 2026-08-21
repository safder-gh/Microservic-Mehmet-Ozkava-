using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopping.Web.Models.Basket;

namespace Shopping.Web.Pages
    {
    public class IndexModel(ICatalogService catalogService,IBasketService basketService, ILogger<IndexModel> logger) : PageModel
        {
        public IEnumerable<ProductModel> ProductList { get; set; } = new List<ProductModel>();
        public async Task<IActionResult> OnGetAsync()
            {
            logger.LogInformation("Index page requested.");
            var result = await catalogService.GetProducts();
            ProductList = result.Products;
            return Page();
            }
        public async Task<IActionResult> OnPostAddToCartAsync(Guid productId)
                {
                logger.LogInformation("Add to cart requested.");
                var productResponse = await catalogService.GetProduct(productId);
                var basket = await basketService.LoadUserBasket();
            var alreadyExists = basket.Items.Any(p => p.ProductId == productId);
            var index = basket.Items.FindIndex(p => p.ProductId == productId);

            if (index >= 0)
                {
                basket.Items[index].Quantity = basket.Items[index].Quantity + 1;
                }
            else
                {
                basket.Items.Add(new ShoppingCartItemModel
                    {
                    ProductId = productResponse.Product.Id,
                    ProductName = productResponse.Product.Name,
                    Price = productResponse.Product.Price,
                    Quantity = 1,
                    Color = "Black"
                    });
                }
            await basketService.StoreBasket(new StoreBasketRequest(basket));
            return RedirectToPage("Cart");
            }
            }
    }
