namespace BlazorConfig.Components.Navbar;

public class LinkItemGroup : ILinkItem

    public string Href { get; set; }
    public string Title { get; set; }
    public List<ILinkItem> LinkItems { get; set; }
    public bool IsOpen { get; set; }
    public RenderFragment? Content { get; set; }

    public LinkItemGroup(string href, string title, List<ILinkItem> linkItems)
    {
        Href = href;
        Title = title;
        LinkItems = linkItems;
        Content = CreateComponent();
    }
    public RenderFragment CreateComponent() => builder =>
    {
        builder.OpenComponent(0, typeof(LinkItemGroupComponent));
        builder.AddAttribute(1, nameof(LinkItemGroupComponent.LinkItemGroup), this);
        builder.CloseComponent();

    };

}
