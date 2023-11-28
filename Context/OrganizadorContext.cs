using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Trabalho_Em_Grupo_BD.Models;

namespace Trabalho_Em_Grupo_BD.Context
{
    public class OrganizadorContext : DbContext
    {
        public OrganizadorContext(DbContextOptions<OrganizadorContext> options) : base(options)
        {

        }
        public DbSet<Cliente> clientes { get; set; }
        public DbSet<Email> emails { get; set; }
        public DbSet<LivrosEmprestimo> livrosEmprestimos { get; set; }
        public DbSet<Livro> livros { get; set; }
        public DbSet<Autor> autors { get; set; }
        public DbSet<Editora> editoras { get; set; }
        public DbSet<Exemplares> exemplares { get; set; }
        public DbSet<Emprestimo> Emprestimos { get; set; }

       /* protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Emprestimo>()
                .HasKey(e => new { e.codigo, e.Data });
        }*/


    }
}