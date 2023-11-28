using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trabalho_Em_Grupo_BD.Models
{
    public class Livro
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ISBN { get; set; }
        public int Edicao { get; set; }
        public double Custo { get; set; }
        public string Titulo { get; set; }

        public Autor autor { get; set; }
        public Editora editora {get;set;}

       
    }
}