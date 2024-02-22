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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ElectronicStore;

namespace GUIProjekt
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        private readonly SklepDbContext _dbContext;
        Sklep sklep;
        public MainWindow(SklepDbContext dbContext)
        {
            _dbContext = dbContext;
            InitializeComponent();
            //sklep = (Sklep)Sklep.OdczytajZXml("sklep.xml");
            sklep = (Sklep)Sklep.OdczytajZBazy(_dbContext);
            Closing += MainWindow_Closing;
        }
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var storyboardLaptop = FindResource("scenorysRotacji") as Storyboard;
            var storyboardPhone = FindResource("scenorysRotacji2") as Storyboard;

            storyboardLaptop?.Begin();
            storyboardPhone?.Begin();
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {

            string enteredUsername = txtUsername.Text;
            string enteredPassword = txtPassword.Password;

            if (sklep != null && sklep.Pracownicy != null)
            {
                bool t = sklep.Pracownicy.Any(p => p.Login == enteredUsername && p.Haslo == enteredPassword);

                if (t)
                {
                    this.Hide();
                    AdminWindow  adminWindow = new AdminWindow(sklep, _dbContext);
                    adminWindow.Show();
                }
                else
                {
                    MessageBox.Show("Login or password is incorrect. Please try again.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show($"Sklep is {(sklep == null ? "null" : "not null")}");
                MessageBox.Show($"sklep.Pracownicy {(sklep?.Pracownicy == null ? "null" : "not null")}");

            }
        }

        private void CheckBox_Changed(object sender, RoutedEventArgs e)
        {
            if (revealModeCheckBox.IsChecked == true)
            {
                txtPassword.Visibility = Visibility.Collapsed;
                txtPasswordRevealed.Visibility = Visibility.Visible;
                txtPasswordRevealed.Text = txtPassword.Password;
            }
            else
            {
                txtPassword.Visibility = Visibility.Visible;
                txtPasswordRevealed.Visibility = Visibility.Collapsed;
            }
        }
    }
}
