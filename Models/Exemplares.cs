using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trabalho_Em_Grupo_BD.Models
{
    public class Exemplares
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Codigo { get; set; }

        [Required]
        public DateTime DataAquisicao { get; set; }

        // Relação 1 para 1 com Livro
        public int LivroId { get; set; }
        [ForeignKey("LivroId")]
        public Livro Livro { get; set; }

    }
}