using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.IO;
using Path = System.IO.Path;
using System.Threading;

namespace Puzzle_jigsaw
{
    public partial class MainWindow : Window
    {
        #region fields
        public int counter;
        private Backgrounds backgroundCombobox = null;
        private FullImage popupFullImageWindow = null;
        private Puzzle_Pieces popupPuzzlePiecesWindow = null;

        DispatcherTimer dt = new DispatcherTimer();
        Stopwatch sw = new Stopwatch();
        string currentTime = string.Empty;

        private const double tileSize = 80;
        private const double tileOffset = 4;
        private const byte movementSpeed = 7;
        //public bool movingTile = false;

        public static byte[,] puzzleMatrix = new byte[4, 4] { { 0, 1, 2, 3 }, { 4, 5, 6, 7 }, { 8, 9, 10, 11 }, { 12, 13, 14, 15 } };
        private Image[] tiles = new Image[15];
        private Puzzle puzzle;

        private delegate void EmptyDelegate();

        #endregion

        #region Main constructor
        public MainWindow()
        {
            counter = 0;
            InitializeComponent();

            backgroundCombobox = new Backgrounds();
            popupFullImageWindow = new FullImage();
            popupPuzzlePiecesWindow = new Puzzle_Pieces();

            dt.Tick += new EventHandler(dt_Tick);
            dt.Interval = new TimeSpan(0, 0, 0, 0, 1);
            clocktxtblock.Text = "00:00";

            #region put tiles in a list
            puzzle = new Puzzle(Puzzle.StartType.Random, this);

            for (byte i = 0; i < 15; ++i)
            {
                BitmapImage temp = new BitmapImage();
                temp.BeginInit();
                temp.UriSource = new Uri("Tiles/Cute_Cat_" + (i + 1).ToString() + ".png", UriKind.Relative);
                temp.EndInit();
                tiles[i] = new Image();
                tiles[i].Source = temp;
                tiles[i].Width = puzzleCanvas.Width /4.27;
                tiles[i].Height = puzzleCanvas.Height/ 4.27;
                puzzleCanvas.Children.Add(tiles[i]);
            }
            ChangeTilesPositions();
            #endregion
        }
        #endregion

        public void DoEvents()
        {
            Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Background, new EmptyDelegate(delegate { }));
        }

        public void ChangeTilesPositions()
        {
            for (byte i = 0; i < 16; ++i)
            {
                if (puzzle[i] == 0)
                    continue;
                Canvas.SetLeft(tiles[puzzle[i] - 1], (tileSize + tileOffset) * (i % 4) + tileOffset / 2);
                Canvas.SetTop(tiles[puzzle[i] - 1], (tileSize + tileOffset) * (i / 4) + tileOffset / 2);
            }
        }

        public void MoveTile(int num, int direction)
        {
            /* dir 0 = left
             * dir 1 = right
             * dir 2 = up
             * dir 3 = down
             */

            int correlation;
            correlation = 1;
            switch (direction)
            {
                case 0:
                    Canvas.SetLeft(tiles[num], Canvas.GetLeft(tiles[num]) - movementSpeed * correlation);
                    break;
                case 1:
                    Canvas.SetLeft(tiles[num], Canvas.GetLeft(tiles[num]) + movementSpeed * correlation);
                    break;
                case 2:
                    Canvas.SetTop(tiles[num], Canvas.GetTop(tiles[num]) - movementSpeed * correlation);
                    break;
                case 3:
                    Canvas.SetTop(tiles[num], Canvas.GetTop(tiles[num]) + movementSpeed * correlation);
                    break;
                default:
                    break;
            }
        }

        private void TileMovementAnimation(int num, int direction)
        {
            int pixelation = (int)Math.Floor((tileSize + tileOffset) / movementSpeed);
            for (int x = 0; x < pixelation; ++x)
            {
                MoveTile(num - 1, direction);
                DoEvents();
                Thread.Sleep(5);
            }
        }

        private void MoveTileOnClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            
            Point mousePosition = Mouse.GetPosition(puzzleCanvas);
            byte x = (byte)Math.Floor(mousePosition.X / (tileSize + tileOffset));
            byte y = (byte)Math.Floor(mousePosition.Y / (tileSize + tileOffset));
            byte chosenTile = puzzleMatrix[y, x];
            for (int direction = 0; direction < 4; direction++)
            {
                int emptyTile = Puzzle.newIndex[chosenTile, direction];
                if ((emptyTile == -1) || (puzzle[emptyTile] != 0))
                    continue;

                counter++;

                sw.Start();
                dt.Start();

                puzzle.swapPositions(emptyTile, chosenTile);
                TileMovementAnimation(puzzle[emptyTile], direction);


                break;
            }
            
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
            puzzle.shuffle();
            ChangeTilesPositions();
            clocktxtblock.Text = "00:00";
            CounterLabel.Content = 0;
        }
        
        private void onclick(object sender, RoutedEventArgs e)
        {
     
            Image img = new Image();
            img.Source = new BitmapImage(new Uri("Image/Cute_Cat.jpg", UriKind.Relative));
            img.Width = imageCanvas.Width;
            img.Height = imageCanvas.Height;
            imageCanvas.Children.Add(img);
            popupFullImageWindow.FullImageImage.Source = img.Source;
           
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

        //private void clickOnFull_Image(object sender, MouseButtonEventArgs e)
        //{
        //    //OpenFileDialog open_File = new OpenFileDialog();
        //    //open_File.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
        //    popupFullImageWindow.Show();
        //    Image img = new Image();
        //    img.Source = new BitmapImage(new Uri("Image/Cute_Cat.jpg", UriKind.Relative));
        //    img.Width = popupFullImageWindow.Width;
        //    img.Height = popupFullImageWindow.Height;
        //    popupFullImageWindow.FullImageImage.Source = img.Source;
        //}
        
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
        
        private void ClickInCanvasGrid(object sender, MouseButtonEventArgs e)
        {
            CounterLabel.Content = counter.ToString();
        }
    }

        public enum ViewMode
    {
        Picture,
        Puzzle
    }
}
