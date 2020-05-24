using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PlanAPark
{
    /// <summary>
    /// Interaction logic for Token.xaml
    /// </summary>
    public partial class Token : UserControl
    {
        private Thickness Off = new Thickness(0);
        private Thickness On = new Thickness(2);

        public Token()
        {
            InitializeComponent();
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
