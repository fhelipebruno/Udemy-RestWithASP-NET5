﻿using Microsoft.AspNetCore.Mvc;
using System.Text;
using Udemy_RestWithASP_NET5.Data.VO;
using Udemy_RestWithASP_NET5.Hypermedia.Constants;

namespace Udemy_RestWithASP_NET5.Hypermedia.Enricher {
    public class BookEnricher : ContentResponseEnricher<BookVO> {
        private readonly object _lock = new object();
        protected override Task EnrichModel(BookVO content, IUrlHelper urlHelper) {
            var path = "api/book";
            string link = getLink(content.Id, urlHelper, path);

            content.Links.Add(new HyperMediaLink() {
                Action = HttpActionVerb.GET,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultGet
            });

            content.Links.Add(new HyperMediaLink() {
                Action = HttpActionVerb.POST,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultPost
            });
            
            content.Links.Add(new HyperMediaLink() {
                Action = HttpActionVerb.PUT,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultPut
            });

            content.Links.Add(new HyperMediaLink() {
                Action = HttpActionVerb.DELETE,
                Href = link,
                Rel = RelationType.self,
                Type = "int"
            });

            return Task.CompletedTask;
        }

        private string getLink(long id, IUrlHelper urlHelper, string path) {
            lock (_lock) {
                var url = new { controller = path, id = id };
                return new StringBuilder(urlHelper.Link("DefaultApi", url)).Replace("%2F", "/").ToString();
            };
        }
    }
}
