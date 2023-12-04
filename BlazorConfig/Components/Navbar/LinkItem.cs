using Microsoft.AspNetCore.Components;
namespace BlazorConfig.Components.Navbar;

public partial class LinkItem : ILinkItem
{
    public string Href { get; set; }
    public string Titel { get; set; }
    public RenderFragment? Content { get; set; }

    public LinkItem(string href, string titel)
    {
        Href = href;
        Titel = titel;
        Content = CreateComponent();
    }

    public RenderFragment CreateComponent() => builder =>
    {
        builder.OpenComponent(0, typeof(LinkItemComponent));
        builder.AddAttribute(1, nameof(LinkItemComponent.LinkItem), this);
        builder.CloseComponent();

    };
}