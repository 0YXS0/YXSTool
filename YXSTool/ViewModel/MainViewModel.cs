using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using iNKORE.UI.WPF.Modern.Common.IconKeys;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using YXSFocTool;

namespace YXSTool.ViewModel;

public partial class MainViewModel : ObservableObject
{
    public record TabItemViewModel(FontIconData Icon, string Header, ContentControl Content);
    public record TabItemInfo(FontIconData Icon, string Header, Func<ContentControl> Factory);
    static public TabItemInfo[] TabItemInfos { get; } = [
        new TabItemInfo(SegoeFluentIcons.Repair, "FocTool", ()=>new FocToolPage( )),
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

    [RelayCommand]
    public static void MoveAndDoubleClick(MouseButtonEventArgs e)
    {
        var mainWindow = System.Windows.Application.Current.MainWindow;
        if(e.LeftButton == MouseButtonState.Pressed && e.ClickCount == 1)
        {
            mainWindow.DragMove( );
        }
        else if(e.ChangedButton == MouseButton.Left && e.ClickCount == 2)
        {
            if(mainWindow.WindowState != System.Windows.WindowState.Maximized)
            {
                mainWindow.WindowState = System.Windows.WindowState.Maximized; // 最大化窗口
            }
            else
            {
                mainWindow.WindowState = System.Windows.WindowState.Normal;    // 恢复窗口
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
    public void AddTabItem(object? param)
    {
        TabItemInfo? tabInfo = null;
        if(TabItemInfos.Length == 1 && param is MouseButtonEventArgs e)
        {
            e.Handled = true;
            tabInfo = TabItemInfos[0];
        }
        else if(param is string tabHeader)
        {
            tabInfo = TabItemInfos.FirstOrDefault(t => t.Header == tabHeader);
        }
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
        if(TabItems.Count > 1)
        {
            var tabInfo = TabItems.FirstOrDefault(t => t.Header == header);
            if(tabInfo is not null)
                TabItems.Remove(tabInfo);
        }
    }
}