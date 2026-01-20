using YXSFocTool.ViewModel;

namespace YXSFocTool.View;

/// <summary>
/// UpdateFirmwarePage.xaml 的交互逻辑
/// </summary>
public partial class UpdateFirmwarePage : iNKORE.UI.WPF.Modern.Controls.ContentDialog, System.Windows.Markup.IComponentConnector
{
    public UpdateFirmwareViewModel ViewModel { get; } = new( );
    public UpdateFirmwarePage( )
    {
        InitializeComponent( );
        this.DataContext = this.ViewModel;
        this.PreviewKeyDown += (s, e) => e.Handled = true;
    }
}
