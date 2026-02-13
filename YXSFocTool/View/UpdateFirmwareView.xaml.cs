using iNKORE.UI.WPF.Modern.Controls;
using YXSFocTool.ViewModel;

namespace YXSFocTool.View;

/// <summary>
/// UpdateFirmwarePage.xaml 的交互逻辑
/// </summary>
public partial class UpdateFirmwareView : ContentDialog
{
    public UpdateFirmwareViewModel ViewModel { get; } = new( );
    public UpdateFirmwareView( )
    {
        InitializeComponent( );
        this.DataContext = this.ViewModel;
        this.PreviewKeyDown += (s, e) => e.Handled = true;
    }
}
