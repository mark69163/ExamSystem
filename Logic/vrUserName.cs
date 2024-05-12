using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ExamSystem;

namespace ExamSystem.Logic
{
    public class vrUserName : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {

            // Valós értéket adott meg a felhasználó?
            if (!double.TryParse((string)value, out double Average))
                return new ValidationResult(false, "Not a number!");
            // A megfelelő szélsőértékek közé esik az érték.
            if (Average < 1 || Average > 5)
                return new ValidationResult(false, "Value out of bounds!");
            // Az érték jó.
            return new ValidationResult(true, null);

        }
    }
}
