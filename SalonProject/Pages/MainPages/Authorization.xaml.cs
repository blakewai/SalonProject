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
    public partial class Authorization : Page
    {
        public static int IdUser;

        public Authorization()
        {
            InitializeComponent();

            var mainWindow = Application.Current.Windows
                            .OfType<MainWindow>()
                            .FirstOrDefault();

            if (mainWindow?.InformationPanel != null)
            {
                mainWindow.InformationPanel.Title = "Авторизация";
            }
        }
        private bool Check_Input()
        {
            if (string.IsNullOrEmpty(TbLogin.Text))
            {
                TextError.Text = "ВВедите логин";
                TbLogin.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(PbPassword.Password))
            {
                TextError.Text = "ВВедите пароль";
                PbPassword.Focus();
                return false;
            }
            return true;
        }

        private void BtnAut_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Check_Input())
                {
                    var user = SalonEntities.GetContext().User
                        .FirstOrDefault(u => u.Login.ToLower()
                        == TbLogin.Text.ToLower() &&
                        u.Password == PbPassword.Password.ToLower());
                    if (user != null)
                    {
                        IdUser = user.IdUser;
                        this.NavigationService.Navigate(new Uri("Pages/MainPages/MainPanel.xaml", UriKind.RelativeOrAbsolute));
                    }
                    else
                    {
                        TextError.Text = "Вы указали не верные данные, проверте правильно ли вы ввели данные!";
                        TextError.Focusable = true;
                        return;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Сообщение об ошибке:\n\n{ex.Message}");
            }
        }
    }
}
