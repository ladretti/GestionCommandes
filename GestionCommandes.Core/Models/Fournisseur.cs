using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCommandes.Core.Models;
public class Fournisseur
{
    public Fournisseur()
    {
    }
    public Fournisseur(string name)
    {
        Name = name;
    }

    public Fournisseur(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id
    {
        get; set;
    }
    public string Name
    {
        get; set;
    }
}
