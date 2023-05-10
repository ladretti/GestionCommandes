using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCommandes.Core.Models;
public class Commande
{
    public Commande(int? id, string numCommande, Fournisseur fournisseur, DateTime? dateCommande, string designation, string @ref, double? quantiteCommande, Int32? quantiteRecu, Client client, double? numCommande2, string sN, DateTime? dateReception)
    {
        Id = id;
        NumCommande = numCommande;
        Fournisseur = fournisseur;
        DateCommande = dateCommande;
        Designation = designation;
        Ref = @ref;
        QuantiteCommande = quantiteCommande;
        QuantiteRecu = quantiteRecu;
        Client = client;
        NumCommande2 = numCommande2;
        SN = sN;
        DateReception = dateReception;
    }
    public Commande()
    {
    
    }
    public int? Id
    {
        get; set;
    }
    public string NumCommande
    {
        get; set;
    }
    public Fournisseur Fournisseur
    {
        get; set;
    }
    public DateTime? DateCommande
    {
        get; set;
    }
    public string Designation
    {
        get; set;
    }
    public string Ref
    {
        get; set;
    }
    public double? QuantiteCommande
    {
        get; set;
    }
    public Int32? QuantiteRecu
    {
        get; set;
    }
    public Client Client
    {
        get; set;
    }
    public double? NumCommande2
    {
        get; set;
    }
    public string SN
    {
        get; set;
    }
    public DateTime? DateReception
    {
        get; set;
    }
}
