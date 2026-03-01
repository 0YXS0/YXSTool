using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using iNKORE.UI.WPF.Modern.Controls;
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
        var dialog = new UpdateFirmwareView( );
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
    private async Task UpdateFirmware( )
    {
        if(!Path.Exists(FirmwareFilePath)) return;
        var dialog = new UpdateFirmwareView( );
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

    public partial class ChartDataInfo(DataInfo info) : ObservableObject
    {
        [ObservableProperty]
        private bool m_IsShow = true;

        [ObservableProperty]
        private bool m_IsAutoUpdate = true;

        [ObservableProperty]
        private DataType m_Type = info.Type;

        [ObservableProperty]
        private string m_Name = info.Name;

        [ObservableProperty]
        private int m_ID = info.ID;

        [ObservableProperty]
        private string m_ValueAsStr = "0.000000";
    }

    [ObservableProperty]
    private int m_DeviceDataUpdataPeriod = 100;

    [ObservableProperty]
    public ObservableCollection<ChartDataInfo> m_DeviceDataInfos =
    [
        new ChartDataInfo(new DataInfo(DataType.Int8, "电流IQ", 0x00)),
        new ChartDataInfo(new DataInfo(DataType.UInt8, "电流ID", 0x01)),
        new ChartDataInfo(new DataInfo(DataType.Int16, "速度", 0x02)),
        new ChartDataInfo(new DataInfo(DataType.UInt16, "位置", 0x03)),
    ];
    [RelayCommand]
    private void SetDataAutoUpdata(ChartDataInfo info)
    {
        info.IsAutoUpdate = !info.IsAutoUpdate;
    }
    [RelayCommand]
    public async Task AddDeviceDataInfo( )
    {
        var dialog = new AddDataInfoView( );
        var res = await dialog.ShowAsync( );
        if(res == ContentDialogResult.Primary)
        {
            var info = dialog.ViewModel.ToDataInfo( );
            if(!DeviceDataInfos.Any(x => x.ID == info.ID))
            {
                var dataInfo = new ChartDataInfo(info);
                DeviceDataInfos.Add(dataInfo);
            }
        }
    }
    [RelayCommand]
    private void DelateDeviceDataInfo(ChartDataInfo info)
    {
        DeviceDataInfos.Remove(info);
    }

    [ObservableProperty]
    private int m_ChartDataBufferSize = 50000;
    [ObservableProperty]
    public ObservableCollection<ChartDataInfo> m_ChartDataInfos =
    [
        new ChartDataInfo(new DataInfo(DataType.Int8, "电流IQ", 0x00)),
        new ChartDataInfo(new DataInfo(DataType.UInt8, "电流ID", 0x01)),
        new ChartDataInfo(new DataInfo(DataType.Int16, "速度", 0x02)),
        new ChartDataInfo(new DataInfo(DataType.UInt16, "位置", 0x03)),
    ];
    [RelayCommand]
    public async Task AddChartDataInfo( )
    {
        var dialog = new AddDataInfoView( );
        var res = await dialog.ShowAsync( );
        if(res == ContentDialogResult.Primary)
        {
            var info = dialog.ViewModel.ToDataInfo( );
            if(DeviceDataInfos.Any(x => x.ID == info.ID))
            {
                var dataInfo = DeviceDataInfos.First(x => x.ID == info.ID);
                if(!ChartDataInfos.Any(x => x.ID == info.ID))
                    ChartDataInfos.Add(dataInfo);
            }
            else
            {
                var dataInfo = new ChartDataInfo(info);
                if(!DeviceDataInfos.Any(x => x.ID == info.ID))
                    ChartDataInfos.Add(dataInfo);
                DeviceDataInfos.Add(dataInfo);
            }
        }
    }
    [RelayCommand]
    private void ShowOrHideChartDataInfo(ChartDataInfo info)
    {
        info.IsShow = !info.IsShow;
    }
    [RelayCommand]
    private void DelateChartDataInfo(ChartDataInfo info)
    {
        ChartDataInfos.Remove(info);
    }

    #region 图表数据选择Expander折叠逻辑
    [ObservableProperty]
    private GridLength m_ChartColumnWidth = new(1, GridUnitType.Star);
    private GridLength m_lastChartColumnWidth = new(1, GridUnitType.Star);

    [ObservableProperty]
    private GridLength m_ChartDataColumnWidth = GridLength.Auto;
    private GridLength m_lastChartDataColumnWidth = GridLength.Auto;

    [ObservableProperty]
    private bool m_IsChartDataExpanded = false;
    partial void OnIsChartDataExpandedChanged(bool value)
    {
        if(value)
        {
            ChartColumnWidth = m_ChartColumnWidth;
            ChartDataColumnWidth = m_lastChartDataColumnWidth;
        }
        else
        {
            m_lastChartColumnWidth = ChartColumnWidth;
            m_lastChartDataColumnWidth = ChartDataColumnWidth;
            ChartColumnWidth = new(1, GridUnitType.Star);
            ChartDataColumnWidth = GridLength.Auto;
        }
    }
    #endregion

    #region 图表区Expander折叠逻辑
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
    #endregion

    [ObservableProperty]
    private uint m_TxPacketCount = 0;
    [ObservableProperty]
    private uint m_RxPacketCount = 0;
}
