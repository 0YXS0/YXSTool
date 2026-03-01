using iNKORE.UI.WPF.Modern.Controls;
using System.Windows.Input;
using YXSFocTool.ViewModel;

namespace YXSFocTool.View;

/// <summary>
/// AddDataInfoView.xaml 的交互逻辑
/// </summary>
public partial class AddDataInfoView : ContentDialog
{
    public AddDataInfoViewModel ViewModel { get; } = new( );
    public AddDataInfoView( )
    {
        InitializeComponent( );
        this.DataContext = this.ViewModel;
        this.PreviewKeyDown += (s, e) =>
        {
            if(e.Key == Key.Enter)
                e.Handled = true;
        };
    }
}
