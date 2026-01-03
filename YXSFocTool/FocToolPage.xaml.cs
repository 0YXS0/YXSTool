using System.Windows.Controls;

namespace YXSFocTool;

/// <summary>
/// FocToolPage.xaml 的交互逻辑
/// </summary>
public partial class FocToolPage : UserControl
{
    public FocToolPage( )
    {
        InitializeComponent( );
        this.PreviewMouseDoubleClick += (_, e) => e.Handled = true;
        this.MouseMove += (_, e) => e.Handled = true;
    }

    private void AutoSuggestBox_IsEnabledChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
    {

    }
}
