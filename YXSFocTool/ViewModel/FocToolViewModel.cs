using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Windows;

namespace YXSFocTool.ViewModel;

internal partial class FocToolViewModel : ObservableObject
{
    public FocToolViewModel( )
    {
        // 获取串口列表
        var ports = SerialPort.GetPortNames( ).Order( );
        foreach(var port in ports)
        {
            SerialPortList.Add(port);
        }
    }

    public enum PIDInfoIndex
    {
        PIDCurrentIQ,
        PIDCurrentID,
        PIDSpeed,
        PIDPosition,
    }

    [ObservableProperty]
    private bool m_IsConnected = false;

    [ObservableProperty]
    private int m_SelectedSerialPortIndex = 0;
    public ObservableCollection<string> SerialPortList { get; } = [];

    [ObservableProperty]
    private int m_SelectedBaudRateIndex = 8;
    public ObservableCollection<int> BaudRateList { get; } = [9600, 19200, 38400, 57600, 115200, 230400, 460800, 921600, 1152000];

    [ObservableProperty]
    private int m_SelectedDeviceIDIndex = 0;
    public ObservableCollection<int> DeviceIDList { get; } = [];

    [ObservableProperty]
    private string m_DeviceBootloaderVersion = "-";
    [ObservableProperty]
    private string m_DeviceBootloaderFilePath = "选择bin文件";

    [ObservableProperty]
    private string m_DeviceFirmwareVersion = "-";
    [ObservableProperty]
    private string m_DeviceFirmwareFilePath = "选择bin文件";

    [ObservableProperty]
    private bool m_IsEnablePIDDebug = false;
    [ObservableProperty]
    private bool m_IsEnableAtuoPID = true;
    [RelayCommand]
    public void SetPIDAutoMode(object? value)
    {
        if(value is string strValue && bool.TryParse(strValue, out var result))
        {
            IsEnableAtuoPID = result;
        }
    }

    [ObservableProperty]
    private int m_SelectedPIDIndex = 2;

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
