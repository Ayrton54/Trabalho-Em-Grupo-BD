using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trabalho_Em_Grupo_BD.Models
{
    public class Cliente
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Codigo { get; set; }
        public String Nome { get; set; }
        public String Rg { get; set; }
        public String Rua { get; set; }
        public String Cep { get; set; }
        public String Bairro { get; set; }
        public Email Email { get; set; }


    }
}