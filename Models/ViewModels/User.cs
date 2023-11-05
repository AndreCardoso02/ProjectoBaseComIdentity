using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectoBaseComIdentity.Models.ViewModels
{
    public class User
    {
        [Required]
        [DisplayName("Nome de Utilizador")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [DisplayName("Email")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DisplayName("Palavra-passe")]
        public string Password { get; set; } = string.Empty;
    }
}