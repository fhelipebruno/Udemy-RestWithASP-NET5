using Microsoft.AspNetCore.Mvc.Filters;

namespace Udemy_RestWithASP_NET5.Hypermedia.Abstract {
    public interface IResponseEnricher {
        bool canEnrich(ResultExecutingContext context);
        Task Enrich(ResultExecutingContext context); 
    }
}
