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
            InfoActionUser();
        }

        private async void InfoActionUser()
        {
            switch (MainPanel.UserActionInfo)
            {
                case 2:
                    ManagerAction();
                    break;
                case 3:
                    break;
            }
        }

        private void ManagerAction()
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
                    break;
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Content = null;
        }

        private void SaveBT_Click(object sender, RoutedEventArgs e)
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
                    var UserInfoAdd = FolderData.SalonEntities.GetContext().User
                                                           .Where(x => x.IdUser == MainPanel.IdUser.IdUser)
                                                           .FirstOrDefault();
                    if (UserInfoAdd != null)
                    {
                        UserInfoAdd.Name = NameTB.Text;
                        UserInfoAdd.Lastname = LastNameTB.Text;
                        UserInfoAdd.Middlename = PatronymicTB.Text;
                        UserInfoAdd.Birthday = Convert.ToDateTime(BirthDayDP.SelectedDate);
                        UserInfoAdd.Phone = NumberTB.Text;
                        UserInfoAdd.Email = EmailTB.Text;
                    }
                    break;
            }
            FolderData.SalonEntities.GetContext().SaveChanges();
        }

        private void CloseBorder_Click(object sender, RoutedEventArgs e)
        {
            this.Content = null;
        }
    }
}
