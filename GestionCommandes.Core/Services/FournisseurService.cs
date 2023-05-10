using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionCommandes.Core.Contracts.Services;
using GestionCommandes.Core.Models;

namespace GestionCommandes.Core.Services;
public  class FournisseurService : IFournisseurDataService
{
    private List<Fournisseur> _allFournisseurs;
    private const string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Z:\\Commandes Fournisseurs\\Gest Com_be.accdb;";

    private static IEnumerable<Fournisseur> AllFournisseurs()
    {

        List<Fournisseur> listCommerciaux = new List<Fournisseur>();
        using (OleDbConnection connection = new OleDbConnection(connectionString))
        {
            connection.Open();

            var query = "SELECT * FROM Fournisseur";
            using (OleDbCommand command = new OleDbCommand(query, connection))
            {
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Récupérer les valeurs des colonnes de la ligne courante
                        int id = (int)reader["N° Four"];
                        string name = (string)reader["Fournisseur"];

                        listCommerciaux.Add(new Fournisseur(id, name));
                    }
                }
            }
        }

        return listCommerciaux;
    }

    public async Task<IEnumerable<Fournisseur>> GetGridDataAsync()
    {
        if (_allFournisseurs == null)
        {
            _allFournisseurs = new List<Fournisseur>(AllFournisseurs());
        }

        await Task.CompletedTask;
        return _allFournisseurs;
    }
    public async Task<IEnumerable<Fournisseur>> RefreshDataAsync()
    {
        if (_allFournisseurs != null)
        {
            _allFournisseurs.Clear();
            _allFournisseurs = new List<Fournisseur>(AllFournisseurs());
        }

        await Task.CompletedTask;
        return _allFournisseurs;
    }
}
