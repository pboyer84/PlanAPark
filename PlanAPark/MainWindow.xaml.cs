using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
        
        private Style imageButtonStyle;
        private Point mousePosition;
        private Token draggedToken;
        private Token selectedToken;
        private string filename = null;
        public MainWindow()
        {
            InitializeComponent();
            imageButtonStyle = TryFindResource("ImageButtonStyle") as Style;
            LoadAircraft();
        }

        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
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

        private void SpawnNewToken(AircraftData aircraft)
        {
            var newToken = new Token();

            newToken.myImage.Source = new BitmapImage(
                    new Uri($"pack://application:,,,/Images/{aircraft.ImageFilename}",
                    UriKind.Absolute));

            newToken.myImage.Stretch = Stretch.Fill;
            newToken.Height = aircraft.LengthInMeters * MetersToPixelsScale;
            newToken.Width = aircraft.WidthInMeters * MetersToPixelsScale;
            var r = new Random();
            var x = r.Next(0, (int)airportCanvas.ActualWidth);
            var y = r.Next(0, (int)airportCanvas.ActualHeight);
            Canvas.SetLeft(newToken, x);
            Canvas.SetTop(newToken, y);
            airportCanvas.Children.Add(newToken);
        }

        private void SpawnNewToken(TokenDTO tokenDTO)
        {
            var newToken = new Token();
            
            newToken.myImage.Source = new BitmapImage(
                    new Uri(tokenDTO.AircraftImageUri,
                    UriKind.Absolute));

            newToken.myImage.Stretch = Stretch.Fill;
            newToken.Height = tokenDTO.Height;
            newToken.Width = tokenDTO.Width;
            Canvas.SetLeft(newToken, tokenDTO.Left);
            Canvas.SetTop(newToken, tokenDTO.Top);
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

        private void MenuItemSaveAs_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new SaveFileDialog
            {
                FileName = "Airshow Plan", // Default file name
                DefaultExt = ".airshow", // Default file extension
                Filter = "Airshow Plans (.airshow)|*.airshow" // Filter files by extension
            };

            bool? result = dlg.ShowDialog();

            if (result == false)
            {
                return;
            }

            Title = $"Plan A Park - {dlg.FileName}";

            using var s = dlg.OpenFile();
            var items = airportCanvas.Children.OfType<Token>().ToArray();
            var dtos = new TokenDTO[items.Length];
            for (int i = 0; i < items.Length; i++)
            {
                var dto = new TokenDTO()
                {
                    Top = Canvas.GetTop(items[i]),
                    Left = Canvas.GetLeft(items[i]),
                    Rotation = (items[i].RenderTransform as RotateTransform)?.Angle ?? 0,
                    AircraftImageUri = items[i].myImage.Source.ToString(),
                    Width = items[i].Width,
                    Height = items[i].Height
                };
                dtos[i] = dto;
            }
            AirshowLayoutDTO airshowLayoutDto = new AirshowLayoutDTO()
            {
                TokenDTOs = dtos
            };

            var formatter = new BinaryFormatter();
            formatter.Serialize(s, airshowLayoutDto);
        }

        private void LoadAircraft()
        {
            var aircraft = Resources.Values.OfType<AircraftData>();
            foreach (var data in aircraft)
            {
                var buttonImage = new Image();
                buttonImage.Source = new BitmapImage(
                    new Uri($"pack://application:,,,/Images/{data.ImageFilename}", 
                    UriKind.Absolute));

                buttonImage.Width = 48;
                buttonImage.Height = 48;

                var button = new Button();
                button.Style = imageButtonStyle;
                button.Content = buttonImage;
                button.Height = 48;
                button.Width = 48;
                button.Click += (object sender, RoutedEventArgs e) =>
                {
                    SpawnNewToken(data);
                };
                pnButtons.Children.Add(button);
            }   
        }

        private void MenuItemOpen_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog()
            {
                FileName = "Airshow Plan", // Default file name
                DefaultExt = ".airshow", // Default file extension
                Filter = "Airshow Plans (.airshow)|*.airshow" // Filter files by extension
            };

            bool? result = dlg.ShowDialog();

            if (result == false)
            {
                return;
            }

            Title = $"Plan A Park - {dlg.FileName}";

            using var s = dlg.OpenFile();
            var formatter = new BinaryFormatter();
            var dtoLayout = formatter.Deserialize(s) as AirshowLayoutDTO;
            foreach (var dto in dtoLayout.TokenDTOs)
            {
                SpawnNewToken(dto);
            }
        }

        private void MenuItemExportPng_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new SaveFileDialog
            {
                FileName = Path.ChangeExtension(filename ?? "untitled", "png"), // Default file name
                DefaultExt = ".png", // Default file extension
                Filter = "PNG (.png)|*.png" // Filter files by extension
            };

            bool? result = dlg.ShowDialog();

            if (result == false)
            {
                return;
            }

            var pngRect = new Rect(airportCanvas.RenderSize);
            var renderTargetBitmap = new RenderTargetBitmap((int)pngRect.Right, (int)pngRect.Bottom, 96d, 96d, PixelFormats.Default);
            renderTargetBitmap.Render(airportCanvas);
            var pngEncoder = new PngBitmapEncoder();
            pngEncoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));            
            using var s = dlg.OpenFile();
            pngEncoder.Save(s);
        }
    }
}