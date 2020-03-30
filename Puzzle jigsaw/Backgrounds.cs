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
        private Brush selectedBackground;
      
        public Backgrounds()
        {

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

        public event PropertyChangedEventHandler PropertyChanged;
        public IEnumerable<Brush> BackgroundOptions { get; }

        public Brush SelectedBackground
        {
            get => selectedBackground;
            set
            {
                selectedBackground = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedBackground)));
            }
        }

        }
    }

