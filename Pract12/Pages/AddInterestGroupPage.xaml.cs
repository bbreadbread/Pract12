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
    /// Логика взаимодействия для AddInterestGroupPage.xaml
    /// </summary>
    public partial class AddInterestGroupPage : Page
    {
        InterestGroup _group = new();
        InterestGroupService service = new();
        bool IsEdit = false;

        public AddInterestGroupPage(InterestGroup? group = null)
        {
            InitializeComponent();
            if (group != null)
            {
                service.LoadRelation(group, "UserInterestGroup");
                _group = group;
                IsEdit = true;
            }
            DataContext = _group;
        }

        private void save(object sender, RoutedEventArgs e)
        {
            if (!(Validation.GetHasError(TitleBox)))
            {
                if (IsEdit)
                    service.Commit();
                else
                    service.Add(_group);
                back(sender, e);
            }
            else MessageBox.Show("Наименование занято");

           
        }
        private void back(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
