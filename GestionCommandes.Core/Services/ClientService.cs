using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionCommandes.Core.Contracts.Services;
using GestionCommandes.Core.Models;

namespace GestionCommandes.Core.Services;
public class ClientService : IClientDataService
{
    private List<Client> _allClients;
    private const string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Z:\\Commandes Fournisseurs\\Gest Com_be.accdb;";

    private static IEnumerable<Client> AllClients()
    {

        List<Client> listCommerciaux = new List<Client>();
        using (OleDbConnection connection = new OleDbConnection(connectionString))
        {
            connection.Open();

            var query = "SELECT * FROM Clients";
            using (OleDbCommand command = new OleDbCommand(query, connection))
            {
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Récupérer les valeurs des colonnes de la ligne courante
                        int id = (int)reader["N° Client"];
                        string name = (string)reader["Client"];

                        listCommerciaux.Add(new Client(id, name));
                    }
                }
            }
        }

        return listCommerciaux;
    }

    public async Task<IEnumerable<Client>> GetGridDataAsync()
    {
        if (_allClients == null)
        {
            _allClients = new List<Client>(AllClients());
        }

        await Task.CompletedTask;
        return _allClients;
    }
    public async Task<IEnumerable<Client>> RefreshDataAsync()
    {
        if (_allClients != null)
        {
            _allClients.Clear();
            _allClients = new List<Client>(AllClients());
        }

        await Task.CompletedTask;
        return _allClients;
    }
}
