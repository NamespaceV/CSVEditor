using CSVEditor.ViewModel;
using System.Windows;
namespace CSVEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainWindowViewModel vm)
        {
            DataContext = vm;
            InitializeComponent();
        }
    }
}
