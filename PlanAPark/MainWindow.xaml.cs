using System;
using System.Windows;
using System.Windows.Controls;
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

        private void SpawnNewToken(Button source, (double width, double length) planeDimensions)
        {
            var s = source.Tag as BitmapSource;
            var newToken = new Token();
            newToken.MouseDown += ImageButton_StartDrag;
            newToken.myImage.Source = s;
            newToken.myImage.Stretch = System.Windows.Media.Stretch.Fill;
            newToken.Height = planeDimensions.length * MetersToPixelsScale;
            newToken.Width = planeDimensions.width * MetersToPixelsScale;
            
            var r = new Random();
            var x = r.Next(0, (int)airportCanvas.ActualWidth);
            var y = r.Next(0, (int)airportCanvas.ActualHeight);
            Canvas.SetLeft(newToken, x);
            Canvas.SetTop(newToken, y);
            airportCanvas.Children.Add(newToken);
        }

        private void ImageButton_StartDrag(object sender, RoutedEventArgs e)
        {
            var drag = new DataObject();
            drag.SetData(typeof(Token), sender);
            DragDrop.DoDragDrop((DependencyObject)e.Source, drag, DragDropEffects.Move);
        }

        private void airportCanvas_Drop(object sender, DragEventArgs e)
        {
            base.OnDrop(e);

            var mousePoint = e.GetPosition(airportCanvas);
            var draggedToken = e.Data.GetData(typeof(Token)) as Token;
            Canvas.SetTop(draggedToken, mousePoint.Y);
            Canvas.SetLeft(draggedToken, mousePoint.X);
        }
    }
}