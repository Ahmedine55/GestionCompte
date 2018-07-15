using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionCompte.Models
{
    public class Compte
    {
        public int CompteID { get; set; }
        public int ClientID { get; set; }
        public DateTime dateCreation { get; set; }
        public Double solde { get; set; }
        public virtual Client client { get; set; }
        public virtual ICollection<Operation> operations { get; set; }
    }
}