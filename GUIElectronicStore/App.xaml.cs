using ElectronicStore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace GUIProjekt
{
    /// <summary>
    /// Logika interakcji dla klasy App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            using IHost host = CreateHostBuilder(e).Build();
            IServiceProvider serviceProvider = host.Services;

            SklepDbContext dbContext = host.Services.GetRequiredService<SklepDbContext>();
            MainWindow mainWindow = new MainWindow(dbContext);
            mainWindow.Closing += (a, b) => Application.Current.Shutdown();

            dbContext.Database.EnsureCreated();
            mainWindow.Show();

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(StartupEventArgs args) =>
            Host.CreateDefaultBuilder()
                .ConfigureServices((services) =>
                {
                    services.AddDbContext<SklepDbContext>(options => options.UseSqlite("FileName=./sklepDataBase.db"));
                });

    }
}
