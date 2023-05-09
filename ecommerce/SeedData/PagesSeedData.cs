using cms.Models;
using cms.Users;
namespace cms.SeedData
{
    public static class PageSeedData
    {
        public static void SeedPages(MyDbContext context)
        {
            if (!context.Pages.Any())
            {
                var dashboardPage = new Page
                {
                    Title = "Dashboard",
                    Url = "/dashboard",
                    Children = new List<Page>(),
                    VisibleInMenu = true,
                    IsSystemPage = true
                };

                var ecommercePage = new Page
                {
                    Title = "E-commerce",
                    Url = "/dashboard/ecommerce",
                    Parent = dashboardPage,
                    Children = new List<Page>(),
                    VisibleInMenu = true,
                    IsSystemPage = true
                };

                dashboardPage.Children.Add(ecommercePage);

                var productsPage = new Page
                {
                    Title = "Products",
                    Url = "/dashboard/ecommerce/products",
                    Parent = ecommercePage,
                    Children = null,
                    VisibleInMenu = false,
                    IsSystemPage = true
                };
                ecommercePage.Children.Add(productsPage);


                var pages = new List<Page> { dashboardPage };

                context.Pages.AddRange(pages);
                context.SaveChanges();
            }
        }
    }
}
