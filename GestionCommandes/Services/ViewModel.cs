using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GestionCommandes.Services;
public abstract class ViewModel
{
    public ICommand ValidationCommande;

    public bool IsCommandExecuted;
}
