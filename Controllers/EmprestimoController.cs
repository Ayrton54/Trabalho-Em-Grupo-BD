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
    public class EmprestimoController : ControllerBase
    {
        private readonly OrganizadorContext _context;
        public EmprestimoController(OrganizadorContext context)
        {
            _context = context;
        }

        [HttpPost("Inserir")]
        public IActionResult InserirEmprestimo(Emprestimo emprestimo)
        {
            try
            {
                _context.Add(emprestimo);
                _context.SaveChanges();
                return CreatedAtAction(nameof(ObterPorCodigoEmprestimo), new { codigo = emprestimo.codigo }, emprestimo);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro a inserir emprestimo {ex.Message}");
            }
        }
        [HttpGet("{codigo}")]
        public IActionResult ObterPorCodigoEmprestimo(int codigo)
        {
            try
            {
                //fazendo a busca no banco pelo filtros sql
                var emprestimo = _context.Emprestimos.Where(e => e.codigo == codigo)
                .Select(e => new
                {
                    Id = e.codigo,
                    Data = e.Data.Date,
                    NomeCliente = e.cliente.Nome,
                    NomeLivro = e.livrosEmprestimo.Titulo,
                    EmailCliente = e.cliente.Email
                }).ToList();//busancando por toda lista


                if (emprestimo == null || emprestimo.Count == 0)
                    return NotFound();
                return Ok(emprestimo);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro na tentativa de busca: {ex.Message}");
            }
        }


        [HttpGet("Emprestimo/{data}")]
        public IActionResult ObterEmprestimosPorData(DateTime data)
        {
            try
            {//fazendo filtro pela data e no select colocando o que ira retorna no json
                var emprestimos = _context.Emprestimos
           .Where(e => e.Data.Date == data.Date)
           .Select(e => new
           {
               Id = e.codigo,
               Data = e.Data,

               NomeCliente = e.cliente.Nome,
               NomeLivro = e.livrosEmprestimo.Titulo
           })
           .ToList();

                return Ok(emprestimos);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro na tentativa de busca por data: {ex.Message}");
            }
        }

        [HttpDelete("{codigo}")]
        public IActionResult Deletar(int codigo)
        {
            try
            {
                // Recuperar a instância de Emprestimo usando apenas o código
                var emprestimoExistente = _context.Emprestimos.FirstOrDefault(e => e.codigo == codigo);
                ;

                if (emprestimoExistente == null)
                {
                    return NotFound();
                }

                // Remover a instância do contexto
                _context.Emprestimos.Remove(emprestimoExistente);

                // Salvar as alterações no banco de dados
                _context.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Falha ao excluir: {ex.Message}");
            }
        }


    }


}
