using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trabalho_Em_Grupo_BD.Context;
using Trabalho_Em_Grupo_BD.Models;

namespace Trabalho_Em_Grupo_BD.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AutorController : ControllerBase
    {
        private readonly OrganizadorContext _context;

        public AutorController(OrganizadorContext Context)
        {
            _context = Context;
        }
        [HttpPost("Inserir")]
        public IActionResult Criar(Autor Autor)
        {
            try
            {
                // Adicionanando o autor
                _context.Add(Autor);
                //Salvando
                _context.SaveChanges();
                return CreatedAtAction(nameof(ObterPorCodigo), new { codigo = Autor.Codigo }, Autor);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao tentar inserir autor{ex.Message}");
            }

        }

        [HttpGet("Autor/{Codigo}")]
        public IActionResult ObterPorCodigo(int Codigo)
        {
            try
            {
                // Buscando o codigo no banco utilizando o EF
                var Autor = _context.autors
            .Include(c => c.Email) // Inclui os dados do campo 'email'
            .SingleOrDefault(a => a.Codigo == Codigo);
                //  Validando o tipo de retorno. Se não encontrar a autor, retornar NotFound,
                if (Autor == null)
                    return NotFound($"Autor com o codigo: {Codigo}, Não foi encontrado");
                // caso contrário retornar OK com a autor encontrado
                return Ok(Autor);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao tentar buscar o codigo no banco {ex.Message}");
            }
        }
        [HttpGet("ListaEmailsAutor")]
        public IActionResult ListaEmailsAutor(string Nome)
        {
            try
            {
                //Buscando no banco com um filtro para nome e email ligado a autor
                var Autor = _context.autors.Include(c => c.Email).Where(c => c.Nome == Nome)
            .ToList();
                //Verificando se o autor existe
                if (Autor == null)
                    return NotFound($"Autor com o nome: {Nome}, Não foi encontrado");
                // retorno do autor encontrado
                return Ok(Autor);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro na busca: {ex.Message}");
            }
        }

        [HttpGet("ListarTodosDadosAutores")]
        public IActionResult ObterTodosDados()
        {
            try
            {   //Buscando no banco com um filtro a autor
                var Autor = _context.autors.Include(c => c.Email);

                return Ok(Autor);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro na tentativa de busca: {ex.Message}");
            }
        }


        [HttpDelete("DeleteAutor")]
        public IActionResult DeleteAutor(int Codigo)
        {
            try
            {
                ///Buscando no banco com um filtro para codigo e email ligado a autor
                var Autor = _context.autors.Include(a => a.Email).SingleOrDefault(a => a.Codigo == Codigo);

                // Verifica se o autor foi encontrado
                if (Autor == null)
                {
                    return NotFound($"autor com o codigo: {Codigo} não foi encontrado " +
                    "para ser excluido"); // Retorna 404 se o autor não foi encontrado
                }

                // Remove o autor do contexto e do banco de dados
                _context.autors.Remove(Autor);
                _context.SaveChanges(); // Salva as alterações no banco de dados

                return NoContent(); // Retorna o autor excluído
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro na tentiva de exclusão de autor:{ex.Message}");
            }
        }

        [HttpPut("AtualizarAutor/{Id}")]
        public IActionResult AtualizarAutor(int Id, Autor Autor)
        {
            try
            {
                // buscando o autor pelo id fornecido
                var Autores = _context.autors.Find(Id);

                if (Autores == null)
                    return NotFound($"autor com o codigo: {Id} não foi encontrado ");

                // Atualizar as informações da variável autorBanco com o autor recebido via parâmetro
                Autores.Nome = Autor.Nome;
                Autores.Email = Autor.Email;

                // Atualizar a variável autorBanco no EF e salvar as mudanças (save changes)
                _context.autors.Update(Autores);
                _context.SaveChanges();

                return Ok(Autores);
            }

            catch (Exception ex)
            {
                return BadRequest($"Error na tentativa de atualização: {ex.Message}");
            }
        }


    }
}