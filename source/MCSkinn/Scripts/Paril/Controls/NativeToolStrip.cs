//
//    MCSkinn, A modern Minecraft 3D skin manager/editor for Windows by NotYoojun.!
//    Copyright © iNKORE! 2023
//
//    The copy of source (only the public part) can be used anywhere with a credit to MCSkinn page at your own risk
//    https://github.com/InkoreStudios/MCSkinn
//

using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using MCSkinn.Forms.Controls;

namespace MCSkinn.Scripts.Paril.Controls
{
    internal static class NativeStripInternals
    {
        internal static ToolStripAeroRenderer Renderer;

        static NativeStripInternals()
        {
            if (VisualStyleInformation.IsSupportedByOS && VisualStyleInformation.IsEnabledByUser)
                Renderer = new ToolStripAeroRenderer(ToolbarTheme.Toolbar);
        }
    }

    internal class NativeToolStrip : ToolStrip
    {
        public NativeToolStrip()
        {
            Renderer = NativeStripInternals.Renderer;
        }
    }

    internal class NativeToolStripContainer : ToolStripContainer
    {
        public NativeToolStripContainer()
        {
            TopToolStripPanel.Renderer = NativeStripInternals.Renderer;
            RightToolStripPanel.Renderer = NativeStripInternals.Renderer;
            LeftToolStripPanel.Renderer = NativeStripInternals.Renderer;
            BottomToolStripPanel.Renderer = NativeStripInternals.Renderer;
        }
    }

    public class NativeMenuStrip : MenuStrip
    {
        public NativeMenuStrip()
        {
            Renderer = NativeStripInternals.Renderer;
        }
    }
}