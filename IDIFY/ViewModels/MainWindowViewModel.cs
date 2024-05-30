using System.Collections.ObjectModel;
using System.IO;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;

namespace IDIFY.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private DirectoryInfo? _directory = null;

    public DirectoryInfo? Directory
    {
        get => _directory;
        set
        {
            this.RaiseAndSetIfChanged(ref _directory, value);
            ScanInputDirectory();
        }
    }

    public string FilterExpression { get; set; } = "*.*";
    public string DataElementExpression { get; set; } = ".*";
    public ObservableCollection<FileInfo> InputFiles { get; set; } = [];

    private void ScanInputDirectory()
    {
        if (Directory != null)
        {
            InputFiles.Add(Directory.EnumerateFiles(FilterExpression, SearchOption.AllDirectories));
        }
    }
}
