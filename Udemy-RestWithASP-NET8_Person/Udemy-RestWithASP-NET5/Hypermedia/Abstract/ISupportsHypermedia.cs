namespace Udemy_RestWithASP_NET5.Hypermedia.Abstract {
    public interface ISupportsHypermedia {
        List<HyperMediaLink> Links { get; set; }
    }
}
