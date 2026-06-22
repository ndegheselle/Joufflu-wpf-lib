using System.Windows;

namespace Joufflu;

public enum ControlSize { xs, sm, md, lg }

public static class ControlProperties
{
    public static readonly DependencyProperty SizeProperty =
        DependencyProperty.RegisterAttached(
            "Size",
            typeof(ControlSize),
            typeof(ControlProperties),
            new FrameworkPropertyMetadata(ControlSize.md, FrameworkPropertyMetadataOptions.Inherits));

    public static ControlSize GetSize(DependencyObject obj) => (ControlSize)obj.GetValue(SizeProperty);
    public static void SetSize(DependencyObject obj, ControlSize value) => obj.SetValue(SizeProperty, value);

    public static readonly DependencyProperty IsSquareProperty =
        DependencyProperty.RegisterAttached(
            "IsSquare",
            typeof(bool),
            typeof(ControlProperties),
            new FrameworkPropertyMetadata(false));

    public static bool GetIsSquare(DependencyObject obj) => (bool)obj.GetValue(IsSquareProperty);
    public static void SetIsSquare(DependencyObject obj, bool value) => obj.SetValue(IsSquareProperty, value);
}
