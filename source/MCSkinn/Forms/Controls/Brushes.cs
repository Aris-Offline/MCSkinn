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
using MCSkinn.Scripts.Paril.Controls;
using MCSkinn.Scripts.Paril.Drawing;
using Inkore.Coreworks.Windows.Helpers;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Newtonsoft.Json.Linq;
using MCSkinn.Scripts;

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
        public static BrushComboBox BrushBox = new BrushComboBox();

        public static Brush SelectedBrush { get; set; }

        private static void BrushBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedBrush = (Brush)BrushBox.SelectedItem;

            if (Editor.MainForm.SelectedTool != null)
                Editor.MainForm.SelectedTool.Tool.SelectedBrushChanged();
        }

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

    public class BrushComboBox : ComboBox
    {
        public BrushComboBox()
        {
            DrawMode = DrawMode.OwnerDrawFixed;
            ItemHeight = 20;
            DropDownWidth = 35;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            base.OnDrawItem(e);

            e.DrawBackground();

            if (e.Index != -1)
            {
                var brush = (Brush)Items[e.Index];

                if (brush.Width <= e.Bounds.Height)
                {
                    e.Graphics.DrawImage(brush.Image, e.Bounds.X + e.Bounds.Height / 2 - brush.Width / 2,
                                         e.Bounds.Y + e.Bounds.Height / 2 - brush.Height / 2, brush.Width, brush.Height);
                }
                else
                    e.Graphics.DrawImage(brush.Image, e.Bounds.X, e.Bounds.Y, e.Bounds.Height, e.Bounds.Height);

                //e.Graphics.DrawRectangle(Pens.Black, new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Height, e.Bounds.Height));

                TextRenderer.DrawText(e.Graphics, brush.Name, Font,
                                      new Rectangle(e.Bounds.X + e.Bounds.Height + 4, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height),
                                      (e.State & DrawItemState.Selected) != 0 ? SystemColors.HighlightText : SystemColors.WindowText,
                                      TextFormatFlags.VerticalCenter);
            }

            e.DrawFocusRectangle();
        }
    }
}