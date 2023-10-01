using Udemy_RestWithASP_NET5.Hypermedia.Abstract;

namespace Udemy_RestWithASP_NET5.Hypermedia.Filters {
    public class HyperMediaFilterOptions {
        public List<IResponseEnricher> ContentResponseEricherList { get; set; } = new List<IResponseEnricher>();
    }
}
