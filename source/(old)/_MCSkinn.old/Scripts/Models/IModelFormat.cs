using MCSkinn.Scripts.Paril.OpenGL;

namespace MCSkinn.Scripts.Models
{
    public interface IModelFormat
    {
        Model Load(string fileName);
    }
}