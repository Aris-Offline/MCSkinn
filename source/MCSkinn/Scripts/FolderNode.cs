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
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Shapes;
using System.Xml.Linq;
using iNKORE.Coreworks;
using iNKORE.Coreworks.Helpers;
using iNKORE.Coreworks.Windows.Helpers;
using OpenTK.Graphics.OpenGL;
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
                try
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
                catch (Exception ex)
                {
                    Program.Log(ex, "FolderNode.FolderNode(), CreatingWatcher");
                }
            }

            Workfolder = wf;

            BindingOperations.EnableCollectionSynchronization(Nodes, new object());
        }

        #region Properties

        public static System.Windows.Media.Imaging.BitmapSource FolderIcon = Properties.Resources.Folder_32x32.ToWpfBitmapSourceB();

        public override System.Windows.Media.Imaging.BitmapSource Icon
        {
            get { return FolderIcon; }
        }

        public override FolderNode Parent { get; set; }

        public override ObservableCollection<LibraryNode> Nodes { get; set; } = new ObservableCollection<LibraryNode>();
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

        public override bool UseChessBackground => false;

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
                if(n is SkinNode)
                {
                    Exception ex = ((SkinNode)n).SetImages();

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
                    node.OnRenamed(e.FullPath);
                }
            }
            catch (Exception ex)
            {
                if (!Program.IsDebugVersion)
                    Program.Log(LogType.Warning, string.Format("Automation failed: Unable to rename node '{0}' to '{1}', because {2}", e.OldName, e.Name, ex.Message), e.OldFullPath + "\r\n" + e.FullPath + "\r\n\r\n" + ex.StackTrace);
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
                        Program.Log(LogType.Warning, string.Format("Automation failed: Unable to remove node '{0}' , because {2}", e, ex.Message), e.FullPath + "\r\n\r\n" + ex.StackTrace);
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
                    Program.Log(LogType.Warning, string.Format("Automation failed: Unable to add file '{0}' into library, because its node has already benn added.", path), "");
                    return;
                }

                List<SkinNode> invalidSkins = new List<SkinNode>();

                try
                {
                    string parentDir = Directory.GetParent(e.FullPath).FullName;
                    string parentDir2 = SkinLibrary.GetAbsolutedPath(parentDir);

                    if (!SkinLibrary.NodesByPath.ContainsKey(parentDir2))
                    {
                        Program.Log(LogType.Warning, string.Format("Automation failed: Unable to automatically add file '{0}' into library, because the parent node '{1}' is missing.", path2, parentDir2), path + "\r\n\r\n" + parentDir);
                        return;
                    }

                    FolderNode parent = SkinLibrary.NodesByPath[parentDir2] as FolderNode;

                    if (IO.Path.GetExtension(path) == ".png")
                    {

                        SkinNode sk = new SkinNode(path, parent);

                        sk.SetImages();

                        if (SkinLibrary.AddNode(sk, path))
                        {
                            parent.Add(sk);
                            Program.Log(LogType.Load, string.Format("Automation success: added skin '{0}' into library.", sk.Name), path + "\r\n\r\n" + parentDir);

                            if (SkinLibrary.NewItemToSelect == SkinLibrary.GetAbsolutedPath(path))
                                SkinLibrary.SelectedNode = sk;

                        }
                        else
                        {
                            Program.Log(LogType.Warning, string.Format("Automation failed: Unable to automatically skin '{0}' into library, because an error happend while adding it to SkinLibrary.NodesByPath collection", sk.Name), path + "\r\n\r\n" + parentDir);
                        }

                        if(Program.Page_Editor?.FilePathToSelect == path)
                        {
                            SkinLibrary.SelectedNode = sk;
                            Program.Page_Editor.FilePathToSelect = null;
                        }
                    }
                    else if (Directory.Exists(path))
                    {
                        //List<Skin> skins = new List<Skin>();

                        FolderNode folder = new FolderNode(path, null, true, Workfolder) { RootDir = path };
                        //SkinLoader.RecurseAddDirectories(path, folder, skins);
                        parent.Add(folder);

                        if(folder.Nodes.Count != Directory.GetFiles(path).Length + Directory.GetDirectories(path).Length)
                        {
                            foreach(var subpath in Directory.GetFiles(path).Union(Directory.GetDirectories(path)))
                            {
                                if(SkinLibrary.FindNode(subpath) == null)
                                {
                                    var fil = new FileInfo(subpath);
                                    Watcher_Created(null, new FileSystemEventArgs(WatcherChangeTypes.Created, fil.DirectoryName, fil.Name));
                                }
                            }
                        }

                        if (SkinLibrary.AddNode(folder, path))
                        {
                            parent.Add(folder);
                            Program.Log(LogType.Load, string.Format("Automation success: added folder '{0}' into library.", path), path + "\r\n\r\n" + parentDir);

                            if (SkinLibrary.NewItemToSelect == SkinLibrary.GetAbsolutedPath(path))
                                SkinLibrary.SelectedNode = folder;
                        }
                        else
                        {
                            Program.Log(LogType.Warning, string.Format("Automation failed: Unable to automatically folder '{0}' into library, because an error happend while adding it to SkinLibrary.NodesByPath collection", folder.Text), path + "\r\n\r\n" + parentDir);
                        }

                        if (Program.Page_Editor?.FilePathToSelect == path)
                        {
                            SkinLibrary.SelectedNode = folder;
                            Program.Page_Editor.FilePathToSelect = null;
                        }

                    }
                    else
                    {
                        Program.Log(LogType.Warning, string.Format("Automation failed: Unable to add file '{0}' into library, because unable to analyze its type.", e.FullPath), "");
                        return;
                    }

                    if (invalidSkins.Count > 0)
                    {
                        foreach (SkinNode s in invalidSkins)
                        {
                            Program.Log(LogType.Warning, string.Format("Automation failed: Unable to add skin '{0}' into library, because it cannot be loaded.", s.Path), "");
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

        public bool IsRecursed { get; private set; } = false;
        public override bool IsDirty
        {
            get
            {
                //return false;

                foreach(var item in Nodes.Reverse())
                {
                    if(item.IsDirty)
                        return true;
                }

                return false;
            }
            set { }
        }

        public override LibraryNodeType NodeType => LibraryNodeType.Folder;

        public override bool IsLoaded => true;

        public void Recurse(bool subfolders = true)
        {
            try
            {
                var di = new DirectoryInfo(Path);

                if (!di.Exists)
                {
                    throw new DirectoryNotFoundException(Path + " cannot be found!");
                }

                foreach(var node in Nodes.ToArray())
                {
                    node.Dispose();
                }

                Nodes.Clear();

                foreach (DirectoryInfo dir in di.EnumerateDirectories())
                {
                    if ((dir.Attributes & FileAttributes.Hidden) != 0)
                        continue;

                    var leadingName = dir.FullName + '\\';
                    var folderNode = new FolderNode(leadingName, this, false, Workfolder);

                    if (subfolders)
                        folderNode.Recurse(subfolders);

                    Add(folderNode);

                    SkinLibrary.AddNode(folderNode, dir.FullName);
                }

                foreach (FileInfo file in di.EnumerateFiles("*.png", SearchOption.TopDirectoryOnly))
                {
                    var skin = new SkinNode(file.FullName, this);
                    skin.SetImages();

                    Add(skin);

                    SkinLibrary.AddNode(skin, file.FullName);
                }

                IsRecursed = true;
            }
            catch(Exception ex)
            {
                Program.Log(ex, "FolderNode.Recurse()");
                IsRecursed = false;
            }

        }



        public void Add(LibraryNode skin)
        {
            bool needSort = Nodes.Count > 0 ? (Nodes[Nodes.Count - 1].GetType() != skin.GetType()) : (skin is SkinNode);

            if (!Nodes.Contains(skin))
                Nodes.Add(skin);

            skin.Parent = this;

            if (needSort)
                Sort();
        }

        public override void OnRenamed(string newName)
        {
            string oldName = Path;

            Path = IO.Path.GetFullPath(newName);

            SkinLibrary.RemoveNode(oldName);
            SkinLibrary.AddNode(this, newName);

            foreach(var node in Nodes)
            {
                node.OnRenamed(IO.Path.Combine(Path, node.Name));
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
                Program.Log(LogType.Error, string.Format("Cannot dispose folder '{0}', because it cannot be found in the dictinary", Path), "");

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

        public override void Reload()
        {
            Recurse(true);
        }
    }

    public enum LibraryNodeType
    {
        Folder,
        Skin
    }

    public abstract class LibraryNode : INotifyPropertyChanged, IDisposable, IComparable<LibraryNode>
    {
        public abstract string Text { get; }
        public abstract string Path { get; set; }
        public abstract string Name { get; }
        public abstract bool IsExpanded { get; set; }
        public abstract System.Windows.Media.Imaging.BitmapSource Icon { get; }
        public abstract FolderNode Parent { get; set; }

        public abstract bool IsDirty { get; set; }
        public abstract LibraryNodeType NodeType { get; }
        public abstract bool IsLoaded { get; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public abstract ObservableCollection<LibraryNode> Nodes { get; set; }
        public abstract void Dispose();
        public abstract void OnRenamed(string newName);

        public abstract void Reload();

        public abstract bool UseChessBackground { get; }

        public virtual bool IsFolder
        {
            get { return NodeType == LibraryNodeType.Folder; }
        }

        public int CompareTo(LibraryNode? other)
        {
            var l = this;
            var r = other;

            if (l == null)
                return 1;
            else if (l is SkinNode && !(r is SkinNode))
                return -1;
            else if (!(l is SkinNode) && r is SkinNode)
                return 1;
            else if (l is SkinNode && r is SkinNode)
                return -((SkinNode)l).Name.CompareTo(((SkinNode)r).Name);

            return -l.Text.CompareTo(r.Text);

        }

        public bool? Contains(LibraryNode child)
        {
            if (this == child)
                return null;

            if (this is FolderNode)
            {
                foreach (var node in Nodes)
                {
                    if (node is FolderNode folder)
                    {
                        if (folder.Contains(child) == true)
                            return true;
                    }
                    else
                    {
                        if (node == child) return true;
                    }
                }
            }
            else
            {
                return false;
            }

            return false;
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