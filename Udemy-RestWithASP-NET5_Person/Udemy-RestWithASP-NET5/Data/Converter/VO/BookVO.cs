using System.ComponentModel.DataAnnotations.Schema;
using Udemy_RestWithASP_NET5.Model.Base;

namespace Udemy_RestWithASP_NET5.Data.VO {
    
    public class BookVO {

        public long Id { get; set; }
        public string? Author { get; set; }
        public DateTime LaunchDate { get; set; }
        public decimal Price { get; set; }
        public string? Title { get; set; }


    }
}
