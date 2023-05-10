using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionCommandes.Core.Models;

namespace GestionCommandes.Core.Contracts.Services;
public interface ICommandeDataService
{
    Task<IEnumerable<Commande>> GetGridDataAsync();
    Task ModifyCommandeAsync(Commande commande);
    Task<IEnumerable<Commande>> RefreshDataAsync();
}
