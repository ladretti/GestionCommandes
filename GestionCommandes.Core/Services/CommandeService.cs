using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionCommandes.Core.Contracts.Services;
using GestionCommandes.Core.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GestionCommandes.Core.Services;
public class CommandeService : ICommandeDataService
{
    private List<Commande> _allCommandes;
    private const string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Z:\\Commandes Fournisseurs\\Gest Com_be.accdb;";
    public async Task<IEnumerable<Commande>> GetGridDataAsync()
    {
        if (_allCommandes == null)
        {
            _allCommandes = new List<Commande>(AllCommandes());
        }

        await Task.CompletedTask;
        return _allCommandes;
    }
    public async Task ModifyCommandeAsync(Commande commande)
    {
        ModifyCommande(commande);
        await Task.CompletedTask;
    }
    public Task<IEnumerable<Commande>> RefreshDataAsync()
    {
        return null;
    }
    private static IEnumerable<Commande> AllCommandes()
    {

        List<Commande> listCommande = new List<Commande>();
        using (OleDbConnection connection = new OleDbConnection(connectionString))
        {
            connection.Open();

            var query = "SELECT * FROM Commandes";
            using (OleDbCommand command = new OleDbCommand(query, connection))
            {
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int? id = null;
                        string numCommande = null;
                        string nameFournisseur = null;
                        DateTime? dateCommande = null;
                        string designation = null;
                        string reference = null;
                        double? quantite = null;
                        Int32? quantiteRecu = null;
                        string nameClient = null;
                        double? numCommande2 = null;
                        string serialNumber = null;
                        DateTime? dateReception = null;

                        // Récupérer les valeurs des colonnes de la ligne courante
                        if (reader["N° Enreg"] != DBNull.Value)
                            id = (int)reader["N° Enreg"];
                        if (reader["N° de cde"] != DBNull.Value)
                            numCommande = (string)reader["N° de cde"];
                        if (reader["FOURN"] != DBNull.Value)
                            nameFournisseur = (string)reader["FOURN"];
                        if (reader["DATE"] != DBNull.Value)
                            dateCommande = (DateTime)reader["DATE"];
                        if (reader["designation"] != DBNull.Value)
                            designation = (string)reader["designation"];
                        if (reader["REF"] != DBNull.Value)
                            reference = (string)reader["REF"];
                        if (reader["QTE"] != DBNull.Value)
                            quantite = (double)reader["QTE"];
                        if (reader["Qté Reçue"] != DBNull.Value)
                            quantiteRecu = (Int32)reader["Qté Reçue"];
                        if (reader["Client"] != DBNull.Value)
                            nameClient = (string)reader["Client"];
                        if (reader["N° cde"] != DBNull.Value)
                            numCommande2 = (double)reader["N° cde"];
                        if (reader["SN"] != DBNull.Value)
                            serialNumber = (string)reader["SN"];
                        if (reader["Date Réception"] != DBNull.Value)
                            dateReception = (DateTime)reader["Date Réception"];

                        listCommande.Add(new Commande(id, numCommande, new Fournisseur(nameFournisseur), dateCommande, designation, reference, quantite, quantiteRecu, new Client(nameClient), numCommande2, serialNumber, dateReception));
                    }
                }
            }
        }

        return listCommande;
    }
    private void ModifyCommande(Commande commande)
    {
        using (OleDbConnection connection = new OleDbConnection(connectionString))
        {
            try
            {
                // Requête d'insertion SQL avec des paramètres
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"UPDATE commandes SET [Date Réception] = '" + commande.DateReception + "' ," +
                                                        " [Qté Reçue] ='" + commande.QuantiteRecu + "'," +
                                                        " SN='" + commande.SN + "' " +
                                                        " WHERE Commandes.[N° Enreg] = " + commande.Id;
                cmd.Connection = connection;
                connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ArgumentException("" + ex.Message);
            }
        }

    }
}
