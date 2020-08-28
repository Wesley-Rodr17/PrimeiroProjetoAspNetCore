using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeiroProjeto.Models.Entidades
{
    [Table("Usuario")]
    public class Usuario
    {
        [Display(Description = "Código") ]
        public int Id { get; set; }

        [Display(Description = "Nome do usuario")]
        public string NomeUsuario { get; set; }

        [Display(Description = "Idade")]
        public int Idade { get; set; }

        [Display(Description = "Tipo de usuario")]
        public int Tipo { get; set; }
        
    }
}
