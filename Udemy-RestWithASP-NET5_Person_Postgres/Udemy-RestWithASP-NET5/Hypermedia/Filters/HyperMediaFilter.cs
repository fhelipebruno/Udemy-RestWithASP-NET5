﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Udemy_RestWithASP_NET5.Hypermedia.Filters {
    public class HyperMediaFilter : ResultFilterAttribute{
        private readonly HyperMediaFilterOptions _hyperMediaFilterOptions;

        public HyperMediaFilter(HyperMediaFilterOptions hyperMediaFilterOptions) {
            _hyperMediaFilterOptions = hyperMediaFilterOptions;
        }

        public override void OnResultExecuting(ResultExecutingContext context) {
            TryEnrichResult(context);
            
        }

        private void TryEnrichResult(ResultExecutingContext context) {
            if (context.Result is OkObjectResult objectResult) {
                var enricher = _hyperMediaFilterOptions
                    .ContentResponseEricherList
                    .FirstOrDefault(x => x.canEnrich(context));

                if (enricher != null) Task.FromResult(enricher.Enrich(context));
            }

        }
    }
}
