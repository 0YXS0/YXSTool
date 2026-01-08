using System.Windows.Controls;
using YXSFocTool.ViewModel;

namespace YXSFocTool;

/// <summary>
/// FocToolPage.xaml 的交互逻辑
/// </summary>
public partial class FocToolPage : UserControl
{
    internal FocToolViewModel ViewModel { get; } = new( );
    public FocToolPage( )
    {
        InitializeComponent( );
        this.DataContext = ViewModel;
    }
}
