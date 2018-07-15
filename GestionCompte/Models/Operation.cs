using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionCompte.Models
{
    public enum TypesOperation { Retrait, Versement };
    public class Operation
    {
        public int OperationID { get; set; }
        public TypesOperation typeOperation { get; set; }
        public int CompteID { get; set; }
        public DateTime dateOperation { get; set; }
        public double montant { get; set; }
        public virtual Compte Compte { get; set; }
    }
}