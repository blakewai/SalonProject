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


            FrameUser.Navigate(new Uri("Pages/PagesAllUser/Profile.xaml", UriKind.RelativeOrAbsolute));
        }

        private void BtnInterior_Click(object sender, RoutedEventArgs e)
        {
            InformationTextPanel("Салон");


            FrameUser.Navigate(new Uri("Pages/PagesAllUser/Profile.xaml", UriKind.RelativeOrAbsolute));
        }

        private void BtnClient_Click(object sender, RoutedEventArgs e)
        {
            InformationTextPanel("Клиент");

            FrameUser.Navigate(new Uri("Pages/PagesAllUser/Profile.xaml", UriKind.RelativeOrAbsolute));
        }

        private void BtnProfile_Click(object sender, RoutedEventArgs e)
        {
            InformationTextPanel("Профиль");

            FrameUser.Navigate(new Uri("Pages/PagesAllUser/Profile.xaml", UriKind.RelativeOrAbsolute));
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
