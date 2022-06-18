using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Faze.Examples.Gallery.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GalleryImageController : ControllerBase
    {
        private readonly ILogger<GalleryImageController> _logger;

        public GalleryImageController(ILogger<GalleryImageController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<GalleryImage> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new GalleryImage
            {
                Width = 100,
                Height = 100,
                Color = "FF0000"
            })
            .ToArray();
        }
    }
}
