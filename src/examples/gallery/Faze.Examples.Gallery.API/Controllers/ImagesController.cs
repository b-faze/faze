using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Faze.Examples.Gallery.API.Controllers
{
    [ApiController]
    [Route("images")]
    public class ImagesController : ControllerBase
    {
        private readonly ILogger<ImagesController> _logger;

        public ImagesController(ILogger<ImagesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<GalleryImage> Get()
        {
            return new GalleryImage[]
            {
                new GalleryImage
                {
                    Id = "1",
                    Name = "Faze image 1",
                    Description = "desc...",
                    Width = 100,
                    Height = 100,
                },
                new GalleryImage
                {
                    Id = "2",
                    Name = "Faze default image",
                    Description = "desc 2...",
                    Width = 200,
                    Height = 200,
                }
            };
        }
    }
}
