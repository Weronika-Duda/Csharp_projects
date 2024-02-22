using ElectronicStore;
using GUIElectronicStore;
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
    /// Logika interakcji dla klasy ProductInfoWindow.xaml
    /// </summary>
    public partial class ProductInfoWindow : Window
    {
        private Sklep sklep;
        private List<Produkt> filteredProducts;
        private readonly SklepDbContext _dbContext;

        public ProductInfoWindow(SklepDbContext dbContext)
        {
            InitializeComponent();
            //sklep = (Sklep)Sklep.OdczytajZXml("sklep.xml");
            filteredProducts = new List<Produkt>();
            InitComboBoxes();
            _dbContext = dbContext;
            sklep = (Sklep)Sklep.OdczytajZBazy(_dbContext);


        }

        private void InitComboBoxes()
        {
            producerCB.ItemsSource = Enum.GetValues(typeof(EnumProducent));
            categoryCB.ItemsSource = new List<string> { "Laptop", "Smartphone" };
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            switch (comboBox.Name)
            {
                case "producerCB":
                    FilterProductsByProducer();
                    break;
                case "categoryCB":
                    FilterProductsByCategory();
                    break;
            }

            UpdateProductComboBox();
        }

        private void FilterProductsByProducer()
        {
            EnumProducent selectedProducer = (EnumProducent)producerCB.SelectedItem;

            if (selectedProducer != null)
            {
                filteredProducts = sklep.ProduktyNaStanie
                                          .Where(p => p.Producent == selectedProducer)
                                          .ToList();
            }
        }

        private void FilterProductsByCategory()
        {
            string selectedCategory = (string)categoryCB.SelectedItem;

            switch (selectedCategory)
            {
                case "Laptop":
                    filteredProducts = sklep.WyszukajProduktyWedlugTypu(typeof(Laptop))
                                              .Where(p => ((Laptop)p).Producent == (EnumProducent)producerCB.SelectedItem)
                                              .ToList();
                    break;
                case "Smartphone":
                    filteredProducts = sklep.WyszukajProduktyWedlugTypu(typeof(Smartphone))
                                              .Where(p => ((Smartphone)p).Producent == (EnumProducent)producerCB.SelectedItem)
                                              .ToList();
                    break;
            }
        }

        private void UpdateProductComboBox()
        {
            productCB.ItemsSource = filteredProducts;
        }

        private void ShowProductInfo_Click(object sender, RoutedEventArgs e)
        {
            // Pobierz wybrany produkt z ComboBoxa
            var selectedProduct = (Produkt)productCB.SelectedItem;

            // Wyświetl podstawowe informacje
            basicInfoTxtBl.Text = selectedProduct?.ToString();

            // Wyświetl dodatkowe informacje (szczegóły)
            if (selectedProduct is Smartphone smartphone)
            {
                moreInfoTxtBl.Text = smartphone.WyswietlSzczegoly();
            }
            else if (selectedProduct is Laptop laptop)
            {
                moreInfoTxtBl.Text = laptop.WyswietlSzczegoly();
            }
        }

        private void ShowProductsInStock_Click(object sender, RoutedEventArgs e)
        {
            ProductsInStock productsInStockWindow = new ProductsInStock(_dbContext);
            productsInStockWindow.Show();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

            AdminWindow adminWindow = new AdminWindow(sklep, _dbContext);
            adminWindow.Show();
        }
    }
}

