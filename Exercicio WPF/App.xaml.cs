using System.Configuration;
using System.Data;
using System.Windows;
using Pomelo.EntityFrameworkCore.MySql;
using Microsoft.Extensions.DependencyInjection;
using Exercicio_WPF.Model;
using Microsoft.EntityFrameworkCore;
using Exercicio_WPF.Controller;

namespace Exercicio_WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;

        public App()
        {
            var services = new ServiceCollection();

            services.AddDbContext<AppDbContext>(options =>
                options.UseMySql("Server=localhost;Database=testdev;User=sa;Password=admin;",
                new MySqlServerVersion(new Version(8, 0, 27))));

            services.AddTransient<ProdutoController>();

            serviceProvider = services.BuildServiceProvider();
        }

        public ServiceProvider GetServiceProvider()
        {
            return serviceProvider;
        }
    }

}
