using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionCompte.Models
{
    public class Client
    {
         public int ClientID { get; set; }
        public string nomClient { get; set; }
        public string adresseClient { get; set; }
        public virtual ICollection<Compte> comptes { get; set; }
    }
}  