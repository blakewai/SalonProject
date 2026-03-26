using SalonProject.FolderData;
using SalonProject.Pages.MainPages;
using SalonProject.Pages.PagesAdmin;
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

namespace SalonProject.Pages.PagesAllUser
{
    /// <summary>
    /// Логика взаимодействия для ActionUser.xaml
    /// </summary>
    public partial class ActionUser : Page
    {
        private MainPanel mainPanel;
        public ActionUser()
        {
            InitializeComponent();
            Action();
        }

        private bool Check_Input()
        {
            if (NameTB.Text == null || NameTB.Text == string.Empty)
            {
                MessageBox.Show("Введите Имя");
                return false;
            }
            if (LastNameTB.Text == null || LastNameTB.Text == string.Empty)
            {
                MessageBox.Show("Введите Фамилию");
                return false;
            }
            if (PatronymicTB.Text == null || PatronymicTB.Text == string.Empty)
            {
                MessageBox.Show("Введите Отчество");
                return false;
            }
            if (BirthDayDP.Text == null || BirthDayDP.Text == string.Empty)
            {
                MessageBox.Show("Введите День рождение");
                return false;
            }
            if (NumberTB.Text == null || NumberTB.Text == string.Empty)
            {
                MessageBox.Show("Введите Номер телефона");
                return false;
            }
            if (EmailTB.Text == null || EmailTB.Text == string.Empty)
            {
                MessageBox.Show("Введите Почта");
                return false;
            }
            return true;
        }

        private bool Check_Input_Add()
        {

            if (PasswordTB.Password == null || PasswordTB.Password == string.Empty && MainPanel.ActionInfo == 1)
            {
                MessageBox.Show("Введите Пароль");
                return false;
            }
            if (LoginTB.Text == null || LoginTB.Text == string.Empty && MainPanel.ActionInfo == 1)
            {
                MessageBox.Show("Введите Логин");
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
                        LastNameTB.Text = UserInfo.Lastname;
                        NameTB.Text = UserInfo.Name;
                        PatronymicTB.Text = UserInfo.Middlename;
                        BirthDayDP.Text = UserInfo.Birthday.ToString();
                        NumberTB.Text = UserInfo.Phone;
                        EmailTB.Text = UserInfo.Email;
                    }
                    break;
                case 1:
                    InfoAction.Text = "Добавить";
                    LoginPassword.Visibility = Visibility.Visible;
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
                        var UserInfo = FolderData.SalonEntities.GetContext().User
                                                               .Where(x => x.IdUser == MainPanel.IdUser.IdUser)
                                                               .FirstOrDefault();
                        if (UserInfo != null)
                        {
                            UserInfo.Name = NameTB.Text;
                            UserInfo.Lastname = LastNameTB.Text;
                            UserInfo.Middlename = PatronymicTB.Text;
                            UserInfo.Birthday = Convert.ToDateTime(BirthDayDP.SelectedDate);
                            UserInfo.Phone = NumberTB.Text;
                            UserInfo.Email = EmailTB.Text;
                        }
                        break;
                    case 1:
                        if (Check_Input_Add()) 
                        {
                            var UserInfoAdd = FolderData.SalonEntities.GetContext().User
                                                                   .FirstOrDefault();
                            if (UserInfoAdd != null)
                            {
                                UserInfoAdd.Name = NameTB.Text;
                                UserInfoAdd.Lastname = LastNameTB.Text;
                                UserInfoAdd.Middlename = PatronymicTB.Text;
                                UserInfoAdd.Birthday = Convert.ToDateTime(BirthDayDP.SelectedDate);
                                UserInfoAdd.Phone = NumberTB.Text;
                                UserInfoAdd.Email = EmailTB.Text;
                                UserInfoAdd.Password = PasswordTB.Password;
                                UserInfoAdd.Login = LoginTB.Text;
                                UserInfoAdd.IdRole = MainPanel.UserActionInfo;
                                FolderData.SalonEntities.GetContext().User.Add(UserInfoAdd);
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
