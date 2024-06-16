using System.Collections.ObjectModel;
using System.IO;
using System.Reactive;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;

namespace IDIFY.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public static string DataElementExpressionStatic = "\\..*";
    
    private DirectoryInfo? _directory = null;
    private string _filterExpression = "*.*";

    public DirectoryInfo? Directory
    {
        get => _directory;
        set
        {
            this.RaiseAndSetIfChanged(ref _directory, value);
            ScanInputDirectory();
        }
    }

    public string FilterExpression
    {
        get => _filterExpression;
        set
        {
            this.RaiseAndSetIfChanged(ref _filterExpression, value);
            ScanInputDirectory();
        }
    }

    public string DataElementExpression
    {
        get => DataElementExpressionStatic;
        set
        {
            DataElementExpressionStatic = value;   
        }
    }
    
    public ObservableCollection<FileInfo> InputFiles { get; set; } = [];
    
    public ReactiveCommand<Unit, Unit> ApplyInputParameters { get; }

    public MainWindowViewModel()
    {
        ApplyInputParameters = ReactiveCommand.Create(ScanInputDirectory);
    }

    private void ScanInputDirectory()
    {
        if (Directory != null)
        {
            InputFiles.Clear();
            InputFiles.Add(Directory.EnumerateFiles(FilterExpression, SearchOption.AllDirectories));
        }
    }
}
