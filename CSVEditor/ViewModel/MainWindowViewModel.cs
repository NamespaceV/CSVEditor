using CSVEditor.Commands;
using CsvHelper;
using Microsoft.Win32;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace CSVEditor.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private DataTable _data;

        private string _defaultPath = @"F:\ProjectCsv\DataExamples\enemies.csv";
        private string _savePath;
        private CsvHelper.Configuration.Configuration _csvConfig =
            new CsvHelper.Configuration.Configuration()
        {
            Delimiter = ",",
            IgnoreBlankLines = false
        };


        public DataTable Data => _data;

        public ICommand OpenCommand { get; }
        public ICommand SaveCommand { get; }

        public MainWindowViewModel() {
            _data = new DataTable();
            _data.Columns.Add("test1");
            _data.Columns.Add("test2");
            _data.Rows.Add(_data.NewRow());

            OpenCommand = new SimpleCommand( Open );
            SaveCommand = new SimpleCommand( Save );
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Open()
        {
            var dialog = new OpenFileDialog { InitialDirectory = _defaultPath };
            if (dialog.ShowDialog() != true)
            {
                return;
            }

            var path = dialog.FileName;
            _defaultPath = path;
            _savePath = path;

            _data = new DataTable();

            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, _csvConfig))
            {
                csv.Read();
                csv.ReadHeader();
                var header = csv.Parser.Context.HeaderRecord;
                foreach (var hearedCol in header)
                {
                    _data.Columns.Add(new DataColumn(hearedCol));
                }
                while (csv.Read())
                {
                    var dataRow = _data.NewRow();
                    foreach (var hearedCol in header)
                    {
                        dataRow[hearedCol] = csv.GetField(hearedCol);
                    }
                    _data.Rows.Add(dataRow);
                }
            }
            OnPropertyChanged(nameof(Data));
        }

        private void Save()
        {
            if (_savePath == null)
            {
                return;
            }

            using (var write = new StreamWriter(_savePath))
            using (var csv = new CsvWriter(write, _csvConfig))
            {
                var header = _data.Columns.Cast<DataColumn>().ToList();
                foreach (var hearedCol in header)
                {
                    csv.WriteField(new DataColumn(hearedCol.ColumnName));
                }
                foreach (var row in _data.AsEnumerable())
                {
                    csv.NextRecord();
                    var dataRow = _data.NewRow();
                    foreach (var column in header)
                    {
                        csv.WriteField(row[column].ToString());
                    }
                }
            }

        }
    }
}
