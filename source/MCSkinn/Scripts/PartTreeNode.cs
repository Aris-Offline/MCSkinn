using iNKORE.Coreworks.Helpers;
using MCSkinn.Scripts.Paril.OpenGL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace MCSkinn.Scripts
{
    public class PartTreeNode : INotifyPropertyChanged
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name")); }
        }
        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Title")); }
        }

        private ObservableCollection<PartTreeNode> _nodes = new ObservableCollection<PartTreeNode>();
        public ObservableCollection<PartTreeNode> Nodes
        {
            get { return _nodes; }
            set { _nodes = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Nodes")); }
        }


        public PartTreeNode(Model model, List<Mesh> meshes, string name)
        {
            Name = name;
            Title = GetPartName(name);
            Model = model;
            
            PartIndex = -2;

            Meshes = meshes;
        }

        public static string GetPartName(string name)
        {
            string[] args = name.AddSpaceBetweenDigitAndLetter().Replace(' ', '_').CamelToSnakeCase().Split('_');

            string n = "";
            bool isFirst = true;
            foreach(string s in args)
            {
                if (!s.IsNullOrEmptyOrWhitespace())
                {
                    if (isFirst)
                    {
                        n = n + s.ToProper();
                        isFirst = false;
                    }
                    else
                    {
                        n = n + " " + s.ToProper();
                    }
                }
            }

            return n;
        }

        public PartTreeNode(Model model, Mesh mesh, int index)
        {
            Title = GetPartName(mesh.Name);
            Model = model;
            Mesh = mesh;
            PartIndex = index;

            if (PartIndex != -1)
                PartEnabled = model.PartsEnabled[Mesh];
        }

        public PartTreeNode(Model model, Mesh mesh, int index, string name) :
            this(model, mesh, index)
        {
            Name = name;

            Title = GetPartName(name);

            if (name.StartsWith("@"))
            {
                IsRadio = true;
                Title = name.Substring(1);
            }

            Name = Title = name;
        }

        private Model Model { get; set; }
        public Mesh Mesh { get; set; }
        public List<Mesh> Meshes { get; set; }
        private int PartIndex { get; set; }
        public bool IsRadio { get; set; }

        private bool _folderIsEnabled = true;

        public bool PartEnabled
        {
            get
            {
                if(PartIndex == -2)
                {
                    //bool hasEnabled = _folderIsEnabled; //, hasDisabled = false;

                    //if (hasEnabled == true)
                    //{
                    //    foreach (PartTreeNode n in Nodes)
                    //    {
                    //        if (!n.PartEnabled)
                    //        {
                    //            hasEnabled = false;
                    //            break;
                    //        }
                    //    }
                    //}

                    return _folderIsEnabled;
                }
                else
                {
                    return Model.PartsEnabled[Mesh];
                }           
            }

            set
            {
                //bool isChanged = false;
                //if (PartEnabled != value && Program.Editor != null && Program.Editor.MeshRenderer != null)
                //    isChanged = true;

                if(PartIndex == -2)
                {
                    foreach(Mesh m in Meshes)
                    {
                        Model.PartsEnabled[m] = value;
                    }
                    _folderIsEnabled = value;
                }
                else
                {
                    Model.PartsEnabled[Mesh] = value;
                    //SelectedImageIndex = ImageIndex = (value) ? IsRadio ? 10 : 3 : IsRadio ? 9 : 0;

                    //for (TreeNode node = Parent; node != null; node = node.Parent)
                    //    ((PartTreeNode)node).CheckGroupImage();
                }

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PartEnabled"));

                //if (isChanged)
                //{
                //    Program.Editor.CalculateMatrices();
                //    Program.Editor.InvalidRenderer();
                //}
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void RecursiveGroupImageCheck(PartTreeNode node, ref bool hasEnabled, ref bool hasDisabled)
        {
            if (hasEnabled && hasDisabled)
                return;

            if (node.Nodes.Count == 0)
            {
                if (node.PartEnabled)
                    hasEnabled = true;
                else
                    hasDisabled = true;
            }
            else
            {
                foreach (PartTreeNode n in node.Nodes)
                    RecursiveGroupImageCheck(n, ref hasEnabled, ref hasDisabled);
            }
        }

        public PartTreeNode IsOtherRadioButtonEnabled()
        {
            //TreeNodeCollection parentCollection;

            //if (Parent != null)
            //    parentCollection = Parent.Nodes;
            //else
            //    parentCollection = TreeView.Nodes;

            //foreach (PartTreeNode node in parentCollection)
            //{
            //    if (node == this)
            //        continue;
            //    if (node.IsRadio && node.PartEnabled)
            //        return node;
            //}

            return null;
        }

        //public void CheckGroupImage()
        //{
        //    bool hasEnabled = false, hasDisabled = false;

        //    foreach (PartTreeNode node in Nodes)
        //        RecursiveGroupImageCheck(node, ref hasEnabled, ref hasDisabled);

        //    if (hasEnabled && hasDisabled)
        //        SelectedImageIndex = ImageIndex = 6;
        //    else if (hasEnabled)
        //        SelectedImageIndex = ImageIndex = IsRadio ? 10 : 3;
        //    else if (hasDisabled)
        //        SelectedImageIndex = ImageIndex = IsRadio ? 9 : 0;
        //}

        public static void RecursiveAssign(PartTreeNode node, bool setAll, bool setTo = true)
        {
            foreach (PartTreeNode subNode in node.Nodes)
            {
                if (subNode.Nodes.Count != 0)
                    RecursiveAssign(subNode, setAll, setTo);
                else
                {
                    if (setAll)
                        subNode.PartEnabled = setTo;
                    else
                        subNode.PartEnabled = !subNode.PartEnabled;
                }
            }

            //node.CheckGroupImage();
        }

        public void TogglePart()
        {
            //if (Nodes.Count != 0)
            //{
            //    if (ImageIndex == 9)
            //    {
            //        RecursiveAssign(this, true);

            //        var other = IsOtherRadioButtonEnabled();

            //        if (other != null)
            //        {
            //            RecursiveAssign(other, true, false);
            //            other.CheckGroupImage();
            //        }

            //        CheckGroupImage();
            //    }
            //    else
            //    {
            //        bool setAll = ImageIndex == 6;
            //        RecursiveAssign(this, setAll);

            //        CheckGroupImage();
            //    }
            //}
            //else
            PartEnabled = !PartEnabled;
        }

        public override string ToString()
        {
            return Title == null ? "null" : Title;
        }

        public void ToggleChildren(bool v)
        {
            foreach(PartTreeNode n in Nodes)
            {
                n.PartEnabled = v;
                n.ToggleChildren(v);
            }
        }
    }

}
