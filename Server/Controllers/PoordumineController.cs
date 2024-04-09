using Microsoft.AspNetCore.Mvc;
using AgileWorksProjekt.Server.Models; 
using System;
using System.Collections.Generic;

namespace AgileWorksProjekt.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PoordumineController : ControllerBase
    {
        private static List<Poordumine> poordumised = new List<Poordumine>();

        [HttpGet]
        public IEnumerable<Poordumine> Get()
        {
            return poordumised;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Poordumine poordumine)
        {
            poordumine.SisestamiseAeg = DateTime.Now;
            poordumised.Add(poordumine);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id)
        {
            var poordumine = poordumised.Find(p => p.Id == id);
            if (poordumine == null)
            {
                return NotFound();
            }
            poordumine.Lahendatud = true; 
            return Ok();
        }
    }
}