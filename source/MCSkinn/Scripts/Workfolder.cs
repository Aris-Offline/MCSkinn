using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF = System.Windows;
using IO = System.IO;
using MCSkinn.Scripts.Macros;

namespace MCSkinn.Scripts
{
    public class Workfolder : INotifyPropertyChanged, IDisposable
    {

        public event PropertyChangedEventHandler? PropertyChanged;

        private string _path = "";
        private string _name = "";
        public string Path
        {
            get { return _path; }
            set
            {
                _path = value;

                RaisePropertyChangedEvent(nameof(Path));
            }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChangedEvent(nameof(Name)); }
        }

        private FolderNode _root = null;
        public FolderNode Root
        {
            get { return _root; }
            set
            {
                _root = value;
                RaisePropertyChangedEvent(nameof(Root));
            }
        }
        private WPF.Media.Color _color = WPF.Media.Colors.Blue;
        public WPF.Media.Color Color
        {
            get { return _color; }
            set
            {
                _color = value;
                RaisePropertyChangedEvent(nameof(Color));
            }
        }

        private List<Skin> DirtySkins = new List<Skin>();
        public bool IsDirty
        {
            get { return DirtyCount != 0; }
        }
        public int DirtyCount
        {
            get { return DirtySkins.Count; }
        }

        public Workfolder(string path)
        {
            Path = path;
        }


        public void SetDirtySkin(Skin skin)
        {
            if (skin.IsDirty)
            {
                if(!DirtySkins.Contains(skin))
                    DirtySkins.Add(skin);
            }
            else
            {
                if (DirtySkins.Contains(skin))
                    DirtySkins.Remove(skin);
            }

            RaisePropertyChangedEvent(nameof(DirtyCount));
            RaisePropertyChangedEvent(nameof(IsDirty));
        }

        public void Dispose()
        {
            Root?.Dispose();
            SkinLibrary.RootFolders.Remove(Root);

            GC.Collect();

            GC.SuppressFinalize(this);
        }
        public void RaisePropertyChangedEvent(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Initialize()
        {
            string x = Path;

            var expanded = IO.Path.GetFullPath(MacroHandler.ReplaceMacros(x));
            var folder = new FolderNode(expanded, null, true, this) { RootDir = expanded };
            //RecurseAddDirectories(expanded, folder, skins);

            folder.Recurse();

            SkinLibrary.AddNode(folder, expanded);
            Program.Log(Inkore.Common.LogType.Load, "Loaded folder: " + expanded, expanded);

            Root = folder;
        }

        public Inkore.UI.WPF.Modern.Controls.NavigationViewItem NavigationItem { get; set; }

    }
}
