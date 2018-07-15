using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionCompte.Models
{
    public class Roles
    {
        public int RolesID { get; set; }
        public String NomRole { get; set; }
        public virtual ICollection<Users> Users { get; set; }
    }
}