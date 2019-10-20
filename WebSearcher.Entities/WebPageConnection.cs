namespace WebSearcher.Entities
{
    public class WebPageConnection : Entity
    {
        public virtual WebPage WebPageFrom { get; set; }
        public virtual WebPage WebPageTo { get; set; }
    }
}
