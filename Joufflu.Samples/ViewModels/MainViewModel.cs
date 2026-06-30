using CommunityToolkit.Mvvm.ComponentModel;
using Joufflu.Assets.Fonts;
using Joufflu.Navigation;
using System.Collections.ObjectModel;

namespace Joufflu.Samples.ViewModels;

/// <summary>
/// Shell view model: owns the shared navigation services and builds the side menu.
/// </summary>
public class MainViewModel : ObservableObject
{
    public Navigator Navigator { get; } = new();

    public OverlayService Overlays { get; } = new();

    public ToastService Toasts { get; } = new();

    public ObservableCollection<NavigationMenuEntry> Menu { get; } = new();

    public MainViewModel()
    {
        var buttons = new ButtonsViewModel();
        var inputs = new InputsViewModel();
        var display = new DisplayViewModel();
        var custom = new CustomControlsViewModel();
        var helpers = new HelpersViewModel();
        var overlays = new OverlaysViewModel(Overlays, Toasts);
        var toasts = new ToastsViewModel(Toasts);

        Menu.Add(new NavigationMenuTitle("Controls"));
        Menu.Add(new NavigationMenuItem { Icon = LucideFontIcons.MousePointerClick, Title = "Buttons", Target = buttons });
        Menu.Add(new NavigationMenuItem { Icon = LucideFontIcons.TextCursorInput, Title = "Inputs", Target = inputs });
        Menu.Add(new NavigationMenuItem { Icon = LucideFontIcons.LayoutDashboard, Title = "Display", Target = display });
        Menu.Add(new NavigationMenuItem { Icon = LucideFontIcons.Component, Title = "Custom controls", Target = custom });

        Menu.Add(new NavigationMenuTitle("Toolkit"));
        Menu.Add(new NavigationMenuItem { Icon = LucideFontIcons.SlidersHorizontal, Title = "Helpers", Target = helpers });
        Menu.Add(new NavigationMenuItem { Icon = LucideFontIcons.SquareStack, Title = "Overlays", Target = overlays });
        Menu.Add(new NavigationMenuItem { Icon = LucideFontIcons.Bell, Title = "Toasts", Target = toasts });

        Navigator.Navigate(buttons);
    }
}
