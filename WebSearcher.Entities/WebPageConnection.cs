namespace WebSearcher.Entities
{
    public class WebPageConnection : Entity
    {
        public int WebPageFromId { get; set; }
        public int WebPageToId { get; set; }
        public string WebPageToUrl { get; set; }
    }
}
