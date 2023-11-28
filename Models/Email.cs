using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Trabalho_Em_Grupo_BD.Models
{
    public class Email
    {
        [Key]
        [EmailAddress(ErrorMessage = "Endereço de email inválido")]
        [Remote(action: "CheckEmail", controller: "Clientes", ErrorMessage = "Este email já está em uso.")]
        public string Emails { get; set; }

    }
}