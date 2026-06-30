using Joufflu.Data.Shared;
using Joufflu.Assets.Fonts;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace Joufflu.Data.Schema.Components
{
    public partial class IconDataType : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register(
            nameof(Type),
            typeof(EnumDataType),
            typeof(IconDataType),
            new PropertyMetadata(EnumDataType.String, (d, e) => ((IconDataType)d).OnTypeChanged()));

        /// <summary>
        /// Type of the value represented by this icon.
        /// </summary>
        public EnumDataType Type
        {
            get { return (EnumDataType)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        public string Icon { get; private set; } = LucideFontIcons.Quote;
        public bool WithText { get; set; } = true;

        public IconDataType()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Change the icon based on the type of value.
        /// </summary>
        private void OnTypeChanged()
        {
            Icon = Type switch
            {
                EnumDataType.Object => LucideFontIcons.Braces,
                EnumDataType.Array => LucideFontIcons.Brackets,
                EnumDataType.Enum => LucideFontIcons.ListOrdered,
                EnumDataType.String => LucideFontIcons.Quote,
                EnumDataType.Integer => LucideFontIcons.Hash,
                EnumDataType.Decimal => LucideFontIcons.Hash,
                EnumDataType.Boolean => LucideFontIcons.Check,
                EnumDataType.DateTime => LucideFontIcons.Calendar,
                EnumDataType.TimeSpan => LucideFontIcons.Timer,
                _ => LucideFontIcons.CircleHelp
            };
        }
    }
}
