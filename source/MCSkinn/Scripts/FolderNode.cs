//
//    MCSkinn, A modern Minecraft 3D skin manager/editor for Windows by NotYoojun.!
//    Copyright © iNKORE! 2023
//
//    The copy of source (only the public part) can be used anywhere with a credit to MCSkinn page at your own risk
//    https://github.com/InkoreStudios/MCSkinn
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Xml.Linq;
using Inkore.Coreworks.Helpers;
using Inkore.Coreworks.Windows.Helpers;
using OpenTK.Graphics.OpenGL;
using Windows.ApplicationModel.Contacts;
using Windows.Media.Capture;
using Windows.Networking.PushNotifications;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using static OpenTK.Graphics.OpenGL.GL;
using IO = System.IO;

namespace MCSkinn.Scripts
{
    [Serializable]
    public class FolderNode : LibraryNode
    {
        FileSystemWatcher watcher;

        public FolderNode(string path, FolderNode parent, bool isRoot, Workfolder wf)
        {
            Path = path;
            Parent = parent;

            if (isRoot)
            {
                watcher = new FileSystemWatcher();
                watcher.IncludeSubdirectories = true;
                watcher.Path = path;
                
                watcher.Changed += Watcher_Changed;
                watcher.Created += Watcher_Created;
                watcher.Deleted += Watcher_Deleted;
                watcher.Renamed += Watcher_Renamed;
                watcher.Error += Watcher_Error;
                watcher.EnableRaisingEvents = true;
            }

            Workfolder = wf;
        }

        #region Properties

        public static System.Windows.Media.Imaging.BitmapSource FolderIcon = Properties.Resources.Folder_32x32.ToWpfBitmapSourceB();

        public override System.Windows.Media.Imaging.BitmapSource Icon
        {
            get { return FolderIcon; }
        }

        public override FolderNode Parent { get; set; }

        public ObservableCollection<LibraryNode> Nodes { get; private set; } = new ObservableCollection<LibraryNode>();
        public string RootDir { get; set; }

        private string _path = "";
        private string _name = "";
        private string _text = "";
        private DirectoryInfo _directoryInfo = null;
        public override string Path
        {
            get { return _path; }
            set
            {
                _path = value;

                _directoryInfo = new DirectoryInfo(Path);

                _name = DirectoryInfo.Name;
                _text = Name;

                RaisePropertyChangedEvent(nameof(Text));
                RaisePropertyChangedEvent(nameof(Path));
                RaisePropertyChangedEvent(nameof(Name));
                RaisePropertyChangedEvent(nameof(DirectoryInfo));
            }
        }

        public DirectoryInfo DirectoryInfo => _directoryInfo;


        public override string Text => _text;

        public override string Name => _name;

        public override string ToString()
        {
            return Text;
        }

        private bool _isExpanded;
        public override bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                _isExpanded = value;

                RaisePropertyChangedEvent(nameof(IsExpanded));

                if (!isSkinsLoaded && IsExpanded)
                    LoadSubitems();
            }
        }

        private bool isSkinsLoaded = false;
        public void LoadSubitems()
        {
            foreach(LibraryNode n in Nodes)
            {
                if(n is Skin)
                {
                    Exception ex = ((Skin)n).SetImages();

                    if(ex != null)
                    {
                        Program.Log(ex, "Error loading skin " + n.Path);
                    }
                }
            }

            isSkinsLoaded = true;
        }

        public Workfolder Workfolder { get; private set; }


        #endregion

        #region Watcher Events
        private void Watcher_Renamed(object sender, RenamedEventArgs e)
        {
            System.Windows.Application.Current.Dispatcher.BeginInvoke((() =>
            {
                try
                {

                LibraryNode node = SkinLibrary.FindNode(e.OldFullPath);
                if (node != null)
                {
                    node.Renamed(e.FullPath);
                }
            }
            catch (Exception ex)
            {
                if (!Program.IsDebugVersion)
                    Program.Log(Inkore.Common.LogType.Warning, string.Format("Automation failed: Unable to rename node '{0}' to '{1}', because {2}", e.OldName, e.Name, ex.Message), e.OldFullPath + "\r\n" + e.FullPath + "\r\n\r\n" + ex.StackTrace);
                else
                    throw;
            }
            }));


        }
        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {

        }

        private void Watcher_Error(object sender, ErrorEventArgs e)
        {

        }

        private void Watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            System.Windows.Application.Current.Dispatcher.BeginInvoke((() =>
            {
                try
                {

                    LibraryNode node = SkinLibrary.FindNode(e.FullPath);
                    if (node != null)
                    {
                        node.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    if (!Program.IsDebugVersion)
                        Program.Log(Inkore.Common.LogType.Warning, string.Format("Automation failed: Unable to remove node '{0}' , because {2}", e, ex.Message), e.FullPath + "\r\n\r\n" + ex.StackTrace);
                    else
                        throw;
                }
            }));

        }

        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            System.Windows.Application.Current.Dispatcher.BeginInvoke((() =>
            {
                string path2 = SkinLibrary.GetAbsolutedPath(e.FullPath);
                string path = IO.Path.GetFullPath(e.FullPath);

                if (SkinLibrary.NodesByPath.ContainsKey(path2))
                {
                    Program.Log(Inkore.Common.LogType.Warning, string.Format("Automation failed: Unable to add file '{0}' into library, because its node has already benn added.", path), "");
                    return;
                }

                List<Skin> invalidSkins = new List<Skin>();

                try
                {
                    string parentDir = Directory.GetParent(e.FullPath).FullName;
                    string parentDir2 = SkinLibrary.GetAbsolutedPath(parentDir);

                    if (!SkinLibrary.NodesByPath.ContainsKey(parentDir2))
                    {
                        Program.Log(Inkore.Common.LogType.Warning, string.Format("Automation failed: Unable to automatically add file '{0}' into library, because the parent node '{1}' is missing.", path2, parentDir2), path + "\r\n\r\n" + parentDir);
                        return;
                    }

                    FolderNode parent = SkinLibrary.NodesByPath[parentDir2] as FolderNode;

                    if (IO.Path.GetExtension(path) == ".png")
                    {

                        Skin sk = new Skin(path, parent);

                        sk.SetImages();

                        if (SkinLibrary.AddNode(sk, path))
                        {
                            parent.Add(sk);
                            Program.Log(Inkore.Common.LogType.Load, string.Format("Automation success: added skin '{0}' into library.", sk.Name), path + "\r\n\r\n" + parentDir);

                            if (SkinLibrary.NewItemToSelect == SkinLibrary.GetAbsolutedPath(path))
                                SkinLibrary.SelectedNode = sk;

                        }
                        else
                        {
                            Program.Log(Inkore.Common.LogType.Warning, string.Format("Automation failed: Unable to automatically skin '{0}' into library, because an error happend while adding it to SkinLibrary.NodesByPath collection", sk.Name), path + "\r\n\r\n" + parentDir);
                        }
                    }
                    else if (Directory.Exists(path) && !File.Exists(path))
                    {
                        //List<Skin> skins = new List<Skin>();

                        FolderNode folder = new FolderNode(path, null, true, Workfolder) { RootDir = path };
                        //SkinLoader.RecurseAddDirectories(path, folder, skins);
                        parent.Add(folder);

                        if (SkinLibrary.AddNode(folder, path))
                        {
                            parent.Add(folder);
                            Program.Log(Inkore.Common.LogType.Load, string.Format("Automation success: added folder '{0}' into library.", path), path + "\r\n\r\n" + parentDir);

                            if (SkinLibrary.NewItemToSelect == SkinLibrary.GetAbsolutedPath(path))
                                SkinLibrary.SelectedNode = folder;
                        }
                        else
                        {
                            Program.Log(Inkore.Common.LogType.Warning, string.Format("Automation failed: Unable to automatically folder '{0}' into library, because an error happend while adding it to SkinLibrary.NodesByPath collection", folder.Text), path + "\r\n\r\n" + parentDir);
                        }

                        //foreach (Skin s in skins)
                        //{
                        //    if (!s.SetImages())
                        //        invalidSkins.Add(s);
                        //}

                    }
                    else
                    {
                        Program.Log(Inkore.Common.LogType.Warning, string.Format("Automation failed: Unable to add file '{0}' into library, because unable to analyze its type.", e.FullPath), "");
                        return;
                    }

                    if (invalidSkins.Count > 0)
                    {
                        foreach (Skin s in invalidSkins)
                        {
                            Program.Log(Inkore.Common.LogType.Warning, string.Format("Automation failed: Unable to add skin '{0}' into library, because it cannot be loaded.", s.Path), "");
                        }
                    }

                }
                catch { throw; }

            }));

        }

        #endregion

        public void Sort()
        {
            System.Windows.Application.Current.Dispatcher.Invoke((Action)(() =>
            {
                Nodes.Sort();
            }));
        }

        public void Recurse(bool subfolders = true)
        {
            var di = new DirectoryInfo(Path);

            foreach (DirectoryInfo dir in di.EnumerateDirectories())
            {
                if ((dir.Attributes & FileAttributes.Hidden) != 0)
                    continue;

                var leadingName = dir.FullName + '\\';
                var folderNode = new FolderNode(leadingName, this, false, Workfolder);
                
                if(subfolders)
                    folderNode.Recurse(subfolders);

                Add(folderNode);

                SkinLibrary.AddNode(folderNode, dir.FullName);
            }

            foreach (FileInfo file in di.EnumerateFiles("*.png", SearchOption.TopDirectoryOnly))
            {
                var skin = new Skin(file.FullName, this);

                Add(skin);

                SkinLibrary.AddNode(skin, file.FullName);
            }

        }



        public void Add(LibraryNode skin)
        {
            System.Windows.Application.Current.Dispatcher.Invoke((Action)(() =>
            {
                bool needSort = Nodes.Count > 0 ? (Nodes[Nodes.Count - 1].GetType() != skin.GetType()) : (skin is Skin);

                if (!Nodes.Contains(skin))
                    Nodes.Add(skin);

                skin.Parent = this;

                if (needSort)
                    Sort();
            }));
        }

        public override void Renamed(string newName)
        {
            string oldName = Path;

            Path = IO.Path.GetFullPath(newName);

            SkinLibrary.RemoveNode(oldName);
            SkinLibrary.AddNode(this, newName);

            foreach(var node in Nodes)
            {
                node.Renamed(IO.Path.Combine(Path, node.Name));
            }

            RaisePropertyChangedEvent(nameof(Text));
            RaisePropertyChangedEvent(nameof(Path));
        }

        public override void Dispose()
        {
            if (Parent != null)
                Parent.Remove(this);

            if (SkinLibrary.SelectedNode == this)
                SkinLibrary.SelectedNode = null;

            if (SkinLibrary.NodesByPath.ContainsKey(SkinLibrary.GetAbsolutedPath(Path)))
                SkinLibrary.NodesByPath.Remove(SkinLibrary.GetAbsolutedPath(Path));
            else
                Program.Log(Inkore.Common.LogType.Error, string.Format("Cannot dispose folder '{0}', because it cannot be found in the dictinary", Path), "");

            LibraryNode[] children = new LibraryNode[Nodes.Count];
            Nodes.CopyTo(children, 0);
            foreach (var node in children)
            {
                node.Dispose();
            }
        }

        public void Remove(LibraryNode skin)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                Nodes.Remove(skin);
            });
        }
    }

    public abstract class LibraryNode : INotifyPropertyChanged, IDisposable, IComparable<LibraryNode>
    {
        public abstract string Text { get; }
        public abstract string Path { get; set; }
        public abstract string Name { get; }
        public abstract bool IsExpanded { get; set; }
        public abstract System.Windows.Media.Imaging.BitmapSource Icon { get; }
        public abstract FolderNode Parent { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public abstract void Dispose();
        public abstract void Renamed(string newName);

        public int CompareTo(LibraryNode? other)
        {
            var l = this;
            var r = other;

            if (l == null)
                return 1;
            else if (l is Skin && !(r is Skin))
                return -1;
            else if (!(l is Skin) && r is Skin)
                return 1;
            else if (l is Skin && r is Skin)
                return -((Skin)l).Name.CompareTo(((Skin)r).Name);

            return -l.Text.CompareTo(r.Text);

        }

        public void RaisePropertyChangedEvent(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private bool _isSelected;
        public virtual bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;

                RaisePropertyChangedEvent(nameof(IsSelected));
            }
        }
    }

}