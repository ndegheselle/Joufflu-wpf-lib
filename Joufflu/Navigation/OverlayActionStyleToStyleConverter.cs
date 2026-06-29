using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Joufflu.Navigation;

/// <summary>
/// Resolves an <see cref="OverlayActionStyle"/> to one of the library's named button styles
/// (<c>Primary</c>, <c>Secondary</c>, <c>Success</c>, <c>Danger</c>) or the default button style.
/// Binding the <c>Style</c> property directly (rather than from within a Style) is allowed.
/// </summary>
public class OverlayActionStyleToStyleConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        Application? app = Application.Current;
        if (app == null)
            return null;

        object key = value switch
        {
            OverlayActionStyle.Primary => "Primary",
            OverlayActionStyle.Secondary => "Secondary",
            OverlayActionStyle.Success => "Success",
            OverlayActionStyle.Danger => "Danger",
            _ => typeof(Button)
        };

        return app.TryFindResource(key);
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotSupportedException();
}
