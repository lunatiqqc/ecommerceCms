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
                    Children = new List<Page>()
                };

                var test = new Page
                {
                    Title = "test",
                    Url = "home/test",
                    VisibleInMenu = true,
                    GridContent = new List<GridRow>()
                    {
                        new GridRow()
                        {
                            Columns = new List<GridColumn>()
                            {
                                new GridColumn()
                                {
                                    Width = 6,
                                    Component = new TextComponent()
                                    {
                                        Text="test--"
                                    },
                                }, new GridColumn()
                                {
                                    Width = 6,
                                    Component = new ImageComponent()
                                    {
                                        ImageUrl = "deeeeeez.png"
                                    },
                                },
                            }
                        }
                    }
                };

                var test2 = new Page
                {
                    Title = "test2",
                    Url = "test2",

                    VisibleInMenu = true,
                    GridContent = new List<GridRow>()
                    {
                        new GridRow()
                        {
                            Columns = new List<GridColumn>()
                            {
                                new GridColumn()
                                {
                                    Width = 12,
                                    Component = new TextComponent()
                                    {
                                       Text="test2-"
                                    }
                                }
                            }
                        }
                    }
                };

                homepage.Children.Add(test);
                test.ParentPage = homepage;

                pages.Add(homepage);
                pages.Add(test2);

                context.Pages.AddRange(pages);
                context.SaveChanges();
            }
        }
    }
}
