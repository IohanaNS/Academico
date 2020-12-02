using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Academico.DAOs;
using Academico.Models;
using Microsoft.AspNetCore.Mvc;

namespace Academico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {
        // GET: api/<ProfessorController>
        [HttpGet]
        public IEnumerable<Professor> Get()
        {
            return (new ProfessorDAO()).SelectAll();
        }

        // GET api/<ProfessorController>/5
        [HttpGet("{id}")]
        public IActionResult GetId(int id)
        {
            return new ObjectResult((new ProfessorDAO()).SelectId(id));
        }

        
        // GET api/<ProfessorController>/nome?nome={nome}
        [HttpGet("nome")]
        public IEnumerable<Professor> GetNome([FromQuery]string nome)
        {
            return (new ProfessorDAO()).SelectNome(nome);
        }

        // POST api/<ProfessorController>
        [HttpPost]
        public IActionResult Post([FromBody] Professor obj)
        {
            try
            {
                (new ProfessorDAO()).Insert(obj);
                return CreatedAtAction(nameof(GetId), new { id = obj.Id }, obj);
            }
            catch
            {
                return BadRequest();
            }
        }

        // PUT api/<ProfessorController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Professor obj)
        {
            var dao = new ProfessorDAO();
            try
            {
                dao.Update(obj);
                return NoContent();
            }
            catch
            {
                if (dao.SelectId(id) == null)
                    return NotFound();

                return BadRequest();
            }
        }

        // DELETE api/<ProfessorController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                (new ProfessorDAO()).Delete(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }

}
