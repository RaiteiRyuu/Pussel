//using System.Collections.Generic;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Media.Imaging;

//namespace Puzzle_jigsaw
//{
//    public class CuttingImage
//    {
//        public List<Pieces> currentPuzzle = new List<Pieces>();
//        public BitmapImage Img;

//        public CuttingImage(BitmapImage img)
//        {
//            this.Img = img;
//        }

//        public void Cut()
//        {

        
//            Image[,] image = new Image[4, 4];
//            for (int x = 0; x < 4; x++)
//            {

//                for (int y = 0; y < 4; y++)
//                {

//                    image[x, y] = new Image();
//                    image[x, y].Width = 200;
//                    image[x, y].Height = 95;
//                    image[x, y].Name = $"cb_x{x}_y{y}";
//                    image[x, y].HorizontalAlignment = HorizontalAlignment.Left;
//                    image[x, y].VerticalAlignment = VerticalAlignment.Top;
//                    image[x, y].Margin = new Thickness(x * 96, y * 96, 0, 0);
//                    //PuzzleGrid.Children.Add(image[x, y]);
//                }
//            }
//            CroppedBitmap cb;
//            for (int x = 0; x < 4; x++)
//            {

//                for (int y = 0; y < 4; y++)
//                {
//                    cb = new CroppedBitmap(Img, new Int32Rect(x * 201, y * 201, 200, 200));

//                    image[x, y].Source = cb;
//                }
//            }
//        }
//    }
//}
