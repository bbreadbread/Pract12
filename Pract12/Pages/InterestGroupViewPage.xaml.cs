using Pract12.Data;
using Pract12.Service;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для InterestGroupViewPage.xaml
    /// </summary>
    public partial class InterestGroupViewPage : Page
    {
        InterestGroup _group = new();
        UserInterestGroupService _service = new();
        public UserInterestGroupService service => _service;
        public UserInterestGroup? current { get; set; } = null;

        public InterestGroupViewPage(InterestGroup? group = null)
        {
            InitializeComponent();
            if (group != null)
            {
                _group = group;
                _service.GetAll(_group.Id);
            }
            DataContext = this;
        }

        private void back(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void OnUserDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (((ListView)sender).SelectedItem is UserInterestGroup selected)
            {
                var profile = selected.User?.UserProfile;

                current = selected; 
                DataContext = null; 
                DataContext = this;

                if (profile == null)
                {
                    EmptyProfileText.Visibility = Visibility.Visible;
                    ProfilePanel.Visibility = Visibility.Collapsed;  
                }
                else
                {
                    EmptyProfileText.Visibility = Visibility.Collapsed;
                    ProfilePanel.Visibility = Visibility.Visible;      
                }

                ProfilePanel.DataContext = new { SelectedUserProfile = profile };
            }
        }
    }
}
