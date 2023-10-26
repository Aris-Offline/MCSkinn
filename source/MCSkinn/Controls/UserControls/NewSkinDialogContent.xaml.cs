using GongSolutions.Wpf.DragDrop.Utilities;
using iNKORE.Coreworks.Helpers;
using iNKORE.Coreworks.Localization;
using iNKORE.Coreworks.Windows.Presentation;
using MCSkinn.Forms.Controls;
using MCSkinn.Scripts;
using MCSkinn.Scripts.Paril.Imaging;
using MCSkinn.Scripts.Paril.OpenGL;
using System;
using System.Collections.Generic;
using System.IO;
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
using Drawing = System.Drawing;
using Forms = System.Windows.Forms;

namespace MCSkinn.Dialogs
{
    /// <summary>
    /// NewSkinDialogContent.xaml 的交互逻辑
    /// </summary>
    public partial class NewSkinDialogContent : UserControl
    {
        public NewSkinDialogContent()
        {
            InitializeComponent();
        }

        private void UserControl_Initialized(object sender, EventArgs e)
        {
            TreeView_Models.ItemsSource = Program.Page_Editor.MenuFlyout_Model.Items;
        }

        public System.Windows.Forms.OpenFileDialog OpenFileDialog_Local_Broswe;

        private void Button_Local_Dropfile_Click(object sender, RoutedEventArgs e)
        {
            if(OpenFileDialog_Local_Broswe == null)
            {
                OpenFileDialog_Local_Broswe = new System.Windows.Forms.OpenFileDialog();
                OpenFileDialog_Local_Broswe.Multiselect = false;
                OpenFileDialog_Local_Broswe.Filter = "Minecraft Skin (*.png)|*.png|All files|*.*";
            }

            if (OpenFileDialog_Local_Broswe.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TextBlock_Dropfile_Path.Text = OpenFileDialog_Local_Broswe.FileName;
                TryGetModel();
            }

            SetDialogButtonEnabled();
        }

        private void Button_Local_Dropfile_Drop(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    var fileName = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();


                    if (System.IO.Path.GetExtension(fileName) == ".png")
                    {
                        TextBlock_Dropfile_Path.Text = System.IO.Path.GetFullPath(fileName);
                        TryGetModel();
                        e.Effects = DragDropEffects.Copy;
                    }
                    else
                        e.Effects = DragDropEffects.None;
                }
            }
            catch (Exception ex)
            {
                Program.Log(ex);
                MessageBox.Show(ex.Message);
            }
            finally
            {
                SetDialogButtonEnabled();
               
            }
        }

        System.Windows.Forms.SaveFileDialog SaveFileDialog_SaveTo;

        private void Button_SaveTo_Browse_Click(object sender, RoutedEventArgs e)
        {
            if (SaveFileDialog_SaveTo == null)
            {
                SaveFileDialog_SaveTo = new System.Windows.Forms.SaveFileDialog();
                SaveFileDialog_SaveTo.Filter = "Minecraft Skin (*.png)|*.png|All files|*.*";
            }

            SaveFileDialog_SaveTo.FileName = TextBox_SaveTo.Text;

            if(SaveFileDialog_SaveTo.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TextBox_SaveTo.Text = SaveFileDialog_SaveTo.FileName;
            }
        }

        private void Button_Local_Dropfile_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                try
                {
                    var fileName = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();

                    if (System.IO.Path.GetExtension(fileName) == ".png")
                        e.Effects = DragDropEffects.Copy;
                    else
                        e.Effects = DragDropEffects.None;
                }
                catch { e.Effects = DragDropEffects.None; }
            }
            else if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                e.Effects = DragDropEffects.None;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }

        }

        public void CreateSkin()
        {
            if (!(TreeView_Models.SelectedItem is ModelToolStripMenuItem) || string.IsNullOrEmpty(TextBox_SaveTo.Text))
                return;

            //if (specificModel == null)
            //    specificModel = ModelLoader.Models["Players/Steve"];

            string newSkinName = TextBox_SaveTo.Text;
            Model specificModel = ((ModelToolStripMenuItem)TreeView_Models.SelectedItem).Model;


            Drawing.Bitmap bmp = null;

            if (TabControl_SourceSelector.SelectedItem == TabItem_SourceSelector_Empty)
            {
                if(CheckBox_EmptySkin_UseTemplate.IsChecked == true)
                {
                    string templatePath = System.IO.Path.Combine(GlobalSettings.FullPath_Templates, specificModel.DefaultTexture);

                    if (File.Exists(templatePath) && templatePath.EndsWith(".png"))
                    {
                        using (var mp = new FileStream(templatePath, FileMode.Open, FileAccess.Read))
                        using (var bitmap = Drawing.Image.FromStream(mp))
                        {
                            bmp = new Drawing.Bitmap(bitmap.Width, bitmap.Height);
                            Drawing.Graphics g = Drawing.Graphics.FromImage(bmp);
                            g.DrawImage(bitmap, 0, 0, bitmap.Width, bitmap.Height);

                            g.Dispose();
                        }

                    }

                }

                if (bmp == null)
                {
                    bmp = new Drawing.Bitmap((int)specificModel.DefaultWidth, (int)specificModel.DefaultHeight);
                    Drawing.Graphics g = Drawing.Graphics.FromImage(bmp);
                    g.DrawRectangle(Drawing.Pens.Transparent, new Drawing.Rectangle(Drawing.Point.Empty, bmp.Size));
                    foreach (var mesh in specificModel.Meshes)
                    {
                        foreach (var face in mesh.Faces)
                        {
                            var color = new Scripts.Paril.Drawing.ConvertableColor(Scripts.Paril.Drawing.ColorRepresentation.RGB, (face.Normal.X / 2) + 0.5f, (face.Normal.Y / 2) + 0.5f, (face.Normal.Z / 2) + 0.5f, !mesh.IsArmor ? 1.0f : 0.5f);

                            var baseColor = color.ToColor();
                            color.L += 0.1f;
                            var lightColor = color.ToColor();

                            if (mesh.IsArmor)
                                lightColor = baseColor;

                            Drawing.Rectangle coords = face.TexCoordsToInteger(bmp.Width, bmp.Height);

                            for (var y = coords.Top; y < coords.Bottom; ++y)
                            {
                                for (var x = coords.Left; x < coords.Right; ++x)
                                    bmp.SetPixel(x, y, ((x + y) % 2) == 0 ? baseColor : lightColor);
                            }
                        }
                    }

                    g.Dispose();
                }

                
            }
            else if (TabControl_SourceSelector.SelectedItem == TabItem_SourceSelector_Local)
            {
                using (var mp = new FileStream(TextBlock_Dropfile_Path.Text, FileMode.Open, FileAccess.Read))
                using (var bitmap = Drawing.Image.FromStream(mp))
                {
                    bmp = new Drawing.Bitmap(bitmap.Width, bitmap.Height);
                    Drawing.Graphics g = Drawing.Graphics.FromImage(bmp);
                    g.DrawImage(bitmap, 0, 0, bitmap.Width, bitmap.Height);

                    g.Dispose();
                }
            }
            else
            {
                bmp = new Drawing.Bitmap((int)specificModel.DefaultWidth, (int)specificModel.DefaultHeight);
            }

            bmp.SaveSafe(newSkinName);

            bmp.Dispose();

            var md = new Dictionary<string, string>();
            md.Add("Model", specificModel.Name);
            PNGMetadata.WriteMetadata(newSkinName, md);


            switch (TabControl_SourceSelector.SelectedIndex)
            {
                case 0:

                    break;
            }


        }

        public void SetDialogButtonEnabled()
        {
            if(!(TreeView_Models.SelectedItem is ModelToolStripMenuItem) || TextBox_SaveTo.Text.IsNullOrEmptyOrWhitespace())
            {
                SetDialogButtonEnabled(false); return;
            }

            switch (TabControl_SourceSelector.SelectedIndex)
            {
                case 0:
                    SetDialogButtonEnabled(true);
                    break;
                case 1:
                    if (System.IO.File.Exists(TextBlock_Dropfile_Path.Text))
                        SetDialogButtonEnabled(true);
                    else
                        SetDialogButtonEnabled(false);
                    break;
            }
        }
        public void SetDialogButtonEnabled(bool isPrimaryButtonEnabled)
        {
            if(Program.Page_Editor?.ContentDialog_NewSkin != null)
            {
                Program.Page_Editor.ContentDialog_NewSkin.IsPrimaryButtonEnabled = isPrimaryButtonEnabled;
            }
        }

        private void TabControl_SourceSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetDialogButtonEnabled();
        }

        private void TreeView_Models_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
     
            try
            {
                ModelToolStripMenuItem m = TreeView_Models.SelectedItem as ModelToolStripMenuItem;

                bool isTemplateAvailable = false;

                if (m != null)
                {
                    if (m.Model?.DefaultTexture != null)
                    {
                        string templatePath = System.IO.Path.Combine(GlobalSettings.FullPath_Templates, m.Model.DefaultTexture);

                        if (File.Exists(templatePath) && templatePath.EndsWith(".png"))
                        {
                            isTemplateAvailable = true;
                        }
                    }
                }

                if (isTemplateAvailable && !CheckBox_EmptySkin_UseTemplate.IsEnabled && CheckBox_EmptySkin_UseTemplate.IsChecked == false)
                {
                    CheckBox_EmptySkin_UseTemplate.IsChecked = true;
                }

                CheckBox_EmptySkin_UseTemplate.IsEnabled = isTemplateAvailable;

            }
            catch (Exception ex)
            {
                Program.Log(ex, "NewSkinDialogContent.TreeView_Models_SelectedItemChanged()");
            }

            if (!CheckBox_EmptySkin_UseTemplate.IsEnabled && CheckBox_EmptySkin_UseTemplate.IsChecked == true)
                CheckBox_EmptySkin_UseTemplate.IsChecked = false;

            SetDialogButtonEnabled();
        }

        public void TryGetModel()
        {
            string path = TextBlock_Dropfile_Path.Text;

            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(path);

            Model model = Skin.TryGetModel(path, bmp, TreeView_Models.SelectedItem == null);

            if (model != null)
            {
                TreeView_Models.GetTreeViewItem(model.DropDownItem).IsSelected = true;

            }
        }

        private void TextBox_SaveTo_LostFocus(object sender, RoutedEventArgs e)
        {
            SetDialogButtonEnabled();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LanguageWpf.Register(this, TabItem_SourceSelector_Empty, TabItem.HeaderProperty);
            LanguageWpf.Register(this, TabItem_SourceSelector_Local, TabItem.HeaderProperty);
         
            LanguageWpf.Register(this, TextBlock_Dropfile, TextBlock.TextProperty);
            LanguageWpf.Register(this, TextBlock_EmptySkin, TextBlock.TextProperty);
            LanguageWpf.Register(this, TextBlock_SaveTo, TextBlock.TextProperty);
            LanguageWpf.Register(this, CheckBox_EmptySkin_UseTemplate, ContentProperty);
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            UnregisterLangauge();
        }

        public void UnregisterLangauge()
        {
            LanguageWpf.UnregisterContainer(this);
        }
    }
}
