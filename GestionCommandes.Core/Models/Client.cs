using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCommandes.Core.Models;
public class Client
{
    public Client()
    {
    }
    public Client(string name)
    {
        Name = name;
    }

    public Client(int id, string name)
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
