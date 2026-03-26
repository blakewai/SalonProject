using SalonProject.FolderData;
using SalonProject.Windows;
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

namespace SalonProject.Pages.MainPages
{
    public partial class MainPanel : Page
    {
        public static int UserActionInfo;
        public static User IdUser;
        public static int ActionInfo;
        public MainPanel()
        {
            InitializeComponent();
            InformationTextPanel("Главная страница");
            Root();
        }

        private void Root()
        {
            var RoleInfo = FolderData.SalonEntities.GetContext().Role.Where(r => r.IdRole == Authorization.CurrentUser.IdRole).FirstOrDefault();
            switch (RoleInfo.IdRole)
            {
                case 1:
                    BtnTime.Visibility = Visibility.Collapsed;
                    PanelUsers.Text = "Админ";
                    break;
                case 2:
                    BtnManager.Visibility = Visibility.Collapsed;
                    BtnInterior.Visibility = Visibility.Collapsed;
                    PanelUsers.Text = "Менеджер";
                    break;
                case 3:
                    BtnManager.Visibility = Visibility.Collapsed;
                    BtnInterior.Visibility = Visibility.Collapsed;
                    BtnEmployee.Visibility = Visibility.Collapsed;
                    BtnService.Visibility = Visibility.Collapsed;
                    PanelUsers.Text = "Сотрудник";
                    break;
                case 4:
                    BtnManager.Visibility = Visibility.Collapsed;
                    BtnInterior.Visibility = Visibility.Collapsed;
                    BtnEmployee.Visibility = Visibility.Collapsed;
                    BtnService.Visibility = Visibility.Collapsed;
                    BtnTime.Visibility = Visibility.Collapsed;
                    BtnClient.Visibility = Visibility.Collapsed;
                    PanelUsers.Text = "Клиент";
                    break;
            }
        }

        private void InformationTextPanel(string Text)
        {
            var mainWindows = Application.Current.Windows
                              .OfType<MainWindow>()
                              .FirstOrDefault();
            if (mainWindows?.InformationPanel != null)
            {
                mainWindows.InformationPanel.Title = Text;
            }
        }
        private void BtnManager_Click(object sender, RoutedEventArgs e)
        {
            UserActionInfo = 2;
            InformationTextPanel("Менеджеры");

            FrameUser.Navigate(new Uri("Pages/PagesAdmin/Manager.xaml", UriKind.RelativeOrAbsolute));
        }

        private void BtnEmployee_Click(object sender, RoutedEventArgs e)
        {
            UserActionInfo = 3;
            InformationTextPanel("Сотрудники");

            FrameUser.Navigate(new Uri("Pages/PagesManager/Employee.xaml", UriKind.RelativeOrAbsolute));
        }

        private void BtnService_Click(object sender, RoutedEventArgs e)
        {
            InformationTextPanel("Услуги");

            FrameUser.Navigate(new Uri("Pages/PagesManager/Service.xaml", UriKind.RelativeOrAbsolute));
        }

        private void BtnInterior_Click(object sender, RoutedEventArgs e)
        {
            InformationTextPanel("Салон");


            FrameUser.Navigate(new Uri("Pages/PagesAdmin/Salons.xaml", UriKind.RelativeOrAbsolute));
        }

        private void BtnClient_Click(object sender, RoutedEventArgs e)
        {
            UserActionInfo = 4;
            InformationTextPanel("Клиент");

            FrameUser.Navigate(new Uri("Pages/PagesManager/Clients.xaml", UriKind.RelativeOrAbsolute));
        }

        private void BtnProfile_Click(object sender, RoutedEventArgs e)
        {
            InformationTextPanel("Профиль");

            FrameUser.Navigate(new Uri("Pages/PagesAllUser/Profile.xaml", UriKind.RelativeOrAbsolute));
        }

        private void BtnTime_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnExitAd_Click(object sender, RoutedEventArgs e)
        {
            InformationTextPanel("Данил Колбасенко!");
            var resultExit = MessageBox.Show("Вы уверены что хотите выйти?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (resultExit == MessageBoxResult.Yes)
            {
                this.NavigationService.Navigate(new Uri("Pages/MainPages/Authorization.xaml", UriKind.RelativeOrAbsolute));
                InformationTextPanel("Отмена выхода!");
            }
        }
    }
}
