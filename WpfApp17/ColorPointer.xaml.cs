using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp17
{
    /// <summary>
    /// Логика взаимодействия для ColorPointer.xaml
    /// </summary>
    public partial class ColorPointer : UserControl
    {
        public static readonly DependencyProperty ColorProperty;
        public static readonly DependencyProperty RedProperty;
        public static readonly DependencyProperty GreenProperty;
        public static readonly DependencyProperty BlueProperty;

        public static readonly RoutedEvent ColorChangedEvent;

        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }
        public byte Red
        {
            get { return (byte)GetValue(RedProperty); }
            set { SetValue(RedProperty, value); }
        }

        public byte Green
        {
            get { return (byte)GetValue(GreenProperty); }
            set { SetValue(GreenProperty, value); }
        }
        public byte Blue
        {
            get { return (byte)GetValue(BlueProperty); }
            set { SetValue(BlueProperty, value); }
        }
        public ColorPointer()
        {
            InitializeComponent();
        }
        static ColorPointer()
        {
            ColorProperty = DependencyProperty.Register("Color", typeof(Color), typeof(ColorPointer),
               new FrameworkPropertyMetadata(Colors.Black, new PropertyChangedCallback(OnColorChanged)));

            RedProperty = DependencyProperty.Register("Red", typeof(byte), typeof(ColorPointer),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnColorRGBChanged)));

            GreenProperty = DependencyProperty.Register("Green", typeof(byte), typeof(ColorPointer),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnColorRGBChanged)));

            BlueProperty = DependencyProperty.Register("Blue", typeof(byte), typeof(ColorPointer),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnColorRGBChanged)));

            ColorChangedEvent = EventManager.RegisterRoutedEvent("ColorChanged", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<Color>), typeof(ColorPointer));
        }


        public event RoutedPropertyChangedEventHandler<Color> ColorChanged
        {
            add { AddHandler(ColorChangedEvent, value); }
            remove { RemoveHandler(ColorChangedEvent, value); }
        }

        private static void OnColorRGBChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ColorPointer colorPointer = (ColorPointer)sender;
            Color color = colorPointer.Color;
            if (e.Property == RedProperty)
                color.R = (byte)e.NewValue;
            else if (e.Property == GreenProperty)
                color.G = (byte)e.NewValue;
            else if (e.Property == BlueProperty)
                color.B = (byte)e.NewValue;
            colorPointer.Color = color;
        }
        private static void OnColorChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ColorPointer colorPointer = (ColorPointer)sender;
            Color oldColor = (Color)e.OldValue;
            Color newColor = (Color)e.NewValue;
            colorPointer.Red = newColor.R;
            colorPointer.Green = newColor.G;
            colorPointer.Blue = newColor.B;

            RoutedPropertyChangedEventArgs<Color> args = new RoutedPropertyChangedEventArgs<Color>(oldColor, newColor);
            args.RoutedEvent = ColorChangedEvent;
            colorPointer.RaiseEvent(args);
        }

    }

}
