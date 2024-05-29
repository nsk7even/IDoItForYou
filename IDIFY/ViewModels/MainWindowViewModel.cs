using System.IO;
using DynamicData.Binding;
using ReactiveUI;

namespace IDIFY.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private DirectoryInfo? _directory = null;

    public DirectoryInfo? Directory
    {
        get => _directory;
        set => this.RaiseAndSetIfChanged(ref _directory, value);
    }

    public string FilterExpression { get; set; } = "*.*";
    public string DataElementExpression { get; set; } = ".*";
}