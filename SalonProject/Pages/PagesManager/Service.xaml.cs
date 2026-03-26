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

namespace SalonProject.Pages.PagesManager
{
    /// <summary>
    /// Логика взаимодействия для Service.xaml
    /// </summary>
    public partial class Service : Page
    {
        public static int IdService;
        public Service()
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
                    .Services
                    .ToListAsync();

                DGInfo.ItemsSource = employeeData;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async Task<List<Services>> UsersSearchLogic(string searchUserWrite)
        {
            try
            {
                var context = FolderData.SalonEntities.GetContext();

                
                string searchLower = searchUserWrite.ToLower();

                var query = context.Services.Where(u =>
                                    u.NameServices.ToLower().Contains(searchLower)||
                                    u.Cost.ToString().ToLower().Contains(searchLower)||
                                    u.IdClient.ToString().ToLower().Contains(searchLower)||
                                    u.IdServices.ToString().ToLower().Contains(searchLower));
            
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при поиске: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<Services>();
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
                    MessageBox.Show("Услуга не найдена", "Результат поиска",
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
            ServiceFrame.Content = null;
            var ServiceAction = new ActionService();
            ServiceFrame.NavigationService?.Navigate(ServiceAction);
        }

        private void EditBT_Click(object sender, RoutedEventArgs e)
        {
            IdService = DGInfo.SelectedItem as User;
            MainPanel.ActionInfo = 0;
            ServiceFrame.Content = null;
            var ServiceAction = new ActionService();
            ServiceFrame.NavigationService?.Navigate(ServiceAction);
        }

        private void DeleteBT_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var user = DGInfo.SelectedItem as User;
                var result = MessageBox.Show($"Вы уверены что хотите удалить услегу - {user.Name}?", "Delete",
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
