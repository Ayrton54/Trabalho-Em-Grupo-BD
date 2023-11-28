using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trabalho_Em_Grupo_BD.Context;
using Trabalho_Em_Grupo_BD.Models;

namespace Trabalho_Em_Grupo_BD.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExemplarController : ControllerBase
    {
        private readonly OrganizadorContext _context;


        public ExemplarController(OrganizadorContext context)
        {
            _context = context;
        }
        [HttpPost("InserirExemplares")]
        public IActionResult CriarExemplar(Exemplares livro)
        {
            try
            {
                // Adicionando os exemplares recebida no EF e salvar as mudanças (save changes)
                _context.Add(livro);
                _context.SaveChanges();
                return CreatedAtAction(nameof(ObterPorCodigoExemplar), new { Codigo = livro.Codigo }, livro);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro na tentativa de criar exemplares: {ex.Message}");
            }

        }
        [HttpGet("Exemplar{Codigo}")]
        public IActionResult ObterPorCodigoExemplar(int codigo)
        {
            try
            {
                //Buscando pelo codigo
                var exemplar = _context.exemplares.Find(codigo);

                if (exemplar == null)
                    return NotFound();

                return Ok(exemplar);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro na busca pelo codigo: {ex.Message}");
            }
        }
        [HttpGet("ListaNomeEDataAquisicao")]
        public IActionResult NomeEDataAquisicao(int codigo)
        {
            try
            {
                //Buscando pelo filtro da data e nome
                var exemplar = _context.exemplares.Where(e => e.Livro.ISBN == codigo).Select(e => new
                {
                    e.Livro.Titulo,
                    Exemplares = new
                    {
                        e.DataAquisicao.Date
                    }
                });
                // Verificando se tem o exemplar no banco
                if (exemplar == null)
                    return NotFound();
                return Ok(exemplar);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro na tentativa de listagem: {ex.Message}");
            }
        }
        [HttpGet("ListaDataAquisicao")]
        public IActionResult DataAquisicao(DateTime data)
        {
            try
            {
                //Buscar pelo Filtro da data passada
                var exemplar = _context.exemplares.Where(e => e.DataAquisicao.Date == data.Date).Select(e => new
                {
                    e.Livro.Titulo,
                    Exemplares = new
                    {
                        e.DataAquisicao.Date
                    }
                }).ToList();
                if (exemplar.Count == 0)
                    return NotFound();
                return Ok(exemplar);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro na tentativa de listagem por data: {ex.Message}");
            }
        }
        [HttpGet("ListarTodosExemplares")]
        public IActionResult ObterTodosDados()
        {
            try
            {
                var exemplares = _context.exemplares.Include(c => c.Livro)
                .Include(c => c.Livro.editora)
                .Include(c => c.Livro.autor);
                return Ok(exemplares);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro na tentativa de listagem: {ex.Message}");
            }
        }

        [HttpDelete("deleteExemplar{codigo}")]
        public IActionResult DeleteExemplar(int codigo)
        {
            try
            {
                //Deletando pelo codigo e incluindo O livro também
                var excluir = _context.exemplares.Include(a => a.Livro).SingleOrDefault(a => a.Codigo == codigo);

                if (excluir == null)
                    return NotFound();

                //Salavando as alterações
                _context.exemplares.Remove(excluir);
                _context.SaveChanges();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro na tentativa de deletar: {ex.Message}");
            }

        }
        [HttpPut("Atualizar/{codigo}")]
        public IActionResult Atualizar(int codigo, Exemplares exemplares)
        {
            try
            {   //buscando pelo codigo no banco
                var ExemplarExistente = _context.exemplares.FirstOrDefault(e => e.Codigo == codigo);

                if (ExemplarExistente == null)
                    return NotFound();

                //Aplicando as atualizações
                ExemplarExistente.DataAquisicao = exemplares.DataAquisicao;
                ExemplarExistente.LivroId = exemplares.LivroId;
                ExemplarExistente.Livro = exemplares.Livro;
                // fazendo o update e salvando
                _context.exemplares.Update(ExemplarExistente);
                _context.SaveChanges();
                return Ok(ExemplarExistente);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro na tentativa de atualização: {ex.Message}");
            }
        }


    }
}