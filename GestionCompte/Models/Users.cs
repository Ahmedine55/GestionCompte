using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GestionCompte.Models
{
    public class Users
    {
        public int UsersID { get; set; }
        public int RolesID { get; set; }
        [Required]
        public String Username { get; set; }
        [Required]
        public String Password { get; set; }
        public virtual Roles Roles{ get; set; }

    }
}