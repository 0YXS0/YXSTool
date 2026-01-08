using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows;

namespace YXSFocTool.ViewModel;

internal partial class FocToolViewModel : ObservableObject
{
    [ObservableProperty]
    private GridLength m_ChartRowHeight = new(2.5, GridUnitType.Star);
    private GridLength m_lastChartRowHeight = new(2.5, GridUnitType.Star);

    [ObservableProperty]
    private bool m_IsChartExpanded = true;
    partial void OnIsChartExpandedChanged(bool value)
    {
        if(value)
        {
            ChartRowHeight = m_lastChartRowHeight;
        }
        else
        {
            m_lastChartRowHeight = ChartRowHeight;
            ChartRowHeight = GridLength.Auto;
        }
    }

    [ObservableProperty]
    private GridLength m_OutputRowHeight = new(1, GridUnitType.Star);
    private GridLength m_lastOutputRowHeight = new(1, GridUnitType.Star);

    [ObservableProperty]
    private bool m_IsOutputExpanded = true;
    partial void OnIsOutputExpandedChanged(bool value)
    {
        if(value)
        {
            OutputRowHeight = m_lastOutputRowHeight;
        }
        else
        {
            m_lastOutputRowHeight = OutputRowHeight;
            OutputRowHeight = GridLength.Auto;
        }
    }

    [ObservableProperty]
    private string m_ChartControlHeader = "图\n表\n数\n据\n选\n择";
}
