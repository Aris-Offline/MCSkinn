//
//    MCSkinn, A modern Minecraft 3D skin manager/editor for Windows by NotYoojun.!
//    Copyright © iNKORE! 2023
//
//    The copy of source (only the public part) can be used anywhere with a credit to MCSkinn page at your own risk
//    https://github.com/InkoreStudios/MCSkinn
//

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Xml.Linq;
using MCSkinn.Scripts.Paril.Settings;
using MCSkinn.Scripts.Paril.Settings.Serializers;
using Modern = Inkore.UI.WPF.Modern;
using static MCSkinn.Scripts.StaticHolder;
using WPFM = System.Windows.Media;
using Inkore.Coreworks.Helpers;

namespace MCSkinn.Scripts
{
    public static class GlobalSettings
    {
        private static Settings Settings;
        public static bool? Loaded;

        [Savable("Render")]
        [DefaultValue(true)]
        public static bool ShowGround { get; set; }

        [Savable("Render")]
        [DefaultValue(false)]
        public static bool Ghost { get; set; }

        [Savable("Prefers")]
        [DefaultValue("")]
        public static string LastSkin { get; set; }

        [Savable("Render")]
        [DefaultValue(TransparencyMode.Helmet)]
        [TypeSerializer(typeof(EnumSerializer<TransparencyMode>), true)]
        public static TransparencyMode Transparency { get; set; }

        [Savable("Render")]
        [DefaultValue(true)]
        public static bool AlphaCheckerboard { get; set; }

        [Savable("Render")]
        [DefaultValue(true)]
        public static bool TextureOverlay { get; set; }

        [Savable("Render")]
        [DefaultValue("Dynamic")]
        public static string LastBackground { get; set; }

        [DefaultValue("")]
        public static string ShortcutKeys { get; set; }

        [Savable("Advanced")]
        [DefaultValue(false)]
        public static bool StylusToDraw { get; set; }

        [Savable("Advanced")]
        [DefaultValue(true)]
        public static bool IsManipulationEnabled { get; set; }

        [Savable("Advanced")]
        [DefaultValue(false)]
        public static bool CompatibilityMode { get; set; }


        [Savable("Render")]
        [DefaultValue("null")]
        [TypeSerializer(typeof(ColorSerializer), true)]
        public static Color? BackgroundColor { get; set; }

        [Savable("Render")]
        [DefaultValue("0 0 0 255")]
        [TypeSerializer(typeof(ColorSerializer), true)]
        public static Color LastPalettePrimaryColor { get; set; }

        [Savable("Render")]
        [DefaultValue("255 255 255 255")]
        [TypeSerializer(typeof(ColorSerializer), true)]
        public static Color LastPaletteSecondaryColor { get; set; }

        [Savable("Render")]
        [DefaultValue(0)]
        public static int Multisamples { get; set; }

        [Savable("Tools")]
        public static bool Tool_Pencil_Incremental { get; set; }

        [Savable("Tools")]
        public static bool Tool_DodgeBurn_Incremental { get; set; }

        [Savable("Tools")]
        [DefaultValue(0.70f)]
        public static float Tool_DodgeBurn_Exposure { get; set; }

        [Savable("Tools")]
        public static bool Tool_DarkenLighten_Incremental { get; set; }

        [Savable("Tools")]
        [DefaultValue(0.25f)]
        public static float Tool_DarkenLighten_Exposure { get; set; }

        [Savable("Tools")]
        [DefaultValue(0.20f)]
        public static float Tool_Noise_Saturation { get; set; }

        [Savable("General")]
        public static string LanguageFile { get; set; }

        //[Savable]
        //[DefaultValue(48)]
        //public static int TreeViewHeight { get; set; }

        [Savable("Tools")]
        [DefaultValue(0)]
        public static float Tool_FloodFill_Threshold { get; set; }

        [Savable("Prefers")]
        public static bool ResChangeDontShowAgain { get; set; }

        public static ObservableCollection<Workfolder> SkinDirectories { get; set; }

        [Savable("Render")]
        [DefaultValue("255 255 255 255")]
        [TypeSerializer(typeof(ColorSerializer), true)]
        public static Color DynamicOverlayLineColor { get; set; }

        [Savable("Render")]
        [DefaultValue("255 255 255 255")]
        [TypeSerializer(typeof(ColorSerializer), true)]
        public static Color DynamicOverlayTextColor { get; set; }

        [Savable("Render")]
        [DefaultValue(1)]
        public static int DynamicOverlayLineSize { get; set; }

        [Savable("Render")]
        [DefaultValue(1)]
        public static int DynamicOverlayTextSize { get; set; }

        [Savable("Render")]
        [DefaultValue("255 255 255 127")]
        [TypeSerializer(typeof(ColorSerializer), true)]
        public static Color DynamicOverlayGridColor { get; set; }

        [Savable]
        [DefaultValue(true)]
        public static bool InfiniteMouse { get; set; }

        [Savable("Render")]
        [DefaultValue(false)]
        public static bool GridEnabled { get; set; }

        [Savable]
        [DefaultValue(true)]
        public static bool UseTextureBases { get; set; }


        [Savable("Advaced")]
        [DefaultValue(false)]
        public static bool RememberPrefers { get; set; }

        [Savable("Advaced")]
        [DefaultValue(false)]
        public static bool AutoGC { get; set; }
        [Savable("Advaced")]
        [DefaultValue(1d)]
        public static double RenderScale { get; set; }

        [Savable("Appearance")]
        [DefaultValue(Modern.ElementTheme.Default)]
        public static Modern.ElementTheme RequestedTheme { get; set; }

        [Savable("Appearance")]
        [DefaultValue(1d)]
        public static double UIScale { get; set; }

        [Savable("Appearance")]
        [DefaultValue(Modern.Controls.Primitives.BackdropType.Mica)]
        public static Modern.Controls.Primitives.BackdropType BackdropType { get; set; }


        #region Functions
        public static event PropertyChangedEventHandler PropertyChanged;

        public static void RaisePropertCHangedEvent(string prop)
        {
            PropertyChanged?.Invoke(null, new PropertyChangedEventArgs(prop));
        }

        public static string FullPath_Config = null;
        public static string FullPath_Languages = null;
        public static string FullPath_Models = null;
        public static string FullPath_Brushes = null;
        public static string FullPath_Templates = null;

        public static bool? Load()
        {
            FullPath_Config = Program.GetDataPath(ConfigFilename, false);

            FullPath_Languages = Program.GetDataPath("Languages", true);
            FullPath_Models = Program.GetDataPath("Models", true);
            FullPath_Brushes = Program.GetDataPath("Brushes", true);
            FullPath_Templates = Program.GetDataPath("Templates", true);

            try
            {
                Settings = new Settings();
                Settings.LoadDefaults(typeof(GlobalSettings));

                SkinDirectories = new ObservableCollection<Workfolder>();
         
                if (string.IsNullOrEmpty(FullPath_Config))
                {
                    Loaded = null;
                    return Loaded;
                }


                Program.Log(Inkore.Coreworks.LogType.Info, "Loading config from file: " + FullPath_Config, "GlobalSettings.Load()");

                XElement root = XElement.Load(FullPath_Config);

                foreach (XElement e1 in root.Elements())
                {
                    switch (e1.Name.LocalName)
                    {
                        case "Settings":
                            Settings.Load(e1, typeof(GlobalSettings));
                            break;
                        case "Workfolders":
                            foreach (XElement e2 in e1.Elements())
                            {
                                string dir = e2.Value;
                                if (!dir.EndsWith("\\") && !dir.EndsWith("/"))
                                    dir += "\\";
                                Workfolder f = new Workfolder(dir);
                                if (e2.Attribute("Name") != null)
                                    f.Name = e2.Attribute("Name").Value;
                                if (e2.Attribute("Color") != null)
                                {
                                    try
                                    {
                                        f.Color = ((WPFM.Color?)Converter_ColorConverter.ConvertFrom(e2.Attribute("Color").Value)).GetValueOrDefault(f.Color = WPFM.Colors.CornflowerBlue);
                                    }
                                    catch { f.Color = WPFM.Colors.CornflowerBlue; }
                                }
                                else
                                    f.Name = new DirectoryInfo(dir).Name;

                                SkinDirectories.Add(f);
                            }
                            break;

                    }
                }

                Loaded = true;
                return true;
            }
            catch (Exception ex)
            {
                Program.Log(ex, false);

                Loaded = false;

            }

            return Loaded;

            //MacroHandler.RegisterMacro("DefaultSkinFolder", MacroHandler.ReplaceMacros(".\\Skins\\"));
        }

        public static bool Save()
        {
            if (string.IsNullOrEmpty(FullPath_Config))
                return false;

            XElement root = new XElement("MCSkinn.Config");

            root.Add(Settings.Save(typeof(GlobalSettings)));

            XElement elementWorkfolders = new XElement("Workfolders");
            foreach (var s in SkinDirectories)
            {
                if (s.Path.IsNullOrEmptyOrWhitespace())
                    continue;
                if(s.Name == null)
                    s.Name=new DirectoryInfo(s.Path).Name;

                XElement ele = new XElement("Folder", s.Path);
                ele.Add(new XAttribute("Name", s.Name));
                ele.Add(new XAttribute("Color", Converter_ColorConverter.ConvertToString(s.Color)));
                elementWorkfolders.Add(ele);
            }
            root.Add(elementWorkfolders);

            root.Save(FullPath_Config);

            return true;
        }

        public const string ConfigFilename = "MCSkinn.userconfig.xml";

        #endregion

        
    }
}