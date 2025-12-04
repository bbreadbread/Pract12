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
    /// Логика взаимодействия для InterestGroupListPage.xaml
    /// </summary>
    public partial class InterestGroupListPage : Page
    {
        public InterestGroupService service { get; set; } = new();
        public InterestGroup? current { get; set; } = null;
        public InterestGroupListPage()
        {
            InitializeComponent();
        }

        private void back(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        private void add(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddInterestGroupPage());
        }
        private void edit(object sender, RoutedEventArgs e)
        {
            if (current != null)
            {
                NavigationService.Navigate(new AddInterestGroupPage(current));
            }
            else
            {
                MessageBox.Show("Выберите группу интереса");
            }
        }
        private void remove(object sender, RoutedEventArgs e)
        {
            if (current != null)
            {
                if (MessageBox.Show("Вы действительно хотите удалить группу интереса?",
                "Удалить группу?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    service.Remove(current);
                }
            }
            else
            {
                MessageBox.Show("Выберите группу интереса для удаления", "Выберите группу интереса",
                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void toList(object sender, RoutedEventArgs e)
        {
            if (current != null)
            {
                NavigationService.Navigate(new InterestGroupViewPage(current));
            }
            else
            {
                MessageBox.Show("Выберите группу");
            }
        }

    }
}
