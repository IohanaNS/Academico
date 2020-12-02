using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Academico.DAOs;
using Academico.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Academico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        // GET: api/<AlunoController>
        [HttpGet]
        public IEnumerable<Aluno> Get()
        {
            return (new AlunoDAO()).RetornarTodos();
        }

        // GET api/<AlunoController>/5
        [HttpGet("{id}")]
        public IActionResult GetId(string id)
        {
            return new ObjectResult((new AlunoDAO()).RetornarPorId(id));
        }

        // POST api/<AlunoController>
        [HttpPost]
        public IActionResult Post([FromBody] Aluno obj)
        {
            try
            {
                if (obj.Id == null)
                    obj.Id = Guid.NewGuid().ToString();

                (new AlunoDAO()).Inserir(obj);
                return CreatedAtAction(nameof(GetId), new { id = obj.Id }, obj);
            }
            catch
            {
                return BadRequest();
            }
        }

        // PUT api/<AlunoController>/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] Aluno obj)
        {
            var dao = new AlunoDAO();
            try
            {
                dao.Alterar(obj);
                return NoContent();
            }
            catch
            {
                if (dao.RetornarPorId(id) == null)
                    return NotFound();

                return BadRequest();
            }
        }

        // DELETE api/<AlunoController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                (new AlunoDAO()).Excluir(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
