using ElectronicStore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Logika interakcji dla klasy InventoryWindow.xaml
    /// </summary>
    public partial class InventoryWindow : Window
    {
        private readonly SklepDbContext _dbContext;

        Sklep sklep;
        Produkt produkt;
        public InventoryWindow(SklepDbContext dbContext)
        {
            _dbContext = dbContext;
            InitializeComponent();
            //sklep = (Sklep)Sklep.OdczytajZXml("sklep.xml");
            sklep = (Sklep)Sklep.OdczytajZBazy(_dbContext);
            if (sklep is object)
            {
                producerCB.ItemsSource = Enum.GetValues(typeof(EnumProducent)).Cast<EnumProducent>();
            }
        }

        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedCategory = categoryCB.SelectedItem as ComboBoxItem;

            if (selectedCategory != null)
            {
                // Wyczyszczenie i ukrycie obecnych pól formularza
                productFormPanel.Children.Clear();
                productFormPanel.Visibility = Visibility.Collapsed;

                // Utworzenie nowych pól formularza w zależności od wybranej kategorii
                if (selectedCategory.Content.ToString() == "Laptop")
                {

                    Thickness currentMarginLabels = new Thickness(0, -300, 0, 0);
                    Thickness currentMarginBoxes = new Thickness(200, -300, 0, 0);

                    productFormPanel.Children.Add(new Label()
                    {
                        Content = "Screen:",
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = currentMarginLabels,
                        FontSize = 20,
                        FontFamily = new FontFamily("Tw Cen MT Condensed Extra Bold"),
                        Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF518FCF")),
                        Width = 150,
                        Height = 30
                    });

                    TextBox ekranTextBox = new TextBox()
                    {
                        Name = "ekranTextBox",
                        Width = 185,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = currentMarginBoxes,
                        Height = 30,
                        FontSize = 16
                    };
                    productFormPanel.Children.Add(ekranTextBox);

                    currentMarginLabels.Top += 90;
                    currentMarginBoxes.Top += 90;

                    productFormPanel.Children.Add(new Label()
                    {
                        Content = "RAM memory:",
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = currentMarginLabels,
                        FontSize = 20,
                        FontFamily = new FontFamily("Tw Cen MT Condensed Extra Bold"),
                        Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF518FCF")),
                        Width = 150,
                        Height = 30
                    });
                    TextBox ramTextBox = new TextBox()
                    {
                        Name = "ramTextBox",
                        Width = 185,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = currentMarginBoxes,
                        Height = 30,
                        FontSize = 16
                    };
                    productFormPanel.Children.Add(ramTextBox);


                    currentMarginLabels.Top += 90;
                    currentMarginBoxes.Top += 90;

                    productFormPanel.Children.Add(new Label()
                    {
                        Content = "Disc capacity:",
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = currentMarginLabels,
                        FontSize = 20,
                        FontFamily = new FontFamily("Tw Cen MT Condensed Extra Bold"),
                        Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF518FCF")),
                        Width = 150,
                        Height = 30
                    });
                    TextBox dyskTextBox = new TextBox()
                    {
                        Name = "dyskTextBox",
                        Width = 185,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = currentMarginBoxes,
                        Height = 30,
                        FontSize = 16
                    };
                    productFormPanel.Children.Add(dyskTextBox);


                    currentMarginLabels.Top += 90;
                    currentMarginBoxes.Top += 90;

                    productFormPanel.Children.Add(new Label()
                    {
                        Content = "System:",
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = currentMarginLabels,
                        FontSize = 20,
                        FontFamily = new FontFamily("Tw Cen MT Condensed Extra Bold"),
                        Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF518FCF")),
                        Width = 150,
                        Height = 30
                    });
                    ComboBox systemComboBox = new ComboBox()
                    {
                        Name = "systemComboBox",
                        Width = 185,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = currentMarginBoxes,
                        Height = 30,
                        FontSize = 16
                    };


                    currentMarginLabels.Top += 90;
                    currentMarginBoxes.Top += 90;

                    foreach (var systemValue in Enum.GetValues(typeof(SystemLaptop)))
                    {
                        systemComboBox.Items.Add(systemValue.ToString());
                    }
                    productFormPanel.Children.Add(systemComboBox);

                    // Ustawienie widoczności panelu
                    productFormPanel.Visibility = Visibility.Visible;
                }

                else if (selectedCategory.Content.ToString() == "Smartphone")
                {
                    Thickness currentMarginLabels = new Thickness(0, -300, 0, 0);
                    Thickness currentMarginBoxes = new Thickness(200, -300, 0, 0);


                    productFormPanel.Children.Add(new Label()
                    {
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Content = "Screen:",
                        Margin = currentMarginLabels,
                        FontSize = 20,
                        FontFamily = new FontFamily("Tw Cen MT Condensed Extra Bold"),
                        Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF518FCF")),
                        Width = 150,
                        Height = 30
                    });
                    TextBox wyswietlaczTextBox = new TextBox()
                    {
                        Name = "wyswietlaczTextBox",
                        Width = 185,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = currentMarginBoxes,
                        Height = 30,
                        FontSize = 16
                    };
                    productFormPanel.Children.Add(wyswietlaczTextBox);

                    currentMarginLabels.Top += 90;
                    currentMarginBoxes.Top += 90;

                    productFormPanel.Children.Add(new Label()
                    {
                        Content = "RAM memory:",
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = currentMarginLabels,
                        FontSize = 20,
                        FontFamily = new FontFamily("Tw Cen MT Condensed Extra Bold"),
                        Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF518FCF")),
                        Width = 150,
                        Height = 30
                    });
                    TextBox ramTextBox = new TextBox()
                    {
                        Name = "ramTextBox",
                        Width = 185,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = currentMarginBoxes,
                        Height = 30,
                        FontSize = 16
                    };
                    productFormPanel.Children.Add(ramTextBox);


                    currentMarginLabels.Top += 90;
                    currentMarginBoxes.Top += 90;


                    productFormPanel.Children.Add(new Label()
                    {
                        Content = "Build-in memory:",
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = currentMarginLabels,
                        FontSize = 20,
                        FontFamily = new FontFamily("Tw Cen MT Condensed Extra Bold"),
                        Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF518FCF")),
                        Width = 150,
                        Height = 30
                    });
                    TextBox wbudowanaTextBox = new TextBox()
                    {
                        Name = "wbudowanaTextBox",
                        Width = 185,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = currentMarginBoxes,
                        Height = 30,
                        FontSize = 16
                    };
                    productFormPanel.Children.Add(wbudowanaTextBox);


                    currentMarginLabels.Top += 90;
                    currentMarginBoxes.Top += 90;


                    productFormPanel.Children.Add(new Label()
                    {
                        Content = "Battery capacity:",
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = currentMarginLabels,
                        FontSize = 20,
                        FontFamily = new FontFamily("Tw Cen MT Condensed Extra Bold"),
                        Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF518FCF")),
                        Width = 150,
                        Height = 30
                    });
                    productFormPanel.Children.Add(new TextBox()
                    {
                        Name = "akumulatorTextBox",
                        Width = 185,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = currentMarginBoxes,
                        Height = 30,
                        FontSize = 16
                    });


                    currentMarginLabels.Top += 90;
                    currentMarginBoxes.Top += 90;


                    productFormPanel.Children.Add(new Label()
                    {
                        Content = "Rear camera:",
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = currentMarginLabels,
                        FontSize = 20,
                        FontFamily = new FontFamily("Tw Cen MT Condensed Extra Bold"),
                        Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF518FCF")),
                        Width = 150,
                        Height = 30
                    });


                    productFormPanel.Children.Add(new TextBox()
                    {
                        Name = "aparatTylnyTextBox",
                        Width = 185,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = currentMarginBoxes,
                        Height = 30,
                        FontSize = 16
                    });


                    currentMarginLabels.Top += 90;
                    currentMarginBoxes.Top += 90;


                    productFormPanel.Children.Add(new Label()
                    {
                        Content = "Front camera:",
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = currentMarginLabels,
                        FontSize = 20,
                        FontFamily = new FontFamily("Tw Cen MT Condensed Extra Bold"),
                        Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF518FCF")),
                        Width = 150,
                        Height = 30
                    });
                    productFormPanel.Children.Add(new TextBox()
                    {
                        Name = "aparatPrzedniTextBox",
                        Width = 185,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = currentMarginBoxes,
                        Height = 30,
                        FontSize = 16
                    });


                    currentMarginLabels.Top += 90;
                    currentMarginBoxes.Top += 90;



                    productFormPanel.Children.Add(new Label()
                    {
                        Content = "System:",
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = currentMarginLabels,
                        FontSize = 20,
                        FontFamily = new FontFamily("Tw Cen MT Condensed Extra Bold"),
                        Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF518FCF")),
                        Width = 150,
                        Height = 30
                    });
                    ComboBox systemComboBox = new ComboBox()
                    {
                        Name = "systemComboBox",
                        Width = 185,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = currentMarginBoxes,
                        Height = 30,
                        FontSize = 16
                    };

                    currentMarginLabels.Top += 90;
                    currentMarginBoxes.Top += 90;


                    foreach (var systemValue in Enum.GetValues(typeof(SystemTel)))
                    {
                        systemComboBox.Items.Add(systemValue.ToString());
                    }
                    productFormPanel.Children.Add(systemComboBox);

                    // Ustawienie widoczności panelu
                    productFormPanel.Visibility = Visibility.Visible;
                }

            }
        }


        private EnumProducent selectedProducer;


        private void InitializeProducerComboBox()
        {
            producerCB.ItemsSource = Enum.GetValues(typeof(EnumProducent)).Cast<EnumProducent>();
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem selectedCategory = categoryCB.SelectedItem as ComboBoxItem;

            if (selectedCategory != null)
            {
                EnumProducent producent = (EnumProducent)producerCB.SelectedItem;
                string model = modelTextBox.Text;
                decimal cena = decimal.Parse(priceTextBox.Text);
                int iloscNaStanie = int.Parse(stockTextBox.Text);
                string opis = descriptionTextBox.Text;


                Produkt produkt = null;
                if (selectedCategory.Content.ToString() == "Laptop")
                {
                    float ekran = float.Parse((productFormPanel.Children[1] as TextBox)?.Text);
                    int ram = int.Parse((productFormPanel.Children[3] as TextBox)?.Text);
                    int dysk = int.Parse((productFormPanel.Children[5] as TextBox)?.Text);
                    SystemLaptop system = (SystemLaptop)Enum.Parse(typeof(SystemLaptop), (productFormPanel.Children[7] as ComboBox)?.SelectedItem.ToString());

                    produkt = new Laptop(ekran, ram, dysk, system, producent, model, cena, iloscNaStanie, opis);
                    sklep.DodajNowyProdukt(produkt);
                    sklep.ZapiszDoXml("sklep.xml");
                    sklep.ZapiszDoBazy(_dbContext);
                    MessageBox.Show($"The laptop has been successfully added to the store!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                else if (selectedCategory.Content.ToString() == "Smartphone")
                {
                    float wyswietlacz = float.Parse((productFormPanel.Children[1] as TextBox)?.Text);
                    int ram = int.Parse((productFormPanel.Children[3] as TextBox)?.Text);
                    int wbudowana = int.Parse((productFormPanel.Children[5] as TextBox)?.Text);
                    int akumulator = int.Parse((productFormPanel.Children[7] as TextBox)?.Text);
                    int aparatTylny = int.Parse((productFormPanel.Children[9] as TextBox)?.Text);
                    int aparatPrzedni = int.Parse((productFormPanel.Children[11] as TextBox)?.Text);
                    SystemTel system = (SystemTel)Enum.Parse(typeof(SystemTel), (productFormPanel.Children[13] as ComboBox)?.SelectedItem.ToString());

                    produkt = new Smartphone(wyswietlacz, ram, wbudowana, akumulator, aparatTylny, aparatPrzedni, system, producent, model, cena, iloscNaStanie, opis);
                    sklep.DodajNowyProdukt(produkt);
                    sklep.ZapiszDoXml("sklep.xml");
                    sklep.ZapiszDoBazy(_dbContext);
                    MessageBox.Show($"The smartphone has been successfully added to the store!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                }

                ClearFormFields();
            }
        }
        private void ClearFormFields()
        {
            ComboBoxItem selectedCategory = categoryCB.SelectedItem as ComboBoxItem;

            modelTextBox.Text = "";
            priceTextBox.Text = "";
            stockTextBox.Text = "";
            descriptionTextBox.Text = "";
            producerCB.SelectedIndex = -1;
            categoryCB.SelectedIndex = -1;

            // Wyczyść pola specyficzne dla laptopa
            if (selectedCategory.Content.ToString() == "Laptop")
            {
                (productFormPanel.Children[1] as TextBox)?.Clear();
                (productFormPanel.Children[3] as TextBox)?.Clear();
                (productFormPanel.Children[5] as TextBox)?.Clear();
                ComboBox comboBox1 = (productFormPanel.Children[7] as ComboBox);
                if (comboBox1 != null && comboBox1.Items.Count > 0)
                {
                    comboBox1.SelectedIndex = -1;
                }
            }

            if (selectedCategory.Content.ToString() == "Smartphone")
            {
                // Wyczyść pola specyficzne dla smartfona
                (productFormPanel.Children[1] as TextBox)?.Clear();
                (productFormPanel.Children[3] as TextBox)?.Clear();
                (productFormPanel.Children[5] as TextBox)?.Clear();
                (productFormPanel.Children[7] as TextBox)?.Clear();
                (productFormPanel.Children[9] as TextBox)?.Clear();
                (productFormPanel.Children[11] as TextBox)?.Clear();
                ComboBox comboBox = (productFormPanel.Children[13] as ComboBox);
                if (comboBox != null && comboBox.Items.Count > 0)
                {
                    comboBox.SelectedIndex = -1;
                }

            }
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

            AdminWindow adminWindow = new AdminWindow(sklep, _dbContext);
            adminWindow.Show();
        }
    }
}

