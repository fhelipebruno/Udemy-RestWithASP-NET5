namespace Udemy_RestWithASP_NET5.Hypermedia.Abstract {
    public interface ISupportHypermedia {
        List<HyperMediaLink> Links { get; set; }
    }
}
