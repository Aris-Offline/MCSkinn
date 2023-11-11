
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MCSkinn.Properties;
using iNKORE.Coreworks.Localization;
using MCSkinn.Scripts.Models;
using MCSkinn.Scripts.Paril.Components.Shortcuts;
using MCSkinn.Scripts.Paril.Components.Undo;
using MCSkinn.Scripts.Paril.Drawing;
using MCSkinn.Scripts.Paril.OpenGL;
using Microsoft.VisualBasic.FileIO;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using KeyPressEventArgs = System.Windows.Forms.KeyPressEventArgs;
using PixelFormat = OpenTK.Graphics.OpenGL.PixelFormat;
using Brushes = MCSkinn.Scripts.Brushes;
using Modern = iNKORE.UI.WPF.Modern;
using WPF = System.Windows;
using WPFC = System.Windows.Controls;
using iNKORE.Coreworks;
using System.Collections.ObjectModel;
using iNKORE.Coreworks.Windows.Helpers;
using WPFI = System.Windows.Input;
using WPFD = System.Windows.Data;
using MCSkinn.Scripts.Tools;
using OpenTK.Graphics;
using MCSkinn.Controls;

namespace MCSkinn.Scripts
{
    public partial class Editor
    {
        #region Variables

        public static readonly ShortcutEditor _shortcutEditor = new ShortcutEditor();
        private List<BackgroundImage> _backgrounds = new List<BackgroundImage>();
        private Dictionary<Size, Texture> _charPaintSizes = new Dictionary<Size, Texture>();
        private List<ToolIndex> _tools = new List<ToolIndex>();

        private float _2DCamOffsetX;
        private float _2DCamOffsetY;
        private float _3DCamOffsetX;
        private float _3DCamOffsetY;
        private float _2DZoom = 8;
        private Vector3 _3DOffset = Vector3.Zero;
        static float _3DRotationX = 180, _3DRotationY;
        private float _3DZoom = -80;
        private Texture _alphaTex;
        public UndoBuffer _currentUndoBuffer;
        private ViewMode _currentViewMode = ViewMode.Perspective;
        private Texture _font;
        private Texture _grassTop;
        private Skin _lastSkin;
        private bool _mouseIsDown;
        private Point _mousePoint;
        internal Texture _previewPaint;
        private int _selectedBackground;
        private ToolIndex _selectedTool;
        static Texture _cubeSides;

        public ViewMode CurrentViewMode => _currentViewMode;

        #endregion

        #region Fields

        // ===============================================
        // Private/Static variables
        // ===============================================

        //public static Stopwatch _renderTimer = new Stopwatch();
        //public static Stopwatch _sortTimer = new Stopwatch();
        //public static Stopwatch _batchTimer = new Stopwatch();
        //public static Stopwatch _compileTimer = new Stopwatch();
        private static bool _firstCalc;
        private Rectangle _currentViewport;
        private BackgroundImage _dynamicOverlay;
        private bool _isValidPick;
        private bool _mouseIn3D;
        private ModelToolStripMenuItem _oldModel;
        private Point _pickPosition = new Point(-1, -1);

        public ToolIndex SelectedTool
        {
            get { return _selectedTool; }
        }

        public Renderer MeshRenderer { get; private set; }

        // ===============================================
        // Constructor
        // ===============================================

        public GLControl RendererControl { get; private set; }

        public OpenTK.WPF.OtkWpfControl Renderer
        {
            get; private set;
        }

        public bool IsRendererInitialized { get; private set; }

        public MouseButtons CameraRotate
        {
            get
            {
                if (_selectedTool == _tools[(int)ViewportTool.Camera])
                    return MouseButtons.Left;
                else
                    return MouseButtons.Right;
            }
        }

        public MouseButtons CameraZoom
        {
            get
            {
                if (_selectedTool == _tools[(int)ViewportTool.Camera])
                    return MouseButtons.Middle;
                else
                    return MouseButtons.None;
            }
        }
        public MouseButtons CameraTranslate
        {
            get
            {
                if (_selectedTool == _tools[(int)ViewportTool.Camera])
                    return MouseButtons.Right;
                else
                    return MouseButtons.Middle;
            }
        }

        public iNKORE.UI.WPF.ColorPicker.ColorDisplay ColorPanel
        {
            get { return Program.Page_Editor.ColorDisplay_MainDisplay; }
        }

        public static Model CurrentModel
        {
            get { return Program.Editor._lastSkin == null ? null : Program.Editor._lastSkin.Model; }
        }

        public float ToolScale
        {
            get { return 200.0f / ViewportWidth; }
        }

        public int ViewportWidth
        {
            get
            {
                if (IsCompatibilityModeOn)
                {
                    return RendererControl.Width;
                }
                else
                {
                    return (int)Renderer.ActualWidth;
                }
            }
        }
        public int ViewportHeight
        {
            get
            {
                if (IsCompatibilityModeOn)
                {
                    return RendererControl.Height;
                }
                else
                {
                    return (int)Renderer.ActualHeight;
                }
            }
        }

        public int RenderWidth
        {
            get
            {
                if (IsCompatibilityModeOn)
                {
                    return RendererControl.Width;
                }
                else
                {
                    return (int)Renderer.RenderWidth;
                }
            }
        }
        public int RenderHeight
        {
            get
            {
                if (IsCompatibilityModeOn)
                {
                    return RendererControl.Height;
                }
                else
                {
                    return (int)Renderer.RenderHeight;
                }
            }
        }

        public double RenderScale
        {
            get
            {
                if (IsCompatibilityModeOn)
                {
                    return 1d;
                }
                else
                {
                    return Renderer.RenderScale;
                }
            }
        }




        public static Vector3 CameraPosition;

        public static float GrassY { get; private set; }
        #endregion

        #region Function Methods
        private void CreatePartList()
        {
            //_partItems = new[] { null, headToolStripMenuItem, helmetToolStripMenuItem, chestToolStripMenuItem, leftArmToolStripMenuItem, rightArmToolStripMenuItem, leftLegToolStripMenuItem, rightLegToolStripMenuItem, chestArmorToolStripMenuItem, leftArmArmorToolStripMenuItem, rightArmArmorToolStripMenuItem, leftLegArmorToolStripMenuItem, rightLegArmorToolStripMenuItem };
            //_partButtons = new[] { null, toggleHeadToolStripButton, toggleHelmetToolStripButton, toggleChestToolStripButton, toggleLeftArmToolStripButton, toggleRightArmToolStripButton, toggleLeftLegToolStripButton, toggleRightLegToolStripButton, toggleChestArmorToolStripButton, toggleLeftArmArmorToolStripButton, toggleRightArmArmorToolStripButton, toggleLeftLegArmorToolStripButton, toggleRightLegArmorToolStripButton };

            //var list = new ImageList();
            //list.ColorDepth = ColorDepth.Depth32Bit;
            //list.ImageSize = new Size(16, 16);
            //list.Images.Add(GenerateCheckBoxBitmap(CheckBoxState.UncheckedNormal));
            //list.Images.Add(GenerateCheckBoxBitmap(CheckBoxState.UncheckedHot));
            //list.Images.Add(GenerateCheckBoxBitmap(CheckBoxState.UncheckedPressed));
            //list.Images.Add(GenerateCheckBoxBitmap(CheckBoxState.CheckedNormal));
            //list.Images.Add(GenerateCheckBoxBitmap(CheckBoxState.CheckedHot));
            //list.Images.Add(GenerateCheckBoxBitmap(CheckBoxState.CheckedPressed));
            //list.Images.Add(GenerateCheckBoxBitmap(CheckBoxState.MixedNormal));
            //list.Images.Add(GenerateCheckBoxBitmap(CheckBoxState.MixedHot));
            //list.Images.Add(GenerateCheckBoxBitmap(CheckBoxState.MixedPressed));
            //list.Images.Add(Resources.radio_unchecked);
            //list.Images.Add(Resources.radio_checked);

            //treeView2.ImageList = list;
        }

        private PartTreeNode CreateNodePath(ObservableCollection<PartTreeNode> collection, Model m, Mesh part, string[] path, List<PartTreeNode> owners, List<PartTreeNode> radios)
        {
            PartTreeNode node = null;

            for (int i = 0; i < path.Length - 1; ++i)
            {
                bool isFound = false;
                foreach (PartTreeNode no in node == null ? collection : node.Nodes)
                {
                    if (no.Name == path[i])
                    {
                        node = no;
                        isFound = true;
                        break;
                    }
                }
                if (!isFound)
                {
                    (i == 0 ? collection : node.Nodes).Add(node = new PartTreeNode(CurrentModel, part, -1, path[i]));
                    owners.Add(node);

                }
                //if (node == null)
                //{

                //    //TreeNode[] nodes = collection.Find(path[i], false);

                //    //if (nodes.Length == 0)
                //    //{
                //    //    treeView.Nodes.Add(node = new PartTreeNode(CurrentModel, part, -1, path[i]));
                //    //    owners.Add(node);

                //    //    if (node.IsRadio)
                //    //        radios.Add(node);
                //    //}
                //    //else
                //    //    node = (PartTreeNode)nodes[0];


                //}
                //else
                //{
                //    TreeNode[] nodes = node.Nodes.Find(path[i], false);

                //    if (nodes.Length == 0)
                //    {
                //        PartTreeNode old = node;
                //        old.Nodes.Add(node = new PartTreeNode(CurrentModel, part, -1, path[i]));
                //        owners.Add(node);

                //        if (node.IsRadio)
                //            radios.Add(node);
                //    }
                //    else
                //        node = (PartTreeNode)nodes[0];
                //}
            }

            return node;
        }

        private void FillPartList()
        {
            if (Program.Page_Editor == null)
                return;


            if (CurrentModel.IsTCNFile)
            {
                var owners = new List<PartTreeNode>();
                var radios = new List<PartTreeNode>();

                Program.Page_Editor.Parts.Clear();

                int meshIndex = 0;
                foreach (Mesh part in CurrentModel.Meshes)
                {
                    string name = part.Name;
                    PartTreeNode node;

                    if (name != null && name.Contains('.'))
                    {
                        string[] args = name.Split('.');
                        PartTreeNode owner = CreateNodePath(Program.Page_Editor.Parts, CurrentModel, part, args, owners, radios);

                        owner.Nodes.Add(node = new PartTreeNode(CurrentModel, part, meshIndex, args[args.Length - 1]));
                    }
                    else
                        Program.Page_Editor.Parts.Add(node = new PartTreeNode(CurrentModel, part, meshIndex));

                    if (node.IsRadio)
                        radios.Add(node);

                    meshIndex++;
                }

            }
            else
            {
                Program.Page_Editor.Parts.Clear();

                Dictionary<string, List<Mesh>> meshFolders = new Dictionary<string, List<Mesh>>();
                Dictionary<string, string> folderParents = new Dictionary<string, string>();

                foreach (Mesh part in CurrentModel.Meshes)
                {
                    if (!meshFolders.ContainsKey(part.Folder))
                        meshFolders.Add(part.Folder, new List<Mesh>());

                    meshFolders[part.Folder].Add(part);

                    if (!string.IsNullOrEmpty(part.FolderParent) && !folderParents.ContainsKey(part.Folder))
                        folderParents.Add(part.Folder, part.FolderParent);
                }

                Dictionary<string, PartTreeNode> folders = new Dictionary<string, PartTreeNode>();


                foreach (string n in meshFolders.Keys)
                {
                    PartTreeNode node;

                    node = new PartTreeNode(CurrentModel, meshFolders[n], n);

                    folders.Add(n, node);

                }

                foreach (KeyValuePair<string, PartTreeNode> pair in folders)
                {
                    if (folderParents.ContainsKey(pair.Key) && folders.ContainsKey(folderParents[pair.Key]))
                    {
                        folders[folderParents[pair.Key]].Nodes.Add(pair.Value);
                    }
                    else
                    {
                        Program.Page_Editor.Parts.Add(pair.Value);
                    }
                }

            }
        }

        public void PerformResetCamera()
        {
            Program.Log(LogType.Load, "Performing ResetCamera", "at MCSkinn.Editor.PerformResetCamera()");

            _2DCamOffsetX = 0;
            _2DCamOffsetY = 0;
            _2DZoom = 8;
            _3DZoom = -80;
            _3DRotationX = 180;
            _3DRotationY = 0;
            _3DOffset = Vector3.Zero;
            _3DCamOffsetX = 0;
            _3DCamOffsetY = 0;

            CalculateMatrices();
            InvalidateRenderer();
        }


        public void PerformImportFromSite()
        {
            //if (!treeView1.Enabled)
            //    return;

            //string accountName = _importFromSite.Show();

            //if (string.IsNullOrEmpty(accountName))
            //    return;

            //string url = "http://s3.amazonaws.com/MinecraftSkins/" + accountName + ".png";

            //string folderLocation;
            //TreeNodeCollection collection;

            //if (_rightClickedNode == null)
            //    _rightClickedNode = SelectedNode;

            //GetFolderLocationAndCollectionForNode(treeView1, _rightClickedNode, out folderLocation, out collection);

            //string newSkinName = accountName;

            //while (File.Exists(folderLocation + newSkinName + ".png"))
            //    newSkinName += " - " + GetLanguageString("C_NEW");

            //try
            //{
            //    byte[] pngData = WebHelpers.DownloadFile(url);

            //    using (FileStream file = File.Create(folderLocation + newSkinName + ".png"))
            //        file.Write(pngData, 0, pngData.Length);

            //    var skin = new Skin(folderLocation + newSkinName + ".png");
            //    collection.Add(skin);
            //    skin.SetImages();

            //    treeView1.Invalidate();
            //}
            //catch (Exception ex)
            //{
            //    Program.Log(ex, true);
            //    MessageBox.Show(this, GetLanguageString("M_SKINERROR") + "\r\n" + ex);
            //    return;
            //}
        }

        public async void AskResizeType(Skin sk, Model model)
        {
            try
            {
                Dialogs.SkinSizeMismatchDialug dialog = new Dialogs.SkinSizeMismatchDialug();

                Modern.Controls.ContentDialogResult result = await dialog.ShowAsync();

                Dialogs.ResizeType resizeType;
                switch (result)
                {
                    case Modern.Controls.ContentDialogResult.Primary:
                        resizeType = Dialogs.ResizeType.Crop;
                        break;
                    case Modern.Controls.ContentDialogResult.Secondary:
                        resizeType = Dialogs.ResizeType.Scale;
                        break;
                    default:
                        return;
                }

                sk.Model = model;
                sk.Resize((int)model.DefaultWidth, (int)model.DefaultHeight, resizeType);

                using (var grabber = new ColorGrabber(sk.GLImage, sk.Width, sk.Height))
                {
                    grabber.Load();
                    grabber.Texture = GlobalDirtiness.CurrentSkin;
                    grabber.Save();
                    grabber.Texture = _previewPaint;
                    grabber.Save();
                }
            }
            catch (Exception ex)
            {
                Program.RaiseException(ex);
            }
        }


        public void SetModel(Model Model)
        {
            if (_lastSkin == null)
                return;

            if (_oldModel != null &&
                _oldModel.Model == Model)
                return;

            if (_lastSkin.Model != Model)
            {
                var oldAspect = _lastSkin.Width / (float)_lastSkin.Height;
                var newAspect = Model.DefaultWidth / Model.DefaultHeight;

                if (Math.Abs(oldAspect - newAspect) > 0.01f)
                {
                    AskResizeType(_lastSkin, Model);
                }
                else
                    _lastSkin.Model = Model;

                _lastSkin.IsDirty = true;
                SetCanSave(true);
                CheckUndo();
            }

            if (_oldModel != null)
            {
                _oldModel.IsChecked = false;

                //for (WPF.DependencyObject parent = _oldModel.Parent; parent != null; parent = (parent as WPF.FrameworkElement).Parent as WPF.DependencyObject)
                //    (parent as WPFC.Control).FontWeight = WPF.FontWeights.Light;
                _oldModel.FontWeight = WPF.FontWeights.Normal;

                if (_oldModel.ParentItem != null)
                    _oldModel.ParentItem.FontWeight = WPF.FontWeights.Normal;
            }

            Program.Page_Editor.AppBarButton_Viewport_Model.Label = _lastSkin.Model.DisplayName;
            _oldModel = _lastSkin.Model.DropDownItem;

            _lastSkin.TransparentParts.Clear();
            _lastSkin.SetTransparentParts();
            FillPartList();

            //for (WPF.DependencyObject parent = _oldModel.Parent; parent != null; parent = (parent as WPF.FrameworkElement).Parent as WPF.DependencyObject)
            //    (parent as WPFC.Control).FontWeight = WPF.FontWeights.Bold;

            if (_oldModel.ParentItem != null)
                _oldModel.ParentItem.FontWeight = WPF.FontWeights.Bold;

            _oldModel.FontWeight = WPF.FontWeights.Bold;
            //_oldModel.IsChecked = true;


            CalculateMatrices();
            InvalidateRenderer();

            Program.Log(LogType.Info, string.Format("Set current model to '{0}'", Model.Name), "at MCSkinn.Editor.treeView1_AfterSelect(object, TreeViewEventArgs)"); ;

        }


        public void PerformBrowseTo()
        {
            Program.Log(LogType.Load, "Performing BrowseTo", "at MCSkinn.Editor.PerformSaveAll()");

            if (SkinLibrary.SelectedNode == null)
                return;

            if (SkinLibrary.SelectedNode is Skin)
                Process.Start("explorer.exe", "/select,\"" + ((Skin)SkinLibrary.SelectedNode).FileInfo.FullName + "\"");
            else
                Process.Start("explorer.exe", ((FolderNode)SkinLibrary.SelectedNode).DirectoryInfo.FullName);
        }

        #endregion

        #region Graphics Renderer
        public void InvalidateRenderer()
        {
            if (IsCompatibilityModeOn)
            {
                RendererControl.Invalidate();
            }
            else
            {
                refreshNeeded = true;
                Renderer.InvalidateVisual();
            }
        }
        byte[] _charWidths = new byte[128];
        public static Color BackgrounColor_Light = Color.FromArgb(255, 230, 240, 250);
        public static Color BackgrounColor_Dark = Color.FromArgb(255, 32, 32, 32);
        public void InitGL()
        {
            GLVendor = GL.GetString(StringName.Vendor);
            GLVersion = GL.GetString(StringName.Version);
            GLRenderer = GL.GetString(StringName.Renderer);
            GLExtensions = GL.GetString(StringName.Extensions);

            GL.Enable(EnableCap.Texture2D);
            GL.ShadeModel(ShadingModel.Smooth); // Enable Smooth Shading
            GL.Enable(EnableCap.DepthTest); // Enables Depth Testing
            GL.DepthFunc(DepthFunction.Lequal); // The Type Of Depth Testing To Do
            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest); // Really Nice Perspective Calculations
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            GL.TexEnv(TextureEnvTarget.TextureEnv, TextureEnvParameter.TextureEnvMode, (int)TextureEnvMode.Modulate);
            _cubeSides = new TextureGL(Resources.cube_sides);
            _cubeSides.SetMipmapping(true);
            _cubeSides.SetRepeat(true);

            GL.Enable(EnableCap.LineSmooth);
            GL.Hint(HintTarget.LineSmoothHint, HintMode.Nicest);
            GL.Enable(EnableCap.PointSmooth);
            GL.Hint(HintTarget.PointSmoothHint, HintMode.Nicest);
            GL.Enable(EnableCap.PolygonSmooth);
            GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);


            var tinyFont = Resources.tinyfont;

            _font = new TextureGL(tinyFont);
            _font.SetMipmapping(false);
            _font.SetRepeat(false);

            for (var c = 0; c < 128; ++c)
            {
                var x = c % 16 * 8;
                var y = c / 16 * 8;

                for (var px = x + 7; px >= x; --px)
                {
                    var empty = true;

                    for (var py = y; py < y + 8; ++py)
                    {
                        var pixl = tinyFont.GetPixel(px, py);

                        if (pixl.A != 0)
                        {
                            empty = false;
                            break;
                        }
                    }

                    if (!empty)
                    {
                        _charWidths[c] = (byte)(px - x + 1);
                        break;
                    }
                }
            }

            _charWidths[(byte)' ' - 32] = 4;

            InitializeGround();

            _dynamicOverlay = new BackgroundImage("Dynamic", "Dynamic", null);
            _backgrounds.Add(_dynamicOverlay);

            //foreach (string file in Directory.EnumerateFiles(GlobalSettings.GetDataURI("Overlays"), "*.png"))
            //{
            //    try
            //    {
            //        var image = new TextureGL(file);
            //        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
            //        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            //        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            //        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);

            //        _backgrounds.Add(new BackgroundImage(file, Path.GetFileNameWithoutExtension(file), image));
            //    }
            //    catch(Exception ex)
            //    {
            //        Program.Log(ex, false);

            //        MessageBox.Show(this, string.Format(GetLanguageString("B_MSG_OVERLAYERROR"), file));
            //    }
            //}

            int index = 0;
            foreach (BackgroundImage b in _backgrounds)
            {
                ToolStripMenuItem item = b.Item ?? new ToolStripMenuItem(b.Name);
                b.Item = item;

                if (b.Path == GlobalSettings.LastBackground)
                {
                    item.Checked = true;
                    _selectedBackground = index;
                }

                item.Click += item_Clicked;
                item.Tag = index++;
            }

            _previewPaint = new TextureGL();
            GlobalDirtiness.CurrentSkin = new TextureGL();

            _previewPaint.SetMipmapping(false);
            _previewPaint.SetRepeat(false);

            GlobalDirtiness.CurrentSkin.SetMipmapping(false);
            GlobalDirtiness.CurrentSkin.SetRepeat(false);

            _alphaTex = new TextureGL();
            _alphaTex.Upload(new byte[]
            {
                127, 127, 127, 255,
                80, 80, 80, 255,
                80, 80, 80, 255,
                127, 127, 127, 255
            }, 2, 2);
            _alphaTex.SetMipmapping(false);
            _alphaTex.SetRepeat(true);

            bool supportsArrays = GL.GetString(StringName.Extensions).Contains("GL_EXT_vertex_array");

            if (supportsArrays)
            {
                Program.Log(LogType.Info, "The system supports arrays. Creating ClientArrayRenderer", "Editor.InitGL()");
                MeshRenderer = new ClientArrayRenderer();
            }
            else
            {
                Program.Log(LogType.Info, "The system DOES NOT supports arrays. Creating ImmediateRenderer", "Editor.InitGL()");
                MeshRenderer = new ImmediateRenderer();
            }

        }

        public void InitializeGround()
        {
            if (_grassTop != null)
                _grassTop.Dispose(); _grassTop = null;

            GL.ClearColor(GlobalSettings.BackgroundColor.HasValue ? GlobalSettings.BackgroundColor.Value : Modern.ThemeManager.Current.ActualApplicationTheme == Modern.ApplicationTheme.Light ? BackgrounColor_Light : BackgrounColor_Dark);

            if (!File.Exists(Program.GetDataPath("grass.png", false)))
            {
                _grassTop = new TextureGL(Modern.ThemeManager.Current.ActualApplicationTheme == Modern.ApplicationTheme.Light ? Resources.ground_white : Resources.ground_dark);
            }
            else
            {
                _grassTop = new TextureGL(Program.GetDataPath("grass.png", false));

            }

            _grassTop.SetMipmapping(false);
            _grassTop.SetRepeat(true);

        }

        private Texture GetPaintTexture(int width, int height)
        {
            if (!_charPaintSizes.ContainsKey(new Size(width, height)))
            {
                var tex = new TextureGL();

                var arra = new int[width * height];
                unsafe
                {
                    fixed (int* texData = arra)
                    {
                        int* d = texData;

                        for (int y = 0; y < height; ++y)
                        {
                            for (int x = 0; x < width; ++x)
                            {
                                *d = y * width + x | 255 << 24;
                                d++;
                            }
                        }
                    }
                }

                tex.Upload(arra, width, height);
                tex.SetMipmapping(false);
                tex.SetRepeat(false);

                _charPaintSizes.Add(new Size(width, height), tex);

                return tex;
            }

            return _charPaintSizes[new Size(width, height)];
        }

        private void DrawSkinnedRectangle
            (float x, float y, float z, float width, float length, float height,
             int topSkinX, int topSkinY, int topSkinW, int topSkinH,
             Texture texture, int skinW = 64, int skinH = 32)
        {
            texture.Bind();

            GL.Begin(PrimitiveType.Quads);

            width /= 2;
            length /= 2;
            height /= 2;

            float tsX = (float)topSkinX / skinW;
            float tsY = (float)topSkinY / skinH;
            float tsW = (float)topSkinW / skinW;
            float tsH = (float)topSkinH / skinH;

            GL.TexCoord2(tsX, tsY + tsH - 0.00005);
            GL.Vertex3(x - width, y + length, z + height); // Bottom Right Of The Quad (Top)
            GL.TexCoord2(tsX + tsW - 0.00005, tsY + tsH - 0.00005);
            GL.Vertex3(x + width, y + length, z + height); // Bottom Left Of The Quad (Top)
            GL.TexCoord2(tsX + tsW - 0.00005, tsY);
            GL.Vertex3(x + width, y + length, z - height); // Top Left Of The Quad (Top)
            GL.TexCoord2(tsX, tsY);
            GL.Vertex3(x - width, y + length, z - height); // Top Right Of The Quad (Top)

            GL.End();
        }

        private void DrawCharacter2D(Texture font, byte c, float xOfs, float yOfs, float width, float height)
        {
            font.Bind();

            float tx = (c - 32) % 16 * 8 / 128.0f;
            float ty = (c - 32) / 16 * 8 / 64.0f;
            const float txw = 8.0f / 128.0f;
            const float txh = 8.0f / 64.0f;

            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(tx, ty);
            GL.Vertex2(xOfs, yOfs);
            GL.TexCoord2(tx + txw, ty);
            GL.Vertex2(xOfs + width, yOfs);
            GL.TexCoord2(tx + txw, ty + txh);
            GL.Vertex2(xOfs + width, height + yOfs);
            GL.TexCoord2(tx, ty + txh);
            GL.Vertex2(xOfs, height + yOfs);
            GL.End();
        }

        private void DrawStringWithinRectangle(Texture font, RectangleF rect, string s, float spacing, float size)
        {
            float start = rect.X + 2 * GlobalSettings.DynamicOverlayTextSize / _2DZoom, x = start;
            float y = rect.Y;

            foreach (char c in s)
            {
                DrawCharacter2D(font, (byte)c, x, y, size, size);
                x += (_charWidths[(byte)c - 32] + 1) * GlobalSettings.DynamicOverlayTextSize / _2DZoom;

                if (x + spacing > rect.X + rect.Width)
                {
                    x = start;
                    y += spacing;
                }

                if (y + spacing > rect.Y + rect.Height)
                    break;
            }

            TextureGL.Unbind();
        }

        private void DrawPlayer2D(Texture tex, Skin skin, bool drawPlayer = true)
        {
            if (GlobalSettings.AlphaCheckerboard)
            {
                _alphaTex.Bind();

                GL.Begin(PrimitiveType.Quads);
                GL.TexCoord2(0, 0);
                GL.Vertex2(0, 0);
                GL.TexCoord2(_currentViewport.Width / 16.0f, 0);
                GL.Vertex2(_currentViewport.Width, 0);
                GL.TexCoord2(_currentViewport.Width / 16.0f, _currentViewport.Height / 16.0f);
                GL.Vertex2(_currentViewport.Width, _currentViewport.Height);
                GL.TexCoord2(0, _currentViewport.Height / 16.0f);
                GL.Vertex2(0, _currentViewport.Height);
                GL.End();
            }

            if (!drawPlayer)
                return;

            if (skin != null)
                tex.Bind();

            GL.PushMatrix();

            GL.Translate(_2DCamOffsetX, _2DCamOffsetY, 0);
            GL.Translate(_currentViewport.Width / 2 + -_2DCamOffsetX, _currentViewport.Height / 2 + -_2DCamOffsetY, 0);
            GL.Scale(_2DZoom, _2DZoom, 1);

            GL.Enable(EnableCap.Blend);

            GL.Translate(_2DCamOffsetX, _2DCamOffsetY, 0);
            if (skin != null)
            {
                float w = skin.Width;
                float h = skin.Height;
                GL.Begin(PrimitiveType.Quads);
                GL.TexCoord2(0, 0);
                GL.Vertex2(-(CurrentModel.DefaultWidth / 2), -(CurrentModel.DefaultHeight / 2));
                GL.TexCoord2(1, 0);
                GL.Vertex2(CurrentModel.DefaultWidth / 2, -(CurrentModel.DefaultHeight / 2));
                GL.TexCoord2(1, 1);
                GL.Vertex2(CurrentModel.DefaultWidth / 2, CurrentModel.DefaultHeight / 2);
                GL.TexCoord2(0, 1);
                GL.Vertex2(-(CurrentModel.DefaultWidth / 2), CurrentModel.DefaultHeight / 2);
                GL.End();
            }

            TextureGL.Unbind();

            if (GlobalSettings.GridEnabled && GlobalSettings.DynamicOverlayGridColor.A > 0)
            {
                GL.Color4(GlobalSettings.DynamicOverlayGridColor);
                GL.PushMatrix();
                GL.Translate(-(CurrentModel.DefaultWidth / 2), -(CurrentModel.DefaultHeight / 2), 0);
                GL.Begin(PrimitiveType.Lines);

                float wX = skin.Width / CurrentModel.DefaultWidth;
                float wY = skin.Height / CurrentModel.DefaultHeight;

                for (int i = 0; i <= skin.Width; ++i)
                {
                    GL.Vertex2(i / wX, 0);
                    GL.Vertex2(i / wX, skin.Height / wY);
                }

                for (int i = 0; i <= skin.Height; ++i)
                {
                    GL.Vertex2(0, i / wY);
                    GL.Vertex2(skin.Width / wX, i / wY);
                }

                GL.End();
                GL.PopMatrix();
            }

            if (GlobalSettings.TextureOverlay && skin != null)
            {
                if (_backgrounds[_selectedBackground] == _dynamicOverlay)
                {
                    GL.PushMatrix();
                    GL.Translate(-(CurrentModel.DefaultWidth / 2), -(CurrentModel.DefaultHeight / 2), 0);

                    float stub = GlobalSettings.DynamicOverlayLineSize / _2DZoom;
                    float one = 1.0f / _2DZoom;

                    var done = new List<RectangleF>();
                    foreach (Mesh mesh in CurrentModel.Meshes)
                    {
                        foreach (Face face in mesh.Faces)
                        {
                            RectangleF toint = face.TexCoordsToFloat((int)CurrentModel.DefaultWidth, (int)CurrentModel.DefaultHeight);

                            if (toint.Width == 0 ||
                                toint.Height == 0)
                                continue;
                            if (done.Contains(toint))
                                continue;

                            done.Add(toint);

                            GL.Color4(GlobalSettings.DynamicOverlayLineColor);
                            GL.Begin(PrimitiveType.Quads);
                            GL.Vertex2(toint.X, toint.Y);
                            GL.Vertex2(toint.X + toint.Width, toint.Y);
                            GL.Vertex2(toint.X + toint.Width, toint.Y + stub);
                            GL.Vertex2(toint.X, toint.Y + stub);

                            GL.Vertex2(toint.X, toint.Y);
                            GL.Vertex2(toint.X + stub, toint.Y);
                            GL.Vertex2(toint.X + stub, toint.Y + toint.Height);
                            GL.Vertex2(toint.X, toint.Y + toint.Height);

                            GL.Vertex2(toint.X + toint.Width + one, toint.Y);
                            GL.Vertex2(toint.X + toint.Width + one, toint.Y + toint.Height);
                            GL.Vertex2(toint.X + toint.Width + one - stub, toint.Y + toint.Height);
                            GL.Vertex2(toint.X + toint.Width + one - stub, toint.Y);

                            GL.Vertex2(toint.X, toint.Y + toint.Height + one);
                            GL.Vertex2(toint.X, toint.Y + toint.Height + one - stub);
                            GL.Vertex2(toint.X + toint.Width, toint.Y + toint.Height + one - stub);
                            GL.Vertex2(toint.X + toint.Width, toint.Y + toint.Height + one);
                            GL.End();
                            GL.Color4(Color.White);

                            GL.Color4(Color.Black);
                            var shadow = toint;
                            shadow.Offset(1.0f * GlobalSettings.DynamicOverlayTextSize / _2DZoom, 1.0f * GlobalSettings.DynamicOverlayTextSize / _2DZoom);

                            DrawStringWithinRectangle(_font, shadow, mesh.Name + " " + Model.SideFromNormal(face.Normal),
                                                        6 * GlobalSettings.DynamicOverlayTextSize / _2DZoom,
                                                        8.0f * GlobalSettings.DynamicOverlayTextSize / _2DZoom);
                            GL.Color4(GlobalSettings.DynamicOverlayTextColor);
                            DrawStringWithinRectangle(_font, toint, mesh.Name + " " + Model.SideFromNormal(face.Normal),
                                                        6 * GlobalSettings.DynamicOverlayTextSize / _2DZoom,
                                                        8.0f * GlobalSettings.DynamicOverlayTextSize / _2DZoom);
                            GL.Color4(Color.White);
                        }
                    }

                    GL.PopMatrix();
                }
                else
                {
                    _backgrounds[_selectedBackground].GLImage.Bind();

                    GL.Begin(PrimitiveType.Quads);
                    GL.TexCoord2(0, 0);
                    GL.Vertex2(-(CurrentModel.DefaultWidth / 2), -(CurrentModel.DefaultHeight / 2));
                    GL.TexCoord2(1, 0);
                    GL.Vertex2(CurrentModel.DefaultWidth / 2, -(CurrentModel.DefaultHeight / 2));
                    GL.TexCoord2(1, 1);
                    GL.Vertex2(CurrentModel.DefaultWidth / 2, CurrentModel.DefaultHeight / 2);
                    GL.TexCoord2(0, 1);
                    GL.Vertex2(-(CurrentModel.DefaultWidth / 2), CurrentModel.DefaultHeight / 2);
                    GL.End();
                }
            }

            GL.PopMatrix();

            GL.Disable(EnableCap.Blend);
        }

        Point mousePos;

        public Point GetRenderCursorPos()
        {
            if (IsCompatibilityModeOn)
            {
                return mousePos;
            }
            else
            {
                return Renderer.GetMousePointAtRenderer().ToDrawingRectangle();
            }
        }

        public const int GrassLength = 1024;

        private void DrawPlayer(Texture tex, Skin skin, bool drawPlayer = true)
        {
            TextureGL.Unbind();

            Point clPt = GetRenderCursorPos();
            int x = clPt.X - _currentViewport.Width / 2;
            int y = clPt.Y - _currentViewport.Height / 2;

            if (GlobalSettings.Transparency == TransparencyMode.All)
                GL.Enable(EnableCap.Blend);
            else
                GL.Disable(EnableCap.Blend);

            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Front);

            if (GlobalSettings.ShowGround)
                DrawSkinnedRectangle(0, GrassY + 0.25f, 0, GrassLength, 0, GrassLength, 0, 0, GrassLength, GrassLength, _grassTop, 16, 16);

            GL.Disable(EnableCap.CullFace);

            if (!drawPlayer)
                return;

            // add meshes
            //if (GlobalSettings.RenderBenchmark && IsRendering)
            //    _compileTimer.Start();

            if (CurrentModel != null)
            {
                int meshIndex = -1;
                foreach (Mesh mesh in CurrentModel.Meshes)
                {
                    meshIndex++;
                    bool meshVisible = CurrentModel.PartsEnabled[mesh];

                    if (meshVisible == false && !GlobalSettings.Ghost)
                        continue;

                    if (!mesh.IsSolid)
                        mesh.HasTransparency = _lastSkin.TransparentParts[meshIndex];

                    mesh.Texture = tex;

                    // Lazy Man Update!
                    mesh.LastDrawTransparent = mesh.DrawTransparent;
                    mesh.DrawTransparent = meshVisible == false && GlobalSettings.Ghost;

                    if (mesh.LastDrawTransparent != mesh.DrawTransparent)
                        MeshRenderer.UpdateUserData(mesh);

                    MeshRenderer.AddMesh(mesh);
                }
            }

            //if (GlobalSettings.RenderBenchmark && IsRendering)
            //    _compileTimer.Stop();

            MeshRenderer.Render();
        }

        private void SetPreview()
        {
            if (_lastSkin == null)
            {
                using (var preview = new ColorGrabber(_previewPaint, 64, 32))
                    preview.Save();
            }
            else
            {
                Skin skin = _lastSkin;

                using (var currentSkin = new ColorGrabber(GlobalDirtiness.CurrentSkin, skin.Width, skin.Height))
                {
                    bool pick = GetPick(_mousePoint.X, _mousePoint.Y, out _pickPosition);

                    currentSkin.Load();
                    if (_selectedTool.Tool.RequestPreview(currentSkin, skin, _pickPosition.X, _pickPosition.Y))
                    {
                        currentSkin.Texture = _previewPaint;
                        currentSkin.Save();
                    }
                    else
                    {
                        currentSkin.Texture = _previewPaint;
                        currentSkin.Save();
                    }
                }
            }
        }

        static bool prettyDarnCloseToZero(float v, float ep = float.Epsilon)
        {
            return v > -ep && v < ep;
        }

        // adapted from http://java3d.sourcearchive.com/documentation/0.0.cvs.20090202.dfsg/Intersect_8java-source.html#l01099
        public static bool segmentAndPoly(Vector3[] coordinates,
                                      Vector3 start, Vector3 end,
                                      out float dist)
        {
            dist = 0;

            Vector3 vec0 = new Vector3(); // Edge vector from point 0 to point 1;
            Vector3 vec1 = new Vector3(); // Edge vector from point 0 to point 2 or 3;
            Vector3 pNrm = new Vector3();
            float absNrmX, absNrmY, absNrmZ, pD = 0.0f;
            Vector3 tempV3d = new Vector3();
            Vector3 direction = new Vector3();
            float pNrmDotrDir = 0.0f;
            int axis, nc, sh, nsh;

            Vector3 iPnt = new Vector3(); // Point of intersection.

            float[] uCoor = new float[4]; // Only need to support up to quad.
            float[] vCoor = new float[4];
            float tempD;

            int i, j;

            // Compute plane normal.
            for (i = 0; i < coordinates.Length - 1;)
            {
                vec0.X = coordinates[i + 1].X - coordinates[i].X;
                vec0.Y = coordinates[i + 1].Y - coordinates[i].Y;
                vec0.Z = coordinates[i + 1].Z - coordinates[i++].Z;
                if (vec0.Length > 0.0f)
                    break;
            }

            for (j = i; j < coordinates.Length - 1; j++)
            {
                vec1.X = coordinates[j + 1].X - coordinates[j].X;
                vec1.Y = coordinates[j + 1].Y - coordinates[j].Y;
                vec1.Z = coordinates[j + 1].Z - coordinates[j].Z;
                if (vec1.Length > 0.0f)
                    break;
            }

            if (j == coordinates.Length - 1)
                return false;  // Degenerated polygon.

            Vector3.Cross(ref vec0, ref vec1, out pNrm);

            if (pNrm.Length == 0.0f)
                return false;  // Degenerated polygon.

            // Compute plane D.
            tempV3d = coordinates[0];
            Vector3.Dot(ref pNrm, ref tempV3d, out pD);

            direction.X = end.X - start.X;
            direction.Y = end.Y - start.Y;
            direction.Z = end.Z - start.Z;

            Vector3.Dot(ref pNrm, ref direction, out pNrmDotrDir);

            // Segment is parallel to plane. 
            if (pNrmDotrDir == 0.0f)
                return false;

            tempV3d = start;

            dist = (pD - Vector3.Dot(pNrm, tempV3d)) / pNrmDotrDir;

            // Segment intersects the plane behind the segment's start.
            // or exceed the segment's length.
            if (dist < 0.0f || dist > 1.0f)
                return false;

            // Now, one thing for sure the segment intersect the plane.
            // Find the intersection point.
            iPnt.X = start.X + direction.X * dist;
            iPnt.Y = start.Y + direction.Y * dist;
            iPnt.Z = start.Z + direction.Z * dist;

            // System.out.println("dist " + dist[0] + " iPnt : " + iPnt);

            // Project 3d points onto 2d plane and apply Jordan curve theorem. 
            // Note : Area of polygon is not preserve in this projection, but
            // it doesn't matter here. 

            // Find the axis of projection.
            absNrmX = Math.Abs(pNrm.X);
            absNrmY = Math.Abs(pNrm.Y);
            absNrmZ = Math.Abs(pNrm.Z);

            if (absNrmX > absNrmY)
                axis = 0;
            else
                axis = 1;

            if (axis == 0)
            {
                if (absNrmX < absNrmZ)
                    axis = 2;
            }
            else if (axis == 1)
            {
                if (absNrmY < absNrmZ)
                    axis = 2;
            }

            for (i = 0; i < coordinates.Length; i++)
            {
                switch (axis)
                {
                    case 0:
                        uCoor[i] = coordinates[i].Y - iPnt.Y;
                        vCoor[i] = coordinates[i].Z - iPnt.Z;
                        break;
                    case 1:
                        uCoor[i] = coordinates[i].X - iPnt.X;
                        vCoor[i] = coordinates[i].Z - iPnt.Z;
                        break;
                    case 2:
                        uCoor[i] = coordinates[i].X - iPnt.X;
                        vCoor[i] = coordinates[i].Y - iPnt.Y;
                        break;
                }
            }

            // initialize number of crossing, nc.
            nc = 0;

            if (vCoor[0] < 0.0f)
                sh = -1;
            else
                sh = 1;

            for (i = 0; i < coordinates.Length; i++)
            {
                j = i + 1;
                if (j == coordinates.Length)
                    j = 0;

                if (vCoor[j] < 0.0)
                    nsh = -1;
                else
                    nsh = 1;

                if (sh != nsh)
                {
                    if (uCoor[i] > 0.0f && uCoor[j] > 0.0f)
                    {
                        // This line must cross U+.
                        nc++;
                    }
                    else if (uCoor[i] > 0.0f || uCoor[j] > 0.0f)
                    {
                        // This line might cross U+. We need to compute intersection on U azis.
                        tempD = uCoor[i] - vCoor[i] * (uCoor[j] - uCoor[i]) / (vCoor[j] - vCoor[i]);
                        if (tempD > 0)
                            // This line cross U+.
                            nc++;
                    }
                    sh = nsh;
                }
            }

            if (nc % 2 == 1)
            {
                // calculate the distance
                dist *= direction.Length;
                return true;
            }

            return false;
        }

        static bool rayTriangleIntersect(
            Vector3 orig, Vector3 dir,
            Vector3 v0, Vector3 v1, Vector3 v2,
            ref float t, ref float u, ref float v)
        {
            Vector3 v0v1 = v1 - v0;
            Vector3 v0v2 = v2 - v0;
            Vector3 pvec = Vector3.Cross(dir, v0v2);
            float det = Vector3.Dot(v0v1, pvec);
#if CULLING
			// if the determinant is negative the triangle is backfacing
			// if the determinant is close to 0, the ray misses the triangle
			if (det < 1e-8f) return false;
#else
            // ray and triangle are parallel if det is close to 0
            if (prettyDarnCloseToZero(det, 1e-8f)) return false;
#endif

            float invDet = 1 / det;

            Vector3 tvec = orig - v0;
            u = Vector3.Dot(tvec, pvec) * invDet;
            if (u < 0 || u > 1) return false;

            Vector3 qvec = Vector3.Cross(tvec, v0v1);
            v = Vector3.Dot(dir, qvec) * invDet;
            if (v < 0 || u + v > 1) return false;

            t = Vector3.Dot(v0v2, qvec) * invDet;

            return true;
        }

        private static bool pointAndPoly(Vector3[] coordinates, Vector3 point)
        {
            Vector3 vec0 = new Vector3(); // Edge vector from point 0 to point 1;
            Vector3 vec1 = new Vector3(); // Edge vector from point 0 to point 2 or 3;
            Vector3 pNrm = new Vector3();
            float pD = 0.0f;// float absNrmX, absNrmY, absNrmZ, pD = 0.0f;
            Vector3 tempV3d = new Vector3();
            //float pNrmDotrDir = 0.0f;

            //float tempD;

            int i, j;

            // Compute plane normal.
            for (i = 0; i < coordinates.Length - 1;)
            {
                vec0.X = coordinates[i + 1].X - coordinates[i].X;
                vec0.Y = coordinates[i + 1].Y - coordinates[i].Y;
                vec0.Z = coordinates[i + 1].Z - coordinates[i++].Z;
                if (vec0.Length > 0.0)
                    break;
            }

            for (j = i; j < coordinates.Length - 1; j++)
            {
                vec1.X = coordinates[j + 1].X - coordinates[j].X;
                vec1.Y = coordinates[j + 1].Y - coordinates[j].Y;
                vec1.Z = coordinates[j + 1].Z - coordinates[j].Z;
                if (vec1.Length > 0.0)
                    break;
            }

            if (j == coordinates.Length - 1)
            {
                // System.out.println("(1) Degenerated polygon.");
                return false;  // Degenerated polygon.
            }

            /* 
			   System.out.println("Ray orgin : " + ray.origin + " dir " + ray.direction);
			   System.out.println("Triangle/Quad :");
			   for(i=0; i<coordinates.length; i++) 
			   System.out.println("P" + i + " " + coordinates[i]);
			   */

            pNrm = Vector3.Cross(vec0, vec1);

            if (pNrm.Length == 0.0)
            {
                // System.out.println("(2) Degenerated polygon.");
                return false;  // Degenerated polygon.
            }
            // Compute plane D.
            tempV3d = coordinates[0];
            pD = Vector3.Dot(pNrm, tempV3d);

            if (prettyDarnCloseToZero(pD - Vector3.Dot(pNrm, point), 0.001f))
                return true;

            return false;

        }

        public bool Unproject(ref Matrix4 matrix, Rectangle viewport, Vector2 inPoint, out Vector3 outPoint, float z, float minDepth, float maxDepth)
        {
            if (viewport.X != 0)
                inPoint.X -= viewport.X;
            if (viewport.Y != 0)
                inPoint.Y -= viewport.Y;

            var mp = new Vector3((inPoint.X - 0) / viewport.Width * 2.0f - 1.0f,
                                    -((inPoint.Y - 0) / viewport.Height * 2.0f - 1.0f),
                                    (z - minDepth) / (maxDepth - minDepth));

            var invert = Matrix4.Invert(matrix);
            Vector3.Transform(ref mp, ref invert, out outPoint);

            float a = mp.X * invert.M14 + mp.Y * invert.M24 + mp.Z * invert.M34 + invert.M44;

            if (a <= float.Epsilon)
                return false;

            outPoint /= a;
            return true;
        }

        static float distValue(float min, float max, float v)
        {
            if (v < min || v > max)
                throw new Exception();

            return (v - min) / (max - min);
        }

        public bool GetPick(int x, int y, out Point hitPixel)
        {
            hitPixel = new Point(-1, -1);

            if (x < 0 || y < 0 || x >= RenderWidth || y >= RenderHeight)
                return false;
            var vp2d = new Rectangle(GetViewport2D().X, RenderHeight - GetViewport2D().Bottom, GetViewport2D().Width, GetViewport2D().Height);
            var vp3d = GetViewport3D();
            var mousePos = new Vector2(x, y);
            if (_currentViewMode == ViewMode.Orthographic || _currentViewMode == ViewMode.Hybrid && GetViewport2D().Contains(x, y))
            {
                Vector3 output;

                if (Unproject(ref _orthoCameraMatrix, vp2d, new Vector2(mousePos.X - GetViewport2D().X, mousePos.Y - GetViewport2D().Top * 2 + (RenderHeight - GetViewport2D().Bottom)), out output, 0, -1, 1))
                {
                    if (output.X < 0 || output.Y < 0 ||
                        output.X >= CurrentModel.DefaultWidth ||
                        output.Y >= CurrentModel.DefaultHeight)
                        return false;

                    hitPixel = new Point((int)Math.Floor(output.X * (_lastSkin.Width / CurrentModel.DefaultWidth)), (int)Math.Floor(output.Y * (_lastSkin.Width / CurrentModel.DefaultWidth)));
                    return true;
                }

                return false;
            }
            else if (_currentViewMode == ViewMode.Perspective || _currentViewMode == ViewMode.Hybrid && vp3d.Contains(x, y))
            {
                Vector3 outputNear, outputFar;

                if (Unproject(ref _projectionMatrix, vp3d, mousePos, out outputNear, 8, 8, 512) &&
                    Unproject(ref _projectionMatrix, vp3d, mousePos, out outputFar, 512, 8, 512))
                {
                    float nearest = float.MaxValue;
                    Face? hitFace = null;
                    Vector3[] hitVerts = null;

                    foreach (var mesh in CurrentModel.Meshes)
                    {
                        if (!CurrentModel.PartsEnabled[mesh])
                            continue;

                        foreach (var face in mesh.Faces)
                        {
                            var verts = (Vector3[])face.Vertices.Clone();
                            float dist;

                            for (var i = 0; i < face.Vertices.Length; ++i)
                                Vector3.Transform(ref verts[i], ref mesh.Matrix, out verts[i]);

                            if (segmentAndPoly(verts, CameraPosition, outputFar, out dist))
                            {
                                if (hitFace == null || dist < nearest)
                                {
                                    nearest = dist;
                                    hitFace = face;

                                    var dir = Vector3.Normalize(outputFar - CameraPosition);
                                    outputFar = CameraPosition + dir * dist;
                                    hitVerts = verts;
                                }
                            }
                        }
                    }

                    if (hitFace.HasValue)
                    {
                        var face = hitFace.Value;
                        /*var tl = hitVerts[1];
						var br = hitVerts[3];

						var inside = outputFar - tl;
						br -= tl;

						var axis = 0;
						Vector2 texcoord;

						for (var i = 0; i < 3; ++i)
						{
							if (Math.Abs(br[i]) <= 0.001f)
								axis = i;
							else
								inside[i] /= br[i];
						}

						switch (axis)
						{
							case 2:
							default:
								texcoord = new Vector2(inside[0], inside[1]);
								break;
							case 0:
								texcoord = new Vector2(inside[2], inside[1]);
								break;
							case 1:
								texcoord = new Vector2(inside[0], inside[2]);
								break;
						}
							
						hitPixel = new Point(
							(int)Math.Floor((face.TexCoords[1].X + ((face.TexCoords[3].X - face.TexCoords[1].X) * texcoord.X)) * CurrentModel.DefaultWidth),
							(int)Math.Floor((face.TexCoords[1].Y + ((face.TexCoords[3].Y - face.TexCoords[1].Y) * texcoord.Y)) * CurrentModel.DefaultHeight));*/

                        var tri0 = new int[] { face.Indices[0], face.Indices[1], face.Indices[2] };
                        var tri1 = new int[] { face.Indices[0], face.Indices[2], face.Indices[3] };

                        var orig = CameraPosition;
                        var dir = outputFar - CameraPosition;

                        float t = 0, u = 0, v = 0;
                        int[] indicesHit;

                        if (rayTriangleIntersect(orig, dir, hitVerts[tri0[0]], hitVerts[tri0[1]], hitVerts[tri0[2]], ref t, ref u, ref v))
                        {
                            indicesHit = tri0;
                        }
                        else if (rayTriangleIntersect(orig, dir, hitVerts[tri1[0]], hitVerts[tri1[1]], hitVerts[tri1[2]], ref t, ref u, ref v))
                        {
                            indicesHit = tri1;
                        }
                        else
                            // how?
                            return false;

                        var st0 = face.TexCoords[indicesHit[0]];
                        var st1 = face.TexCoords[indicesHit[1]];
                        var st2 = face.TexCoords[indicesHit[2]];

                        //var coord = u * tc0 + v * tc1 + (1 - u - v) * tc2;
                        var coord = (1 - u - v) * st0 + u * st1 + v * st2;
                        hitPixel = new Point((int)Math.Floor(coord.X * _lastSkin.Width), (int)Math.Floor(coord.Y * _lastSkin.Height));

                        return true;
                    }

                    return false;
                }

                return false;
            }

            return false;
        }


        public Rectangle GetViewport3D()
        {
            return rectangle3D.GetValueOrDefault(Rectangle.Empty);
        }

        public Rectangle GetViewport2D()
        {
            return rectangle2D.GetValueOrDefault(Rectangle.Empty);
        }

        public static bool IsRendering { get; private set; }

        //static uint frameCount = 0;
        static TimeSpan _compileSpan = TimeSpan.MinValue,
                        _batchSpan = TimeSpan.MinValue,
                        _sortSpan = TimeSpan.MinValue,
                        _renderSpan = TimeSpan.MinValue;

        static void DrawCube(float width, float height, float depth, bool textured)
        {
            if (textured)
            {
                _cubeSides.Bind();
                GL.Color4((byte)255, (byte)255, (byte)255, (byte)(255 - 88));
            }
            else
                GL.Color4((byte)88, (byte)88, (byte)88, (byte)(255 - 88));

            GL.Begin(PrimitiveType.Quads);

            float xSep = 64.0f / 256.0f;
            float ySep = 64.0f / 128.0f;

            // front
            GL.TexCoord2(0, ySep); GL.Vertex3(-(width / 2), -(height / 2), -(depth / 2));
            GL.TexCoord2(xSep, ySep); GL.Vertex3(width / 2, -(height / 2), -(depth / 2));
            GL.TexCoord2(xSep, 0); GL.Vertex3(width / 2, height / 2, -(depth / 2));
            GL.TexCoord2(0, 0); GL.Vertex3(-(width / 2), height / 2, -(depth / 2));

            // back
            GL.TexCoord2(xSep, ySep); GL.Vertex3(width / 2, -(height / 2), depth / 2);
            GL.TexCoord2(xSep * 2, ySep); GL.Vertex3(-(width / 2), -(height / 2), depth / 2);
            GL.TexCoord2(xSep * 2, 0); GL.Vertex3(-(width / 2), height / 2, depth / 2);
            GL.TexCoord2(xSep, 0); GL.Vertex3(width / 2, height / 2, depth / 2);

            bool invertTopBottom = !(Math.Cos(MathHelper.DegreesToRadians(_3DRotationY)) < 0);

            // top
            if (invertTopBottom)
            {
                GL.TexCoord2(xSep * 2, ySep); GL.Vertex3(-(width / 2), height / 2, -(depth / 2));
                GL.TexCoord2(xSep * 3, ySep); GL.Vertex3(width / 2, height / 2, -(depth / 2));
                GL.TexCoord2(xSep * 3, 0); GL.Vertex3(width / 2, height / 2, depth / 2);
                GL.TexCoord2(xSep * 2, 0); GL.Vertex3(-(width / 2), height / 2, depth / 2);
            }
            else
            {
                GL.TexCoord2(xSep * 3, 0); GL.Vertex3(-(width / 2), height / 2, -(depth / 2));
                GL.TexCoord2(xSep * 2, 0); GL.Vertex3(width / 2, height / 2, -(depth / 2));
                GL.TexCoord2(xSep * 2, ySep); GL.Vertex3(width / 2, height / 2, depth / 2);
                GL.TexCoord2(xSep * 3, ySep); GL.Vertex3(-(width / 2), height / 2, depth / 2);
            }

            // bottom
            if (invertTopBottom)
            {
                GL.TexCoord2(xSep * 4, 0); GL.Vertex3(width / 2, -(height / 2), -(depth / 2));
                GL.TexCoord2(xSep * 3, 0); GL.Vertex3(-(width / 2), -(height / 2), -(depth / 2));
                GL.TexCoord2(xSep * 3, ySep); GL.Vertex3(-(width / 2), -(height / 2), depth / 2);
                GL.TexCoord2(xSep * 4, ySep); GL.Vertex3(width / 2, -(height / 2), depth / 2);
            }
            else
            {
                GL.TexCoord2(xSep * 3, ySep); GL.Vertex3(width / 2, -(height / 2), -(depth / 2));
                GL.TexCoord2(xSep * 4, ySep); GL.Vertex3(-(width / 2), -(height / 2), -(depth / 2));
                GL.TexCoord2(xSep * 4, 0); GL.Vertex3(-(width / 2), -(height / 2), depth / 2);
                GL.TexCoord2(xSep * 3, 0); GL.Vertex3(width / 2, -(height / 2), depth / 2);
            }

            // left
            GL.TexCoord2(0, 1); GL.Vertex3(-(width / 2), -(height / 2), depth / 2);
            GL.TexCoord2(xSep, 1); GL.Vertex3(-(width / 2), -(height / 2), -(depth / 2));
            GL.TexCoord2(xSep, ySep); GL.Vertex3(-(width / 2), height / 2, -(depth / 2));
            GL.TexCoord2(0, ySep); GL.Vertex3(-(width / 2), height / 2, depth / 2);

            // right
            GL.TexCoord2(xSep * 2, ySep); GL.Vertex3(width / 2, height / 2, depth / 2);
            GL.TexCoord2(xSep, ySep); GL.Vertex3(width / 2, height / 2, -(depth / 2));
            GL.TexCoord2(xSep, 1); GL.Vertex3(width / 2, -(height / 2), -(depth / 2));
            GL.TexCoord2(xSep * 2, 1); GL.Vertex3(width / 2, -(height / 2), depth / 2);

            GL.End();

            TextureGL.Unbind();
        }

        private double _firstViewLengthRatio = 0.5;
        public double FirstViewLengthRatio
        {
            get { return _firstViewLengthRatio; }
            set { _firstViewLengthRatio = value; CalculateMatrices(); InvalidateRenderer(); }
        }

        public int FirstViewLength
        {
            get
            {
                if (IsSplitterHorizontal)
                    return (int)(RenderHeight * FirstViewLengthRatio);
                else
                    return (int)(RenderWidth * FirstViewLengthRatio);
            }
        }
        public bool IsSplitViewInvert { get; set; } = false;


        private bool _isSplitterHorizontal = false;
        public bool IsSplitterHorizontal
        {
            get { return _isSplitterHorizontal; }
            set
            {
                _isSplitterHorizontal = value;

                CalculateMatrices();
                InvalidateRenderer();
            }
        }

        Rectangle? rectangle2D = null;
        Rectangle? rectangle3D = null;

        public void CalculateViewportRectangle()
        {
            if (_currentViewMode == ViewMode.Perspective)
            {
                rectangle3D = new Rectangle(0, 0, RenderWidth, RenderHeight);
                rectangle2D = null;
            }
            else if (_currentViewMode == ViewMode.Orthographic)
            {
                rectangle2D = new Rectangle(0, 0, RenderWidth, RenderHeight);
                rectangle3D = null;
            }
            else
            {
                if (IsSplitterHorizontal)
                {
                    if (IsSplitViewInvert)
                    {
                        rectangle2D = new Rectangle(0, 0, RenderWidth, FirstViewLength);
                        rectangle3D = new Rectangle(0, FirstViewLength, RenderWidth, RenderHeight - FirstViewLength);
                    }
                    else
                    {
                        rectangle3D = new Rectangle(0, 0, RenderWidth, FirstViewLength);
                        rectangle2D = new Rectangle(0, FirstViewLength, RenderWidth, RenderHeight - FirstViewLength);

                        //Console.WriteLine(rectangle3D);
                    }
                }
                else
                {
                    if (IsSplitViewInvert)
                    {
                        rectangle2D = new Rectangle(0, 0, FirstViewLength, RenderHeight);
                        rectangle3D = new Rectangle(FirstViewLength, 0, RenderWidth - FirstViewLength, RenderHeight);
                    }
                    else
                    {
                        rectangle3D = new Rectangle(0, 0, FirstViewLength, RenderHeight);
                        rectangle2D = new Rectangle(FirstViewLength, 0, RenderWidth - FirstViewLength, RenderHeight);
                    }

                }
            }


        }

        public void rendererControl_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                GL.Color4((byte)255, (byte)255, (byte)255, (byte)255);

                IsRendering = true;
                //if (GlobalSettings.RenderBenchmark)
                //    _renderTimer.Start();

                RenderMakeCurrent();

                _mousePoint = GetRenderCursorPos();

                if (!GlobalSettings.StylusToDraw || !isTouchDown)
                    SetPreview();

                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

                Skin skin = _lastSkin;

                GL.PushMatrix();

                if (_currentViewMode == ViewMode.Perspective)
                {
                    if (rectangle2D != null || rectangle3D == null)
                        CalculateViewportRectangle();

                    Setup3D(GetViewport3D());

                    DrawPlayer(_previewPaint, skin, !Program.Page_Editor.IsViewportError);

                    int sizeOfMiniport = (int)(120 * RenderScale);
                    float sizeOfCube = sizeOfMiniport;


                    GL.Clear(ClearBufferMask.DepthBufferBit);

                    Setup2D(new Rectangle(GetViewport3D().Width - sizeOfMiniport, 0, sizeOfMiniport, sizeOfMiniport));
                    GL.Enable(EnableCap.DepthTest);
                    GL.Enable(EnableCap.CullFace);
                    GL.Enable(EnableCap.Blend);
                    GL.CullFace(CullFaceMode.Back);

                    TextureGL.Unbind();

                    GL.Translate(sizeOfCube / 2, sizeOfCube / 2, -8);
                    GL.Rotate(-_3DRotationX, 1, 0, 0);
                    GL.Rotate(-_3DRotationY, 0, 1, 0);
                    DrawCube(sizeOfCube / 2, sizeOfCube / 2, sizeOfCube / 2, true);

                    GL.Disable(EnableCap.DepthTest);
                    GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
                    DrawCube(sizeOfCube / 2, sizeOfCube / 2, sizeOfCube / 2, false);
                    GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);

                    GL.Disable(EnableCap.Blend);
                    GL.Disable(EnableCap.CullFace);
                    GL.CullFace(CullFaceMode.Front);

                }
                else if (_currentViewMode == ViewMode.Orthographic)
                {
                    if (rectangle2D == null || rectangle3D != null)
                        CalculateViewportRectangle();

                    Setup2D(GetViewport2D());
                    DrawPlayer2D(_previewPaint, skin, !Program.Page_Editor.IsViewportError);
                }
                else
                {
                    if (rectangle2D == null || rectangle3D == null)
                        CalculateViewportRectangle();

                    if (rectangle2D.HasValue)
                    {
                        Setup2D(rectangle2D.Value);
                        DrawPlayer2D(_previewPaint, skin, !Program.Page_Editor.IsViewportError);
                    }

                    if (rectangle3D.HasValue)
                    {
                        Setup3D(rectangle3D.Value);
                        DrawPlayer(_previewPaint, skin, !Program.Page_Editor.IsViewportError);
                    }

                }

                GL.PopMatrix();
                //_renderTimer.Stop();

                //if (!_screenshotMode)
                RendererControl?.SwapBuffers();
                IsRendering = false;
            }
            catch (Exception ex)
            {
                Program.RaiseException(ex);
            }
        }

        Matrix4 _orthoMatrix, _orthoCameraMatrix, _projectionMatrix, _viewMatrix3d;

        public void CalculateMatrices()
        {
            try
            {
                CalculateViewportRectangle();

                Rectangle viewport = GetViewport3D();
                _projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45), viewport.Width / (float)viewport.Height, 8, 512);

                viewport = GetViewport2D();
                _orthoMatrix = Matrix4.CreateOrthographicOffCenter(viewport.Left, viewport.Right, viewport.Bottom, viewport.Top, -1, 1);

                Bounds3 vec = Bounds3.EmptyBounds;
                Bounds3 allBounds = Bounds3.EmptyBounds;
                int count = 0;

                if (CurrentModel != null)
                {
                    var viewMatrix =
                        Matrix4.CreateTranslation(-(CurrentModel.DefaultWidth / 2), -(CurrentModel.DefaultHeight / 2), 0) *
                        Matrix4.CreateTranslation(_2DCamOffsetX, _2DCamOffsetY, 0) *
                        Matrix4.CreateScale(_2DZoom, _2DZoom, 1) *
                        Matrix4.CreateTranslation(viewport.Width / 2, viewport.Height / 2, 0);

                    _orthoCameraMatrix = viewMatrix * _orthoMatrix;

                    int meshIndex = 0;
                    foreach (Mesh mesh in CurrentModel.Meshes)
                    {
                        allBounds += mesh.Bounds;

                        vec += mesh.Bounds;
                        count++;
                        //if (CurrentModel.PartsEnabled[mesh])
                        //{
                        //    vec += mesh.Bounds;
                        //    count++;
                        //}

                        meshIndex++;
                    }
                }

                if (count == 0)
                    vec.Mins = vec.Maxs = allBounds.Mins = allBounds.Maxs = Vector3.Zero;

                GrassY = allBounds.Maxs.Y;

                if (CurrentModel == null)
                    GrassY = GrassY + 10;

                Vector3 center = vec.Center;

                _viewMatrix3d =
                    Matrix4.CreateTranslation(-center.X + _3DOffset.X, -center.Y + _3DOffset.Y, -center.Z + _3DOffset.Z) *
                    Matrix4.CreateFromAxisAngle(new Vector3(0, -1, 0), MathHelper.DegreesToRadians(_3DRotationY)) *
                    Matrix4.CreateFromAxisAngle(new Vector3(1, 0, 0), MathHelper.DegreesToRadians(_3DRotationX)) *
                    Matrix4.CreateTranslation(_3DCamOffsetX, _3DCamOffsetY, _3DZoom);

                _projectionMatrix = _viewMatrix3d * _projectionMatrix;

                var cameraMatrix = _viewMatrix3d;
                cameraMatrix.Invert();


                CameraPosition = Vector3.TransformPosition(Vector3.Zero, cameraMatrix);
            }
            catch (Exception ex)
            {
                Program.Log(ex, false);
            }
        }

        private void Setup3D(Rectangle viewport)
        {
            _currentViewport = viewport;

            if (!_firstCalc)
            {
                CalculateMatrices();
                _firstCalc = true;
            }

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            GL.Viewport(new Rectangle(viewport.X, RenderHeight - viewport.Bottom, viewport.Width, viewport.Height));
            GL.MultMatrix(ref _projectionMatrix);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            //GL.LoadMatrix(ref _viewMatrix3d);

            GL.Enable(EnableCap.DepthTest);
        }

        private void Setup2D(Rectangle viewport)
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();


            GL.Viewport(new Rectangle(viewport.X, RenderHeight - viewport.Bottom, viewport.Width, viewport.Height));
            GL.Ortho(0, viewport.Width, viewport.Height, 0, -99, 99);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            _currentViewport = viewport;

            GL.Disable(EnableCap.DepthTest);
        }

        public void RotateView(Point delta, float factor)
        {
            if (_currentViewMode == ViewMode.Perspective || _currentViewMode == ViewMode.Hybrid && _mouseIn3D)
            {
                _3DRotationY += delta.X * ToolScale * factor;
                _3DRotationX += delta.Y * ToolScale * factor;
            }
            else
            {
                _2DCamOffsetX += delta.X / _2DZoom;
                _2DCamOffsetY += delta.Y / _2DZoom;
            }

            CalculateMatrices();
            InvalidateRenderer();
        }

        public void ScaleView(Point delta, float factor)
        {
            if (_currentViewMode == ViewMode.Perspective || _currentViewMode == ViewMode.Hybrid && _mouseIn3D)
            {
                _3DZoom += -delta.Y * ToolScale * factor;
            }
            else
            {
                _2DZoom += -delta.Y / 25.0f;

                if (_2DZoom < 0.25f)
                    _2DZoom = 0.25f;
            }

            CalculateMatrices();
            InvalidateRenderer();
        }
        public void RotateView(double x, double y)
        {
            if (_currentViewMode == ViewMode.Perspective || _currentViewMode == ViewMode.Hybrid && _mouseIn3D)
            {
                _3DRotationY += (float)(x * ToolScale);
                _3DRotationX += (float)(y * ToolScale);
            }
            else
            {
                _2DCamOffsetX += (float)(x / _2DZoom);
                _2DCamOffsetY += (float)(y / _2DZoom);
            }
        }
        public void TranslateView(Point delta, float factor)
        {
            if (_currentViewMode == ViewMode.Perspective || _currentViewMode == ViewMode.Hybrid && _mouseIn3D)
            {
                _3DCamOffsetX -= delta.X / _3DZoom;
                _3DCamOffsetY += delta.Y / _3DZoom;
            }
            else
            {
                _2DCamOffsetX += delta.X / _2DZoom;
                _2DCamOffsetY += delta.Y / _2DZoom;
            }

            CalculateMatrices();
            InvalidateRenderer();
        }
        public void ScaleView(float d, bool refresh = true)
        {
            if (_currentViewMode == ViewMode.Perspective || _currentViewMode == ViewMode.Hybrid && _mouseIn3D)
            {
                _3DZoom = _3DZoom * d;

                Debug.WriteLine(_3DZoom);

                if (_3DZoom < -450f)
                    _3DZoom = -450f;
                if (_3DZoom > -10f)
                    _3DZoom = -10f;

            }
            else
            {
                _2DZoom = _2DZoom / d;

                if (_2DZoom < 0.25f)
                    _2DZoom = 0.25f;
                if (_2DZoom > 256f)
                    _2DZoom = 256f;
            }

            if (refresh)
            {
                CalculateMatrices();
                InvalidateRenderer();
            }
        }
        private void CheckMouse(int x, int y)
        {
            if (rectangle3D != null && rectangle3D.Value.Contains(x, y))
                _mouseIn3D = true;
            else
                _mouseIn3D = false;
        }
        private void CheckMouse(Point p)
        {
            CheckMouse(p.X, p.Y);
        }
        private void CheckMouse(WPF.Point p)
        {
            CheckMouse(p.ToDrawingRectangle());
        }

        public void SetPartTransparencies()
        {
            using (var grabber = new ColorGrabber(GlobalDirtiness.CurrentSkin, _lastSkin.Width, _lastSkin.Height))
            {
                grabber.Load();

                var paintedParts = new Dictionary<int, bool>();

                foreach (var p in _paintedPixels)
                {
                    List<int> parts = CurrentModel.GetIntersectingParts(p.Key, _lastSkin);

                    foreach (int part in parts)
                    {
                        if (!paintedParts.ContainsKey(part))
                            paintedParts.Add(part, true);
                    }
                }

                foreach (var p in paintedParts)
                    _lastSkin.CheckTransparentPart(grabber, p.Key);
            }

            _paintedPixels.Clear();
        }

        private void SkinLibrary_SelectedNodeChanged(object sender, EventArgs e)
        {
            try
            {
                if (SkinLibrary.SelectedNode == null)
                {
                    Program.Page_Editor?.SetViewportPopupAbout(true);
                    return;
                }
                if (SkinLibrary.SelectedNode is FolderNode)
                {
                    //Program.Page_Editor?.SetViewportPopupError(SkinLibrary.SelectedNode.Text +"\r\n(" + Directory.GetFiles((SkinLibrary.SelectedNode as FolderNode).Path, "*.png", System.IO.SearchOption.AllDirectories).Length.ToString() + " skins)", SkinLibrary.SelectedNode.Path, Pages.PageEditor.FontIcon_FolderOpen, false);
                    Program.Page_Editor?.SetViewportPopupAbout(true);
                    return;
                }

                if (!(SkinLibrary.SelectedNode as Skin).IsLoaded)
                {
                    Exception ex = (SkinLibrary.SelectedNode as Skin).SetImages();
                    if (ex != null)
                    {
                        Program.Page_Editor.SetViewportPopupError(Program.GetLanguageString("M_UNABLELOADSKIN") + "\r\n" + ex.Message, SkinLibrary.SelectedNode.Path + "\r\n\r\n" + ex.StackTrace);
                        return;
                    }
                    else
                    {
                        Program.Page_Editor.SetViewportPopupError();
                    }
                }


                RenderMakeCurrent();

                if (_lastSkin != null) // && SelectedNode != _lastSkin)
                {
                    // Copy over the current changes to the tex stored in the skin.
                    // This allows us to pick up where we left off later, without undoing any work.
                    _lastSkin.CommitChanges(GlobalDirtiness.CurrentSkin, false);

                    // if we aren't dirty, unload
                    if (!_lastSkin.IsDirty)
                        _lastSkin.Unload();
                }

                //if (_lastSkin != null)
                //	_lastSkin.Undo.Clear();

                var skin = (Skin)SkinLibrary.SelectedNode;
                SetCanSave(skin.IsDirty);


                if (skin.GLImage == null)
                    skin.Create();

                if (skin == null)
                {
                    _currentUndoBuffer = null;
                    TextureGL.Unbind();

                    using (var currentSkin = new ColorGrabber(GlobalDirtiness.CurrentSkin, 64, 32))
                        currentSkin.Save();

                    if (Program.Window_Main != null)
                    {
                        Program.Page_Editor.SetTrayButtonEnabled(1, false);
                        Program.Page_Editor.SetTrayButtonEnabled(2, false);
                    }

                }
                else
                {
                    using (var glImage = new ColorGrabber(skin.GLImage, skin.Width, skin.Height))
                    {
                        glImage.Load();

                        glImage.Texture = GlobalDirtiness.CurrentSkin;
                        glImage.Save();
                        glImage.Texture = _previewPaint;
                        glImage.Save();
                    }

                    _currentUndoBuffer = skin.Undo;
                    CheckUndo();
                }

                _lastSkin = (Skin)SkinLibrary.SelectedNode;

                SetModel(skin.Model);
                InvalidateRenderer();

                Program.Page_Editor.MenuItem_Resoultion_Hint.Header = string.Format("{2}: {0}×{1}", _lastSkin.Width, _lastSkin.Height, Program.GetLanguageString("M_CURRENT"));

                FillPartList();

                Program.Page_Editor.SetViewportPopupError();


                Program.Log(LogType.Info, string.Format("Set current skin to '{0}'", _lastSkin.FileInfo.Name), "at MCSkinn.Editor.treeView1_AfterSelect(object, TreeViewEventArgs)");

            }
            catch (Exception ex)
            {
                if (ex != null)
                {
                    Program.Page_Editor.SetViewportPopupError(Program.GetLanguageString("M_UNABLELOADSKIN") + "\r\n" + SkinLibrary.SelectedNode.Path + "\r\n\r\n" + ex.Message, ex.StackTrace);
                    Program.Log(ex);
                }
            }
            finally
            {
                refreshNeeded = true;
            }

        }
        static void CreateModelDropdownItems(WPFC.ItemCollection mainCollection, bool main, Action<Model> callback, string prefix)
        {
            Dictionary<string, WPFC.MenuItem> FolderMenus = new Dictionary<string, WPFC.MenuItem>();
            foreach (var x in ModelLoader.Models)
            {
                WPFC.ItemCollection collection = mainCollection;
                string path = Path.GetDirectoryName(x.Value.File.ToString()).Substring(GlobalSettings.FullPath_Models.Length);

                WPFC.MenuItem subi = null;

                while (!string.IsNullOrEmpty(path))
                {
                    string sub = path.Substring(1, path.IndexOf('\\', 1) == -1 ? path.Length - 1 : path.IndexOf('\\', 1) - 1);

                    if (FolderMenus.ContainsKey(ModelToolStripMenuItem.GetName(sub, prefix) + "_FOLDER"))
                        subi = FolderMenus[ModelToolStripMenuItem.GetName(sub, prefix) + "_FOLDER"];
                    else
                        subi = null;

                    if (subi == null)
                    {
                        var item = new WPFC.MenuItem();
                        item.Header = sub;
                        item.Name = ModelToolStripMenuItem.GetName(sub, prefix) + "_FOLDER";
                        collection.Add(item);
                        FolderMenus.Add(item.Name, item);
                        collection = item.Items;
                        subi = item;

                    }
                    else
                    {
                        collection = subi.Items;
                    }

                    path = path.Remove(0, sub.Length + 1);
                }
                collection.Add(new ModelToolStripMenuItem(x.Value, main, callback, prefix, subi));
            }
        }

        public void FinishedLoadingModels()
        {
            CreateModelDropdownItems(Program.Page_Editor.MenuFlyout_Model.Items, true, (model) =>
            {
                SetModel(model);
            }, "model_");
            //CreateModelDropdownItems(Program.Page_Editor.MenuItem_NewSkin.Items, false, (model) =>
            //{
            //    PerformNewSkin(model);
            //}, "create_");


            RenderMakeCurrent();

            // Compile model userdata
            foreach (var model in ModelLoader.Models)
            {
                foreach (var m in model.Value.Meshes)
                {
                    m.UserData = MeshRenderer.CreateUserData(m);
                    MeshRenderer.UpdateUserData(m);
                }
            }
        }

        #endregion

        #region UI Events
        private void bROWSEIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PerformBrowseTo();
        }
        void _popoutForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }
        private void item_Clicked(object sender, EventArgs e)
        {
            var item = (ToolStripMenuItem)sender;
            _backgrounds[_selectedBackground].Item.Checked = false;
            _selectedBackground = (int)item.Tag;
            GlobalSettings.LastBackground = _backgrounds[_selectedBackground].Path;
            item.Checked = true;
        }


        public void CheckUndo()
        {
            if (_currentUndoBuffer != null)
            {
                Program.Page_Editor?.SetTrayButtonEnabled(1, _currentUndoBuffer.CanUndo);
                Program.Page_Editor?.SetTrayButtonEnabled(2, _currentUndoBuffer.CanRedo);
            }
            else
            {
                Program.Page_Editor?.SetTrayButtonEnabled(1, false);
                Program.Page_Editor?.SetTrayButtonEnabled(2, false);
            }
        }





        public static string GetLanguageString(string id)
        {
            return Program.GetLanguageString(id);
        }



        private void toolStripMenuItem4_Click_1(object sender, EventArgs e)
        {
            Dictionary<Vector3, int> vertDict = new Dictionary<Vector3, int>();
            Dictionary<Vector2, int> texCoordDict = new Dictionary<Vector2, int>();

            foreach (var m in CurrentModel.Meshes)
            {
                foreach (var f in m.Faces)
                {
                    if (f.Indices.Length != 4)
                    {
                        Debugger.Break();
                        continue;
                    }

                    for (var i = 0; i < 4; ++i)
                    {
                        var fv = Vector3.Transform(f.Vertices[i], m.Matrix);
                        var ft = f.TexCoords[i];

                        if (!vertDict.ContainsKey(fv))
                            vertDict.Add(fv, vertDict.Count);
                        if (!texCoordDict.ContainsKey(ft))
                            texCoordDict.Add(ft, texCoordDict.Count);
                    }
                }
            }

            string s = "";

            foreach (var v in vertDict)
                s += string.Format("v {0} {1} {2}\r\n", v.Key.X, -v.Key.Y, v.Key.Z);

            foreach (var v in texCoordDict)
                s += string.Format("vt {0} {1}\r\n", v.Key.X, 1 - v.Key.Y);

            foreach (var m in CurrentModel.Meshes)
            {
                s += string.Format("g {0}\r\n", m.Name);
                s += "s 1\r\n";

                foreach (var f in m.Faces)
                {
                    if (f.Indices.Length != 4)
                    {
                        Debugger.Break();
                        continue;
                    }

                    int[] vi = new int[4];
                    int[] vt = new int[4];

                    for (var i = 0; i < 4; ++i)
                    {
                        vi[i] = vertDict[Vector3.Transform(f.Vertices[i], m.Matrix)] + 1;
                        vt[i] = texCoordDict[f.TexCoords[i]] + 1;
                    }

                    s += string.Format("f {0}/{1} {2}/{3} {4}/{5} {6}/{7}\r\n", vi[0], vt[0], vi[1], vt[1], vi[2], vt[2], vi[3], vt[3]);
                }
            }

            Clipboard.SetText(s);
        }
        #endregion

        #region Shortcuts

        private string CompileShortcutKeys()
        {
            string c = "";

            for (int i = 0; i < _shortcutEditor.ShortcutCount; ++i)
            {
                IShortcutImplementor shortcut = _shortcutEditor.ShortcutAt(i);

                if (i != 0)
                    c += "|";

                Keys key = shortcut.Keys & ~Keys.Modifiers;
                var modifiers = (Keys)((int)shortcut.Keys - (int)key);

                if (modifiers != 0)
                    c += shortcut.SaveName + "=" + key + "+" + modifiers;
                else
                    c += shortcut.SaveName + "=" + key;
            }

            return c;
        }

        private IShortcutImplementor FindShortcut(string name)
        {
            foreach (IShortcutImplementor s in _shortcutEditor.Shortcuts)
            {
                if (s.SaveName == name)
                    return s;
            }

            return null;
        }

        private void LoadShortcutKeys(string s)
        {
            if (string.IsNullOrEmpty(s))
                return; // leave defaults

            string[] shortcuts = s.Split('|');

            foreach (string shortcut in shortcuts)
            {
                string[] args = shortcut.Split('=');

                string name = args[0];
                string key;
                string modifiers = "0";

                if (args[1].Contains('+'))
                {
                    string[] mods = args[1].Split('+');

                    key = mods[0];
                    modifiers = mods[1];
                }
                else
                    key = args[1];

                IShortcutImplementor sh = FindShortcut(name);

                if (sh == null)
                    continue;

                sh.Keys = (Keys)Enum.Parse(typeof(Keys), key) | (Keys)Enum.Parse(typeof(Keys), modifiers);
            }
        }

        private void InitMenuShortcut(ToolStripMenuItem item, Action callback)
        {
            var shortcut = new MenuStripShortcut(item);
            shortcut.Pressed = callback;

            _shortcutEditor.AddShortcut(shortcut);
        }

        private void InitMenuShortcut(ToolStripMenuItem item, Keys keys, Action callback)
        {
            var shortcut = new MenuStripShortcut(item, keys);
            shortcut.Pressed = callback;

            _shortcutEditor.AddShortcut(shortcut);
        }

        private void InitUnlinkedShortcut(string name, Keys defaultKeys, Action callback)
        {
            var shortcut = new ShortcutBase(name, defaultKeys);
            shortcut.Pressed = callback;

            _shortcutEditor.AddShortcut(shortcut);
        }

        private void InitControlShortcut(string name, Control control, Keys defaultKeys, Action callback)
        {
            var shortcut = new ControlShortcut(name, defaultKeys, control);
            shortcut.Pressed = callback;

            _shortcutEditor.AddShortcut(shortcut);
        }


        private void PerformSwitchColor()
        {
            //colorPanel.SwitchColors();
        }

        public void SetSelectedTool(ToolIndex index)
        {
            if (_selectedTool != null)
                _selectedTool.MenuItem.Checked = _selectedTool.Button.Checked = false;

            ToolIndex oldTool = _selectedTool;
            _selectedTool = index;
            index.MenuItem.Checked = index.Button.Checked = true;

            SelectedToolChanged?.Invoke(this, EventArgs.Empty);

            //toolStripStatusLabel1.Text = index.Tool.GetStatusLabelText();

            if (Program.Window_Main != null)
                Program.Page_Editor.TextBlock_Status.Text = index.Tool.GetStatusLabelText();

            Program.Log(LogType.Info, string.Format("Set current tool to {0}", index.Identifer), "at MCSkinn.Editor.SetSelectedTool(ToolIndex)");

        }

        public event EventHandler SelectedToolChanged;

        private void ToolMenuItemClicked(object sender, EventArgs e)
        {
            var item = (ToolStripItem)sender;
            SetSelectedTool((ToolIndex)item.Tag);
        }



        public static bool PerformShortcut(Keys key, Keys modifiers)
        {
            Program.Log(LogType.Info, string.Format("Shortcut key down '{0}' ({1})", key.ToString(), modifiers.ToString()), "at MCSkinn.Editor.PerformShortcut()");

            foreach (IShortcutImplementor shortcut in _shortcutEditor.Shortcuts)
            {
                if (shortcut.CanEvaluate() && (shortcut.Keys & ~Keys.Modifiers) == key &&
                    (shortcut.Keys & ~(shortcut.Keys & ~Keys.Modifiers)) == modifiers)
                {
                    shortcut.Pressed();
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region Overrides

        private bool CheckKeyShortcut(KeyEventArgs e)
        {
            //if (!colorPanel.HexTextBox.ContainsFocus &&
            //    !labelEditTextBox.ContainsFocus &&
            //    !colorPanel.SwatchContainer.SwatchRenameTextBoxHasFocus)
            //{
            //    if (PerformShortcut(e.KeyCode & ~Keys.Modifiers, e.Modifiers))
            //        return true;
            //}

            return false;
        }

        //protected override void OnKeyDown(KeyEventArgs e)
        //{
        //    if ((e.Handled = CheckKeyShortcut(e)))
        //        return;

        //    base.OnKeyDown(e);
        //}

        private void popoutForm_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = CheckKeyShortcut(e);
        }

        public bool FormClosing()
        {
            bool isDirty = false;
            foreach (Workfolder f in GlobalSettings.SkinDirectories)
            {
                if (!f.IsInitialized)
                    continue;

                if (f.IsDirty)
                {
                    isDirty = true;
                    break;
                }
            }
            if (isDirty)
            {
                if (
                    MessageBox.Show(GetLanguageString("C_UNSAVED"), GetLanguageString("C_UNSAVED_CAPTION"),
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return false;
                }
            }

            GlobalSettings.ShortcutKeys = CompileShortcutKeys();


            if (GlobalSettings.RememberPrefers && _lastSkin != null)
                GlobalSettings.LastSkin = SkinLibrary.GetAbsolutedPath(_lastSkin.FileInfo.FullName);
            else
                GlobalSettings.LastSkin = "";

            GlobalSettings.LastPalettePrimaryColor = Program.Page_Editor.ColorDisplay_MainDisplay.Color.N2D();
            GlobalSettings.LastPaletteSecondaryColor = Program.Page_Editor.ColorDisplay_MainDisplay.SecondColor.N2D();

            GlobalSettings.Save();

            return true;
        }

        public void RenderMakeCurrent()
        {
            if (IsCompatibilityModeOn)
            {
                RendererControl.MakeCurrent();
            }
        }

        private void DontCloseMe(object sender, ToolStripDropDownClosingEventArgs e)
        {
            if (e.CloseReason == ToolStripDropDownCloseReason.ItemClicked ||
                e.CloseReason == ToolStripDropDownCloseReason.Keyboard)
                e.Cancel = true;
        }

        #endregion

        #region Private Functions

        private readonly Dictionary<Point, bool> _paintedPixels = new Dictionary<Point, bool>();

        public Dictionary<Point, bool> PaintedPixels
        {
            get { return _paintedPixels; }
        }

        private void PixelWritten(Point p, ColorPixel c)
        {
            if (!_paintedPixels.ContainsKey(p))
                _paintedPixels.Add(p, true);
        }

        private void UseToolOnViewport(int x, int y, bool begin = false)
        {
            if (_lastSkin == null)
                return;

            if (_isValidPick)
            {
                Skin skin = _lastSkin;

                using (var currentSkin = new ColorGrabber(GlobalDirtiness.CurrentSkin, skin.Width, skin.Height))
                {
                    currentSkin.Load();
                    currentSkin.OnWrite = PixelWritten;

                    if (_selectedTool.Tool.MouseMoveOnSkin(currentSkin, skin, _pickPosition.X, _pickPosition.Y))
                    {
                        SetCanSave(true);
                        skin.IsDirty = true;
                        currentSkin.Save();
                    }
                }
            }

            InvalidateRenderer();
        }

        public void ToggleGrass()
        {
            //grassToolStripMenuItem.Checked = !grassToolStripMenuItem.Checked;
            //GlobalSettings.Grass = grassToolStripMenuItem.Checked;

            //Program.Page_Editor.MenuItem_View_3D_Grass.IsChecked = !Program.Page_Editor.MenuItem_View_3D_Grass.IsChecked;
            GlobalSettings.ShowGround = Program.Page_Editor.MenuItem_View_3D_Grass.IsChecked;

            InvalidateRenderer();
        }

        public void ToggleGhosting()
        {
            //ghostHiddenPartsToolStripMenuItem.Checked = !ghostHiddenPartsToolStripMenuItem.Checked;
            //GlobalSettings.Ghost = ghostHiddenPartsToolStripMenuItem.Checked;
            GlobalSettings.Ghost = Program.Page_Editor.MenuItem_View_3D_Ghost.IsChecked;

            InvalidateRenderer();
        }

        public void DoneEditingNode(string newName, LibraryNode _currentlyEditing)
        {
            //labelEditTextBox.Hide();

            //if (_currentlyEditing is Skin)
            //{
            //    var skin = (Skin)_currentlyEditing;

            //    if (skin.Name == newName)
            //        return;

            //    if (skin.ChangeName(newName) == false)
            //        SystemSounds.Beep.Play();
            //}
            //else
            //{
            //    var folder = (FolderNode)_currentlyEditing;

            //    folder.MoveTo(GetFolderForNode(_currentlyEditing.Parent) + newName);
            //}
        }

        public void BeginUndo()
        {
            RenderMakeCurrent();
        }

        public void DoUndo()
        {
            if (!_currentUndoBuffer.CanUndo)
                throw new Exception();

            _currentUndoBuffer.Undo();

            Program.Log(LogType.Info, "Undone action", "at MCSkinn.Editor.PerformNewFolder()");

        }

        public void EndUndo()
        {
            if (Program.Window_Main != null)
            {
                Program.Page_Editor.SetTrayButtonEnabled(1, _currentUndoBuffer.CanUndo);
                Program.Page_Editor.SetTrayButtonEnabled(2, _currentUndoBuffer.CanRedo);
            }

            SetCanSave(_lastSkin.IsDirty = true);

            InvalidateRenderer();
        }

        public void PerformUndo()
        {
            Program.Log(LogType.Load, "Performing Undo", "at MCSkinn.Editor.PerformNameUndo()");

            if (_currentUndoBuffer == null || !_currentUndoBuffer.CanUndo)
                return;

            BeginUndo();
            DoUndo();
            EndUndo();
        }

        public void undoToolStripButton_DropDownOpening(object sender, EventArgs e)
        {
            Program.Page_Editor?.ListView_UndoPanel.Items.Clear();

            if (_currentUndoBuffer == null)
                return;


            int n = _currentUndoBuffer.UndoList.Count();
            foreach (IUndoable x in _currentUndoBuffer.UndoList)
            {
                //WPFC.MenuItem i = new WPFC.MenuItem();
                //i.Header = x.Action;
                //i.Click += MenuItem_UndoListItem_Click;
                if (x == null)
                    continue;

                Program.Page_Editor?.ListView_UndoPanel.Items.Insert(0, new WPFC.ListViewItem { DataContext = x.Action, Content = x.Action, Tag = n });

                n = n - 1;
            }
            //_undoListBox.ListBox.Items.Insert(0, x.Action);
        }


        public void BeginRedo()
        {
            RenderMakeCurrent();
        }

        public void DoRedo()
        {
            if (_currentUndoBuffer?.CanRedo == false)
                return;

            _currentUndoBuffer.Redo();

            Program.Log(LogType.Info, "Redone action", "at MCSkinn.Editor.PerformNewFolder()");

        }

        public void EndRedo()
        {
            SetCanSave(_lastSkin.IsDirty = true);

            InvalidateRenderer();
        }

        public void PerformRedo()
        {
            if (_currentUndoBuffer == null || _currentUndoBuffer?.CanRedo == false)
                return;

            Program.Log(LogType.Load, "Performing Redo", "at MCSkinn.Editor.PerformRedo()");

            BeginRedo();
            DoRedo();
            EndRedo();
        }

        private void redoToolStripButton_ButtonClick(object sender, EventArgs e)
        {
            PerformRedo();
        }

        public void redoToolStripButton_DropDownOpening(object sender, EventArgs e)
        {
            Program.Page_Editor?.ListView_RedoPanel.Items.Clear();

            if (_currentUndoBuffer == null)
                return;

            int n = _currentUndoBuffer.RedoList.Count();
            foreach (IUndoable x in _currentUndoBuffer.RedoList)
            {
                Program.Page_Editor?.ListView_RedoPanel.Items.Insert(0, new WPFC.ListViewItem { DataContext = x.Action, Content = x.Action, Tag = n });
                n = n - 1;

            }
            // _redoListBox.ListBox.Items.Insert(0, x.Action);
        }

        public void SetViewMode(ViewMode newMode)
        {
            _currentViewMode = newMode;

            if (Program.Window_Main != null)
            {
                Program.Page_Editor.MenuItem_View_ViewMode_3D.IsChecked = false;
                Program.Page_Editor.MenuItem_View_ViewMode_2D.IsChecked = false;
                Program.Page_Editor.MenuItem_View_ViewMode_Split.IsChecked = false;
            }

            switch (_currentViewMode)
            {
                case ViewMode.Orthographic:
                    if (Program.Window_Main != null)
                        Program.Page_Editor.MenuItem_View_ViewMode_2D.IsChecked = true;
                    break;
                case ViewMode.Perspective:
                    if (Program.Window_Main != null)
                        Program.Page_Editor.MenuItem_View_ViewMode_3D.IsChecked = true;
                    break;
                case ViewMode.Hybrid:
                    if (Program.Window_Main != null)
                        Program.Page_Editor.MenuItem_View_ViewMode_Split.IsChecked = true;
                    break;
            }

            CalculateMatrices();
            InvalidateRenderer();
        }

        public void SetTransparencyMode(TransparencyMode trans)
        {
            GlobalSettings.Transparency = trans;

            if (Program.Window_Main != null)
            {
                Program.Page_Editor.MenuItem_View_Transparency_All.IsChecked = false;
                Program.Page_Editor.MenuItem_View_Transparency_Helmet.IsChecked = false;
                Program.Page_Editor.MenuItem_View_Transparency_Off.IsChecked = false;
            }

            switch (GlobalSettings.Transparency)
            {
                case TransparencyMode.Off:
                    if (Program.Window_Main != null)
                        Program.Page_Editor.MenuItem_View_Transparency_Off.IsChecked = true;
                    break;
                case TransparencyMode.Helmet:
                    if (Program.Window_Main != null)
                        Program.Page_Editor.MenuItem_View_Transparency_Helmet.IsChecked = true;
                    break;
                case TransparencyMode.All:
                    if (Program.Window_Main != null)
                        Program.Page_Editor.MenuItem_View_Transparency_All.IsChecked = true;
                    break;
            }



            InvalidateRenderer();
        }

        public void ToggleVisiblePart(ModelPart part)
        {
            //var meshes = new List<Mesh>();
            //var item = _partItems[(int)part];
            //var button = _partButtons[(int)part];

            //foreach (var m in CurrentModel.Meshes)
            //    if (m.Part == part)
            //        meshes.Add(m);

            //if (meshes.Count != 0)
            //{
            //    item.Checked = button.Checked = !item.Checked;

            //    foreach (var m in meshes)
            //        CurrentModel.PartsEnabled[m] = item.Checked;
            //}

            CalculateMatrices();
            InvalidateRenderer();
        }

        public void ToggleAlphaCheckerboard()
        {
            GlobalSettings.AlphaCheckerboard = !GlobalSettings.AlphaCheckerboard;
            InvalidateRenderer();

            if (Program.Window_Main != null)
                Program.Page_Editor.MenuItem_View_2D_Checkerboard.IsChecked = GlobalSettings.AlphaCheckerboard;
        }

        public void ToggleOverlay()
        {
            GlobalSettings.TextureOverlay = !GlobalSettings.TextureOverlay;
            InvalidateRenderer();
        }

        private void ToggleTransparencyMode()
        {
            switch (GlobalSettings.Transparency)
            {
                case TransparencyMode.Off:
                    SetTransparencyMode(TransparencyMode.Helmet);
                    break;
                case TransparencyMode.Helmet:
                    SetTransparencyMode(TransparencyMode.All);
                    break;
                case TransparencyMode.All:
                    SetTransparencyMode(TransparencyMode.Off);
                    break;
            }
        }

        private void ToggleViewMode()
        {
            switch (_currentViewMode)
            {
                case ViewMode.Orthographic:
                    SetViewMode(ViewMode.Perspective);
                    break;
                case ViewMode.Perspective:
                    SetViewMode(ViewMode.Hybrid);
                    break;
                case ViewMode.Hybrid:
                    SetViewMode(ViewMode.Orthographic);
                    break;
            }
        }

        #region Screenshots

        private Bitmap CopyScreenToBitmap()
        {
            RenderMakeCurrent();
            rendererControl_Paint(null, null);

            var b = new Bitmap(RenderWidth, RenderHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            var pixels = new int[RenderWidth * RenderHeight];
            GL.ReadPixels(0, 0, RenderWidth, RenderHeight, PixelFormat.Rgba,
                          PixelType.UnsignedByte, pixels);

            BitmapData locked = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite,
                                           System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            unsafe
            {
                fixed (void* inPixels = pixels)
                {
                    void* outPixels = locked.Scan0.ToPointer();

                    var inInt = (int*)inPixels;
                    var outInt = (int*)outPixels;

                    for (int y = 0; y < b.Height; ++y)
                    {
                        for (int x = 0; x < b.Width; ++x)
                        {
                            Color color = Color.FromArgb(*inInt >> 24 & 0xFF, *inInt >> 0 & 0xFF, *inInt >> 8 & 0xFF,
                                                         *inInt >> 16 & 0xFF);
                            *outInt = color.ToArgb();

                            inInt++;
                            outInt++;
                        }
                    }
                }
            }

            b.UnlockBits(locked);
            b.RotateFlip(RotateFlipType.RotateNoneFlipY);

            return b;
        }

        public void TakeScreenshot()
        {
            Clipboard.SetImage(CopyScreenToBitmap());
        }

        public void SaveScreenshot()
        {
            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "PNG Image|*.png";
                sfd.RestoreDirectory = true;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (Bitmap bmp = CopyScreenToBitmap())
                        bmp.Save(sfd.FileName);
                }
            }
        }

        #endregion

        #region Saving

        private void SetCanSave(bool value)
        {

            if (Program.Window_Main != null)
            {
                Program.Page_Editor.SetTrayButtonEnabled(0, value);

            }

            CheckUndo();

            //treeView1.Invalidate();
        }

        public void PerformSaveAs()
        {
            Skin skin = _lastSkin;

            using (var grabber = new ColorGrabber(GlobalDirtiness.CurrentSkin, skin.Width, skin.Height))
            {
                grabber.Load();

                var b = new Bitmap(skin.Width, skin.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                BitmapData locked = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite,
                                               System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                unsafe
                {
                    void* inPixels = grabber.Array;
                    void* outPixels = locked.Scan0.ToPointer();

                    var inInt = (int*)inPixels;
                    var outInt = (int*)outPixels;

                    for (int y = 0; y < b.Height; ++y)
                    {
                        for (int x = 0; x < b.Width; ++x)
                        {
                            Color color = Color.FromArgb(*inInt >> 24 & 0xFF, *inInt >> 0 & 0xFF, *inInt >> 8 & 0xFF,
                                                            *inInt >> 16 & 0xFF);
                            *outInt = color.ToArgb();

                            inInt++;
                            outInt++;
                        }
                    }
                }

                b.UnlockBits(locked);

                using (var sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Skin Image|*.png";
                    sfd.RestoreDirectory = true;

                    if (sfd.ShowDialog() == DialogResult.OK)
                        b.Save(sfd.FileName);
                }

                b.Dispose();
            }
        }

        private void PerformSaveSkin(Skin s)
        {
            RenderMakeCurrent();

            s.CommitChanges(s == _lastSkin ? GlobalDirtiness.CurrentSkin : s.GLImage, true);

            Program.Log(LogType.Info, string.Format("Saved skin '{0}'", s.FileInfo.Name), "at MCSkinn.Editor.PerformSaveSkin(Skin)");

        }

        public bool RecursiveNodeIsDirty(IEnumerable<LibraryNode> nodes)
        {
            foreach (LibraryNode node in nodes)
            {
                if (node is Skin)
                {
                    var skin = (Skin)node;

                    if (skin.IsDirty)
                        return true;
                }
                else if (RecursiveNodeIsDirty((node as FolderNode)?.Nodes))
                    return true;
            }

            return false;
        }

        private void RecursiveNodeSave(IEnumerable<LibraryNode> nodes)
        {
            foreach (LibraryNode node in nodes)
            {
                if (node is Skin)
                {
                    try
                    {
                        var skin = (Skin)node;

                        if (skin.IsDirty)
                            PerformSaveSkin(skin);
                    }
                    catch(Exception ex)
                    {
                        Program.RaiseException(ex);
                    }
                }
                else
                    RecursiveNodeSave((node as FolderNode).Nodes);
            }
        }

        public void PerformSaveAll()
        {
            Program.Log(LogType.Load, "Performing Save all", "at MCSkinn.Editor.PerformSaveAll()");

            foreach (Workfolder f in GlobalSettings.SkinDirectories)
            {
                if (f.IsInitialized)
                {
                    RecursiveNodeSave(f.Root.Nodes);
                }
            }

        }

        public void PerformSave()
        {
            try
            {
                Program.Log(LogType.Load, "Performing Save", "at MCSkinn.Editor.PerformSave()");

                Skin skin = _lastSkin;

                if (skin == null || !skin.IsDirty)
                    return;

                PerformSaveSkin(skin);

                SetCanSave(false);
            }
            catch (Exception ex)
            {
                Program.Log(ex, "Editor.PerformSave()");
                MessageBox.Show(ex.Message + "\r\n\r\n" + Program.GetLanguageString("M_UNABLESAVESKIN"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Skin Management

        private void ImportSkin(string fileName, string folderLocation, LibraryNode parentNode)
        {
            string name = Path.GetFileNameWithoutExtension(fileName);

            while (File.Exists(folderLocation + name + ".png"))
                name += " - " + GetLanguageString("C_NEW");

            File.Copy(fileName, folderLocation + name + ".png");

            var skin = new Skin(folderLocation + name + ".png");

            if (parentNode != null)
            {
                if (parentNode is FolderNode)
                    (parentNode as FolderNode).Add(skin);
                else
                    (parentNode as Skin).Parent.Add(skin);
            }
            else
            {
                SkinLibrary.RootFolders[0]?.Add(skin);
            }

            skin.SetImages();
            SkinLibrary.SelectedNode = skin;
        }

        public static string GetFolderForNode(LibraryNode node)
        {
            return node.Path;
        }

        public static string GetFolderLocationForNode(LibraryNode node)
        {

            if (node != null)
            {
                if (node is FolderNode)
                {
                    return (node as FolderNode).Path;
                }
                else if (node is Skin)
                {
                    return (node as Skin).Parent.Path;
                }
            }
            return "";
        }

        public static void GetFolderLocationAndCollectionForNode(LibraryNode _rightClickedNode,
                                                                 out string folderLocation, out IEnumerable<LibraryNode> collection)
        {
            folderLocation = "";
            collection = SkinLibrary.RootFolders;

            if (_rightClickedNode != null)
            {
                if (_rightClickedNode is FolderNode)
                {
                    folderLocation = GetFolderForNode(_rightClickedNode);
                    collection = ((FolderNode)_rightClickedNode).Nodes;
                }
                else if (_rightClickedNode.Parent != null)
                {
                    folderLocation = GetFolderForNode(_rightClickedNode.Parent);
                    collection = _rightClickedNode.Parent.Nodes;
                }
            }
        }




        void FillRectangleAlternating(Bitmap b, int x, int y, int w, int h)
        {
            for (var rx = 0; rx < w; ++rx)
                for (var ry = 0; ry < h; ++ry)
                {
                    Color c;

                    if ((rx + x + ry + y & 1) == 1)
                        c = Color.LightGray;
                    else
                        c = Color.DarkGray;

                    b.SetPixel(rx + x, ry + y, c);
                }
        }


        private void RecursiveDeleteSkins(FolderNode node)
        {
            foreach (LibraryNode sub in node.Nodes)
            {
                if (!(sub is Skin))
                    RecursiveDeleteSkins(sub as FolderNode);
                else
                {
                    var skin = (Skin)sub;

                    if (_lastSkin == skin)
                        _lastSkin = null;

                    skin.Dispose();
                }
            }

            //Directory.Delete(GetFolderForNode(node), true);
            FileSystem.DeleteDirectory(GetFolderForNode(node), UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin, UICancelOption.DoNothing);
        }




        #endregion


        #endregion

        #region Constructor

        public Editor()
        {
            SkinLibrary.SelectedNodeChanged += SkinLibrary_SelectedNodeChanged;
        }

        public static string GLExtensions { get; private set; }
        public static string GLRenderer { get; private set; }
        public static string GLVendor { get; private set; }
        public static string GLVersion { get; private set; }


        public ToolIndex Tool_Camera;
        public ToolIndex Tool_Pencil;
        public ToolIndex Tool_Eraser;
        public ToolIndex Tool_Dropper;
        public ToolIndex Tool_Dodge;
        public ToolIndex Tool_Darken;
        public ToolIndex Tool_Fill;
        public ToolIndex Tool_Noise;
        public ToolIndex Tool_Stamp;

        public void Initialize(Language language)
        {
            IsCompatibilityModeOn = GlobalSettings.CompatibilityMode;

            Tool_Camera = new ToolIndex(new CameraTool(), "camera", "T_TOOL_CAMERA", Resources.eye__1_, Keys.C);
            _tools.Add(Tool_Camera);

            Tool_Pencil = new ToolIndex(new PencilTool(), "pencil", "T_TOOL_PENCIL", Resources.pen, Keys.P);
            _tools.Add(Tool_Pencil);


            Tool_Eraser = new ToolIndex(new EraserTool(), "eraser", "T_TOOL_ERASER", Resources.erase, Keys.E);
            _tools.Add(Tool_Eraser);

            Tool_Dropper = new ToolIndex(new DropperTool(), "dropper", "T_TOOL_DROPPER", Resources.pipette, Keys.D);
            _tools.Add(Tool_Dropper);

            Tool_Dodge = new ToolIndex(new DodgeBurnTool(), "dodge/burn", "T_TOOL_DODGEBURN", Resources.dodge, Keys.B);
            _tools.Add(Tool_Dodge);

            Tool_Darken = new ToolIndex(new DarkenLightenTool(), "darken/lighten", "T_TOOL_DARKENLIGHTEN", Resources.darkenlighten, Keys.L);
            _tools.Add(Tool_Darken);

            Tool_Fill = new ToolIndex(new FloodFillTool(), "floodfill", "T_TOOL_BUCKET", Resources.fill_bucket, Keys.F);
            _tools.Add(Tool_Fill);

            Tool_Noise = new ToolIndex(new NoiseTool(), "noise", "T_TOOL_NOISE", Resources.noise, Keys.N);
            _tools.Add(Tool_Noise);

            Tool_Stamp = new ToolIndex(new StampTool(), "stamp", "T_TOOL_STAMP", Resources.stamp_pattern, Keys.M);
            _tools.Add(Tool_Stamp);


            //_tools.Add(new ToolIndex(new PencilTool(), PencilOptions, "T_TOOL_PENCIL", Resources.pen, Keys.P));
            //_tools.Add(new ToolIndex(new EraserTool(), EraserOptions, "T_TOOL_ERASER", Resources.erase, Keys.E));
            //_tools.Add(new ToolIndex(new DropperTool(), null, "T_TOOL_DROPPER", Resources.pipette, Keys.D));
            //_tools.Add(new ToolIndex(new DodgeBurnTool(), DodgeBurnOptions, "T_TOOL_DODGEBURN", Resources.dodge, Keys.B));
            //_tools.Add(new ToolIndex(new DarkenLightenTool(), DarkenLightenOptions, "T_TOOL_DARKENLIGHTEN", Resources.darkenlighten, Keys.L));
            //_tools.Add(new ToolIndex(new FloodFillTool(), FloodFillOptions, "T_TOOL_BUCKET", Resources.fill_bucket, Keys.F));
            //_tools.Add(new ToolIndex(new NoiseTool(), NoiseOptions, "T_TOOL_NOISE", Resources.noise, Keys.N));
            //_tools.Add(new ToolIndex(new StampTool(), StampOptions, "T_TOOL_STAMP", Resources.stamp_pattern, Keys.M));

            for (int i = _tools.Count - 1; i >= 0; --i)
            {
                _tools[i].MenuItem.Click += ToolMenuItemClicked;
                _tools[i].Button.Click += ToolMenuItemClicked;
            }

            // Shortcuts

            //Program.CurrentLanguage = language;
            SetSelectedTool(_tools[0]);

            Brushes.LoadBrushes();

            //foreach (string x in GlobalSettings.SkinDirectories)
            //    Directory.CreateDirectory(MacroHandler.ReplaceMacros(x));

            InitializeRenderer();
            InitGL();


            SetTransparencyMode(TransparencyMode.All);

            CreatePartList();
            InvalidateRenderer();
        }

        public bool IsCompatibilityModeOn;

        private void InitializeRenderer()
        {
            if (IsCompatibilityModeOn)
            {

                Program.Log(LogType.Load, "Initializing renderer, compatibility mode is ON", "Editor.InitializeRenderer()");
             
                // set up the GL control
                var mode = new GraphicsMode();

                reset:

                RendererControl =
                    new GLControl(new GraphicsMode(mode.ColorFormat, mode.Depth, mode.Stencil, 0));

                if (RendererControl.Context == null)
                {
                    mode = new GraphicsMode();
                    goto reset;
                }


                RendererControl.BackColor = Color.Black;
                RendererControl.Dock = DockStyle.Fill;
                RendererControl.Location = new Point(0, 25);
                RendererControl.Name = "rendererControl";
                RendererControl.Size = new Size(641, 580);
                RendererControl.TabIndex = 4;

                RendererControl.Paint += RendererControl_Paint;
                RendererControl.Resize += RendererControl_Resize;
                RendererControl.MouseMove += RendererControl_MouseMove;
                RendererControl.MouseDown += RendererControl_MouseDown;
                RendererControl.MouseUp += RendererControl_MouseUp;
                RendererControl.MouseWheel += RendererControl_MouseWheel;

                WindowsFormsHost_RenderHost = new System.Windows.Forms.Integration.WindowsFormsHost();
                WindowsFormsHost_RenderHost.Child = RendererControl;

                Program.Page_Editor.Border_ViewportBox.Child = WindowsFormsHost_RenderHost;

                RenderMakeCurrent();
                InvalidateRenderer();


                Program.Page_Editor.Border_Library.CornerRadius = new WPF.CornerRadius(0);
                Program.Page_Editor.Border_ViewportBackground.CornerRadius = new WPF.CornerRadius(0);
                Program.Page_Editor.Border_ViewportBackground.UseLayoutRounding = true;
                Program.Page_Editor.Border_ViewportBackground.SnapsToDevicePixels = true;
            }
            else
            {
                Program.Log(LogType.Info, "Initializing renderer, compatibility mode is OFF", "Editor.InitializeRenderer()");

                Renderer = new OpenTK.WPF.OtkWpfControl(true);
                Renderer.RenderScale = GlobalSettings.RenderScale;
                Renderer.OpenGLDraw += Renderer_OpenGLDraw;
                Renderer.MouseDown += Renderer_MouseDown;
                Renderer.MouseMove += Renderer_MouseMove;
                Renderer.MouseUp += Renderer_MouseUp;
                Renderer.MouseLeave += Renderer_MouseLeave;
                Renderer.SizeChanged += Renderer_SizeChanged;
                Renderer.MouseWheel += Renderer_MouseWheel;
                Renderer.MouseEnter += Renderer_MouseEnter;

                Renderer.IsManipulationEnabled = GlobalSettings.IsManipulationEnabled;
                Renderer.ManipulationStarting += Renderer_ManipulationStarting;
                Renderer.ManipulationDelta += Renderer_ManipulationDelta;
                Renderer.ManipulationCompleted += Renderer_ManipulationCompleted;

                Renderer.TouchDown += Renderer_TouchDown;
                Renderer.TouchMove += Renderer_TouchMove;
                Renderer.TouchLeave += Renderer_TouchLeave;
                Renderer.TouchUp += Renderer_TouchUp;

                Renderer.StylusEnter += Renderer_StylusEnter;
                Renderer.StylusInAirMove += Renderer_StylusInAirMove;
                Renderer.StylusLeave += Renderer_StylusLeave;
                Renderer.StylusDown += Renderer_StylusDown;
                Renderer.StylusMove += Renderer_StylusMove;
                Renderer.StylusUp += Renderer_StylusUp;

                Renderer.Focusable = true;
                Renderer.FocusVisualStyle = null;

                Program.Page_Editor.Border_ViewportBox.Child = Program.Editor.Renderer;
                Program.Page_Editor.Run_Framerate.SetBinding(WPF.Documents.Run.TextProperty, new WPFD.Binding() { Source = Program.Editor.Renderer, Path = new WPF.PropertyPath("Framerate"), Mode = WPFD.BindingMode.OneWay });

            }

            IsRendererInitialized = true;
        }

        #region Compatibility Mode
        private void RendererControl_MouseWheel(object? sender, MouseEventArgs e)
        {
            if (Program.Page_Editor?.IsViewportError == true)
                return;
    
            Renderer_MouseWheel(sender, new WPFI.MouseWheelEventArgs(WPFI.Mouse.PrimaryDevice, 0, e.Delta));
        }

        private void RendererControl_MouseUp(object? sender, MouseEventArgs e)
        {
            if (Program.Page_Editor?.IsViewportError == true)
                return;

            mousePos = new Point(e.X, e.Y);
            Renderer_MouseUp(sender, new WPFI.MouseButtonEventArgs(WPFI.Mouse.PrimaryDevice, 0, e.Button.ToWpf()));
        }


        private void RendererControl_MouseDown(object? sender, MouseEventArgs e)
        {
            if (Program.Page_Editor?.IsViewportError == true)
                return;

            mousePos = new Point(e.X, e.Y);
            Renderer_MouseDown(sender, new WPFI.MouseButtonEventArgs(WPFI.Mouse.PrimaryDevice, 0, e.Button.ToWpf()));
        }

        private void RendererControl_Resize(object? sender, EventArgs e)
        {
            RenderMakeCurrent();

            CalculateMatrices();

            InvalidateRenderer();
        }

        private void RendererControl_MouseMove(object? sender, MouseEventArgs e)
        {
            if (Program.Page_Editor?.IsViewportError == true)
                return;

            mousePos = new Point(e.X, e.Y);
            Renderer_MouseMove(sender, new WPFI.MouseEventArgs(WPFI.Mouse.PrimaryDevice, 0));
        }

        private void RendererControl_Paint(object? sender, PaintEventArgs e)
        {
            rendererControl_Paint(sender, e);
        }
        #endregion

        public System.Windows.Forms.Integration.WindowsFormsHost WindowsFormsHost_RenderHost;

        private void Renderer_StylusInAirMove(object sender, WPFI.StylusEventArgs e)
        {
            if (!GlobalSettings.StylusToDraw)
                return;

        }

        private void Renderer_StylusLeave(object sender, WPFI.StylusEventArgs e)
        {
            if (!GlobalSettings.StylusToDraw)
                return;

        }

        private void Renderer_StylusUp(object sender, WPFI.StylusEventArgs e)
        {
            if (!GlobalSettings.StylusToDraw)
                return;
        }

        private void Renderer_StylusMove(object sender, WPFI.StylusEventArgs e)
        {
            if (!GlobalSettings.StylusToDraw)
                return;
        }

        private void Renderer_StylusDown(object sender, WPFI.StylusDownEventArgs e)
        {
            if (!GlobalSettings.StylusToDraw)
                return;
        }

        private void Renderer_StylusEnter(object sender, WPFI.StylusEventArgs e)
        {
            if (!GlobalSettings.StylusToDraw)
                return;
        }
        #region Touch

        bool isTouchDown;
        private void Renderer_TouchLeave(object? sender, WPFI.TouchEventArgs e)
        {

        }

        private void Renderer_TouchUp(object? sender, WPFI.TouchEventArgs e)
        {

            //isTouchDown = false;

            if (GlobalSettings.StylusToDraw)
            {
                e.Handled = false;
            }
        }


        private void Renderer_TouchMove(object? sender, WPFI.TouchEventArgs e)
        {

            if (GlobalSettings.StylusToDraw)
            {
                e.Handled = false;
            }

        }

        private void Renderer_TouchDown(object? sender, WPFI.TouchEventArgs e)
        {
            WPFI.Keyboard.Focus(Renderer);

            if (GlobalSettings.StylusToDraw)
            {
                isTouchDown = true;
                e.Handled = false;
            }
        }
        #endregion

        private void Renderer_MouseEnter(object sender, WPFI.MouseEventArgs e)
        {

        }

        private void Renderer_MouseLeave(object sender, WPFI.MouseEventArgs e)
        {
            _mousePoint = new Point(-1, -1);

            if (_mouseIsDown)
                Renderer_MouseUp(sender, new WPFI.MouseButtonEventArgs(e.MouseDevice, e.Timestamp, WPFI.MouseButton.Left));
        }

        #region Renderer Events

        #region Manipulation

        private void Renderer_ManipulationCompleted(object? sender, WPFI.ManipulationCompletedEventArgs e)
        {
            if (!GlobalSettings.IsManipulationEnabled)
            {
                e.Cancel();
                Renderer.IsManipulationEnabled = false;
                return;
            }

            if (e.Manipulators.Count() <= 1)
            {
                e.Cancel();
                return;
            }
            //lockRefreshState = false;
        }

        private void Renderer_ManipulationDelta(object? sender, WPFI.ManipulationDeltaEventArgs e)
        {
            if (!GlobalSettings.IsManipulationEnabled)
            {
                e.Cancel();
                Renderer.IsManipulationEnabled = false;
                return;
            }
            e.Handled = true;
            var delta = e.DeltaManipulation;

            if (e.Manipulators.Count() <= 1 && delta.Scale.X == 1)
            {
                e.Cancel();
                return;
            }

            if (delta.Translation.X != 0 || delta.Translation.Y != 0)
            {
                if (e.Manipulators.Count() > 1)
                    TranslateView(new Point((int)delta.Translation.X, (int)delta.Translation.Y), 1);
                //else
                //    RotateView(delta.Translation.X, delta.Translation.Y);
            }

            if (delta.Scale.X != 1)
            {
                ScaleView((float)(1 / delta.Scale.X));
            }

            //CalculateMatrices();
            //InvalidateRenderer();

        }

        private void Renderer_ManipulationStarting(object? sender, WPFI.ManipulationStartingEventArgs e)
        {
            WPFI.Keyboard.Focus(Renderer);

            if (!GlobalSettings.IsManipulationEnabled)
            {
                e.Cancel();
                Renderer.IsManipulationEnabled = false;
                return;
            }
            //if(e.Manipulators.Count() <= 1)
            //{
            //    e.Cancel();
            //    return;
            //}
            //if(manipulationTimer == null)
            //{
            //    manipulationTimer = new WPFT.DispatcherTimer();
            //    manipulationTimer.Interval = TimeSpan.FromMilliseconds(1);
            //    manipulationTimer.Tick += ManipulationTimer_Tick;
            //}

            //e.ManipulationContainer = Program.Window_Main;
            //e.Mode = WPFI.ManipulationModes.Translate | WPFI.ManipulationModes.Scale;
            //e.IsSingleTouchEnabled = true;
            ////e.Pivot = new WPFI.ManipulationPivot() { Radius = 0.1 };
            ////manipulationTimer.Start();

            //InvalidateRenderer();
        }


        #endregion

        private void Renderer_SizeChanged(object sender, WPF.SizeChangedEventArgs e)
        {

            CalculateMatrices();
            InvalidateRenderer();
        }

        #region Mouse Events

        private void Renderer_MouseWheel(object sender, WPFI.MouseWheelEventArgs e)
        {
            if (Renderer != null)
                WPFI.Keyboard.Focus(Renderer);

            CheckMouse(GetRenderCursorPos());

            if (_currentViewMode == ViewMode.Perspective || _currentViewMode == ViewMode.Hybrid && _mouseIn3D)
                _3DZoom += e.Delta / 50;
            else
                _2DZoom += e.Delta / 50;

            if (_2DZoom < 0.25f)
                _2DZoom = 0.25f;

            CalculateMatrices();
            InvalidateRenderer();

        }

        private void Renderer_MouseUp(object sender, WPFI.MouseButtonEventArgs e)
        {
            if (Renderer != null)
                WPFI.Keyboard.Focus(Renderer);

            Skin skin = _lastSkin;

            if (skin == null)
                return;

            using (var backup = new ColorGrabber(GlobalDirtiness.CurrentSkin, skin.Width, skin.Height))
            {
                backup.Load();

                try
                {
                    if (_mouseIsDown || isTouchDown)
                    {
                        var currentSkin = new ColorGrabber();

                        if (e.ChangedButton == WPFI.MouseButton.Left && !isTouchDown)
                        {
                            currentSkin = new ColorGrabber(GlobalDirtiness.CurrentSkin, skin.Width, skin.Height);
                            currentSkin.Load();

                            if (_selectedTool.Tool.EndClick(currentSkin, skin, GetRenderCursorPos(), e.ChangedButton))
                            {
                                SetCanSave(true);
                                skin.IsDirty = true;
                                currentSkin.Save();
                            }

                            SetPartTransparencies();
                        }
                        else
                            _tools[(int)ViewportTool.Camera].Tool.EndClick(currentSkin, _lastSkin, GetRenderCursorPos(), e.ChangedButton);
                    }
                }
                catch (Exception ex)
                {
                    backup.Save();
                    PerformSaveAll();

                    Program.Log(ex, true);
                }
            }

            _mouseIsDown = false;
            isTouchDown = false;
            //treeView1.Invalidate();
            refreshNeeded = true;
        }

        private void Renderer_MouseMove(object sender, WPFI.MouseEventArgs e)
        {
            if (Renderer != null)
                WPFI.Keyboard.Focus(Renderer);


            Skin skin = _lastSkin;

            if (skin == null)
                return;

            _isValidPick = GetPick(GetRenderCursorPos().X, GetRenderCursorPos().Y, out _pickPosition);

            using (var backup = new ColorGrabber(GlobalDirtiness.CurrentSkin, skin.Width, skin.Height))
            {
                backup.Load();

                try
                {
                    if (_mouseIsDown || isTouchDown)
                    {
                        if ((e.LeftButton == WPFI.MouseButtonState.Pressed || Control.MouseButtons.HasFlag(MouseButtons.Left)) && (!isTouchDown || !GlobalSettings.StylusToDraw))
                        {
                            _selectedTool.Tool.MouseMove(_lastSkin, GetRenderCursorPos());
                            UseToolOnViewport((int)GetRenderCursorPos().X, (int)GetRenderCursorPos().Y);
                        }
                        else
                            _tools[(int)ViewportTool.Camera].Tool.MouseMove(_lastSkin, GetRenderCursorPos());
                    }

                    _mousePoint = GetRenderCursorPos();

                    InvalidateRenderer();
                }
                catch (Exception ex)
                {
                    backup.Save();
                    PerformSaveAll();

                    Program.Log(ex, true);
                }
            }

            refreshNeeded = true;
        }

        private void Renderer_MouseDown(object sender, WPFI.MouseButtonEventArgs e)
        {
            if(Renderer != null)
                WPFI.Keyboard.Focus(Renderer);

            Skin skin = _lastSkin;

            if (skin == null)
                return;

            CheckMouse(GetRenderCursorPos());

            _mousePoint = GetRenderCursorPos();

            if (e.Device != null)
                _mouseIsDown = true;

            //_isValidPick = GetPick(e.X, e.Y, ref _pickPosition);

            using (var backup = new ColorGrabber(GlobalDirtiness.CurrentSkin, skin.Width, skin.Height))
            {
                backup.Load();

                try
                {
                    if (e.ChangedButton == WPFI.MouseButton.Left && !isTouchDown)
                    {
                        if (_isValidPick)
                            _selectedTool.Tool.BeginClick(_lastSkin, GetRenderCursorPos(), e.ChangedButton);
                        else
                            _selectedTool.Tool.BeginClick(_lastSkin, GetRenderCursorPos(), e.ChangedButton);
                        UseToolOnViewport(GetRenderCursorPos().X, GetRenderCursorPos().Y);
                    }
                    else
                        _tools[(int)ViewportTool.Camera].Tool.BeginClick(_lastSkin, GetRenderCursorPos(), e.ChangedButton);
                }
                catch (Exception ex)
                {
                    backup.Save();
                    PerformSaveAll();

                    Program.Log(ex, true);
                }
            }

            refreshNeeded = true;
        }
        #endregion

        bool _refreshNeeded = true;
        public bool refreshNeeded
        {
            get 
            {
                if (IsCompatibilityModeOn)
                    return false;

                return _refreshNeeded; 
            }
            set
            {
                _refreshNeeded = value;

                if(Renderer != null)
                Renderer.IsRenderingPaused = !value;
            }
        }

        private void Renderer_OpenGLDraw(object? sender, OpenTK.WPF.OtkWpfControl.OpenGLDrawEventArgs e)
        {
            if (refreshNeeded)
            {
                rendererControl_Paint(null, null);

                e.Redrawn = true;
                refreshNeeded = false;
            }
            else
            {
                e.Redrawn = false;
            }
        }

        #endregion

        private void _shortcutEditor_ShortcutExists(object sender, ShortcutExistsEventArgs e)
        {
            //MessageBox.Show(string.Format(GetLanguageString("B_MSG_SHORTCUTEXISTS"), e.ShortcutName, e.OtherName));
        }

        #endregion

    }
}