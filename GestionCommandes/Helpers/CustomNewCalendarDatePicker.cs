using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;

namespace GestionCommandes.Helpers
{
    public class CustomNewCalendarDatePicker : CalendarDatePicker
    {
        public CustomNewCalendarDatePicker()
        {
            DateChanged += NullReadyCalendarDatePicker_DateChanged;
        }

        private void NullReadyCalendarDatePicker_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            // If the new date looks like it should be null, set it to null.
            if (args.NewDate.HasValue && args.NewDate.Value == MinDate)
            {
                Date = null;
            }
        }
    }
}
