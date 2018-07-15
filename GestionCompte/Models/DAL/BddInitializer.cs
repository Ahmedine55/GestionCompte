using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GestionCompte.Models.DAL
{
    public class BddInitializer : DropCreateDatabaseAlways<GcomptesContext>
    {
        protected override void Seed(GcomptesContext context)
        {
            var clients = new List<Client>
            {
                new Client{nomClient="Alex",adresseClient="lille"},
                new Client{nomClient="Paul",adresseClient="roncq"},
                new Client{nomClient="Jean",adresseClient="villeneuve d'asq"},
            };
            clients.ForEach(s => context.Clients.Add(s));
            context.SaveChanges();

            var Comptes = new List<Compte>
            {
                new Compte{ClientID=1,dateCreation=DateTime.Parse("01-01-2018"),solde=150000},
                new Compte{ClientID=2,dateCreation=DateTime.Parse("03-11-2017"),solde=450000},
                new Compte{ClientID=3,dateCreation=DateTime.Parse("01-04-2015"),solde=70000},
            };

            Comptes.ForEach(s => context.Comptes.Add(s));
            context.SaveChanges();
        }
    }
}