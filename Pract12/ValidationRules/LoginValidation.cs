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
    public class LoginValidation : ValidationRule
    {
        public UsersService service { get; set; } = new();
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var input = (value ?? "").ToString().Trim().ToLower();
            if (input == string.Empty)
            {
                return new ValidationResult(false, "Ввод информации в поле обязателен");
            }
            if (input.Length <= 5)
            {
                return new ValidationResult(false, "Должно быть больше пяти символов");
            }
            foreach (User user in service.Users)
            {
                if (user.Login == input)
                    return new ValidationResult(false, "Такой логин уже существует");
            }
            return ValidationResult.ValidResult;
        }

    }
}
