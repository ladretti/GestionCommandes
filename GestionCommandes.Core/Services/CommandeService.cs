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
    public async Task<IEnumerable<Commande>> GetGridDataAsync(bool ok)
    {
        if (_allCommandes == null)
        {
            _allCommandes = new List<Commande>(AllCommandes());
        }
        if (ok)
        {
            GetExcel();
            _allCommandes = new List<Commande>(AllCommandes());
        }
        await Task.CompletedTask;
        return _allCommandes;
    }
    public async Task GetExcelAsync()
    {
        GetExcel();
        await Task.CompletedTask;
    }
    public async Task ModifyCommandeAsync(Commande commande)
    {
        ModifyCommande(commande);
        await Task.CompletedTask;
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
            connection.Close();
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

    private void GetExcel()
    {
        var connectionString2 = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = Z:\\Technique\\Atelier\\RECAPCDE.xlsx; Extended Properties = 'Excel 12.0 Xml;HDR=YES;'";


        using (OleDbConnection connection = new OleDbConnection(connectionString2))
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand("SELECT * FROM [Commande a faire$] WHERE [DATE] >= #" + _allCommandes.OrderByDescending(c => c.DateCommande).FirstOrDefault()?.DateCommande?.ToString("MM/dd/yyyy") + "#", connection);
            using (OleDbDataReader reader = command.ExecuteReader())
            {
                var records = new List<Dictionary<string, object>>();
                while (reader.Read())
                {
                    // Récupération des données de chaque colonne

                    string numCommande = reader["N° de cde"] != DBNull.Value ? reader["N° de cde"].ToString() : null;
                    string designation = reader["designation "] != DBNull.Value ? reader["designation "].ToString() : null;
                    string reference = reader["REF"] != DBNull.Value ? reader["REF"].ToString() : null;
                    string fourn = reader["FOURN"] != DBNull.Value ? reader["FOURN"].ToString() : null;
                    double? quantiteCommande = reader["QTE"] != DBNull.Value ? Convert.ToDouble(reader["QTE"]) : (double?)null;
                    string statut = reader["statut"] != DBNull.Value ? reader["statut"].ToString() : null;
                    double? numCommande2 = reader["N° cde "] != DBNull.Value ? Convert.ToDouble(reader["N° cde "]) : null;
                    DateTime? dateCommande = reader["DATE"] != DBNull.Value ? Convert.ToDateTime(reader["DATE"]) : (DateTime?)null;
                    string client = reader["Client"] != DBNull.Value ? reader["Client"].ToString() : null;
                    double? prixUnitaire = reader["PU € HT"] != DBNull.Value ? Convert.ToDouble(reader["PU € HT"]) : (double?)null;


                    // Création d'une instance de la classe Commande et stockage des données
                    Commande commande = new Commande(null, numCommande, new(fourn), dateCommande, designation, reference, quantiteCommande, 0, new Client(client), numCommande2, null, null);

                    if (!_allCommandes.Where(e => e.DateCommande == commande.DateCommande).Any(c => c.NumCommande2 == commande.NumCommande2 && c.Designation == commande.Designation))
                        InsertCommande(commande);
                }

            }
            connection.Close();
        }

    }
    private void InsertCommande(Commande commande)
    {
        using (OleDbConnection connection = new OleDbConnection(connectionString))
        {
            try
            {
                // Requête d'insertion SQL avec des paramètres
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into Commandes ([N° de cde],[FOURN],[DATE],[designation],[REF],[QTE],[Client],[N° cde]) values (?,?,?,?,?,?,?,?)";

                cmd.Parameters.AddWithValue("@N° de cde", commande.NumCommande ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@FOURN", commande.Fournisseur.Name ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@DATE", commande.DateCommande ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@designation", commande.Designation ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@REF", commande.Ref ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@QTE", commande.QuantiteCommande ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Client", commande.Client.Name ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@N° cde", commande.NumCommande2 ?? (object)DBNull.Value);
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
