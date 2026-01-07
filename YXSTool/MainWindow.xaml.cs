using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
namespace YXSTool;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow( )
    {
        InitializeComponent( );
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
        var temp = VisualTreeHelper.GetParent(e.OriginalSource as DependencyObject);
        temp = VisualTreeHelper.GetParent(temp);
        if(temp == sender && e.LeftButton == MouseButtonState.Pressed)
        {
            this.DragMove( );
        }
    }


    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        this.Close( );  // 关闭窗口
    }

    private void MinimizeButton_Click(object sender, RoutedEventArgs e)
    {
        if(this.WindowState != WindowState.Minimized)
        {
            this.WindowState = WindowState.Minimized; // 最小化窗口
        }
        else
        {
            this.WindowState = WindowState.Normal;    // 恢复窗口
        }
    }
}