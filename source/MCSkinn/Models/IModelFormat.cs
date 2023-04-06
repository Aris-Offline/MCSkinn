using Paril.OpenGL;

namespace MCSkinn.Models
{
	public interface IModelFormat
	{
		Model Load(string fileName);
	}
}