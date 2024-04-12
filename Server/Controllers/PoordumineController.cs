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
        public ActionResult<IEnumerable<Poordumine>> Get()
        {
            return Ok(poordumised);
        }

        [HttpPost]
        public ActionResult<Poordumine> Post([FromBody] Poordumine poordumine)
        {
            if (poordumine == null)
            {
                return BadRequest();
            }

            poordumine.SisestamiseAeg = DateTime.Now;
            poordumised.Add(poordumine);
            return CreatedAtAction(nameof(Get), new { id = poordumine.Id }, poordumine);
        }

        [HttpDelete("{id}")]
        public IActionResult KustutaPoordumine(int id)
        {
            var poordumine = poordumised.FirstOrDefault(p => p.Id.Equals(id));
            if (poordumine == null)
            {
                return NotFound(); 
            }

            poordumised.Remove(poordumine);
            return Ok(); 
}


        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Poordumine updatedPoordumine)
        {
            if (updatedPoordumine == null || !updatedPoordumine.Id.Equals(id))
            {
                return BadRequest();
            }

            var existingPoordumine = poordumised.Find(p => p.Id.Equals(id));
            if (existingPoordumine == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
