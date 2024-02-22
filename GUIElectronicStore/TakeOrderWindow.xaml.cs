using ElectronicStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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
    /// Logika interakcji dla klasy TakeOrderWindow.xaml
    /// </summary>
    public partial class TakeOrderWindow : Window
    {
        private List<Produkt> filteredProducts;
        private Sklep sklep ;
        private Paragon paragon;

        private SklepDbContext _dbContext;

        public TakeOrderWindow(SklepDbContext dbContext)
        {
            _dbContext = dbContext;
            InitializeComponent();
            sklep = (Sklep)Sklep.OdczytajZBazy(_dbContext);
            //sklep = (Sklep)Sklep.OdczytajZXml("sklep.xml"); 
            paragon = new Paragon();
            filteredProducts = new List<Produkt>();
            InitComboBoxes();
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

        private void AddToReceipt_Click(object sender, RoutedEventArgs e)
        {
            Produkt selectedProduct = (Produkt)productCB.SelectedItem;

            if (selectedProduct == null)
            {
                MessageBox.Show("Please select a product.");
                return;
            }

            if (int.TryParse(quantityTextBox.Text, out int quantity))
            {
                for (int i = 0; i < quantity; i++)
                {
                    paragon.DodajProduktDoParagonu(selectedProduct, 1);
                }

                ShowReceipt();
            }
            else
            {
                MessageBox.Show("Invalid quantity. Please enter a valid number.");
            }
        }

        private void RemoveFromReceipt_Click(object sender, RoutedEventArgs e)
        {
            if (receiptTxtBl.SelectedIndex != -1)
            {
                int selectedIndex = receiptTxtBl.SelectedIndex;

                // Sprawdź, czy indeks jest prawidłowy
                if (selectedIndex >= 0 && selectedIndex < paragon.PozycjeParagonu.Count)
                {
                    // Usuń element z listy PozycjeParagonu
                    paragon.PozycjeParagonu.RemoveAt(selectedIndex);

                    // Odśwież ListBox
                    ShowReceipt();
                }
            }
            else
            {
                MessageBox.Show("Please select a product to remove.");
            }
        }
        private void ShowReceipt()
        {
            receiptTxtBl.ItemsSource = null; // Wyczyść listę przed ponownym ustawieniem
            receiptTxtBl.ItemsSource = paragon.PozycjeParagonu.Select(p => p.ToString()).ToList();
        }

        private void SortReceipt_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            KierunekSortowania sortDirection = (clickedButton.Name == "sortReceiptUpBtn") ? KierunekSortowania.Rosnaco : KierunekSortowania.Malejaco;

            paragon.SortujParagonPoCenie(sortDirection);
            ShowReceipt();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        // Sprawdzamy, czy wprowadzony tekst to liczba całkowita
            if (int.TryParse((sender as TextBox)?.Text, out int quantity))
            {
                int enteredQuantity = quantity;
            }
            else
            {
                int enteredQuantity = 0;
            }
        }


        private void Order_Click(object sender, RoutedEventArgs e)
        {
            sklep.RealizujZakupy(paragon);
            ShowReceipt();
            sklep.ZapiszDoXml("sklep.xml");
            sklep.ZapiszDoBazy(_dbContext);
            MessageBox.Show("The order has been accepted for execution");
            paragon = new Paragon();
        }



        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

            AdminWindow adminWindow = new AdminWindow(sklep, _dbContext);
            adminWindow.Show();
        }
    }
}
       

