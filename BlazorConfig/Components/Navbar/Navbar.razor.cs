namespace BlazerConfig.Components.Navbar;

public partial class Navbar     
    private List<LinkItem> _linkItems = new();

    protected override void OnInitialized()
    {
        _linkItems = new List<LinkItem>()
        {
            new LinkItem(href: "/test", title: "Test"),
            new LinkItem(href: "/test2", title: "Test2")
        };
    }
}
