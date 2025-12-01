using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Pract12.ValidationRules
{
    public class DateValidation : ValidationRule
    {

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var input = (value ?? "").ToString().Trim();

            if (string.IsNullOrEmpty(input))
            {
                return new ValidationResult(false, "Ввод даты в поле обязателен");
            }

            CultureInfo culture = cultureInfo ?? CultureInfo.CurrentCulture;

            if (!DateTime.TryParseExact(input, "dd.MM.yyyy", culture, DateTimeStyles.None, out DateTime result))
            {
                return new ValidationResult(false, "Введите корректную дату");
            }
            else
            {
                if (result > DateTime.Now)
                {
                    return new ValidationResult(false, "Дата не может быть в будущем");
                }

            }

            return ValidationResult.ValidResult;
        }

    }
}
