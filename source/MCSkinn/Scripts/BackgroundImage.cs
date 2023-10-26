//
//    MCSkinn, A modern Minecraft 3D skin manager/editor for Windows by NotYoojun.!
//    Copyright © iNKORE! 2023
//
//    The copy of source (only the public part) can be used anywhere with a credit to MCSkinn page at your own risk
//    https://github.com/iNKOREStudios/MCSkinn
//

using System.Windows.Forms;
using MCSkinn.Scripts.Paril.OpenGL;

namespace MCSkinn.Scripts
{
    internal class BackgroundImage
    {
        public Texture GLImage;
        public ToolStripMenuItem Item;
        public string Name, Path;

        public BackgroundImage(string path, string name, Texture image)
        {
            Path = path;
            Name = name;
            GLImage = image;
        }
    }
}