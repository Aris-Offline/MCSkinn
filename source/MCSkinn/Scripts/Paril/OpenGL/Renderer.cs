//
//    MCSkinn, A modern Minecraft 3D skin manager/editor for Windows by NotYoojun.!
//    Copyright © iNKORE! 2023
//
//    The copy of source (only the public part) can be used anywhere with a credit to MCSkinn page at your own risk
//    https://github.com/iNKOREStudios/MCSkinn
//

using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;

namespace MCSkinn.Scripts.Paril.OpenGL
{
    public abstract class Renderer
    {
        private readonly List<Mesh> _opaqueMeshes = new List<Mesh>();
        private readonly List<Mesh> _transparentMeshes = new List<Mesh>();

        public void Sort()
        {
            _transparentMeshes.Sort(
                (left, right) =>
                {
                    float leftDist = (Editor.CameraPosition - left.Center).Length;
                    float rightDist = (Editor.CameraPosition - right.Center).Length;

                    return rightDist.CompareTo(leftDist);
                }
                );

        }

        public void Render()
        {
            Sort();

            PreRender();

            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Front);

            foreach (Mesh mesh in _opaqueMeshes)
                RenderMesh(mesh);

            GL.Enable(EnableCap.Blend);

            foreach (Mesh mesh in _transparentMeshes)
            {
                GL.CullFace(CullFaceMode.Back);
                RenderMesh(mesh);
                GL.CullFace(CullFaceMode.Front);
                RenderMesh(mesh);
            }

            GL.Disable(EnableCap.Blend);
            GL.Disable(EnableCap.CullFace);

            PostRender();

            _opaqueMeshes.Clear();
            _transparentMeshes.Clear();
        }

        public void RenderWithoutTransparency()
        {
            GL.Disable(EnableCap.Blend);

            PreRender();

            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Back);

            foreach (Mesh mesh in _opaqueMeshes)
                RenderMesh(mesh);

            foreach (Mesh mesh in _transparentMeshes)
            {
                GL.CullFace(CullFaceMode.Front);
                RenderMesh(mesh);
                GL.CullFace(CullFaceMode.Back);
                RenderMesh(mesh);
            }

            GL.Disable(EnableCap.CullFace);

            PostRender();

            _opaqueMeshes.Clear();
            _transparentMeshes.Clear();
        }

        public void AddMesh(Mesh mesh)
        {
            if (mesh.HasTransparency || mesh.DrawTransparent)
                _transparentMeshes.Add(mesh);
            else
                _opaqueMeshes.Add(mesh);
        }

        protected virtual void PreRender()
        {
        }

        protected virtual void PostRender()
        {
        }

        public abstract void RenderMesh(Mesh mesh);
        public virtual IMeshUserData CreateUserData(Mesh mesh) { return null; }

        // Some change occured in the mesh which requires an update to its userdata.
        // At the moment, only used to properly set the color array.
        public virtual void UpdateUserData(Mesh mesh) { }
    }
}