//
//    MCSkinn, A modern Minecraft 3D skin manager/editor for Windows by NotYoojun.!
//    Copyright © iNKORE! 2023
//
//    The copy of source (only the public part) can be used anywhere with a credit to MCSkinn page at your own risk
//    https://github.com/InkoreStudios/MCSkinn
//

using OpenTK.Graphics.OpenGL;

namespace MCSkinn.Scripts.Paril.OpenGL
{
    /// <summary>
    /// OpenGL renderer that renders using immediate mode.
    /// 
    /// </summary>
    public class ImmediateRenderer : Renderer
    {
        public override void RenderMesh(Mesh mesh)
        {
            mesh.Texture.Bind();

            GL.PushMatrix();

            GL.MultMatrix(ref mesh.Matrix);

            if (mesh.DrawTransparent)
                GL.Color4((byte)255, (byte)255, (byte)255, (byte)63);
            else
                GL.Color4((byte)255, (byte)255, (byte)255, (byte)255);

            GL.Begin(mesh.Mode);
            foreach (Face face in mesh.Faces)
            {
                foreach (int index in face.Indices)
                {
                    GL.TexCoord2(face.TexCoords[index]);
                    GL.Vertex3(face.Vertices[index]);
                }
            }
            GL.End();

            GL.Color4((byte)255, (byte)255, (byte)255, (byte)255);

            GL.PopMatrix();
        }
    }
}