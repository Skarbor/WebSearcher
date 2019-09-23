namespace WebSearcher.Entities
{
    public class WebPageConnections : Entity
    {
        public int WebPageFromId { get; set; }
        public int WebPageToId { get; set; }
        public string WebPageToUrl { get; set; }
    }
}
