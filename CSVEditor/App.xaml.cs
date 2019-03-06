using Ninject;
using System.Windows;

namespace CSVEditor
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Current.MainWindow = Configure.NinjectKernel.Get<MainWindow>();
            Current.MainWindow.Show();
        }
    }
}
