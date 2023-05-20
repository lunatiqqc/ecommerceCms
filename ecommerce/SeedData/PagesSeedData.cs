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
                var pages = new List<Page>();
                var homepage = new Page
                {
                    Title = "home",
                    Url = "home",
                    VisibleInMenu = true,
                    IsSystemPage = false,
                    Children = new List<Page> { }
                };

                var test = new Page
                {
                    Title = "test",
                    Url = "test",
                    VisibleInMenu = true,
                    IsSystemPage = false
                }; 
                var test2 = new Page
                {
                    Title = "test",
                    Url = "test",
                    VisibleInMenu = true,
                    IsSystemPage = false
                };

                homepage.Children.Add(test);

                pages.Add(homepage);
                pages.Add(test2);

                context.Pages.AddRange(pages);
                context.SaveChanges();
            }
        }
    }
}
