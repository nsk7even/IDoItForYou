using System;
using System.IO;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using IDIFY.ViewModels;

namespace IDIFY.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private MainWindowViewModel ViewModel => DataContext is MainWindowViewModel vm ? vm : throw new ApplicationException("ViewModel is gone!");

    private async void ChooseDirectory_OnClick(object? sender, RoutedEventArgs e)
    {
        var opts = new FolderPickerOpenOptions();
        opts.Title = "Select directory ...";
        
        var folder = await StorageProvider.OpenFolderPickerAsync(opts);
        var path = folder.FirstOrDefault()?.TryGetLocalPath();
        
        if (path != null)
        {
            ViewModel.Directory = new DirectoryInfo(path);
        }
    }
}