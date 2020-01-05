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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ImageButton_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            BitmapSource s = b.Tag as BitmapSource;
            Image i = new Image();
            i.MouseDown += ImageButton_StartDrag;
            i.Source = s;
            i.Height = 48;
            i.Width = 48;
            Random r = new Random();
            var x = r.Next(0, (int)airportCanvas.ActualWidth);
            var y = r.Next(0, (int)airportCanvas.ActualHeight);
            Canvas.SetLeft(i, x);
            Canvas.SetTop(i, y);
            airportCanvas.Children.Add(i);
        }

        private void ImageButton_StartDrag(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Drag start");
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