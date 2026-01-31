using System.Windows;
using System.Windows.Input;
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
}