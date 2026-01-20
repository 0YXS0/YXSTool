using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Ports;
using System.Windows;
using YXSFocTool.Model;
using YXSFocTool.View;

namespace YXSFocTool.ViewModel;

internal partial class FocToolViewModel : ObservableObject
{
    public FocToolViewModel( )
    {
        // 获取串口列表
        var ports = SerialPort.GetPortNames( ).Order( ).ToList( );
        ports?.ForEach(p => SerialPortList.Add(p));
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
    private string m_BootloaderVersion = "-";
    public string BootloaderFilePath { get; private set; } = "";
    [ObservableProperty]
    private string m_BootloaderFileName = "选择bin文件";
    [ObservableProperty]
    private int m_BootloaderUpdateProgress = 0;
    [RelayCommand]
    private void SelectBootloaderFile( )
    {
        var openFileDialog = new Microsoft.Win32.OpenFileDialog
        {
            Filter = "Bin Files (*.bin)|*.bin",
            Title = "选择Bootloader固件文件"
        };
        if(openFileDialog.ShowDialog( ) == true)
        {
            BootloaderFilePath = openFileDialog.FileName;
            BootloaderFileName = Path.GetFileName(BootloaderFilePath);
        }
    }
    [RelayCommand]
    private async Task UpdateBootloader( )
    {
        if(!Path.Exists(BootloaderFilePath)) return;
        var dialog = new UpdateFirmwarePage( );
        dialog.ViewModel.FirmwareType = FirmwareType.Bootloader;
        dialog.ViewModel.FirmwareFilePath = BootloaderFilePath;
        await dialog.ShowAsync( );
    }

    [ObservableProperty]
    private string m_FirmwareVersion = "-";
    public string FirmwareFilePath { get; private set; } = "";
    [ObservableProperty]
    private string m_FirmwareFileName = "选择bin文件";
    [ObservableProperty]
    private int m_FirmwareUpdateProgress = 0;
    [RelayCommand]
    private void SelectFirmwareFile( )
    {
        var openFileDialog = new Microsoft.Win32.OpenFileDialog
        {
            Filter = "Bin Files (*.bin)|*.bin",
            Title = "选择固件文件"
        };
        if(openFileDialog.ShowDialog( ) == true)
        {
            FirmwareFilePath = openFileDialog.FileName;
            FirmwareFileName = Path.GetFileName(FirmwareFilePath);
        }
    }
    [RelayCommand]
    private async Task UpdateFirmloader( )
    {
        if(!Path.Exists(FirmwareFilePath)) return;
        var dialog = new UpdateFirmwarePage( );
        dialog.ViewModel.FirmwareType = FirmwareType.Firmware;
        dialog.ViewModel.FirmwareFilePath = FirmwareFilePath;
        await dialog.ShowAsync( );
    }

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
