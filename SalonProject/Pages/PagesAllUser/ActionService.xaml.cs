using SalonProject.FolderData;
using SalonProject.Pages.MainPages;
using SalonProject.Pages.PagesManager;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace SalonProject.Pages.PagesAllUser
{
    /// <summary>
    /// Логика взаимодействия для ActionService.xaml
    /// </summary>
    public partial class ActionService : Page
    {
        private List<User> _users;
        private List<ServicesName> _services;


        public ActionService()
        {
            InitializeComponent();
            InfoActionUser();
        }

        private async void InfoActionUser()
        {
            switch (MainPanel.UserActionInfo)
            {
                case 2:
                    Action();
                    break;
                case 3:
                    Action();
                    break;
            }
            Action();
        }
        private bool Check_Input()
        {
            if (ServiceTB.Text == null || ServiceTB.Text == string.Empty)
            {
                MessageBox.Show("Введите Имя");
                return false;
            }
            if (CostTB.Text == null || CostTB.Text == string.Empty)
            {
                MessageBox.Show("Введите Фамилию");
                return false;
            }
            return true;
        }
        private bool Check_Input_Add()
        {
            if (ClientTB.SelectedIndex == -1)
            {
                MessageBox.Show("Введите клиент");
                return false;
            }
            if (ServiceType.SelectedIndex == -1)
            {
                MessageBox.Show("Введите тип");
                return false;
            }
            return true;
        }
        private void Action()
        {
            switch (MainPanel.ActionInfo)
            {
                case 0:
                    var ServiceInfo = FolderData.SalonEntities.GetContext().Services
                                                           .Where(x => x.IdServices == Service.IdService.IdServices)
                                                           .FirstOrDefault();
                    if (ServiceInfo != null)
                    {
                        InfoAction.Text = "Редактировать";

                        ServiceTB.Text = ServiceInfo.NameServices;
                        CostTB.Text = ServiceInfo.Cost.ToString();

                    }
                    break;
                case 1:
                    InfoAction.Text = "Добавить";
                    InfoMain.Visibility = Visibility.Visible;

                    Write_ComboBox();
                    break;
            }
        }

        private void Write_ComboBox()
        {
            _services = FolderData.SalonEntities.GetContext().ServicesName.ToList();
            ServiceType.ItemsSource = _services;
            ServiceType.DisplayMemberPath = "ServicesType";
            _users = FolderData.SalonEntities.GetContext().User.ToList();
            ClientTB.ItemsSource = _users;
            ClientTB.DisplayMemberPath = "Phone";

        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Content = null;
        }

        private void SaveBT_Click(object sender, RoutedEventArgs e)
        {
            if (Check_Input())
            {
                switch (MainPanel.ActionInfo)
                {
                    case 0:
                        var ServiceInfo = FolderData.SalonEntities.GetContext().Services
                                                               .Where(x => x.IdServices == Service.IdService.IdServices)
                                                               .FirstOrDefault();
                        if (ServiceInfo != null)
                        {
                            ServiceInfo.NameServices = ServiceTB.Text;
                            ServiceInfo.Cost = Convert.ToInt32(CostTB.Text);
                        }
                        break;
                    case 1:
                        if (Check_Input_Add())
                        {
                            var servicesAdd = new Services();
                            if (servicesAdd != null)
                            {
                                var Users = ClientTB.SelectedItem as User;
                                var Services = ServiceType.SelectedItem as ServicesName;
                                servicesAdd.NameServices = ServiceTB.Text;
                                servicesAdd.Cost = Convert.ToInt32(CostTB.Text);
                                servicesAdd.IdClient = Users.IdUser;
                                servicesAdd.IdServicesType = Services.IdServicesType;
                                FolderData.SalonEntities.GetContext().Services.Add(servicesAdd);
                                MessageBox.Show("Успех");
                            }
                        }
                        break;
                }
                FolderData.SalonEntities.GetContext().SaveChanges();
                this.Content = null;
            }
        }

        private void CloseBorder_Click(object sender, RoutedEventArgs e)
        {
            this.Content = null;
        }
    }
}
