using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;

namespace MusicCenterWPF.Windows.Shared
{
    public static class ComboBoxHelper
    {
        public static string GetPlaceholder(DependencyObject obj) =>
            (string)obj.GetValue(PlaceholderProperty);

        public static void SetPlaceholder(DependencyObject obj, string value) =>
            obj.SetValue(PlaceholderProperty, value);

        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.RegisterAttached(
                "Placeholder",
                typeof(string),
                typeof(ComboBoxHelper),
                new FrameworkPropertyMetadata(
                    defaultValue: null,
                    propertyChangedCallback: OnPlaceholderChanged)
            );

        private static void OnPlaceholderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ComboBox comboBox)
            {
                if (!comboBox.IsLoaded)
                {
                    comboBox.Loaded -= ComboBox_Loaded;
                    comboBox.Loaded += ComboBox_Loaded;
                }

                comboBox.SelectionChanged -= ComboBox_SelectionChanged;
                comboBox.SelectionChanged += ComboBox_SelectionChanged;

                if (GetOrCreateAdorner(comboBox, out PlaceholderAdorner adorner))
                    adorner.InvalidateVisual();
            }
        }

        private static void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                comboBox.Loaded -= ComboBox_Loaded;
                comboBox.IsVisibleChanged -= ComboBox_IsVisibleChanged;
                comboBox.IsVisibleChanged += ComboBox_IsVisibleChanged;
                GetOrCreateAdorner(comboBox, out _);
            }
        }
        private static void ComboBox_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is ComboBox comboBox && GetOrCreateAdorner(comboBox, out PlaceholderAdorner adorner))
            {
                adorner.Visibility = comboBox.IsVisible && comboBox.SelectedItem == null
                    ? Visibility.Visible
                    : Visibility.Hidden;
            }
        }

        private static void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox && GetOrCreateAdorner(comboBox, out PlaceholderAdorner adorner))
            {
                var parent = VisualTreeHelper.GetParent(comboBox) as UIElement;
                adorner.Visibility = comboBox.IsVisible && parent.IsVisible && comboBox.SelectedItem == null? Visibility.Visible : Visibility.Hidden;
            }
        }

        private static bool GetOrCreateAdorner(ComboBox comboBox, out PlaceholderAdorner adorner)
        {
            AdornerLayer layer = AdornerLayer.GetAdornerLayer(comboBox);

            if (layer == null)
            {
                adorner = null;
                return false;
            }

            adorner = layer.GetAdorners(comboBox)?.OfType<PlaceholderAdorner>().FirstOrDefault();

            if (adorner == null)
            {
                adorner = new PlaceholderAdorner(comboBox);
                layer.Add(adorner);
            }

            return true;
        }

    }
    public class PlaceholderAdorner : Adorner
    {
        public PlaceholderAdorner(ComboBox comboBox) : base(comboBox) { }

        protected override void OnRender(DrawingContext drawingContext)
        {
            ComboBox comboBox = (ComboBox)AdornedElement;

            string placeholder = GetPlaceholder(comboBox);
            if (string.IsNullOrEmpty(placeholder) || comboBox.SelectedItem != null)
                return;

            FormattedText text = new FormattedText(
                placeholder,
                System.Globalization.CultureInfo.CurrentCulture,
                comboBox.FlowDirection,
                new Typeface(comboBox.FontFamily, comboBox.FontStyle, comboBox.FontWeight, comboBox.FontStretch),
                comboBox.FontSize,
                SystemColors.GrayTextBrush,
                VisualTreeHelper.GetDpi(comboBox).PixelsPerDip);

            // Use padding and part content host (if exists) to calculate position
            Point offset = new Point(comboBox.Padding.Left, comboBox.Padding.Top);

            if (comboBox.Template.FindName("PART_ContentHost", comboBox) is FrameworkElement part)
            {
                Point partPos = part.TransformToAncestor(comboBox).Transform(new Point(0, 0));
                offset.X += partPos.X;
                offset.Y += partPos.Y;

                text.MaxTextWidth = Math.Max(part.ActualWidth - offset.X, 10);
                text.MaxTextHeight = Math.Max(part.ActualHeight, 10);
            }

            drawingContext.DrawText(text, offset);
        }

        private string GetPlaceholder(ComboBox comboBox)
        {
            return ComboBoxHelper.GetPlaceholder(comboBox);
        }
    }
}
