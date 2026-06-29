using System.Windows;
using System.Windows.Controls;

namespace Joufflu.Samples.Controls;

/// <summary>
/// Sample helper: a card that shows a live control preview together with the
/// XAML/code snippet that produced it.
/// </summary>
public class CodeExample : ContentControl
{
    static CodeExample()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(CodeExample),
            new FrameworkPropertyMetadata(typeof(CodeExample)));
    }

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
        nameof(Title), typeof(string), typeof(CodeExample), new PropertyMetadata(""));

    public string Description
    {
        get => (string)GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(
        nameof(Description), typeof(string), typeof(CodeExample), new PropertyMetadata(""));

    public string Code
    {
        get => (string)GetValue(CodeProperty);
        set => SetValue(CodeProperty, value);
    }

    public static readonly DependencyProperty CodeProperty = DependencyProperty.Register(
        nameof(Code), typeof(string), typeof(CodeExample), new PropertyMetadata(""));
}
