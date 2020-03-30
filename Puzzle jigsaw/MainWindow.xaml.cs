using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
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
using System.Windows.Threading;
using System.Drawing;

namespace Puzzle_jigsaw
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Backgrounds popupBackgroundWindow = null;
        private FullImage popupFullImageWindow = null;
        private Puzzle_Pieces popupPuzzlePiecesWindow = null;

        public MainWindow()
        {
            InitializeComponent();
            popupBackgroundWindow = new Backgrounds();
            popupFullImageWindow = new FullImage();
            popupPuzzlePiecesWindow = new Puzzle_Pieces();
   
        }

     

        //private static ImageList Split(Bitmap image, int width, int height)
        //{
        //    ImageList rows = new ImageList();
        //    rows.ImageSize = new Size(image.Width, height);
        //    rows.Images.AddStrip(image);
        //    ImageList cells = new ImageList();
        //    cells.ImageSize = new Size(width, height);
        //    foreach (Image row in Rows.Images)
        //    {
        //        cells.Images.AddStrip(row);
        //    }
        //    return cells;
        //}

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromSeconds(1);
            dt.Tick += dtTicker;
            dt.Start();
        }

        private int increment = 0;
        private void dtTicker(object sender, EventArgs e)
        {
            increment++;
            TimerLabel.Content = increment.ToString();
        }


        private void onclick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open_File = new OpenFileDialog();
            open_File.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
            if (open_File.ShowDialog() == true)
            {
                BitmapImage img = new BitmapImage(new Uri(open_File.FileName));
                imgPhoto.Source = img;
                popupFullImageWindow.FullImageImage.Source = img;
            }

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(open_File.FileName);
            bitmap.EndInit();

            Image croppedImage = new Image();
            croppedImage.Width = 100;
            croppedImage.Margin = new Thickness(2);

            CroppedBitmap cb = new CroppedBitmap((BitmapSource)bitmap,
            new Int32Rect(20, 20, 100, 100));
            // Set Image.Source to cropped image  
            croppedImage.Source = cb;

            popupFullImageWindow.FullImageImage.Source = cb;
        }

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            //ToolTip visibility
            if (toggle_Btn.IsChecked == true)
            {
                tt_puzzle.Visibility = Visibility.Collapsed;
                tt_Folder.Visibility = Visibility.Collapsed;
        }
            else
            {
                tt_puzzle.Visibility = Visibility.Visible;
                tt_Folder.Visibility = Visibility.Visible;
            }
}

        private void mouseclick(object sender, MouseButtonEventArgs e)
        {
            popupPuzzlePiecesWindow.Show();
        }

        private void Close(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void clickOnFull_Image(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog open_File = new OpenFileDialog();
            open_File.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";

            if (popupFullImageWindow.IsVisible == true)
                popupFullImageWindow.Visibility = Visibility.Hidden;
            else
            {
                popupFullImageWindow.Visibility = Visibility.Visible;
            }
        }

        private void clickBackground(object sender, MouseButtonEventArgs e)
        {
            popupBackgroundWindow.Show();
        }

        private class ImageList
        {
        }
    }
}
