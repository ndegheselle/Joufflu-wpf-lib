using System.Windows;
using System.Windows.Controls;

namespace Joufflu.Controls;

/// <summary>
/// A surface container with an optional header, themed from the design system
/// (background, border, corner radius). The header is hidden when not set.
/// </summary>
public class Card : HeaderedContentControl
{
    static Card()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(Card),
            new FrameworkPropertyMetadata(typeof(Card)));
    }

    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
        nameof(CornerRadius), typeof(CornerRadius), typeof(Card), new PropertyMetadata(new CornerRadius(8)));
}
