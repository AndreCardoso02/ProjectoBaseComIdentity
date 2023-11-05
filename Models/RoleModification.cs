using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectoBaseComIdentity.Models
{
    public class RoleModification
    {
        [Required]
        public string RoleName { get; set; }
        public Guid RoleId { get; set; }
        public string[]? AddIds { get; set; }
        public string[]? DeleteIds { get; set; }
    }
}