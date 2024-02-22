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

namespace GUIElectronicStore
{
    /// <summary>
    /// Logika interakcji dla klasy ProductsInStock.xaml
    /// </summary>
    public partial class ProductsInStock : Window
    {
        private readonly SklepDbContext _dbContext;

        Sklep sklep;
        public ProductsInStock(SklepDbContext dbContext)
        {
            _dbContext = dbContext;
            InitializeComponent();
            //sklep = (Sklep)Sklep.OdczytajZXml("sklep.xml");
            sklep = (Sklep)Sklep.OdczytajZBazy(_dbContext);
            ShowProductsInStock();
        }
        private void ShowProductsInStock()
        {
            productsListBox.Items.Clear();

            foreach (var produkt in sklep.ProduktyNaStanie)
            {
                productsListBox.Items.Add(produkt.ToString());
            }
        }
    }
}
