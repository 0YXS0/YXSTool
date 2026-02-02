using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using iNKORE.UI.WPF.Modern.Common.IconKeys;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using YXSFocTool;

namespace YXSTool.ViewModel;

public partial class MainViewModel : ObservableObject
{
    public record TabItemInfo(FontIconData Icon, string Name, Func<ContentControl> Factory);
    static private TabItemInfo[] TabItemInfos { get; } = [
        new TabItemInfo(SegoeFluentIcons.Settings, "FocTool", ()=>new FocToolPage( )),
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
    private ObservableCollection<TabItem> tabItems = [
        new TabItem( ) { Header = TabItemInfos[0].Name, Content = new FocToolPage( ), DataContext = TabItemInfos[0]}
    ];

    [ObservableProperty]
    private int selectedTabIndex = 0;

    [RelayCommand]
    public void AddTabItem(string tabName)
    {
        var tabInfo = TabItemInfos.FirstOrDefault(t => t.Name == tabName);
        if(tabInfo is not null)
        {
            TabItems.Add(new TabItem( )
            {
                Header = tabInfo.Name,
                Content = tabInfo.Factory( ),
                DataContext = tabInfo
            });
            if(TabItems.Count == 1)
                SelectedTabIndex = 0;
        }
    }

    [RelayCommand]
    public void RemoveTabItem(TabItem tabItem)
    {
        TabItems.Remove(tabItem);
    }
}