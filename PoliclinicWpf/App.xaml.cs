using System.Configuration;
using System.Data;
using System.Windows;
using Policlinic_EF.Model;
using PoliclinicWpf.ModelView;

namespace PoliclinicWpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Exit += App_Exit;
        }

        private void App_Exit(object sender, ExitEventArgs e)
        {
            // Ваш код для сохранения изменений в базе данных Entity Framework
            // Например:
            // using (var context = new YourDbContext())
            // {
            //     context.SaveChanges();
            // }

            // Другие завершающие операции
            using (var context = new ApplicationContext())
            {
                context.SaveChanges();
            }
        }
    }

}
