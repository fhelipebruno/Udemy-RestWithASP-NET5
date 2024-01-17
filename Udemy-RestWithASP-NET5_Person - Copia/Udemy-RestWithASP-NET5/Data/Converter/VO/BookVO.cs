using System.ComponentModel.DataAnnotations.Schema;
using Udemy_RestWithASP_NET5.Hypermedia;
using Udemy_RestWithASP_NET5.Hypermedia.Abstract;
using Udemy_RestWithASP_NET5.Model.Base;

namespace Udemy_RestWithASP_NET5.Data.VO {
    
    public class BookVO : ISupportsHypermedia {

        public long Id { get; set; }
        public string? Author { get; set; }
        public DateTime LaunchDate { get; set; }
        public decimal Price { get; set; }
        public string? Title { get; set; }
        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();

    }
}
