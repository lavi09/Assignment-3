﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace EventCatalogApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PicController : Controller
    {
        private readonly IHostingEnvironment _env;

        public PicController(IHostingEnvironment env)
        {
            _env = env;
        }       

        // GET api/pic/5
        [HttpGet("{id}")]
        public IActionResult GetImage(int id)
        {
            var webroot = _env.WebRootPath;
            var path=Path.Combine(webroot + "/Pics/", "events-" + id + ".png");
            var buffer=System.IO.File.ReadAllBytes(path);
            return File(buffer,"image/png");
        }

    }
}
