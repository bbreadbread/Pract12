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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Pract12.Pages
{
    /// <summary>
    /// Логика взаимодействия для EditProfilePage.xaml
    /// </summary>
    public partial class EditProfilePage : Page
    {
        private UsersService _service = new();
        public User _user = new();
        bool isNewProfile = false;
        public EditProfilePage(User? user = null)
        {
            InitializeComponent();

            if (user != null)
            {                
                _user = user;

                if (_user.UserProfile == null)
                {
                    _user.UserProfile = new UserProfile
                    {
                        UserId = _user.Id,
                        User = _user
                    };
                    isNewProfile = true;
                    _service.AddProfile(_user.UserProfile);
                }
            }

            DataContext = _user;
        }

        private void save(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isNewProfile && _user.UserProfile != null)
                {
                    _user.UserProfile.UserId = _user.Id;
                }

                _service.Commit();

                MessageBox.Show("Профиль сохранен успешно!");
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                string errorMessage = $"Ошибка: {ex.Message}";
                if (ex.InnerException != null)
                {
                    errorMessage += $"\nВнутренняя ошибка: {ex.InnerException.Message}";
                }
                MessageBox.Show(errorMessage);
            }
        }
        private void back(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void AvatarAdd(object sender, MouseButtonEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string fileName = openFileDialog.FileName;

                _user.UserProfile.AvatarUrl = fileName;
            }
        }
    }
}
