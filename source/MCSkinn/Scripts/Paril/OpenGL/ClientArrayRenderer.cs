//
//    MCSkinn, A modern Minecraft 3D skin manager/editor for Windows by NotYoojun.!
//    Copyright © iNKORE! 2023
//
//    The copy of source (only the public part) can be used anywhere with a credit to MCSkinn page at your own risk
//    https://github.com/iNKOREStudios/MCSkinn
//

using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace MCSkinn.Scripts.Paril.OpenGL
{
    /// <summary>
    /// OpenGL renderer that renders using client-side array mode.
    /// 
    /// </summary>
    public class ClientArrayRenderer : Renderer
    {
        public ClientArrayRenderer()
        {
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.EnableClientState(ArrayCap.ColorArray);
            GL.EnableClientState(ArrayCap.TextureCoordArray);
        }

        public override void RenderMesh(Mesh mesh)
        {
            mesh.Texture.Bind();

            GL.PushMatrix();

            GL.MultMatrix(ref mesh.Matrix);

            var data = mesh.GetUserData<ClientArrayMeshUserData>();

            GL.VertexPointer(3, VertexPointerType.Float, 0, data.VerticeArray);
            GL.TexCoordPointer(2, TexCoordPointerType.Float, 0, data.TexCoordArray);
            GL.ColorPointer(4, ColorPointerType.Float, 0, data.ColorArray);

            GL.DrawElements(mesh.Mode, data.IndiceArray.Length, DrawElementsType.UnsignedByte, data.IndiceArray);

            GL.PopMatrix();
        }

        protected override void PreRender()
        {
            
        }

        protected override void PostRender()
        {
        }

        public override IMeshUserData CreateUserData(Mesh mesh)
        {
            ClientArrayMeshUserData data = new ClientArrayMeshUserData();

            List<Vector3> vertices = new List<Vector3>();
            List<Vector2> texCoords = new List<Vector2>();
            List<byte> indices = new List<byte>();

            int totalCount = 0;
            foreach (var f in mesh.Faces)
            {
                vertices.AddRange(f.Vertices);
                texCoords.AddRange(f.TexCoords);

                foreach (var c in f.Indices)
                    indices.Add((byte)(c + totalCount));

                totalCount += f.Vertices.Length;
            }

            data.VerticeArray = new float[vertices.Count * 3];
            data.TexCoordArray = new float[texCoords.Count * 2];
            data.IndiceArray = indices.ToArray();

            int vi = 0;
            foreach (var x in vertices)
            {
                data.VerticeArray[vi++] = x.X;
                data.VerticeArray[vi++] = x.Y;
                data.VerticeArray[vi++] = x.Z;
            }

            vi = 0;
            foreach (var x in texCoords)
            {
                data.TexCoordArray[vi++] = x.X;
                data.TexCoordArray[vi++] = x.Y;
            }

            return data;
        }

        public override void UpdateUserData(Mesh mesh)
        {
            List<Color4> colors = new List<Color4>();

            Color4 color = new Color4(1, 1, 1, mesh.DrawTransparent ? 0.25f : 1.0f);

            foreach (var x in mesh.Faces)
                colors.AddRange(new Color4[] { color, color, color, color });

            ClientArrayMeshUserData data = mesh.GetUserData<ClientArrayMeshUserData>();

            data.ColorArray = new float[colors.Count * 4];

            int vi = 0;
            foreach (var x in colors)
            {
                data.ColorArray[vi++] = x.R;
                data.ColorArray[vi++] = x.G;
                data.ColorArray[vi++] = x.B;
                data.ColorArray[vi++] = x.A;
            }
        }
    }
}