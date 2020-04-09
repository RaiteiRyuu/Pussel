using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Puzzle_jigsaw
{
    public partial class Backgrounds : INotifyPropertyChanged
    {
        #region fields
        //class for creating a background
        private Brush selectedBackground;
        public event PropertyChangedEventHandler PropertyChanged;
        public IEnumerable<Brush> BackgroundOptions { get; }
        #endregion

        #region constructor
        public Backgrounds()
        {
            //Show all the background alternatives in the combobox as a list.

            var brushes = new List<Brush>
            {
                new SolidColorBrush(Colors.SandyBrown),
            };

            string[] fileEntries = Directory.GetFiles("../../Textures");
            foreach (string fileName in fileEntries)
                brushes.Add(new ImageBrush(new BitmapImage(new Uri($"{fileName}", UriKind.Relative))));
            BackgroundOptions = brushes;
            SelectedBackground = BackgroundOptions.FirstOrDefault();
        }
        #endregion

        #region access modifers

        public Brush SelectedBackground
        {
            //selected background is returned to MainWindow.xaml
            get => selectedBackground;
            set
            {
                selectedBackground = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedBackground)));
            }
        }
        #endregion 

    }
}
