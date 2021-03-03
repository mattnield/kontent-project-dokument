using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Kentico.Kontent.Delivery.Builders.DeliveryClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace kontent_project_dokument.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        [BindProperty]
        public string ProjectId{get;set;}
        public IList<Kentico.Kontent.Delivery.Abstractions.IContentType> Types{get;set;}

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public void OnPost()
        {
            var projectId = Request.Form["ProjectId"];
            _logger.Log(LogLevel.Information, projectId);

            if(!Guid.TryParse(projectId, out Guid projectGuid)){
                // Invalid project ID
                return;
            }

            var client = DeliveryClientBuilder
                .WithOptions(builder => builder
                    .WithProjectId(projectId)
                    .UseProductionApi()
                    .Build())
                    .Build();
            
            var typesResult = client.GetTypesAsync();
            Types = typesResult.Result.Types;
        }
    }
}
