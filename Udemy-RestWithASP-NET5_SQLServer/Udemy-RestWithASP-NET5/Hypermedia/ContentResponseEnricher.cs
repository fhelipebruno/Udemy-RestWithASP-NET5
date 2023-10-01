using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Collections.Concurrent;
using Udemy_RestWithASP_NET5.Hypermedia.Abstract;
using Udemy_RestWithASP_NET5.Hypermedia.Utils;

namespace Udemy_RestWithASP_NET5.Hypermedia {
    public abstract class ContentResponseEnricher<T> : IResponseEnricher where T : ISupportsHypermedia {

        public ContentResponseEnricher() {

        }
        public bool canEnrich(Type contentType) {
            return contentType == typeof(T) || contentType == typeof(List<T>) || contentType == typeof(PagedSearchVO<T>);
        }

        protected abstract Task EnrichModel(T content, IUrlHelper urlHelper);
        bool IResponseEnricher.canEnrich(ResultExecutingContext response) {
            if (response.Result is OkObjectResult okObjectResult) {
                return canEnrich(okObjectResult.Value.GetType());
            }
            return false;
        }
        

        public async Task Enrich(ResultExecutingContext response) {
            var urlHelper = new UrlHelperFactory().GetUrlHelper(response);

            if (response.Result is OkObjectResult okObjectResult) {
                if (okObjectResult.Value is T model) {
                    await EnrichModel(model, urlHelper);
                }
                else if (okObjectResult.Value is List<T> collection) {
                    ConcurrentBag<T> bag = new ConcurrentBag<T>(collection);
                    Parallel.ForEach(bag, (element) => {
                        EnrichModel(element, urlHelper);
                    });
                }
                else if (okObjectResult.Value is PagedSearchVO<T> pagedSearch)
                {
                    Parallel.ForEach(pagedSearch.List.ToList(), (element) => {
                        EnrichModel(element, urlHelper);
                    });
                }
                await Task.FromResult<object>(null);
            }            
        }
    }
}
