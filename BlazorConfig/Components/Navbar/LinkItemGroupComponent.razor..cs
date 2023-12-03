namespace BlazorConfig.Components.Navbar
{
    public partial class LinkItemGroupComponent : ComponentBase
    {
        [Parameter]
        public ILinkItemGroup LinkItemGroup { get; set; } = null!;

        private void Toggle()
        {
            LinkItemGroup.IsOpen = !LinkItemGroup.IsOpen;
        }
    }
}