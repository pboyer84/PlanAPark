using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PlanAPark
{
    /// <summary>
    /// Interaction logic for Token.xaml
    /// </summary>
    internal partial class Token : UserControl
    {
        private Thickness Off = new Thickness(0);
        private Thickness On = new Thickness(2);

        private double rotation = 0d;
        internal Token()
        {
            InitializeComponent();
        }

        internal void Rotate(double angle)
        {
            var centerY = Height / 2;
            var centerX = Width / 2;
            rotation += angle;
            var r = new RotateTransform(rotation, centerX, centerY);
            
            RenderTransform = r;
        }

        internal void ShowBorder()
        {
            myBorder.BorderThickness = On;
        }

        internal void HideBorder()
        {
            myBorder.BorderThickness = Off;
        }

        private void MenuItemDelete_Click(object sender, RoutedEventArgs e)
        {
            var canvas = Parent as Canvas;
            canvas.Children.Remove(this);
        }
    }
}