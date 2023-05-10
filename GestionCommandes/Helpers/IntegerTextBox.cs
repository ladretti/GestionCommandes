using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;

namespace GestionCommandes.Helpers;
public class IntegerTextBox : TextBox
{
    public IntegerTextBox() : base()
    {
        this.Text = ""; // Définir une valeur par défaut pour éviter les exceptions lors de la conversion
        this.TextChanged += new TextChangedEventHandler(IntegerTextBox_TextChanged);
    }

    private void IntegerTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        bool ok = int.TryParse(this.Text, out int result);
        if (result > 0)
        {
            if (result > 200)
            {
                result = 200;
                this.Text = "200";
            }
        }
     
        if (!ok && !string.IsNullOrWhiteSpace(this.Text)) // Vérifier si le texte peut être converti en int
        {
            this.Text = ""; // Réinitialiser à 0 si la conversion échoue
        }
        
    }
}

