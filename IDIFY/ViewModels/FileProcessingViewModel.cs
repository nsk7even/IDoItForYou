using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reactive;
using DynamicData;
using IDIFY.Models;
using ReactiveUI;

namespace IDIFY.ViewModels;

public class FileProcessingViewModel : ViewModelBase
{
    private string _msg = String.Empty;
    private DirectoryInfo? _directory = null;
    private string _filterExpression = "*.*";
    private string _dataElementExpression = "\\..*";
    private ApplicationState? _state;

    public string Message { get => _msg; set => this.RaiseAndSetIfChanged(ref _msg, value); }
    
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
        get => _dataElementExpression;
        set => this.RaiseAndSetIfChanged(ref _dataElementExpression, value);
    }
    
    public ObservableCollection<FileInfo> InputFiles { get; set; } = [];
    
    public ReactiveCommand<Unit, Unit> ApplyInputParameters { get; }

    public FileProcessingViewModel()
    {
        ApplyInputParameters = ReactiveCommand.Create(ScanInputDirectory);
        _state = ServiceLocator.GetService<ApplicationState>();

        if (_state == null) Message = "ERROR: internal state is not where it should be!";
    }
    
    private void ScanInputDirectory()
    {
        if (Directory != null)
        {
            try
            {
                InputFiles.Clear();
                InputFiles.Add(Directory.EnumerateFiles(FilterExpression, SearchOption.AllDirectories));
            }
            catch (Exception e)
            {
                Message = $"ERROR: {e.Message}";
            }
        }
    }
    
}