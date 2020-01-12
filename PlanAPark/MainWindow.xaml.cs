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
        public double MetersToPixelsScale = 20d / 28d;

        public double cessnaLengthInMeters = 8.28d * 4;
        public double cessnaWidthInMeters = 11d * 4;
        public double lancasterWidthInMeters = 21.18d * 4;
        public double lancasterLengthInMeters = 31.09d * 4;
        public double hornetLengthInMeters = 17.1d * 4;
        public double hornetWidthInMeters = 12.3d * 4;
        public double tutorLengthInMeters = 9.75d * 4;
        public double tutorWidthInMeters = 11.07d * 4;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ImgCessnaImageButton_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            BitmapSource s = b.Tag as BitmapSource;
            Image i = new Image();
            i.MouseDown += ImageButton_StartDrag;
            i.Source = s;
            i.Stretch = System.Windows.Media.Stretch.Fill;
            i.Height = cessnaLengthInMeters * MetersToPixelsScale;
            i.Width = cessnaWidthInMeters * MetersToPixelsScale;
            Random r = new Random();
            var x = r.Next(0, (int)airportCanvas.ActualWidth);
            var y = r.Next(0, (int)airportCanvas.ActualHeight);
            Canvas.SetLeft(i, x);
            Canvas.SetTop(i, y);
            airportCanvas.Children.Add(i);
        }

        private void ImgHornetImageButton_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            BitmapSource s = b.Tag as BitmapSource;
            Image i = new Image();
            i.Stretch = System.Windows.Media.Stretch.Fill;
            i.MouseDown += ImageButton_StartDrag;
            i.Source = s;
            i.Height = hornetLengthInMeters * MetersToPixelsScale;
            i.Width = hornetWidthInMeters * MetersToPixelsScale;
            Random r = new Random();
            var x = r.Next(0, (int)airportCanvas.ActualWidth);
            var y = r.Next(0, (int)airportCanvas.ActualHeight);
            Canvas.SetLeft(i, x);
            Canvas.SetTop(i, y);
            airportCanvas.Children.Add(i);
        }

        private void ImgTutorImageButton_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            BitmapSource s = b.Tag as BitmapSource;
            Image i = new Image();
            i.MouseDown += ImageButton_StartDrag;
            i.Source = s;
            i.Stretch = System.Windows.Media.Stretch.Fill;
            i.Height = tutorLengthInMeters * MetersToPixelsScale;
            i.Width = tutorWidthInMeters * MetersToPixelsScale;
            Random r = new Random();
            var x = r.Next(0, (int)airportCanvas.ActualWidth);
            var y = r.Next(0, (int)airportCanvas.ActualHeight);
            Canvas.SetLeft(i, x);
            Canvas.SetTop(i, y);
            airportCanvas.Children.Add(i);
        }

        private void ImgLancasterImageButton_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            BitmapSource s = b.Tag as BitmapSource;
            Image i = new Image();
            i.Stretch = System.Windows.Media.Stretch.Fill;
            i.MouseDown += ImageButton_StartDrag;
            i.Source = s;
            i.Height = lancasterLengthInMeters * MetersToPixelsScale;
            i.Width = lancasterWidthInMeters * MetersToPixelsScale;
            Random r = new Random();
            var x = r.Next(0, (int)airportCanvas.ActualWidth);
            var y = r.Next(0, (int)airportCanvas.ActualHeight);
            Canvas.SetLeft(i, x);
            Canvas.SetTop(i, y);
            airportCanvas.Children.Add(i);
        }

        private void ImageButton_StartDrag(object sender, RoutedEventArgs e)
        {
            var d = new DataObject();
            d.SetData(typeof(Image), sender);
            DragDrop.DoDragDrop((DependencyObject)e.Source, d, DragDropEffects.Move);
        }

        private void airportCanvas_Drop(object sender, DragEventArgs e)
        {
            base.OnDrop(e);
            var mousePoint = e.GetPosition(airportCanvas);
            Image i = e.Data.GetData(typeof(Image)) as Image;
            Canvas.SetTop(i, mousePoint.Y);
            Canvas.SetLeft(i, mousePoint.X);
        }
    }
}