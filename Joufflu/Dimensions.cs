using System.Windows;

namespace Joufflu;

public static class Dimensions
{
    public static ComponentResourceKey CornerRadius => new ComponentResourceKey(typeof(Dimensions), "CornerRadius");

    public static ComponentResourceKey BorderThickness => new ComponentResourceKey(typeof(Dimensions), "BorderThickness");

    public static ComponentResourceKey HorizontalSpace => new ComponentResourceKey(typeof(Dimensions), "HorizontalSpace");

    public static ComponentResourceKey VerticalSpace => new ComponentResourceKey(typeof(Dimensions), "VerticalSpace");
}
