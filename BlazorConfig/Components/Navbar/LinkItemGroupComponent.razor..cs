using Microsoft.AspNetCore.Components;
namespace BlazorConfig.Components.Navbar
{
    public partial class LinkItemGroupComponent : ComponentBase
    {
        [Parameter]
        public required LinkItemGroup LinkItemGroup { get; set; }
    }
}