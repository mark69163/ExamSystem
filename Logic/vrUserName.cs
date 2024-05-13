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

            return new ValidationResult(true, null);

        }
    }
}
