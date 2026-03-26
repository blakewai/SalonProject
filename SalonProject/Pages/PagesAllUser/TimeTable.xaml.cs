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
    /// Логика взаимодействия для TimeTable.xaml
    /// </summary>
    public partial class TimeTable : Page
    {
        public static Schedule TimesInfo;

        public TimeTable()
        {
            InitializeComponent();
            LoadData();
        }
        private async void LoadData()
        {
            await DataGridInfo();
        }

        private async Task DataGridInfo()
        {
            try
            {
                var employeeData = await FolderData.SalonEntities
                    .GetContext()
                    .Schedule
                    .Include(w => w.User)
                    .Select(w => new
                    {
                        w.IdSchedule,
                        w.Date,
                        w.Time,
                        Name = w.User.Name,
                        LastName = w.User.Lastname,
                        Patronymic = w.User.Middlename
                    }
                    )
                    .ToListAsync();

                DGManager.ItemsSource = employeeData;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async Task<List<FolderData.Schedule>> UsersSearchLogic(string searchUserWrite)
        {
            try
            {
                var context = FolderData.SalonEntities.GetContext();


                string searchLower = searchUserWrite.ToLower();

                var query = context.Schedule.Where(u =>
                                    u.User.Name.ToLower().Contains(searchLower) ||
                                    u.User.Lastname.ToLower().Contains(searchLower) ||
                                    u.User.Middlename.ToLower().Contains(searchLower) ||
                                    u.Date.ToString().ToLower().Contains(searchLower) ||
                                    u.Time.ToString().ToLower().Contains(searchLower));

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при поиске: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<FolderData.Schedule>();
            }
        }

        private async void SearchInfoBT_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SearchInfoBT.IsEnabled = false;
                SearchInfoTB.IsEnabled = false;

                DGManager.ItemsSource = null;
                var searchResults = await UsersSearchLogic(SearchInfoTB.Text);

                DGManager.ItemsSource = searchResults;

                if (searchResults.Count == 0 && !string.IsNullOrWhiteSpace(SearchInfoTB.Text))
                {
                    MessageBox.Show("не найдены", "Результат поиска",
                                  MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при поиске: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
                await DataGridInfo();
            }
            finally
            {
                SearchInfoBT.IsEnabled = true;
                SearchInfoTB.IsEnabled = true;
            }
        }

        private void AddUserBT_Click(object sender, RoutedEventArgs e)
        {
            MainPanel.ActionInfo = 1;
            EditTime.Content = null;
            var ManagerAction = new ActionUser();
            EditTime.NavigationService?.Navigate(ManagerAction);
        }

        private void EditBT_Click(object sender, RoutedEventArgs e)
        {
            TimesInfo = DGManager.SelectedItem as Schedule;
            MainPanel.ActionInfo = 0;
            EditTime.Content = null;
            var ManagerAction = new ActionUser();
            EditTime.NavigationService?.Navigate(ManagerAction);
        }

        private void DeleteBT_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var Time = DGManager.SelectedItem as Schedule;
                var result = MessageBox.Show($"Вы уверены что хотите удалить Время для пользователя - {Time.User.Name}?", "Delete",
                                                MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    FolderData.SalonEntities.GetContext().Schedule.Remove(Time);
                    FolderData.SalonEntities.GetContext().SaveChanges();

                    LoadData();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка при удалении", "Удаление",
                                  MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
