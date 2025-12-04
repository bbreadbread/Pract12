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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public UsersService service { get; set; } = new();
        public User? user { get; set; } = null;

        public MainPage()
        {
            InitializeComponent();
        }

        public void go_form(object sender, EventArgs e)
        {
            NavigationService.Navigate(new UserFormPage());
        }

        public void go_role(object sender, EventArgs e)
        {
            NavigationService.Navigate(new GroupList());
        }

        //ДВОЙНОЙ КЛИК  
        public void Edit(object sender, EventArgs e)
        {
            if (user == null)
            {
                MessageBox.Show("Выберите элемент из списка!");
                return;
            }
            NavigationService.Navigate(new UserFormPage(user));
        }
        //

        public void remove(object sender, EventArgs e)
        {
            if (user == null)
            {
                MessageBox.Show("Выберите элемент из списка!");
                return;
            }
            if (MessageBox.Show("Вы действительно хотите удалить запись?", "Удалить?",
            MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                service.Remove(user);
            }
        }

        public void editProfile(object sender, EventArgs e)
        {
            if (user == null)
            {
                MessageBox.Show("Выберите элемент из списка!");
                return;
            }
            NavigationService.Navigate(new ProfilePage(user));
        }

        public void go_list_group(object sender, EventArgs e)
        {
            NavigationService.Navigate(new InterestGroupListPage());
        }

        public void go_user_group(object sender, EventArgs e)
        {
            if (user == null)
            {
                MessageBox.Show("Выберите элемент из списка!");
                return;
            }
            NavigationService.Navigate(new UserInterestGroupPage(user));
        }

    }
}
