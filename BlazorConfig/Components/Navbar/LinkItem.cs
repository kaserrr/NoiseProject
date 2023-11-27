namespace BlazerConfig.Components.Navbar
{
    public partial class LinkItem
    {
        public string Href { get; set; }
        public string Titel { get; set; }

        public LinkItem(string href, string titel)
        {
            Href = href;
            Titel = titel;
        }
    }
}