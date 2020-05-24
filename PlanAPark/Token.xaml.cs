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
        public Token()
        {
            InitializeComponent();
        }

        public void ToggleBorder()
        {
            if (myBorder.BorderThickness.Top == 2)
            {
                myBorder.BorderThickness = new Thickness(0);
            }
            else
            {
                myBorder.BorderThickness = new Thickness(2);
            }
        }
    }
}
