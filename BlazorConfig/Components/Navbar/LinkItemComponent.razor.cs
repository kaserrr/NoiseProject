using Microsoft.AspNetCore.Components;
namespace BlazorConfig.Components.Navbar;

    public partial class LinkItemComponent
    {
        [Parameter]
        public LinkItem LinkItem { get; set; }
    }

