using System.Windows;
using System.Windows.Controls;

namespace YXSTool.Behavior;

/// <summary>
/// 用于显示控件ContextMenu的附加行为
/// 同时将Button的DataContext注入到ContextMenu中
/// </summary>
public static class ShowContextMenuBehavior
{
    public static bool GetShowContextMenu(DependencyObject obj)
    {
        return (bool)obj.GetValue(ShowContextMenuProperty);
    }

    public static void SetShowContextMenu(DependencyObject obj, bool value)
    {
        obj.SetValue(ShowContextMenuProperty, value);
    }

    public static readonly DependencyProperty ShowContextMenuProperty =
        DependencyProperty.RegisterAttached(
            "ShowContextMenu",
            typeof(bool),
            typeof(ShowContextMenuBehavior),
            new PropertyMetadata(false, OnShowContextMenuChanged));

    private static void OnShowContextMenuChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not Button button) return;

        if ((bool)e.NewValue)
        {
            button.Click += Button_Click;
        }
        else
        {
            button.Click -= Button_Click;
        }
    }

    private static void Button_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not Button button || button.ContextMenu == null) return;

        // 将Button的DataContext注入到ContextMenu中
        button.ContextMenu.DataContext = button.DataContext;
        button.ContextMenu.IsOpen = true;
    }
}
