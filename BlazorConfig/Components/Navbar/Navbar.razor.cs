namespace BlazorConfig.Components.Navbar;


public partial class Navbar
{
    private List<ILinkItem> _linkItems = new(); // Initialize the list

    protected override void OnInitialized()
    {
        _linkItems = new List<ILinkItem>()
        {
            new LinkItem("/test", "Test"), // Using the LinkItem constructor directly
            new LinkItem("/test2", "Test2"),
            new LinkItemGroup("/test3", "Test3", new List<ILinkItem>()
            {                                                                          
                new LinkItem("/test", "Test"),                                                                                              
                new LinkItem("/test2", "Test2"),
                new LinkItemGroup("/test3", "Test3", new List<ILinkItem>()
                {
                    new LinkItem("/test", "Test"),
                    new LinkItem("/test2", "Test2"),
                    new LinkItemGroup("/test3", "Test3", new List<ILinkItem>()
                    {
                        new LinkItem("/test", "Test"),
                        new LinkItem("/test2", "Test2"),
                        new LinkItem("/test3", "Test3")
                    })
                })
            })
        };
    }
}
