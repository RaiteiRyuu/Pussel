using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using System;
using System.Diagnostics;
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
using System.IO;
using Path = System.IO.Path;

namespace Puzzle_jigsaw
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Backgrounds backgroundCombobox = null;
        private FullImage popupFullImageWindow = null;
        private Puzzle_Pieces popupPuzzlePiecesWindow = null;

        DispatcherTimer dt = new DispatcherTimer();
        Stopwatch sw = new Stopwatch();
        string currentTime = string.Empty;

        public MainWindow()
        {
            InitializeComponent();

            backgroundCombobox = new Backgrounds();
            popupFullImageWindow = new FullImage();
            popupPuzzlePiecesWindow = new Puzzle_Pieces();

            dt.Tick += new EventHandler(dt_Tick);
            dt.Interval = new TimeSpan(0, 0, 0, 0, 1);
        }


        void dt_Tick(object sender, EventArgs e)
        {
            if (sw.IsRunning)
            {
                TimeSpan ts = sw.Elapsed;
                currentTime = String.Format("{0:00}:{1:00}",
                ts.Minutes, ts.Seconds, 10);
                clocktxtblock.Text = currentTime;
            }
        }

        private void startbtn_Click(object sender, RoutedEventArgs e)
        {
            sw.Start();
            dt.Start();
        }

        private void stopbtn_Click(object sender, RoutedEventArgs e)
        {
            if (sw.IsRunning)
            {
                sw.Stop();
            }
        }

        private void resetbtn_Click(object sender, RoutedEventArgs e)
        {
            sw.Reset();
            clocktxtblock.Text = "00:00";
        }
        
        private void onclick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open_File = new OpenFileDialog();
            open_File.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
            if (open_File.ShowDialog() == true)
            {

                BitmapImage img = new BitmapImage(new Uri(open_File.FileName));
                //imgPhoto.Source = img;
                popupFullImageWindow.FullImageImage.Source = img;

                Image[,] image = new Image[4, 4];
                for (int x = 0; x < 4; x++)
                {
               
                    for (int y = 0; y < 4; y++)
                    {
                       
                            image[x, y] = new Image();
                            image[x, y].Width = 200;
                            image[x, y].Height = 95;
                            //image[x, y].Name = $"cb_x{x}_y{y}";
                            image[x, y].HorizontalAlignment = HorizontalAlignment.Left;
                            image[x, y].VerticalAlignment = VerticalAlignment.Top;
                            image[x, y].Margin = new Thickness(x * 96, y * 96, 0, 0);
                            PuzzleGrid.Children.Add(image[x, y]);
                            Console.WriteLine(PuzzleGrid);
                    }
                }
                CroppedBitmap cb;
                for (int x = 0; x < 4; x++)
                {

                    for (int y = 0; y < 4; y++)
                    {
                                //JU STÖRRE BILD. DESTO HÖGRE SKALA OCH ANTAL PIXLAR I "Int32Rect"
                            cb = new CroppedBitmap(img, new Int32Rect(x * 200, y * 200, 200,200));

                        
                        Random pix = new Random();
                        image[x, y].Source = cb;
                    }
                }


                ////BitmapImage bitmap = new BitmapImage();
                ////bitmap.BeginInit();
                //////bitmap.UriSource = new Uri(open_File.FileName);
                ////bitmap.EndInit();

                //// Create a CroppedBitmap from BitmapImage  
                //CroppedBitmap cbit1 = new CroppedBitmap(img, new Int32Rect(0, 0, 250, 200));
                //CroppedBitmap cbit2 = new CroppedBitmap(img, new Int32Rect(10, 10, 200, 100));
                //CroppedBitmap cbit3 = new CroppedBitmap(img, new Int32Rect(20, 20, 200, 100));
                //CroppedBitmap cbit4 = new CroppedBitmap(img, new Int32Rect(30, 30, 200, 100));

                //cb.Source = cbit1;
                //cb2.Source = cbit2;
                //cb3.Source = cbit3;
                //cb4.Source = cbit4;



            }
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
            Puzzle_Pieces popupPuzzleWindow = new Puzzle_Pieces();
            popupPuzzleWindow.Show();
        }

        private void Close(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void clickOnFull_Image(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog open_File = new OpenFileDialog();
            open_File.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
            popupFullImageWindow.Show();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {

        }

        private void chooseBackground_click(object sender, MouseButtonEventArgs e)
        {
            //opens up a combobox with backgrounds
            backgroundCombobox = new Backgrounds();
        }

        private void savePuzzle(object sender, MouseButtonEventArgs e)
        {
            string path = Path.GetTempFileName();
            FileInfo file = new FileInfo(path);
            SaveFileDialog saveGame = new SaveFileDialog();
            saveGame.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
            if (saveGame.ShowDialog() == true)
            {
                file.CopyTo(saveGame.FileName);
            }
        }
    
    }
}
