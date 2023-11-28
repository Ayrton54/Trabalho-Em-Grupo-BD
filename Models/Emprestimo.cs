using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trabalho_Em_Grupo_BD.Models
{
    public class Emprestimo
    {   
         [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int codigo{get;set;}
       [Key]
        public DateTime Data{get; set;}
        public Cliente cliente{get; set;}

        public LivrosEmprestimo livrosEmprestimo {get;set;}
    }
}