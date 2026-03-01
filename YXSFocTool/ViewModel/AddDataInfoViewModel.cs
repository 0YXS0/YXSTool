using CommunityToolkit.Mvvm.ComponentModel;
using YXSFocTool.Model;
using DataType = YXSFocTool.Model.DataType;

namespace YXSFocTool.ViewModel;

public partial class AddDataInfoViewModel : ObservableObject
{
    public static DataType[] DataTypeList { get; } = [DataType.Int8, DataType.UInt8, DataType.Int16, DataType.UInt16, DataType.Int32, DataType.UInt32, DataType.Float, DataType.Double];

    [ObservableProperty]
    private DataType m_SelectedDataType = DataType.Int8;

    [ObservableProperty]
    private string m_Name = "test";

    [ObservableProperty]
    private int m_VerID = 0x00;

    public DataInfo ToDataInfo( ) => new(SelectedDataType, Name, VerID);
}

