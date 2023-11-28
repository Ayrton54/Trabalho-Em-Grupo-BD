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
    public class ClienteController : ControllerBase
    {
        private readonly OrganizadorContext _context;

        public ClienteController(OrganizadorContext context)
        {
            _context = context;
        }

        [HttpGet("{codigo}")]
        public IActionResult ObterPorCodigo(int codigo)
        {
            try
            {
                // Buscando pelo o Id no banco utilizando o EF
                var cliente = _context.clientes
            .Include(c => c.Email) // Inclui os dados do campo 'email'
            .SingleOrDefault(c => c.Codigo == codigo);
                // Verificando se existe o cliente, caso não retornar NotFound,
                if (cliente == null)
                    return NotFound();
                // Se o cliente existe retorna os dados
                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro a busca no banco {ex.Message}");
            }
        }

        [HttpGet("ListarDadosClientes")]
        public IActionResult ObterTodos()
        {
            try
            {
                //Bucando o cliente pelos filtros feito no select
                var clientes = _context.clientes
            .Select(c => new
            {
                Codigo = c.Codigo,
                Nome = c.Nome,
                Email = c.Email
            })
            .ToList();
                // se tudo ocorrer bem retorna os dados
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro na busca do banco {ex.Message}");
            }
        }
        [HttpGet("ListarTodosDadosClientes")]
        public IActionResult ObterTodosDados()
        {
            try
            {
                var clientes = _context.clientes.Include(c => c.Email);
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro a busca no banco {ex.Message}");
            }
        }
        [HttpPost("Inserir")]
        public IActionResult Criar(Cliente cliente)
        {
            try
            {

                // Adicionar a tarefa recebida no EF e salvar as mudanças (save changes)
                _context.Add(cliente);
                _context.SaveChanges();
                // Criação do cliente com o apontamento da nova uri criada
                return CreatedAtAction(nameof(ObterPorCodigo), new { codigo = cliente.Codigo }, cliente);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao tentar adicionar no banco {ex.Message}");
            }
        }

        [HttpGet("BuscarPorBairro")]
        public IActionResult BuscarPorBairro(string bairro)
        {
            try
            {
                //buscando o cliente no banco de dados pelo bairro passado
                var cliente = _context.clientes.Include(c => c.Email).Where(c => c.Bairro == bairro)
            .ToList();
                //var cliente = _context.clientes.Where(c => c.Bairro.Contains(bairro)).ToList();
                //Verificando se o cliente existe
                if (cliente == null)
                    return NotFound();
                // retorno do cliente encontrado
                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao tentar acha o cliente pelo bairro{ex.Message}");
            }
        }
        [HttpDelete("{codigo}")]
        public IActionResult Deletar(int codigo)
        {
            try
            {
                //buscando o cliente pelo id fornecido
                var exluir = _context.clientes.Find(codigo);
                //verificando se existe o cliente 
                if (exluir == null)
                    return NotFound();
                // removendo o cliente e salvando a alteração
                _context.clientes.Remove(exluir);
                _context.SaveChanges();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao tentar deletar o cliente {ex.Message}");
            }
        }

        [HttpPut("Atualizar/{codigo}")]
        public IActionResult Atualizar(int codigo, Cliente cliente)
        {
            try
            {
                //Buscando o cliente existente pelo filtro do id
                var clienteExistente = _context.clientes.FirstOrDefault(c => c.Codigo == codigo);

                if (clienteExistente == null)
                {
                    return NotFound();
                }

                // injetendo as atualizações

                clienteExistente.Codigo = codigo;
                clienteExistente.Nome = cliente.Nome;
                clienteExistente.Rg = cliente.Rg;
                clienteExistente.Rua = cliente.Rua;
                clienteExistente.Cep = cliente.Cep;
                clienteExistente.Bairro = cliente.Bairro;
                clienteExistente.Email = cliente.Email;

                // executando as atualizações
                _context.clientes.Update(clienteExistente);

                // Salvar as alterações
                _context.SaveChanges();
                return Ok(clienteExistente);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao tentar atualizar o cliente {ex.Message}");
            }

        }

    }
}