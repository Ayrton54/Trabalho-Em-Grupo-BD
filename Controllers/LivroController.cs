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
    public class LivroController : ControllerBase
    {
        private readonly OrganizadorContext _context;


        public LivroController(OrganizadorContext context)
        {
            _context = context;
        }

        [HttpPost("Inserir")]
        public IActionResult Criar(Livro livro)
        {
            try
            {
                //  dicionando o Livro recebido no EF e salvar as mudanças (save changes)
                _context.Add(livro);
                _context.SaveChanges();
                return CreatedAtAction(nameof(ObterPorCodigo), new { ISBN = livro.ISBN }, livro);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro na tentativa de Criar o livro: {ex.Message}");
            }

        }
        [HttpGet("{ISBN}")]
        public IActionResult ObterPorCodigo(int ISBN)
        {
            try
            {
                var livro = _context.livros
           .Where(l => l.ISBN == ISBN)
           .Select(l => new
           {
               l.ISBN,
               l.Edicao,
               l.Custo,
               l.Titulo,
               Autor = new
               {

                   l.autor.Nome,

               },
               Editora = new
               {

                   l.editora.Nome
               }
           })
           .FirstOrDefault();

                if (livro == null)
                    return NotFound();

                return Ok(livro);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro na tentativa de pegar os livros: {ex.Message}");
            }
        }


        [HttpGet("MediaPrecoLivros")]
        public IActionResult MediaPrecoLivros()
        {
            try
            {
                //Ultilização do Average, para pegar o preço dos livros passados
                var mediaPreco = _context.livros.Average(l => l.Custo);

                return Ok(new { MediaPreco = mediaPreco });
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro na tentativa de listar o preço medio: {ex.Message}");
            }
        }

        [HttpGet("NomeAutorLivro")]
        public IActionResult NomeLivroAutor(String nome)
        {
            try
            {
                // Buscando o nome do autor pela consulta do titulo do livro
                var livro = _context.livros.Where(l => l.Titulo == nome).Select(l => new
                {
                    l.Titulo,
                    Autor = new
                    {

                        l.autor.Nome, //Retorando o nome do autor

                    }
                });
                if (livro == null)
                    return NotFound();
                return Ok(livro);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro na tentativa de listar o nome do autor do livro: {ex.Message}");
            }
        }


        [HttpGet("NomeEditoraLivro")]
        public IActionResult NomeLivroEditora(String nome)
        {
            try
            {
                var livro = _context.livros.Where(l => l.Titulo == nome).Select(l => new
                {
                    l.Titulo,
                    Editora = new
                    {

                        l.editora.Nome,

                    }
                });
                if (livro == null)
                    return NotFound();
                return Ok(livro);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro na tentativa de listar o nome da editora do livro: {ex.Message}");
            }
        }

        [HttpPut("Atualizar")]
        public IActionResult Atualizar(int ISBN, Livro NovoLivro)
        {
            try
            {
                var LivroExistente = _context.livros.FirstOrDefault(l => l.ISBN == ISBN);
                if (LivroExistente == null)
                    return NotFound();

                LivroExistente.Edicao = NovoLivro.Edicao;
                LivroExistente.Custo = NovoLivro.Custo;
                LivroExistente.Titulo = NovoLivro.Titulo;
                //LivroExistente.autor = NovoLivro.autor;
                //  LivroExistente.editora = NovoLivro.editora;

                _context.livros.Update(LivroExistente);

                _context.SaveChanges();

                return Ok(LivroExistente);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro na tentativa de atualizar os dados do livro: {ex.Message}");
            }
        }

        [HttpDelete("Deletar{id}")]
        public IActionResult DeletarLivro(int id)
        {
            try
            {
                var excluir = _context.livros.Find(id);
                if (excluir == null)
                    return NotFound();

                _context.livros.Remove(excluir);

                _context.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro na tentativa de deletar o livro: {ex.Message}");
            }
        }

    }
}