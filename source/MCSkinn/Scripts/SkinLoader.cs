using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Controls;
using System.Windows.Forms;
using MCSkinn.Scripts.Macros;

namespace MCSkinn.Scripts
{
    public static class SkinLoader
    {

        public static void LoadSkins()
        {
            List<Skin> skins = new List<Skin>();

            foreach (Workfolder f in GlobalSettings.SkinDirectories)
            {
                f.Initialize();

                SkinLibrary.RootFolders.Add(f.Root);
            }

            //Program.Page_Splash.Dispatcher.Invoke((Action<List<TreeNode>>)Editor.MainForm.BeginFinishedLoadingSkins, rootNodes);

            //var invalidSkins = new List<Skin>();

            //foreach (Skin s in skins)
            //{
            //    if (s.SetImages() != null)
            //        invalidSkins.Add(s);
            //}

            //skins.RemoveAll((s) => invalidSkins.Contains(s));

            //Program.Page_Splash.Dispatcher.Invoke((Action<List<Skin>, TreeNode>)Editor.MainForm.FinishedLoadingSkins, skins, _tempToSelect);
        }


    }

    public static class SkinLibrary
    {
        public static ObservableCollection<FolderNode> RootFolders = new ObservableCollection<FolderNode>();

        public static Dictionary<string, LibraryNode> NodesByPath = new Dictionary<string, LibraryNode>();

        public static event EventHandler SelectedNodeChanged;

        private static LibraryNode _selectedNode = null;

        public static LibraryNode SelectedNode
        {
            get
            {
                return _selectedNode;
            }
            set
            {
                _selectedNode = value;

                SelectedNodeChanged?.Invoke(null, EventArgs.Empty);
            }
        }

        public static LibraryNode FindNode(string path)
        {
            string s = GetAbsolutedPath(path);

            if (NodesByPath.ContainsKey(s))
            {
                return NodesByPath[s];
            }
            else
            {
                return null;
            }
        }

        public static bool AddNode(LibraryNode node, string path)
        {
            string s = GetAbsolutedPath(path);

            if (!NodesByPath.ContainsKey(s) && s != null)
            {
                try
                {
                    NodesByPath.Add(s, node);
                    return true;
                }
                catch(Exception ex)
                {
                    Program.Log(ex);
                    return false;                
                }
            }
            else
            {
                return false;
            }

        }

        public static bool RemoveNode(string path)
        {
            string s = GetAbsolutedPath(path);

            return NodesByPath.Remove(s);
        }

        public static string GetAbsolutedPath(string p)
        {
            string s = Path.GetFullPath(p).ToLower().Replace("/", @"\").Replace(@"\\", @"\").Replace(@"\\\", @"\");

            if (s.EndsWith(@"\"))
                return s.Substring(0, s.Length - 1);
            else
                return s;
        }

        public static string NewItemToSelect { get; set; }
    }

}
