using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Joufflu.Extensions;

/// <summary>
/// Builds a border thickness, a margin, a padding or a <see cref="CornerRadius"/> from a single scalar
/// resource, scaled by a per side or per corner factor.
/// <para>
/// A <c>&lt;Thickness&gt;</c> declared in a <see cref="ResourceDictionary"/> is baked at parse time:
/// its <c>Left</c>/<c>Top</c>/<c>Right</c>/<c>Bottom</c> are plain CLR properties, so they can only be
/// fed with <c>StaticResource</c> and never follow a later theme change. These attached properties put
/// a real <c>DynamicResource</c> on the element instead, so a scalar edited at runtime (by a theme
/// customizer, for instance) flows through without any derived key having to be re-pushed by hand.
/// </para>
/// <para>
/// The factor is itself a <see cref="Thickness"/> (or a <see cref="CornerRadius"/>) whose components
/// multiply the derived value: <c>0</c> drops a side, <c>1</c> keeps it as is, and anything else scales
/// it — <c>2</c> for a double margin, <c>0.5</c> for a half radius.
/// </para>
/// <example>
/// A border rounded on its top corners only, drawn on every side but the bottom:
/// <code>
/// &lt;Border
///     extensions:Derive.BorderThickness="{x:Static joufflu:Dimensions.Thickness}"
///     extensions:Derive.BorderThicknessFactor="1,1,1,0"
///     extensions:Derive.CornerRadius="{x:Static joufflu:Dimensions.Radius}"
///     extensions:Derive.CornerRadiusFactor="1,1,0,0" /&gt;
/// </code>
/// </example>
/// </summary>
public static class Derive
{
    /// <summary>
    /// Neutral factor, keeping every side of the derived thickness.
    /// </summary>
    private static readonly Thickness FullThickness = new(1);

    /// <summary>
    /// Neutral factor, keeping every corner of the derived radius.
    /// </summary>
    private static readonly CornerRadius FullCornerRadius = new(1);

    #region BorderThickness

    /// <summary>
    /// Resource key of the scalar (a <see cref="double"/>) or <see cref="Thickness"/> the border
    /// thickness is derived from.
    /// </summary>
    public static readonly DependencyProperty BorderThicknessProperty = DependencyProperty.RegisterAttached(
        "BorderThickness",
        typeof(object),
        typeof(Derive),
        new PropertyMetadata(null, OnBorderKeyChanged));

    /// <summary>
    /// Per side multiplier applied to the derived thickness. Defaults to <c>1,1,1,1</c>.
    /// </summary>
    public static readonly DependencyProperty BorderThicknessFactorProperty = DependencyProperty.RegisterAttached(
        "BorderThicknessFactor",
        typeof(Thickness),
        typeof(Derive),
        new PropertyMetadata(FullThickness, OnBorderFactorChanged));

    /// <summary>
    /// Holds the live value of the resource pointed at by <see cref="BorderThicknessProperty"/>.
    /// </summary>
    private static readonly DependencyProperty BorderSourceProperty = DependencyProperty.RegisterAttached(
        "BorderSource",
        typeof(object),
        typeof(Derive),
        new PropertyMetadata(null, OnBorderFactorChanged));

    public static object? GetBorderThickness(DependencyObject element) => element.GetValue(BorderThicknessProperty);

    public static void SetBorderThickness(DependencyObject element, object? value)
        => element.SetValue(BorderThicknessProperty, value);

    public static Thickness GetBorderThicknessFactor(DependencyObject element)
        => (Thickness)element.GetValue(BorderThicknessFactorProperty);

    public static void SetBorderThicknessFactor(DependencyObject element, Thickness value)
        => element.SetValue(BorderThicknessFactorProperty, value);

    private static void OnBorderKeyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => Track(d, BorderSourceProperty, e.NewValue);

    private static void OnBorderFactorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not FrameworkElement element)
            return;

        object? source = element.GetValue(BorderSourceProperty);
        if (source == null)
            return;

        Thickness value = Scale(ToThickness(source, "BorderThickness"), GetBorderThicknessFactor(element));
        element.SetCurrentValue(ResolveBorderThickness(element), value);
    }

    /// <summary>
    /// <see cref="Border"/> and <see cref="Control"/> each declare their own BorderThickness property.
    /// </summary>
    private static DependencyProperty ResolveBorderThickness(FrameworkElement element) => element switch
    {
        Border => Border.BorderThicknessProperty,
        Control => Control.BorderThicknessProperty,
        _ => throw new InvalidOperationException(
            $"Derive.BorderThickness is only supported on Border and Control, not on {element.GetType().Name}.")
    };

    #endregion

    #region CornerRadius

    /// <summary>
    /// Resource key of the scalar (a <see cref="double"/>) or <see cref="System.Windows.CornerRadius"/>
    /// the corner radius is derived from.
    /// </summary>
    public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.RegisterAttached(
        "CornerRadius",
        typeof(object),
        typeof(Derive),
        new PropertyMetadata(null, OnCornerKeyChanged));

    /// <summary>
    /// Per corner multiplier applied to the derived radius. Defaults to <c>1,1,1,1</c>.
    /// </summary>
    public static readonly DependencyProperty CornerRadiusFactorProperty = DependencyProperty.RegisterAttached(
        "CornerRadiusFactor",
        typeof(CornerRadius),
        typeof(Derive),
        new PropertyMetadata(FullCornerRadius, OnCornerFactorChanged));

    /// <summary>
    /// Holds the live value of the resource pointed at by <see cref="CornerRadiusProperty"/>.
    /// </summary>
    private static readonly DependencyProperty CornerSourceProperty = DependencyProperty.RegisterAttached(
        "CornerSource",
        typeof(object),
        typeof(Derive),
        new PropertyMetadata(null, OnCornerFactorChanged));

    public static object? GetCornerRadius(DependencyObject element) => element.GetValue(CornerRadiusProperty);

    public static void SetCornerRadius(DependencyObject element, object? value)
        => element.SetValue(CornerRadiusProperty, value);

    public static CornerRadius GetCornerRadiusFactor(DependencyObject element)
        => (CornerRadius)element.GetValue(CornerRadiusFactorProperty);

    public static void SetCornerRadiusFactor(DependencyObject element, CornerRadius value)
        => element.SetValue(CornerRadiusFactorProperty, value);

    private static void OnCornerKeyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => Track(d, CornerSourceProperty, e.NewValue);

    private static void OnCornerFactorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not FrameworkElement element)
            return;

        object? source = element.GetValue(CornerSourceProperty);
        if (source == null)
            return;

        CornerRadius value = Scale(ToCornerRadius(source), GetCornerRadiusFactor(element));
        element.SetCurrentValue(ResolveCornerRadius(element), value);
    }

    private static DependencyProperty ResolveCornerRadius(FrameworkElement element) => element switch
    {
        Border => Border.CornerRadiusProperty,
        _ => throw new InvalidOperationException(
            $"Derive.CornerRadius is only supported on Border, not on {element.GetType().Name}.")
    };

    #endregion

    #region Margin

    /// <summary>
    /// Resource key of the scalar (a <see cref="double"/>) or <see cref="Thickness"/> the margin is
    /// derived from.
    /// </summary>
    public static readonly DependencyProperty MarginProperty = DependencyProperty.RegisterAttached(
        "Margin",
        typeof(object),
        typeof(Derive),
        new PropertyMetadata(null, OnMarginKeyChanged));

    /// <summary>
    /// Per side multiplier applied to the derived margin. Defaults to <c>1,1,1,1</c>.
    /// </summary>
    public static readonly DependencyProperty MarginFactorProperty = DependencyProperty.RegisterAttached(
        "MarginFactor",
        typeof(Thickness),
        typeof(Derive),
        new PropertyMetadata(FullThickness, OnMarginFactorChanged));

    /// <summary>
    /// Holds the live value of the resource pointed at by <see cref="MarginProperty"/>.
    /// </summary>
    private static readonly DependencyProperty MarginSourceProperty = DependencyProperty.RegisterAttached(
        "MarginSource",
        typeof(object),
        typeof(Derive),
        new PropertyMetadata(null, OnMarginFactorChanged));

    public static object? GetMargin(DependencyObject element) => element.GetValue(MarginProperty);

    public static void SetMargin(DependencyObject element, object? value) => element.SetValue(MarginProperty, value);

    public static Thickness GetMarginFactor(DependencyObject element)
        => (Thickness)element.GetValue(MarginFactorProperty);

    public static void SetMarginFactor(DependencyObject element, Thickness value)
        => element.SetValue(MarginFactorProperty, value);

    private static void OnMarginKeyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => Track(d, MarginSourceProperty, e.NewValue);

    private static void OnMarginFactorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not FrameworkElement element)
            return;

        object? source = element.GetValue(MarginSourceProperty);
        if (source == null)
            return;

        Thickness value = Scale(ToThickness(source, "Margin"), GetMarginFactor(element));
        element.SetCurrentValue(FrameworkElement.MarginProperty, value);
    }

    #endregion

    #region Scaled (overridable)

    /// <summary>
    /// Base value (a <see cref="double"/> or a <see cref="Thickness"/>) a scaled property is derived
    /// from. Feed it a <c>DynamicResource</c> so a scalar edited at runtime still flows through.
    /// <para>
    /// Unlike <see cref="MarginProperty"/> and its siblings, this pairs with the <see cref="Scaler"/>
    /// converter inside a plain <c>Padding</c> (or <c>Margin</c>, …) setter rather than pushing the value
    /// with <c>SetCurrentValue</c>. The derived value then lands at style setter precedence, so a consumer
    /// local value or a more specific style still overrides it — the way a control property should behave.
    /// </para>
    /// </summary>
    public static readonly DependencyProperty BaseProperty = DependencyProperty.RegisterAttached(
        "Base",
        typeof(object),
        typeof(Derive),
        new PropertyMetadata(null));

    /// <summary>
    /// Per side multiplier applied to <see cref="BaseProperty"/>. Defaults to <c>1,1,1,1</c>.
    /// </summary>
    public static readonly DependencyProperty FactorProperty = DependencyProperty.RegisterAttached(
        "Factor",
        typeof(Thickness),
        typeof(Derive),
        new PropertyMetadata(FullThickness));

    public static object? GetBase(DependencyObject element) => element.GetValue(BaseProperty);

    public static void SetBase(DependencyObject element, object? value) => element.SetValue(BaseProperty, value);

    public static Thickness GetFactor(DependencyObject element) => (Thickness)element.GetValue(FactorProperty);

    public static void SetFactor(DependencyObject element, Thickness value) => element.SetValue(FactorProperty, value);

    /// <summary>
    /// Scales a base <see cref="double"/> or <see cref="Thickness"/> (value one) by a per side
    /// <see cref="Thickness"/> factor (value two) into the final <see cref="Thickness"/>.
    /// </summary>
    public static readonly IMultiValueConverter Scaler = new ThicknessScaler();

    private sealed class ThicknessScaler : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 2 || values[0] is not { } source || values[1] is not Thickness factor)
                return DependencyProperty.UnsetValue;

            return Scale(ToThickness(source, "Base"), factor);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            => throw new NotSupportedException();
    }

    #endregion

    #region Resolution

    /// <summary>
    /// Points <paramref name="source"/> at the resource <paramref name="key"/>, so the derived value
    /// follows every later change of that resource.
    /// </summary>
    private static void Track(DependencyObject d, DependencyProperty source, object? key)
    {
        if (d is not FrameworkElement element)
            return;

        if (key == null)
            element.SetValue(source, null);
        else
            element.SetResourceReference(source, key);
    }

    internal static Thickness ToThickness(object source, string property) => source switch
    {
        Thickness thickness => thickness,
        IConvertible convertible => new Thickness(convertible.ToDouble(null)),
        _ => throw new InvalidOperationException(
            $"Derive.{property} expects a Thickness or a numeric resource, got {source.GetType().Name}.")
    };

    private static CornerRadius ToCornerRadius(object source) => source switch
    {
        CornerRadius radius => radius,
        IConvertible convertible => new CornerRadius(convertible.ToDouble(null)),
        _ => throw new InvalidOperationException(
            $"Derive.CornerRadius expects a CornerRadius or a numeric resource, got {source.GetType().Name}.")
    };

    internal static Thickness Scale(Thickness value, Thickness factor) => new(
        value.Left * factor.Left,
        value.Top * factor.Top,
        value.Right * factor.Right,
        value.Bottom * factor.Bottom);

    private static CornerRadius Scale(CornerRadius value, CornerRadius factor) => new(
        value.TopLeft * factor.TopLeft,
        value.TopRight * factor.TopRight,
        value.BottomRight * factor.BottomRight,
        value.BottomLeft * factor.BottomLeft);

    #endregion
}
