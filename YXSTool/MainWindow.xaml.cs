using System.Windows;
using System.Windows.Input;
namespace YXSTool;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow( )
    {
        InitializeComponent( );
        this.MouseMove += (s, e) => { if(e.LeftButton == MouseButtonState.Pressed) this.DragMove( ); };
        this.MouseDoubleClick += MainWindow_MouseDoubleClick;
    }

    private void MainWindow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if(e.ChangedButton == MouseButton.Left)
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