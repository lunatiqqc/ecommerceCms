using cms.models;
using cms;
namespace cms.SeedData
{
    public static class PageSeedData
    {
	public static void SeedPages(MyDbContext context)
	{
	    if (!context.Pages.Any())
	    {
		//var pages2 = new List<Page>();
		//
		//var homepage2 = new Page
		//{
		//    Title = "home",
		//    Url = "home",
		//    VisibleInMenu = true,
		//    Children = new List<Page>(),
		//    GridRows = new List<GridRow>()
		//    {
		//	new GridRow()
		//	{
		//	    Id = 0,
		//	    Columns = new List<GridColumn>()
		//	    {
		//		new GridColumn()
		//		{
		//		    Width = 6,
		//
		//		    GridRows = new List<GridRow>()
		//		    {
		//			new GridRow()
		//			{
		//			    Id = 0,
		//			    Columns = new List<GridColumn>
		//			    {
		//				new GridColumn()
		//				{
		//				    GridRows = new List<GridRow>
		//				    {
		//					new GridRow()
		//					{
		//					    Id = 0,
		//					}
		//				    }
		//				}
		//			    }
		//			    
		//			}
		//		    }
		//		},
		//	    }
		//	}
		//    }
		//
		//};
		//
		//pages2.Add(homepage2);
		//context.Pages.AddRange(pages2);
		//context.SaveChanges();
		//
		//return;
		var pages = new List<Page>();
		//var homepage = new Page
		//{
		//    Title = "home",
		//    Url = "home",
		//    VisibleInMenu = true,
		//    Children = new List<Page>(),
		//    GridRows = new List<GridRow>()
		//    {
		//    	new GridRow()
		//    	{
		//	    Id = 0,
		//	    Columns = new List<GridColumn>()
		//	    {
		//		    new GridColumn()
		//		    {
		//			Width = 6,
		//
		//			GridRows = new List<GridRow>()
		//			{
		//			    new GridRow()
		//			    {
		//				Id = 0,
		//				Columns = new List<GridColumn>
		//				{
		//				    new GridColumn()
		//				    {
		//					Width = 6,
		//					Component = new TextComponent()
		//					{
		//					    Text=  "testt"
		//					}
		//				    },
		//				    new GridColumn()
		//				    {
		//					Width= 6,
		//					ColumnStart = 6,
		//					Component = new ImageComponent()
		//					{
		//					    ImageUrl=  "deeez.url"
		//					}
		//				    }
		//				}
		//
		//			    },
		//			     new GridRow()
		//			    {
		//				Id = 0,
		//				Columns = new List<GridColumn>
		//				{
		//				    new GridColumn()
		//				    {
		//					Width = 6,
		//					Component = new TextComponent()
		//					{
		//					    Text=  "testt2"
		//					}
		//				    },
		//				    new GridColumn()
		//				    {
		//					Width= 6,
		//					ColumnStart = 6,
		//					Component = new ImageComponent()
		//					{
		//					    ImageUrl=  "deeez2.url"
		//					}
		//				    }
		//				}
		//
		//			    }
		//			}
		//		    },
		//		    new GridColumn()
		//		       {
		//		    	Width = 6,
		//			ColumnStart = 6,
		//		    	GridRows = new List<GridRow>()
		//		    	{
		//				new GridRow()
		//				{
		//		    		Id = 0,
		//		    		Columns = new List<GridColumn>
		//		    		{
		//				    new GridColumn()
		//				    {
		//					Width = 3,
		//					ColumnStart= 9,
		//					Component = new TextComponent()
		//					{
		//					    Text=  "testt"
		//					}
		//				    },
		//				    new GridColumn()
		//				    {
		//					Width= 9,
		//					ColumnStart = 0,
		//					Component = new ImageComponent()
		//					{
		//					    ImageUrl=  "deeez.url"
		//					}
		//				    }
		//		    		}
		//
		//				}
		//		    	}
		//		       },
		//	    }
		//    	}
		//    }
		//};

		var test = new Page
		{
		    Title = "test",
		    Url = "home/test",
		    VisibleInMenu = true
		};

		//var test2 = new Page
		//{
		//    Title = "test3",
		//    Url = "test3",
		//
		//    VisibleInMenu = true,
		//    GridRows = new List<GridRow>()
		//    {
		//	new GridRow()
		//	{
		//	    Columns = new List<GridColumn>()
		//	    {
		//		new GridColumn()
		//		{
		//		    Width = 6,
		//		    Component = new TextComponent()
		//		    {
		//		       Text="test3"
		//		    }
		//		},
		//		new GridColumn()
		//		{
		//		    ColumnStart = 6,
		//		    Width = 6,
		//		    GridRows = new List<GridRow>()
		//		    {
		//			new GridRow()
		//			{
		//			    Columns = new List<GridColumn>()
		//			    {
		//				new GridColumn()
		//				{
		//				    Width = 6,
		//				    Component = new TextComponent()
		//				    {
		//					Text="test--"
		//				    },
		//				},
		//				new GridColumn()
		//				{
		//				    Width = 6,
		//				    Component = new ImageComponent()
		//				    {
		//					ImageUrl = "deeeeeez.png"
		//				    },
		//				    ColumnStart = 6
		//				}
		//			    }
		//			}
		//		    }
		//		}
		//	    }
		//	},
		//
		//    }
		//};

		//homepage.Children.Add(test);
		//test.ParentPage = homepage;

		//pages.Add(homepage);
		pages.Add(test);

		context.Pages.AddRange(pages);
		context.SaveChanges();
	    }
	}
    }
}
