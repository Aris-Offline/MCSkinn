using System.Drawing;

namespace MCSkinn.Scripts.Swatches
{
    public class NamedColor
    {
        public NamedColor(string name, Color color)
        {
            Name = name;
            Color = color;
        }

        public string Name { get; set; }
        public Color Color { get; set; }
    }
}