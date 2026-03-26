using SalonProject.FolderData;
using SalonProject.Pages.MainPages;
using SalonProject.Pages.PagesAllUser;
using SalonProject.Pages.PagesManager;
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
    /// <summary>
    /// Логика взаимодействия для Salons.xaml
    /// </summary>
    public partial class Salons : Page
    {
        public Salons()
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
                    .Salons
                    .ToListAsync();

                DGInfo.ItemsSource = employeeData;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async Task<List<FolderData.Salons>> UsersSearchLogic(string searchUserWrite)
        {
            try
            {
                var context = FolderData.SalonEntities.GetContext();


                string searchLower = searchUserWrite.ToLower();

                var query = context.Salons.Where(u =>
                                    u.NameSalons.ToLower().Contains(searchLower) ||
                                    u.Cost.ToString().ToLower().Contains(searchLower) ||
                                    u.IdServices.ToString().ToLower().Contains(searchLower));

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при поиске: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<FolderData.Salons>();
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
    }
}
