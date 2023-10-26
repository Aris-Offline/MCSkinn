//
//    MCSkinn, A modern Minecraft 3D skin manager/editor for Windows by NotYoojun.!
//    Copyright © iNKORE! 2023
//
//    The copy of source (only the public part) can be used anywhere with a credit to MCSkinn page at your own risk
//    https://github.com/iNKOREStudios/MCSkinn
//

using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace MCSkinn.Scripts.Paril.Extensions
{
    public static class Extensions
    {
        public static FileInfo CopyToParent(this FileInfo me, string newName)
        {
            return me.CopyTo(me.Directory.FullName + newName);
        }

        public static void MoveToParent(this FileInfo me, string newName)
        {
            me.MoveTo(me.Directory.FullName + newName);
        }

        public static TreeNodeCollection GetParentCollection(this TreeNode node)
        {
            if (node.Parent == null)
                return node.TreeView.Nodes;
            return node.Parent.Nodes;
        }

        public static List<TreeNode> GetNodeChain(this TreeNode node)
        {
            var nodes = new List<TreeNode>();

            for (TreeNode n = node; n != null; n = n.Parent)
                nodes.Add(n);

            return nodes;
        }
    }
}