using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trabalho_Em_Grupo_BD.Models
{
    public class Autor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Codigo { get; set; }
        public string Nome { get; set;}
        public  Email Email{get; set;}

    }
}