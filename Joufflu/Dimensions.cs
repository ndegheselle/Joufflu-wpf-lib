using System.Windows;

namespace Joufflu;

public static class Dimensions
{
    public static ComponentResourceKey CornerRadius => new ComponentResourceKey(typeof(Dimensions), "CornerRadius");

    public static ComponentResourceKey BorderThickness => new ComponentResourceKey(typeof(Dimensions), "BorderThickness");

    public static ComponentResourceKey Spacing => new ComponentResourceKey(typeof(Dimensions), "Spacing");
}
