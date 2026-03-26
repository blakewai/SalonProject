using SalonProject.FolderData;
using SalonProject.Pages.MainPages;
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
        public ActionService()
        {
            InitializeComponent();
        }

        private bool Check_Input()
        {
            if (ServiceTB.Text == null || ServiceTB.Text == string.Empty)
            {
                MessageBox.Show("Введите Имя услуги");
                return false;
            }
            if (CostTB.Text == null || CostTB.Text == string.Empty)
            {
                MessageBox.Show("Введите стоимость");
                return false;
            }
            return true;
        }

        private bool Check_Input_Add()
        {
            if (ClientTB.Items == null)
            {
                MessageBox.Show("Введите клиент");
                return false;
            }
            if (ServiceType.Items == null)
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
                    var UserInfo = FolderData.SalonEntities.GetContext().User
                                                           .Where(x => x.IdUser == MainPanel.IdUser.IdUser)
                                                           .FirstOrDefault();
                    if (UserInfo != null)
                    {
                        InfoAction.Text = "Редактировать";

                    }
                    break;
                case 1:
                    InfoAction.Text = "Добавить";
                    InfoMain.Visibility = Visibility.Visible;
                    break;
            }
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
                                                               .Where(x => x.IdServices == MainPanel.IdUser.IdUser)
                                                               .FirstOrDefault();
                        if (ServiceInfo != null)
                        {
                        }
                        break;
                    case 1:
                        if (Check_Input_Add())
                        {
                            var servicesAdd = FolderData.SalonEntities.GetContext().Services
                                                                   .FirstOrDefault();
                            if (servicesAdd != null)
                            {
                                servicesAdd.Cost = Convert.ToInt32(CostTB.Text);
                                servicesAdd.NameServices = ServiceTB.Text;

                                FolderData.SalonEntities.GetContext().Services.Add(servicesAdd);
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
