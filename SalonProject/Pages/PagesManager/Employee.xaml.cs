using SalonProject.FolderData;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SalonProject.Pages.PagesManager
{
    public partial class Employee : Page
    {
        public Employee()
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

                DGInfo.ItemsSource = employeeData;
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

                var query = context.User.Where(u => u.IdRole == 2);

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

                DGInfo.ItemsSource = null;
                var searchResults = await UsersSearchLogic(SearchInfoTB.Text);

                DGInfo.ItemsSource = searchResults;

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

        }

        private void EditBT_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteBT_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}