using Faze.Examples.Gallery.API.Data;
using Faze.Examples.Gallery.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Faze.Examples.Gallery.API.Controllers
{
    [ApiController]
    [Route("visualisations")]
    public class VisualisationsController : ControllerBase
    {
        private readonly ILogger<VisualisationsController> logger;
        private readonly IVisualisationRepository repository;

        public VisualisationsController(ILogger<VisualisationsController> logger, IVisualisationRepository repository)
        {
            this.logger = logger;
            this.repository = repository;
        }

        [HttpGet]
        public Task<IEnumerable<Visualisation>> Get()
        {
            return repository.GetAllAsync();
        }
    }
}
