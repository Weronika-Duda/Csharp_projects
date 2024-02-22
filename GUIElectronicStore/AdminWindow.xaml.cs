using ElectronicStore;
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
using System.Windows.Shapes;

namespace GUIProjekt
{
    /// <summary>
    /// Logika interakcji dla klasy AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private readonly SklepDbContext _dbContext;

        public AdminWindow(
            Sklep sklep, SklepDbContext dbContext)
        {
            InitializeComponent();
            _dbContext = dbContext;
        }

        private void btnInventory_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            InventoryWindow inventoryWindow = new InventoryWindow(_dbContext);
            inventoryWindow.Show();
        }

        private void btnTakeOrder_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            TakeOrderWindow takeOrderWindow = new TakeOrderWindow(_dbContext);
            takeOrderWindow.Show();
        }

        private void btnProductInfo_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ProductInfoWindow productInfoWindow = new ProductInfoWindow(_dbContext);
            productInfoWindow.Show();
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            // Tworzenie nowej instancji MainWindow
            MainWindow mainWindow = new MainWindow(_dbContext);

            // Pokazanie nowego okna i zamknięcie aktualnego
            mainWindow.Show();
            this.Close();
        }
    }
}
