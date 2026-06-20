using System.Windows;

namespace Joufflu;

public static class Colors
{
    public static ComponentResourceKey ForegroundColor => new ComponentResourceKey(typeof(Colors), "ForegroundColor");
    public static ComponentResourceKey Foreground100Color => new ComponentResourceKey(typeof(Colors), "Foreground100Color");
    public static ComponentResourceKey Foreground200Color => new ComponentResourceKey(typeof(Colors), "Foreground200Color");

    public static ComponentResourceKey BackgroundColor => new ComponentResourceKey(typeof(Colors), "BackgroundColor");
    public static ComponentResourceKey Background100Color => new ComponentResourceKey(typeof(Colors), "Background100Color");
    public static ComponentResourceKey Background200Color => new ComponentResourceKey(typeof(Colors), "Background200Color");

    public static ComponentResourceKey BorderColor => new ComponentResourceKey(typeof(Brushes), "BorderColor");
    public static ComponentResourceKey Border100Color => new ComponentResourceKey(typeof(Brushes), "Border100Color");
    public static ComponentResourceKey Border200Color => new ComponentResourceKey(typeof(Brushes), "Border200Color");

    // Primary
    public static ComponentResourceKey PrimaryColor => new ComponentResourceKey(typeof(Colors), "PrimaryColor");
    public static ComponentResourceKey PrimaryContentColor => new ComponentResourceKey(typeof(Colors), "PrimaryContentColor");

    // Secondary
    public static ComponentResourceKey SecondaryColor => new ComponentResourceKey(typeof(Colors), "SecondaryColor");
    public static ComponentResourceKey SecondaryContentColor => new ComponentResourceKey(typeof(Colors), "SecondaryContentColor");

    // Accent
    public static ComponentResourceKey AccentColor => new ComponentResourceKey(typeof(Colors), "AccentColor");
    public static ComponentResourceKey AccentContentColor => new ComponentResourceKey(typeof(Colors), "AccentContentColor");

    // Success
    public static ComponentResourceKey SuccessColor => new ComponentResourceKey(typeof(Colors), "SuccessColor");
    public static ComponentResourceKey SuccessContentColor => new ComponentResourceKey(typeof(Colors), "SuccessContentColor");

    // Info
    public static ComponentResourceKey InfoColor => new ComponentResourceKey(typeof(Colors), "InfoColor");
    public static ComponentResourceKey InfoContentColor => new ComponentResourceKey(typeof(Colors), "InfoContentColor");

    // Warning
    public static ComponentResourceKey WarningColor => new ComponentResourceKey(typeof(Colors), "WarningColor");
    public static ComponentResourceKey WarningContentColor => new ComponentResourceKey(typeof(Colors), "WarningContentColor");

    // Danger
    public static ComponentResourceKey DangerColor => new ComponentResourceKey(typeof(Colors), "DangerColor");
    public static ComponentResourceKey DangerContentColor => new ComponentResourceKey(typeof(Colors), "DangerContentColor");
}

public static class Brushes
{
    public static ComponentResourceKey ForegroundBrush => new ComponentResourceKey(typeof(Brushes), "ForegroundBrush");
    public static ComponentResourceKey Foreground100Brush => new ComponentResourceKey(typeof(Brushes), "Foreground100Brush");
    public static ComponentResourceKey Foreground200Brush => new ComponentResourceKey(typeof(Brushes), "Foreground200Brush");

    public static ComponentResourceKey BackgroundBrush => new ComponentResourceKey(typeof(Brushes), "BackgroundBrush");
    public static ComponentResourceKey Background100Brush => new ComponentResourceKey(typeof(Brushes), "Background100Brush");
    public static ComponentResourceKey Background200Brush => new ComponentResourceKey(typeof(Brushes), "Background200Brush");

    public static ComponentResourceKey BorderBrush => new ComponentResourceKey(typeof(Brushes), "BorderBrush");
    public static ComponentResourceKey Border100Brush => new ComponentResourceKey(typeof(Brushes), "Border100Brush");
    public static ComponentResourceKey Border200Brush => new ComponentResourceKey(typeof(Brushes), "Border200Brush");

    // Primary
    public static ComponentResourceKey PrimaryBrush => new ComponentResourceKey(typeof(Brushes), "PrimaryBrush");
    public static ComponentResourceKey PrimaryContentBrush => new ComponentResourceKey(typeof(Brushes), "PrimaryContentBrush");

    // Secondary
    public static ComponentResourceKey SecondaryBrush => new ComponentResourceKey(typeof(Brushes), "SecondaryBrush");
    public static ComponentResourceKey SecondaryContentBrush => new ComponentResourceKey(typeof(Brushes), "SecondaryContentBrush");

    // Accent
    public static ComponentResourceKey AccentBrush => new ComponentResourceKey(typeof(Brushes), "AccentBrush");
    public static ComponentResourceKey AccentContentBrush => new ComponentResourceKey(typeof(Brushes), "AccentContentBrush");

    // Success
    public static ComponentResourceKey SuccessBrush => new ComponentResourceKey(typeof(Brushes), "SuccessBrush");
    public static ComponentResourceKey SuccessContentBrush => new ComponentResourceKey(typeof(Brushes), "SuccessContentBrush");

    // Info
    public static ComponentResourceKey InfoBrush => new ComponentResourceKey(typeof(Brushes), "InfoBrush");
    public static ComponentResourceKey InfoContentBrush => new ComponentResourceKey(typeof(Brushes), "InfoContentBrush");

    // Warning
    public static ComponentResourceKey WarningBrush => new ComponentResourceKey(typeof(Brushes), "WarningBrush");
    public static ComponentResourceKey WarningContentBrush => new ComponentResourceKey(typeof(Brushes), "WarningContentBrush");

    // Danger
    public static ComponentResourceKey DangerBrush => new ComponentResourceKey(typeof(Brushes), "DangerBrush");
    public static ComponentResourceKey DangerContentBrush => new ComponentResourceKey(typeof(Brushes), "DangerContentBrush");
}