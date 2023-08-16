//
//    MCSkinn, a 3d skin management studio for Minecraft
//    by NotYoojun.! | Copyright (C) 2023 iNKORE! Studios
//
//    Repo: https://github.com/InkoreStudios/MCSkinn
//

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using MCSkinn.Forms;
using MCSkinn.Scripts.Models;
using MCSkinn.Scripts.Paril.Components.Undo;
using MCSkinn.Scripts.Paril.Imaging;
using MCSkinn.Scripts.Paril.OpenGL;
using Microsoft.VisualBasic.FileIO;
using OpenTK;
using MCSkinn.Scripts.Paril.Drawing;
using Inkore.Coreworks;
using System.ComponentModel;
using System.Windows.Media.Imaging;
using System.Collections.ObjectModel;
using Inkore.Coreworks.Windows.Helpers;
using System.Xml.Linq;

namespace MCSkinn.Scripts
{
    [Serializable]
    public class Skin : LibraryNode, IDisposable, INotifyPropertyChanged
    {
        private readonly Dictionary<int, bool> _transparentParts = new Dictionary<int, bool>();
        private bool _isDirty = false;
        public Texture GLImage;
        public Bitmap Head;
        public BitmapSource HeadSource;
        public Size Size;
        public UndoBuffer Undo;
        public bool IsLastSkin { get; set; }

        public Skin(string fileName, FolderNode parent = null)
        {
            Undo = new UndoBuffer(this);

            Path = fileName;

            Program.Log(LogType.Load, string.Format("Loaded skin '{0}'", System.IO.Path.GetFileName(fileName)), "at MCSkinn.Scripts.Skin(string)");

            Parent = parent;
        }

        public override string Text
        {
            get { return _text; }
        }

        public Skin(FileInfo file, FolderNode parent) :
            this(file.FullName, parent)
        {
        }

        private Model _model;

        public Model Model
        {
            get 
            { 
                return _model; 
            }
            set { _model = value; }
        }

        public int Width
        {
            get { return Size.Width; }
        }

        public bool IsDirty
        {
            get { return _isDirty; }
            set
            {
                _isDirty = value;

                Parent?.Workfolder?.SetDirtySkin(this);

                RaisePropertyChangedEvent(nameof(IsDirty));
            }
        }

        public override ObservableCollection<LibraryNode> Nodes { get { return null; } set { } }
        public override bool UseChessBackground => true;

        public int Height
        {
            get { return Size.Height; }
        }

        public Dictionary<int, bool> TransparentParts
        {
            get { return _transparentParts; }
        }

        private string _name = "";
        private string _text = "";
        public override string Name => _name;

        public string Directory => Parent.Path;
        public DirectoryInfo DirectoryInfo => Parent?.DirectoryInfo;

        private string _path = "";
        private FileInfo _fileInfo = null;
        public override string Path
        {
            get { return _path; }
            set
            {
                _path = value;

                _fileInfo = new FileInfo(Path);
       
                _name = System.IO.Path.GetFileName(Path);
                _text = System.IO.Path.GetFileNameWithoutExtension(Path);


                RaisePropertyChangedEvent(nameof(Text));
                RaisePropertyChangedEvent(nameof(Path));
                RaisePropertyChangedEvent(nameof(Name));
                RaisePropertyChangedEvent(nameof(FileInfo));
            }
        }

        public FileInfo FileInfo => _fileInfo;

        public override BitmapSource Icon => HeadSource;

        public override FolderNode Parent { get; set; }

        public override bool IsExpanded
        {
            get { return false; }
            set { }
        }

        #region IDisposable Members

        public override void Dispose()
        {
            if (SkinLibrary.SelectedNode == this)
                SkinLibrary.SelectedNode = null;

            IsDirty = false;
            Parent?.Workfolder?.SetDirtySkin(this);

            if (Parent != null)
                Parent.Remove(this);

            Parent = null;

            IsLoaded = false;

            if (GLImage != null)
            {
                GLImage.Dispose();
                GLImage = null;
            }

            if (Head != null)
            {
                Head.Dispose();
                Head = null;
            }

            if (SkinLibrary.NodesByPath.ContainsKey(SkinLibrary.GetAbsolutedPath(Path)))
                SkinLibrary.NodesByPath.Remove(SkinLibrary.GetAbsolutedPath(Path));
        }

        #endregion

        public bool IsLoaded { get; set; } = false;

        static void SetImage(Skin skin)
        {
            Program.Editor.RenderMakeCurrent();
            skin.GLImage = new TextureGL(skin.Path);
            skin.GLImage.SetMipmapping(false);
            skin.GLImage.SetRepeat(false);
        }

        public Exception SetImages(bool updateGL = true)
        {
            Bitmap image = null;

            try
            {
                using (FileStream file = FileInfo.Open(FileMode.Open, FileAccess.Read, FileShare.Read))
                    image = new Bitmap(file);

                Size = image.Size;

                if (Head != null)
                {
                    Head.Dispose();

                    if (updateGL)
                    {
                        Unload();
                        Create();
                    }
                }

                float scale = Size.Width / 64.0f;
                var headSize = (int)(8.0f * scale);
                var helmetLoc = (int)(40.0f * scale);

                Head = new Bitmap(headSize, headSize);
                using (Graphics g = Graphics.FromImage(Head))
                {
                    g.DrawImage(image, new Rectangle(0, 0, headSize, headSize), new Rectangle(headSize, headSize, headSize, headSize),
                                GraphicsUnit.Pixel);
                    g.DrawImage(image, new Rectangle(0, 0, headSize, headSize), new Rectangle(helmetLoc, headSize, headSize, headSize),
                                GraphicsUnit.Pixel);
                }

                if (Model == null)
                {
                    Model = TryGetModel(FileInfo.FullName, image);
                }

                Program.App_Main.Dispatcher.BeginInvoke(new Action(() =>
                {
                    HeadSource = Head.ToWpfBitmapSourceB();
                    RaisePropertyChangedEvent("Icon");
                }));

                IsLoaded = true;
                RaisePropertyChangedEvent("IsLoaded");

            }
            catch (Exception ex) 
            { 
                Program.Log(ex, true);
#if DEBUG
                //throw;
#endif
                //MessageBox.Show(string.Format(Editor.GetLanguageString("E_SKINERROR"), FileInfo.FullName));
                IsLoaded = false;

                return ex; ;
            }
            finally
            {
                if (image != null)
                    image.Dispose();
            }

            return null ;
        }

        public static Model TryGetModel(string fullName, Bitmap image, bool forceModel = true)
        {
            Model model = null;

            Dictionary<string, string> metadata = PNGMetadata.ReadMetadata(fullName);

            if (metadata.ContainsKey("Model"))
            {
                model = ModelLoader.GetModelByName(metadata["Model"]);
            }

            if (model == null)
            {
                int modelScale = image.Size.Width / 64;
                if (modelScale == 0) modelScale = 1;

                if (image.Height == image.Width && image.Height % 64 == 0)
                {
                    if (image.GetPixel(50 * modelScale, 16 * modelScale).A == 0 || image.GetPixel(55 * modelScale, 20 * modelScale).A == 0)
                    {
                        model = ModelLoader.GetModelByName("geometry.humanoid.customSlim");
                    }
                    else
                    {
                        model = ModelLoader.GetModelByName("geometry.humanoid.custom");
                    }
                }
                else if (image.Height * 2 == image.Width && image.Height % 32 == 0)
                {
                    if (image.GetPixel(50 * modelScale, 16 * modelScale).A == 0 || image.GetPixel(55 * modelScale, 20 * modelScale).A == 0)
                    {
                        model = ModelLoader.GetModelByName("geometry.humanoid.customSlim.minimal");
                    }
                    else
                    {
                        model = ModelLoader.GetModelByName("geometry.humanoid.custom.minimal");
                    }

                }
            }

            if (model == null && forceModel)
            {
                model = ModelLoader.GetModelByName("geometry.humanoid.custom");
            }

            return model;
        }

        public void Create()
        {
            SetImage(this);

            SetTransparentParts();
        }

        public void Unload()
        {
            if (GLImage != null)
                GLImage.Dispose();

            GLImage = null;
        }

        public override string ToString()
        {
            //if (Dirty)
            //    return Name + " *";
            return Name;
        }

        public void CommitChanges(Texture currentSkin, bool save)
        {
            using (var grabber = new ColorGrabber(currentSkin, Width, Height))
            {
                grabber.Load();

                if (currentSkin != GLImage)
                {
                    grabber.Texture = GLImage;
                    grabber.Save();
                }

                if (save)
                {
                    var newBitmap = new Bitmap(Width, Height);

                    using (var fp = new FastPixel(newBitmap, true))
                    {
                        for (int y = 0; y < Height; ++y)
                        {
                            for (int x = 0; x < Width; ++x)
                            {
                                ColorPixel c = grabber[x, y];
                                fp.SetPixel(x, y, Color.FromArgb(c.Alpha, c.Red, c.Green, c.Blue));
                            }
                        }
                    }

                    newBitmap.Save(FileInfo.FullName);
                    newBitmap.Dispose();

                    var md = new Dictionary<string, string>();
                    md.Add("Model", Model.Name);
                    PNGMetadata.WriteMetadata(FileInfo.FullName, md);

                    SetImages(true);

                    IsDirty = false;
                }
            }


        }

        public override void Renamed(string newName)
        {
            if (!newName.EndsWith(".png"))
                newName += ".png";

            //if (System.IO.File.Exists(DirectoryInfo.FullName + "\\" + newName))
            //    return false;

            string oldName = Path;

            Path = System.IO.Path.GetFullPath(newName);

            //FileSystem.RenameFile(FileInfo.FullName, newName);
            //File.MoveToParent(newName);

            SkinLibrary.RemoveNode(oldName);
            SkinLibrary.AddNode(this, newName);
        }

        public void Resize(int width, int height, Dialogs.ResizeType type = Dialogs.ResizeType.Scale)
        {
            using (var newBitmap = new Bitmap(width, height))
            {
                using (Graphics g = Graphics.FromImage(newBitmap))
                {
                    g.SmoothingMode = SmoothingMode.None;
                    g.InterpolationMode = InterpolationMode.NearestNeighbor;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    g.Clear(Color.FromArgb(0, 0, 0, 0));

                    using (Image temp = Image.FromFile(FileInfo.FullName))
                    {
                        if (type == Dialogs.ResizeType.Scale)
                            g.DrawImage(temp, 0, 0, newBitmap.Width, newBitmap.Height);
                        else
                            g.DrawImage(temp, 0, 0, temp.Width, temp.Height);
                    }
                }

                newBitmap.Save(FileInfo.FullName);

                var md = new Dictionary<string, string>();
                md.Add("Model", Model.Name);
                PNGMetadata.WriteMetadata(FileInfo.FullName, md);
            }

            SetImages();

            Undo.Clear();
            Program.Editor.CheckUndo();
        }

        public void Delete()
        {
            FileSystem.DeleteFile(FileInfo.FullName, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
            //File.Delete();
        }

        public void MoveTo(string newPath)
        {
            while (System.IO.File.Exists(newPath))
                newPath = newPath.Insert(newPath.Length - 4, " - " + Editor.GetLanguageString("C_MOVED"));

            FileSystem.MoveFile(FileInfo.FullName, newPath);
            //File.MoveTo(newPath);
        }

        public void CheckTransparentPart(ColorGrabber grabber, int index)
        {
            foreach (Face f in Model.Meshes[index].Faces)
            {
                var bounds = new Bounds(new Point(9999, 9999), new Point(-9999, -9999));

                foreach (Vector2 c in f.TexCoords)
                {
                    var coord = new Vector2(c.X * Width, c.Y * Height);
                    bounds.AddPoint(new Point((int)coord.X, (int)coord.Y));
                }

                Rectangle rect = bounds.ToRectangle();
                bool gotOne = false;

                for (int y = rect.Y; !gotOne && y < rect.Y + rect.Height; ++y)
                {
                    for (int x = rect.X; x < rect.X + rect.Width; ++x)
                    {
                        ColorPixel pixel = grabber[x, y];

                        if (pixel.Alpha != 255)
                        {
                            gotOne = true;
                            break;
                        }
                    }
                }

                if (gotOne)
                {
                    TransparentParts[index] = gotOne;
                    return;
                }
            }

            TransparentParts[index] = false;
        }

        public void SetTransparentParts()
        {
            using (var grabber = new ColorGrabber(GLImage, Width, Height))
            {
                grabber.Load();

                int mesh = 0;

                TransparentParts.Clear();

                foreach (Mesh m in Model.Meshes)
                {
                    TransparentParts.Add(mesh, false);

                    CheckTransparentPart(grabber, mesh);
                    mesh++;
                }
            }
        }
    }
}