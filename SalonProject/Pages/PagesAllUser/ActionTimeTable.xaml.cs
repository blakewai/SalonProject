using SalonProject.FolderData;
using SalonProject.Pages.MainPages;
using SalonProject.Pages.PagesManager;
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
    /// Логика взаимодействия для ActionTimeTable.xaml
    /// </summary>
    public partial class ActionTimeTable : Page
    {
        private List<User> _users;
        public ActionTimeTable()
        {
            InitializeComponent();
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
            if (Date.Text == null || Date.Text == string.Empty)
            {
                MessageBox.Show("Введите Дату");
                return false;
            }
            if (Time.Text == null || Time.Text == string.Empty)
            {
                MessageBox.Show("Введите Время");
                return false;
            }
            return true;
        }

        private void Action()
        {
            switch (MainPanel.ActionInfo)
            {
                case 0:
                    var ServiceInfo = FolderData.SalonEntities.GetContext().Schedule
                                                           .Where(x => x.IdSchedule == TimeTable.TimesInfo.IdSchedule)
                                                           .FirstOrDefault();
                    if (ServiceInfo != null)
                    {
                        InfoAction.Text = "Редактировать";

                        LastNameTB.Text = ServiceInfo.User.Lastname;
                        NameTB.Text = ServiceInfo.User.Name;
                        PatronymicTB.Text = ServiceInfo.User.Middlename;
                        Time.Text = ServiceInfo.Time.ToString();
                        Date.Text = ServiceInfo.Date.ToString();
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
            if (Check_Input())
            {
                switch (MainPanel.ActionInfo)
                {
                    case 0:
                        var TimeTable = FolderData.SalonEntities.GetContext().Schedule
                                                               .Where(x => x.IdSchedule == PagesAllUser.TimeTable.TimesInfo.IdSchedule)
                                                               .FirstOrDefault();
                        if (TimeTable != null)
                        {
                            TimeTable.Date = Convert.ToDateTime(Date.Text);
                            TimeTable.Time = TimeSpan.FromSeconds(Convert.ToInt32(Time.Text));
                            var TimeUser = FolderData.SalonEntities.GetContext().Schedule.Where(x => x.User.Name == NameTB.Text &&
                                                                                                     x.User.Middlename == PatronymicTB.Text &&
                                                                                                     x.User.Lastname == LastNameTB.Text).FirstOrDefault();
                            if (TimeUser != null)
                            {
                                TimeTable.IdUser = TimeUser.IdUser;
                                TimeTable.Time = TimeSpan.FromSeconds(Convert.ToInt32(Time.Text));
                                TimeTable.Date = Convert.ToDateTime(Date.Text);
                            }
                        }
                        break;
                    case 1:
                        var TimeTableAdd = new Schedule();
                        if (TimeTableAdd != null)
                        {
                            var TimeUserAdd = FolderData.SalonEntities.GetContext().Schedule.Where(x => x.User.Name == NameTB.Text &&
                                                                                                     x.User.Middlename == PatronymicTB.Text &&
                                                                                                     x.User.Lastname == LastNameTB.Text).FirstOrDefault();
                            if (TimeTableAdd != null)
                            {
                                TimeTableAdd.Time = TimeSpan.FromSeconds(Convert.ToInt32(Time.Text));
                                TimeTableAdd.Date = Convert.ToDateTime(Date.SelectedDate);
                                TimeTableAdd.IdUser = TimeUserAdd.IdUser;
                                FolderData.SalonEntities.GetContext().Schedule.Add(TimeTableAdd);
                                MessageBox.Show("Успех");
                            }
                            else
                            {
                                MessageBox.Show("Вы ввели не правильного пользователя!", "Такого пользователя нет!", MessageBoxButton.OK, MessageBoxImage.Warning);
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
