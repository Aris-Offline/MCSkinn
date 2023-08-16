//
//    MCSkinn, A modern Minecraft 3D skin manager/editor for Windows by NotYoojun.!
//    Copyright © iNKORE! 2023
//
//    The copy of source (only the public part) can be used anywhere with a credit to MCSkinn page at your own risk
//    https://github.com/InkoreStudios/MCSkinn
//

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using MCSkinn.Scripts.Paril.Drawing;
using Inkore.Coreworks.Windows.Helpers;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Newtonsoft.Json.Linq;
using MCSkinn.Scripts;
using System.Collections;

namespace MCSkinn.Forms.Controls
{
    public class Brush : IComparable<Brush>, INotifyPropertyChanged
    {
        private static readonly AlphanumComparatorFast logical = new AlphanumComparatorFast();
        public float[,] Luminance;

        public Brush(string name, int w, int h)
        {
            Name = name;
            Luminance = new float[w, h];
        }

        public Brush(string file)
        {
            Name = Path.GetFileNameWithoutExtension(file);

            Image = new Bitmap(file);
            Luminance = new float[Image.Width, Image.Height];

            using (var fp = new FastPixel(Image, true))
            {
                for (int y = 0; y < Height; ++y)
                {
                    for (int x = 0; x < Width; ++x)
                        Luminance[x, y] = fp.GetPixel(x, y).A / 255.0f;
                }
            }

            BuildImage(false);
        }

        public string Name { get; set; }

        public int Width
        {
            get { return Luminance.GetLength(0); }
        }

        public int Height
        {
            get { return Luminance.GetLength(1); }
        }


        private Bitmap _image;
        private System.Windows.Media.ImageSource _imageSource;

        public event PropertyChangedEventHandler? PropertyChanged;

        public Bitmap Image
        {
            get { return _image; }
            set 
            { 
                _image = value; 
                
                if(value != null)
                    _imageSource = value.ToWpfBitmapSourceB();

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Image"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ImageSource"));
            }
        }

        public System.Windows.Media.ImageSource ImageSource
        {
            get
            {
                if(_imageSource == null && Image != null)
                {
                    _imageSource = Image.ToWpfBitmapSourceB();
                }

                return _imageSource;
            }
        }

        public float this[int x, int y]
        {
            get { return Luminance[x, y]; }
            set { Luminance[x, y] = value; }
        }

        public string Group { get; set; } = "Built-in";

        #region IComparable<Brush> Members

        public int CompareTo(Brush b)
        {
            return logical.Compare(Name, b.Name);
        }

        #endregion

        public void BuildImage(bool save = true)
        {
            if (Image != null)
                Image.Dispose();

            Bitmap bmp = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);

            using (var fp = new FastPixel(bmp, true))
            {
                for (int y = 0; y < Height; ++y)
                {
                    for (int x = 0; x < Width; ++x)
                        fp.SetPixel(x, y, Color.FromArgb((byte)(Luminance[x, y] * 255), 0, 0, 0));
                }
            }

            Image = bmp;

            if (save)
                Image.Save("Brushes\\" + Editor.GetLanguageString(Name) + " [" + Width + "].png");
        }
    }



    public static class Brushes
    {
        public static int NumBrushes = 10;
        public static ObservableCollection<Brush> BrushList = new ObservableCollection<Brush>();

        public static Brush SelectedBrush { get; set; }


        public static void LoadBrushes()
        {
            // make square brushes
            
            for(int i = 0;  i <= 1; i++)
            {
                for (var s = 1; s <= 16; ++s)
                {
                    if (i == 1)
                    {
                        if ((s & 1) == 1)
                        {
                            var brush = new Brush("Circle (" + s + ")", s, s);
                            brush.Luminance = new float[s, s];

                            int r = s / 2; // radius

                            int ox = s / 2, oy = s / 2; // origin

                            for (int x = -r; x <= r; x++)
                            {
                                int height = (int)Math.Sqrt(r * r - x * x);

                                for (int y = -height; y <= height; y++)
                                    brush.Luminance[ox + x, oy + y] = 1;
                            }

                            brush.BuildImage(false);
                            BrushList.Add(brush);

                        }
                    }
                    else
                    {
                        var brush = new Brush("Square (" + s + ")", s, s);
                        brush.Luminance = new float[s, s];

                        for (var x = 0; x < s; ++x)
                            for (var y = 0; y < s; ++y)
                                brush.Luminance[x, y] = 1;

                        brush.BuildImage(false);
                        BrushList.Add(brush);
                    }
                }
            }

            if (Directory.Exists(GlobalSettings.FullPath_Brushes))
            {
                foreach (string file in Directory.EnumerateFiles(GlobalSettings.FullPath_Brushes, "*.png", SearchOption.AllDirectories))
                {
                    try
                    {
                        string dir = Path.GetFileNameWithoutExtension(Path.GetDirectoryName(file));
                        BrushList.Add(new Brush(file) { Group = dir == "Brushes" ? "Custom" : dir });
                    }
                    catch (Exception ex)
                    {
                        Program.Log(ex, false);
                    }
                }
            }
        }
    }

    public class AlphanumComparatorFast : IComparer, IComparer<string>
    {
        #region IComparer Members

        public int Compare(object o1, object o2)
        {
            string s1, s2;

            if (o1 is string)
                s1 = (string)o1;
            else
                s1 = o1.ToString();

            if (o2 is string)
                s2 = (string)o2;
            else
                s2 = o2.ToString();

            return Compare(s1, s2);
        }

        #endregion

        #region IComparer<string> Members

        public int Compare(string s1, string s2)
        {
            int len1 = s1.Length;
            int len2 = s2.Length;
            int marker1 = 0;
            int marker2 = 0;

            // Walk through two the strings with two markers.
            while (marker1 < len1 && marker2 < len2)
            {
                char ch1 = s1[marker1];
                char ch2 = s2[marker2];

                // Some buffers we can build up characters in for each chunk.
                var space1 = new char[len1];
                int loc1 = 0;
                var space2 = new char[len2];
                int loc2 = 0;

                // Walk through all following characters that are digits or
                // characters in BOTH strings starting at the appropriate marker.
                // Collect char arrays.
                do
                {
                    space1[loc1++] = ch1;
                    marker1++;

                    if (marker1 < len1)
                        ch1 = s1[marker1];
                    else
                        break;
                } while (char.IsDigit(ch1) == char.IsDigit(space1[0]));

                do
                {
                    space2[loc2++] = ch2;
                    marker2++;

                    if (marker2 < len2)
                        ch2 = s2[marker2];
                    else
                        break;
                } while (char.IsDigit(ch2) == char.IsDigit(space2[0]));

                // If we have collected numbers, compare them numerically.
                // Otherwise, if we have strings, compare them alphabetically.
                var str1 = new string(space1);
                var str2 = new string(space2);

                int result;

                if (char.IsDigit(space1[0]) && char.IsDigit(space2[0]))
                {
                    int thisNumericChunk = int.Parse(str1);
                    int thatNumericChunk = int.Parse(str2);
                    result = thisNumericChunk.CompareTo(thatNumericChunk);
                }
                else
                    result = str1.CompareTo(str2);

                if (result != 0)
                    return result;
            }

            return len1 - len2;
        }

        #endregion
    }
}