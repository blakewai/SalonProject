using SalonProject.FolderData;
using SalonProject.Pages.MainPages;
using SalonProject.Pages.PagesAllUser;
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

namespace SalonProject.Pages.PagesAdmin
{
    public partial class Manager : Page
    {
        public Manager()
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
                    .User
                    .Where(x => x.IdRole == 2)
                    .ToListAsync();

                DGManager.ItemsSource = employeeData;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async Task<List<User>> UsersSearchLogic(string searchUserWrite)
        {
            try
            {
                var context = FolderData.SalonEntities.GetContext();

                var query = context.User.Where(u => u.IdRole == 3);

                if (!string.IsNullOrWhiteSpace(searchUserWrite))
                {
                    string searchLower = searchUserWrite.ToLower();

                    query = query.Where(u =>
                                        u.Name.ToLower().Contains(searchLower) ||
                                        u.Lastname.ToLower().Contains(searchLower) ||
                                        u.Middlename.ToLower().Contains(searchLower) ||
                                        u.Phone.ToLower().Contains(searchLower) ||
                                        u.Birthday.ToString().ToLower().Contains(searchLower) ||
                                        u.Login.ToLower().Contains(searchLower) ||
                                        u.Password.ToLower().Contains(searchLower));
                }

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при поиске: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<User>();
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
                    MessageBox.Show("Сотрудники не найдены", "Результат поиска",
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
            EditManager.Content = null;
            var ManagerAction = new ActionUser();
            EditManager.NavigationService?.Navigate(ManagerAction);
        }

        private void EditBT_Click(object sender, RoutedEventArgs e)
        {
            MainPanel.IdUser = DGManager.SelectedItem as User;
            MainPanel.ActionInfo = 0;
            EditManager.Content = null;
            var ManagerAction = new ActionUser();
            EditManager.NavigationService?.Navigate(ManagerAction);
        }

        private void DeleteBT_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var user = DGManager.SelectedItem as User;
                var result = MessageBox.Show($"Вы уверены что хотите удалить пользоавтеля - {user.Name}?", "Delete",
                                                MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    FolderData.SalonEntities.GetContext().User.Remove(user);
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
