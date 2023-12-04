using Microsoft.AspNetCore.Components;
namespace BlazorConfig.Components.Navbar;

    public partial class LinkItemComponent
    {
        [Parameter]
        public required LinkItem LinkItem { get; set; }
    }

