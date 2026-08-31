---
title: Derived dimensions
parent: Toolkit
nav_order: 8
---

# Derived dimensions

A `Thickness` or a `CornerRadius` declared in a `ResourceDictionary` is baked at
parse time. Its `Left`/`Top`/`Right`/`Bottom` (or `TopLeft`/`TopRight`/…) are
plain CLR properties, not dependency properties, so they can only be fed with
`StaticResource` and never follow a later change of the scalar they were built
from:

```xml
<!-- Baked at load: editing Dimensions.Thickness at runtime does nothing here -->
<Thickness x:Key="MenuBorderThickness"
           Right="{StaticResource {x:Static joufflu:Dimensions.Thickness}}" />
```

`Derive.BorderThickness`, `Derive.CornerRadius` and `Derive.Margin` build the
value on the element instead, from a real `DynamicResource`. A scalar edited at
runtime — by the [theme customizer](customize-theme.html), for instance — flows
straight through, and no derived resource key has to be declared or re-pushed by
hand.

## Factors

Each of the three properties has a matching factor, a `Thickness` (or a
`CornerRadius`) whose components multiply the derived value side by side:

| Factor component | Result |
| --- | --- |
| `0` | The side or corner is dropped |
| `1` | It is kept as is |
| anything else | It is scaled — `2` for a doubled margin, `0.5` for a halved radius |

The default is `1,1,1,1`, so the derived value is applied whole.

## Derive.BorderThickness

Point `Derive.BorderThickness` at a resource key and pick the sides with
`Derive.BorderThicknessFactor`, in the usual `Left,Top,Right,Bottom` order.

```xml
<!-- Right edge only, following Dimensions.Thickness live -->
<Border extensions:Derive.BorderThickness="{x:Static joufflu:Dimensions.Thickness}"
        extensions:Derive.BorderThicknessFactor="0,0,1,0" />

<!-- Open at the bottom, with a doubled top edge -->
<Border extensions:Derive.BorderThickness="{x:Static joufflu:Dimensions.Thickness}"
        extensions:Derive.BorderThicknessFactor="1,2,1,0" />
```

Both properties work in a `Style` setter, so a control can derive its own border
without a keyed `Thickness`:

```xml
<Style TargetType="{x:Type nav:NavigationMenu}">
    <Setter Property="extensions:Derive.BorderThickness" Value="{x:Static joufflu:Dimensions.Thickness}" />
    <Setter Property="extensions:Derive.BorderThicknessFactor" Value="0,0,1,0" />
</Style>
```

## Derive.CornerRadius

Same shape, with `Derive.CornerRadiusFactor` in the
`TopLeft,TopRight,BottomRight,BottomLeft` order.

```xml
<!-- Top corners only, matching the border it sits in -->
<Border extensions:Derive.CornerRadius="{x:Static joufflu:Dimensions.Radius}"
        extensions:Derive.CornerRadiusFactor="1,1,0,0" />
```

## Derive.Margin

Same shape again, on any `FrameworkElement`, with `Derive.MarginFactor`.

```xml
<!-- Spaced everywhere but the top -->
<Border extensions:Derive.Margin="{x:Static joufflu:Dimensions.Spacing}"
        extensions:Derive.MarginFactor="1,0,1,1" />
```

## Notes

- The source resource may be a `double` (the usual case, spread over every side
  or corner before scaling) or an already built `Thickness` / `CornerRadius`,
  whose own sides are then scaled.
- `Derive.BorderThickness` applies to `Border` and to any `Control`;
  `Derive.CornerRadius` applies to `Border`; `Derive.Margin` applies to any
  `FrameworkElement`. Anything else throws.
- The derived value is written with `SetCurrentValue`, so a style trigger or an
  animation targeting `BorderThickness`, `CornerRadius` or `Margin` still takes
  over.

Snippets use these XML namespaces:

```xml
xmlns:joufflu="clr-namespace:Joufflu;assembly=Joufflu"
xmlns:extensions="clr-namespace:Joufflu.Extensions;assembly=Joufflu"
```
