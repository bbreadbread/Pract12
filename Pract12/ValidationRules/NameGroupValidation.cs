using Pract12.Data;
using Pract12.Service;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Pract12.ValidationRules
{
    class NameGroupValidation : ValidationRule
    {
        public InterestGroupService service { get; set; } = new();
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var input = (value ?? "").ToString().Trim();

            foreach (InterestGroup group in InterestGroupService.InterestGroups)
            {
                if (group.Title == input)
                    return new ValidationResult(false, "Группа интересов с таким наименованием уже существует");
            }
            return ValidationResult.ValidResult;
        }
    }
}
