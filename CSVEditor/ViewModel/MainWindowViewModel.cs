using CSVEditor.Commands;
using System.Data;
using System.Windows.Input;

namespace CSVEditor.ViewModel
{
    public class MainWindowViewModel
    {
        public DataTable Data { get; set; }

        public ICommand OpenCommand { get; }

        public MainWindowViewModel() {
            Data = new DataTable();
            Data.Columns.Add("test1");
            Data.Rows.Add(Data.NewRow());

            OpenCommand = new SimpleCommand( Open );
        }

        void Open()
        {
            Data.Rows.Add(Data.NewRow());
        }
    }
}
