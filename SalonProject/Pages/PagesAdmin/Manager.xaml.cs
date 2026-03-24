using SalonProject.Pages.MainPages;
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

namespace SalonProject.Pages.PagesAdmin
{
    public partial class Manager : Page
    {
        public Manager()
        {
            InitializeComponent();
        }

        private void Write_Info_User()
        {
            switch (MainPanel.info_input_bt)
            {
                case 1:
                    InformationTB.Text = "Менеджеры";
                    break;
                case 2:
                    InformationTB.Text = "Сотрудников";
                    break;
            }
        }

        private void DataGridInfo()
        {

        }

        private void AddUserBT_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SearchInfoBT_Click(object sender, RoutedEventArgs e)
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
