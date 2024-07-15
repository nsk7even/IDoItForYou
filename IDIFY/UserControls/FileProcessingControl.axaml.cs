using System;
using System.IO;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using IDIFY.ViewModels;

namespace IDIFY.UserControls;

public partial class FileProcessingControl : UserControl
{
    public static readonly StyledProperty<string> TitleProperty = AvaloniaProperty.Register<FileProcessingControl, string>(nameof(TitleProperty), "Input"); 
    
    public string Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }
    
    private FileProcessingViewModel ViewModel => DataContext is FileProcessingViewModel vm ? vm : throw new ApplicationException("ViewModel is gone!");

    public FileProcessingControl()
    {
        InitializeComponent();
        DataContext = new FileProcessingViewModel();
    }

    private async void ChooseDirectory_OnClick(object? sender, RoutedEventArgs e)
    {
        var opts = new FolderPickerOpenOptions();
        opts.Title = "Select directory ...";

        var parent = (Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
        if (parent != null)
        {
            var folder = await parent.StorageProvider.OpenFolderPickerAsync(opts);
            var path = folder.FirstOrDefault()?.TryGetLocalPath();
            
            if (path != null)
            {
                ViewModel.Directory = new DirectoryInfo(path);
            }
        }
    }
    
}