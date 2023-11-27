namespace BlazorConfig.Components.Navbar;

public class LinkItemGroup : ILinkItem

    public string Href { get; set; }
    public string Title { get; set; }
    public List<ILinkItem> LinkItems { get; set; }

    public LinkItemGroup(string href, string title, List<ILinkItem> linkItems)
    {
        Href = href;
        Title = title;
        LinkItems = linkItems;
    }
}
