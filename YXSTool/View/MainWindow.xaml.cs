using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using YXSTool.ViewModel;

namespace YXSTool.View;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainViewModel ViewModel { get; } = new( );
    public MainWindow( )
    {
        InitializeComponent( );
        this.DataContext = ViewModel;
        this.MouseDown += (_, _) => { Keyboard.ClearFocus( ); };
    }

    private void TabControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        var temp = VisualTreeHelper.GetParent(e.OriginalSource as DependencyObject);
        temp = VisualTreeHelper.GetParent(temp);
        if(temp == sender && e.ChangedButton == MouseButton.Left)
        {
            if(this.WindowState != WindowState.Maximized)
            {
                this.WindowState = WindowState.Maximized; // 最大化窗口
            }
            else
            {
                this.WindowState = WindowState.Normal;    // 恢复窗口
            }
        }
    }

    private void TabControl_MouseMove(object sender, MouseEventArgs e)
    {
        try
        {
            var temp = VisualTreeHelper.GetParent(e.OriginalSource as DependencyObject);
            temp = VisualTreeHelper.GetParent(temp);
            if(temp == sender && e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove( );
            }
        }
        catch { }
    }
}