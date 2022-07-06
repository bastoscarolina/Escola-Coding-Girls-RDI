using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoFinal.Models;
using ProjetoFinal.Contexts;


namespace ProjetoFinal.Controllers
{
    
        [Route("api/[controller]")]
        [ApiController]
        public class AlunoController : ControllerBase
        {
            private readonly EscolaContext _context;

            public AlunoController(EscolaContext context)
            {
                _context = context;
            }

            // GET: api/Aluno
            [HttpGet]
            public async Task<JsonResult> GetAlunos()
            {
                if (_context.Aluno == null)
                {
                    return new JsonResult("Ainda não há alunos cadastrados.");
                }
              
                List<Aluno> aluno =  await _context.Aluno.ToListAsync();
                
                return new JsonResult(new {
                    AlunosAtivos = aluno.FindAll(e => e.Turma.Ativo == true),
                });
            }

            // GET: api/Aluno/5
            [HttpGet("{id}")]
            public async Task<ActionResult<Aluno>> GetAluno(int id)
            {
                if (_context.Aluno == null)
                {
                    return NotFound();
                }
                var aluno = await _context.Aluno.FindAsync(id);

                if (aluno == null)
                {
                    return NotFound();
                }

                return aluno;
            }

            // PUT: api/Aluno/5
            // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
            [HttpPut("{id}")]
            public async Task<IActionResult> PutAluno(int id, Aluno aluno)
            {
                if ( id != aluno.ID)
                {
                    return BadRequest();
                }
                _context.Entry(aluno).State = EntityState.Modified;

                var turma = await _context.Turma.FindAsync(aluno.TurmaID);

                try
                {
                   
                    if ( TurmaExists(aluno.TurmaID) && turma.Ativo)
                    {
                       await _context.SaveChangesAsync();
                    }
                    else
                    {
                        throw new Exception("Não é possível mudar o aluno para esta turma pois ela não existe ou está inativa");
                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    
                    if (!AlunoExists(aluno.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return NoContent();
            }

       
            

        // POST: api/Aluno
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
            public async Task<ActionResult<Aluno>> PostAluno(Aluno aluno)
            {
                if (_context.Aluno == null)
                {
                    return Problem("Entity set 'EscolaContext.Alunos'  is null.");
                }
                if (aluno.TurmaID > 0 && TurmaExists(aluno.TurmaID))
                {
                    _context.Aluno.Add(aluno);
                    await _context.SaveChangesAsync();

                    return CreatedAtAction("GetAluno", new { id = aluno.ID }, aluno);
                }
                else
                {
                    throw new Exception("Só é possível cadastrar um aluno com uma turma válida.");
                }
                
            }

            // DELETE: api/Aluno/5
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteAluno(int id)
            {
                if (_context.Aluno == null)
                {
                    return NotFound();
                }
                var aluno = await _context.Aluno.FindAsync(id);
                if (aluno == null)
                {
                    return NotFound();
                }

                _context.Aluno.Remove(aluno);
                await _context.SaveChangesAsync();

                return NoContent();
            }

            private bool AlunoExists(int id)
            {
                return (_context.Aluno?.Any(e => e.ID == id)).GetValueOrDefault();
            }
            private bool TurmaExists(int id)
            {
                return (_context.Turma?.Any(e => e.ID == id)).GetValueOrDefault();
            }
    }

    
}
