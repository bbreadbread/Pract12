using Microsoft.IdentityModel.Tokens;
using Pract12.Data;
using Pract12.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pract12.Pages
{
    /// <summary>
    /// Логика взаимодействия для UserFormPage.xaml
    /// </summary>
    public partial class UserFormPage : Page
    {
        private UsersService _service = new();
        public User _user = new();
        private User _originalUser = null;
        bool isEdit = false;
        public UserFormPage(User? _editUser = null)
        {
            InitializeComponent();
            if (_editUser != null)
            {
                _originalUser = _editUser;
                _user = new User
                {
                    Id = _editUser.Id,
                    Login = _editUser.Login,
                    Name = _editUser.Name,
                    Email = _editUser.Email,
                    Password = _editUser.Password,
                    CreatedAt = _editUser.CreatedAt,
                    RoleId = _editUser.RoleId,
                    Role = _editUser.Role,
                    UserProfile = _editUser.UserProfile
                };
                isEdit = true;
            }
            DataContext = _user;
        }
        private void save(object sender, RoutedEventArgs e)
        {
            if (!(String.IsNullOrEmpty(_user.Login) && String.IsNullOrEmpty(_user.Name) && String.IsNullOrEmpty(_user.Email) && String.IsNullOrEmpty(_user.Password)))
            {
                if (isEdit)
                {
                    _originalUser.Login = _user.Login;
                    _originalUser.Name = _user.Name;
                    _originalUser.Email = _user.Email;
                    _originalUser.Password = _user.Password;
                    _originalUser.RoleId = _user.RoleId;
                    _originalUser.Role = _user.Role;

                    _service.Commit();
                }
                else
                {
                    _user.CreatedAt = DateTime.Now;
                    _service.Add(_user);
                }
                NavigationService.GoBack();
            }
            else MessageBox.Show("Заполните все поля");
        }
        private void back(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
