using CommunityToolkit.Mvvm.ComponentModel;
using System.IO;
using YXSFocTool.Model;

namespace YXSFocTool.ViewModel;

public partial class UpdateFirmwareViewModel : ObservableObject
{
    [ObservableProperty]
    private FirmwareType m_FirmwareType = FirmwareType.Firmware;

    [ObservableProperty]
    private string m_FirmwareVersion = "-";

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FirmwareFileName))]
    private string m_FirmwareFilePath = "";
    public string FirmwareFileName => Path.GetFileName(FirmwareFilePath);

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsUpdating))]
    [NotifyPropertyChangedFor(nameof(IsSuccess))]
    [NotifyPropertyChangedFor(nameof(ProgressStr))]
    private int m_UpdateProgress = 0;
    public bool IsUpdating => UpdateProgress >= 0;
    public bool IsSuccess => UpdateProgress == 100;
    public string ProgressStr => $"{UpdateProgress:F1}%";
}