using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Joufflu.Navigation;

/// <summary>Base type for entries hosted by a <see cref="NavigationMenu"/>.</summary>
public abstract class NavigationMenuEntry : ObservableObject
{
}

/// <summary>
/// A section title. Rendered as a label when the menu is expanded and as a simple
/// line separator when it is collapsed.
/// </summary>
public class NavigationMenuTitle : NavigationMenuEntry
{
    public NavigationMenuTitle()
    { }

    public NavigationMenuTitle(string title) => Title = title;

    public string Title { get; set; } = "";
}

/// <summary>
/// A clickable navigation entry pointing at a <see cref="Target"/> page (view model).
/// </summary>
public class NavigationMenuItem : NavigationMenuEntry
{
    private bool _isSelected;

    public string Icon { get; set; } = "";

    public string Title { get; set; } = "";

    /// <summary>The page (view model) navigated to when this item is selected.</summary>
    public object? Target { get; set; }

    public bool IsSelected
    {
        get => _isSelected;
        internal set => SetProperty(ref _isSelected, value);
    }
}

/// <summary>
/// Side menu that plugs into an <see cref="INavigator"/>. It accepts <see cref="NavigationMenuItem"/>
/// entries (icon, title, target) and <see cref="NavigationMenuTitle"/> section headers, and can
/// collapse to an icons-only rail.
/// </summary>
public class NavigationMenu : ItemsControl
{
    static NavigationMenu()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(NavigationMenu),
            new FrameworkPropertyMetadata(typeof(NavigationMenu)));
    }

    public NavigationMenu()
    {
        SelectCommand = new RelayCommand<NavigationMenuItem>(OnSelect);
        ToggleCollapseCommand = new RelayCommand(() => IsCollapsed = !IsCollapsed);
    }

    /// <summary>Selects (navigates to) the <see cref="NavigationMenuItem"/> passed as parameter.</summary>
    public ICommand SelectCommand { get; }

    /// <summary>Flips <see cref="IsCollapsed"/>.</summary>
    public ICommand ToggleCollapseCommand { get; }

    public bool IsCollapsed
    {
        get => (bool)GetValue(IsCollapsedProperty);
        set => SetValue(IsCollapsedProperty, value);
    }

    public static readonly DependencyProperty IsCollapsedProperty = DependencyProperty.Register(
        nameof(IsCollapsed), typeof(bool), typeof(NavigationMenu), new PropertyMetadata(false));

    public INavigator? Navigator
    {
        get => (INavigator?)GetValue(NavigatorProperty);
        set => SetValue(NavigatorProperty, value);
    }

    public static readonly DependencyProperty NavigatorProperty = DependencyProperty.Register(
        nameof(Navigator), typeof(INavigator), typeof(NavigationMenu),
        new PropertyMetadata(null, OnNavigatorChanged));

    private static void OnNavigatorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var menu = (NavigationMenu)d;

        if (e.OldValue is INavigator oldNavigator)
            oldNavigator.Navigated -= menu.OnNavigated;

        if (e.NewValue is INavigator newNavigator)
        {
            newNavigator.Navigated += menu.OnNavigated;
            menu.UpdateSelection(newNavigator.CurrentPage);
        }
    }

    private void OnNavigated(object? sender, object? page) => UpdateSelection(page);

    protected override void OnItemsChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        base.OnItemsChanged(e);
        UpdateSelection(Navigator?.CurrentPage);
    }

    private void OnSelect(NavigationMenuItem? item)
    {
        if (item?.Target == null)
            return;

        Navigator?.Navigate(item.Target);
    }

    private void UpdateSelection(object? currentPage)
    {
        IEnumerable source = ItemsSource ?? Items;
        foreach (object? entry in source)
        {
            if (entry is NavigationMenuItem item)
                item.IsSelected = currentPage != null && ReferenceEquals(item.Target, currentPage);
        }
    }
}
