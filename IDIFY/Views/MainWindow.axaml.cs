using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using Avalonia.Threading;
using IDIFY.ViewModels;

namespace IDIFY.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private MainWindowViewModel ViewModel => DataContext is MainWindowViewModel vm ? vm : throw new ApplicationException("ViewModel is gone!");

    /// <summary>
    ///   A value indicating whether the UI is currently busy
    /// </summary>
    private bool _isBusy;

    /// <summary>
    /// Sets the busystate as busy.
    /// </summary>
    public void SetBusyState()
    {
        SetBusyState(true);
    }

    /// <summary>
    /// Sets the busystate to busy or not busy.
    /// </summary>
    /// <param name="busy">if set to <c>true</c> the application is now busy.</param>
    private void SetBusyState(bool busy)
    {
        if (busy != _isBusy)
        {
            _isBusy = busy;
            Cursor = busy ? new Cursor(StandardCursorType.Wait) : Cursor.Default;

            if (_isBusy)
            {
                new DispatcherTimer(TimeSpan.FromSeconds(0), DispatcherPriority.ApplicationIdle, dispatcherTimer_Tick); //, Application.Current.Dispatcher
            }
        }
    }

    /// <summary>
    /// Handles the Tick event of the dispatcherTimer control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    private void dispatcherTimer_Tick(object sender, EventArgs e)
    {
        if (sender is DispatcherTimer dispatcherTimer)
        {
            SetBusyState(false);
            dispatcherTimer.Stop();
        }
    }
}