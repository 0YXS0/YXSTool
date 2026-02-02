using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using YXSFocTool;

namespace YXSTool.ViewModel;

public partial class MainViewModel : ObservableObject
{
    public record TabItemViewModel(PackIconKind Icon, string Header, ContentControl Content);
    public record TabItemInfo(PackIconKind Icon, string Header, Func<ContentControl> Factory);
    static public TabItemInfo[] TabItemInfos { get; } = [
        new TabItemInfo(PackIconKind.Settings, "FocTool", ()=>new FocToolPage( )),
    ];

    [RelayCommand]
    public static void Close( )
    {
        System.Windows.Application.Current.Shutdown( );
    }

    [RelayCommand]
    public static void Minimize( )
    {
        var mainWindow = System.Windows.Application.Current.MainWindow;
        if(mainWindow != null)
        {
            if(mainWindow.WindowState != System.Windows.WindowState.Minimized)
            {
                mainWindow.WindowState = System.Windows.WindowState.Minimized;
            }
            else
            {
                mainWindow.WindowState = System.Windows.WindowState.Normal;
            }
        }
    }

    [ObservableProperty]
    private ObservableCollection<TabItemViewModel> tabItems = [
        new TabItemViewModel(TabItemInfos[0].Icon, TabItemInfos[0].Header, TabItemInfos[0].Factory()),
    ];

    [ObservableProperty]
    private int selectedTabIndex = 0;

    [RelayCommand]
    public void AddTabItem(string tabName)
    {
        var tabInfo = TabItemInfos.FirstOrDefault(t => t.Header == tabName);
        if(tabInfo is not null)
        {
            TabItems.Add(new TabItemViewModel(
                tabInfo.Icon,
                tabInfo.Header,
                tabInfo.Factory( )
            ));
            if(TabItems.Count >= 1)
                SelectedTabIndex = TabItems.Count - 1;
        }
    }

    [RelayCommand]
    public void RemoveTabItem(string header)
    {
        var tabInfo = TabItems.FirstOrDefault(t => t.Header == header);
        if(tabInfo is not null)
            TabItems.Remove(tabInfo);
    }
}
