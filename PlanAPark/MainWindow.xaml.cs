using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PlanAPark
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly double MetersToPixelsScale = 20d / 7d;
        private readonly double cessnaLengthInMeters = 8.28d;
        private readonly double cessnaWidthInMeters = 11d;
        private readonly double lancasterWidthInMeters = 31.09d;
        private readonly double lancasterLengthInMeters = 21.18d;
        private readonly double hornetLengthInMeters = 17.1d;
        private readonly double hornetWidthInMeters = 12.3d;
        private readonly double tutorLengthInMeters = 9.75d;
        private readonly double tutorWidthInMeters = 11.07d;

        private Point mousePosition;
        private Token draggedToken;
        private Token selectedToken;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ImgLancasterImageButton_Click(object sender, RoutedEventArgs e)
        {
            SpawnNewToken(sender as Button, (lancasterWidthInMeters, lancasterLengthInMeters));
        }

        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ImgCessnaImageButton_Click(object sender, RoutedEventArgs e)
        {
            SpawnNewToken(sender as Button, (cessnaWidthInMeters, cessnaLengthInMeters));
        }

        private void ImgHornetImageButton_Click(object sender, RoutedEventArgs e)
        {
            SpawnNewToken(sender as Button, (hornetWidthInMeters, hornetLengthInMeters));
        }

        private void ImgTutorImageButton_Click(object sender, RoutedEventArgs e)
        {
            SpawnNewToken(sender as Button, (tutorWidthInMeters, tutorLengthInMeters));
        }

        private void OnMouseButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (e.Source is Token)
            {
                draggedToken = null;
            }
        }

        private void OnMouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            airportCanvas.Focus();
            mousePosition = e.GetPosition(airportCanvas);
            selectedToken?.HideBorder();
            if (e.Source is Token)
            {
                draggedToken = e.Source as Token;
                selectedToken = draggedToken;
                selectedToken.ShowBorder();
            }
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (draggedToken == null)
            {
                return;
            }
            
            Point currentPosition = e.GetPosition(airportCanvas);
            double deltaX = currentPosition.X - mousePosition.X;
            double deltaY = currentPosition.Y - mousePosition.Y;
            Canvas.SetTop(draggedToken, Canvas.GetTop(draggedToken) + deltaY);
            Canvas.SetLeft(draggedToken, Canvas.GetLeft(draggedToken) + deltaX);
            mousePosition = currentPosition;
        }

        private void SpawnNewToken(Button source, (double width, double length) planeDimensions)
        {
            var s = source.Tag as BitmapSource;
            var newToken = new Token();
            
            newToken.myImage.Source = s;
            newToken.myImage.Stretch = Stretch.Fill;
            newToken.Height = planeDimensions.length * MetersToPixelsScale;
            newToken.Width = planeDimensions.width * MetersToPixelsScale;
            
            var r = new Random();
            var x = r.Next(0, (int)airportCanvas.ActualWidth);
            var y = r.Next(0, (int)airportCanvas.ActualHeight);
            Canvas.SetLeft(newToken, x);
            Canvas.SetTop(newToken, y);
            airportCanvas.Children.Add(newToken);
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.R)
            {
                selectedToken.Rotate(-15);
            }
            if (e.Key == Key.T)
            {
                selectedToken.Rotate(15);
            }
        }
    }
}