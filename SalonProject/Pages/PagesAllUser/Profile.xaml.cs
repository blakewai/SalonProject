using SalonProject.FolderData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    public partial class Profile : Page
    {
        public Profile()
        {
            InitializeComponent();
            UserDataInfo();
        }

        private void UserDataInfo()
        {
            var UserData = SalonEntities.GetContext()
                                        .User.Where(x => x.IdUser == MainPages.Authorization.IdUser)
                                        .FirstOrDefault();
            if (UserData != null)
            {
                PatronymicTB.Text = UserData.Middlename;
                NameTB.Text = UserData.Name;
                LastNameTB.Text = UserData.Lastname;
                BirthDayTB.Text = UserData.Birthday.ToString();
                NumberTB.Text = UserData.Phone;
                EmailTB.Text = UserData.Email;
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
