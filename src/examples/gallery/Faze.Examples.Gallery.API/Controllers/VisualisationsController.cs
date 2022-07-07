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
    public class VisualisationsController : ControllerBase
    {
        private readonly ILogger<VisualisationsController> logger;
        private readonly IVisualisationRepository repository;

        public VisualisationsController(ILogger<VisualisationsController> logger, IVisualisationRepository repository)
        {
            this.logger = logger;
            this.repository = repository;
        }

        [HttpGet("visualisations/")]
        public Task<IEnumerable<Visualisation>> GetAll()
        {
            return repository.GetAll();
        }

        [HttpGet("visualisations/{id}")]
        [ProducesResponseType(typeof(Visualisation), statusCode:200)]
        [ProducesResponseType(typeof(string), statusCode:404)]
        public async Task<IActionResult> Get(string id)
        {
            var result = await repository.Get(id);
            if (result == null)
                return NotFound(id);

            return Ok(result);
        }

        [HttpPost("visualisations/")]
        [ProducesResponseType(typeof(Visualisation), statusCode: 200)]
        public async Task<IActionResult> Create(Visualisation vis)
        {
            var result = await repository.Create(vis);

            return Ok(result);
        }

        [HttpPut("visualisations/{id}")]
        [ProducesResponseType(typeof(Visualisation), statusCode: 200)]
        public async Task<IActionResult> Update(string id, Visualisation vis)
        {
            vis.Id = id;
            var result = await repository.Update(vis);

            return Ok(result);
        }

        [HttpDelete("visualisations/{id}")]
        [ProducesResponseType(typeof(string), statusCode: 200)]
        public async Task<IActionResult> Delete(string id)
        {
            await repository.Delete(id);

            return Ok(id);
        }
    }
}
