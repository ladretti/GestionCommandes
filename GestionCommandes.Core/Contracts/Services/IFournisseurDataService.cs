using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionCommandes.Core.Models;

namespace GestionCommandes.Core.Contracts.Services;
public interface IFournisseurDataService
{
    Task<IEnumerable<Fournisseur>> GetGridDataAsync();
    Task<IEnumerable<Fournisseur>> RefreshDataAsync();
}
