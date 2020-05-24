using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PlanAPark
{
    /// <summary>
    /// Interaction logic for Token.xaml
    /// </summary>
    public partial class Token : UserControl
    {
        private Thickness Off = new Thickness(0);
        private Thickness On = new Thickness(2);

        private double rotation = 0d;
        public Token()
        {
            InitializeComponent();
        }

        public void Rotate(double angle)
        {
            var centerY = Height / 2;
            var centerX = Width / 2;
            rotation += angle;
            var r = new RotateTransform(rotation, centerX, centerY);
            
            RenderTransform = r;
        }

        public void ShowBorder()
        {
            myBorder.BorderThickness = On;
        }

        public void HideBorder()
        {
            myBorder.BorderThickness = Off;
        }
    }
}