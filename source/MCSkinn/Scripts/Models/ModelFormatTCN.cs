using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;
using ICSharpCode.SharpZipLib.Zip;
using MCSkinn.Scripts.Paril.OpenGL;
using OpenTK;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection.Metadata;
using System.Xml.Linq;
using Windows.ApplicationModel.Appointments.DataProvider;
using System.Windows.Media;
using Inkore.Coreworks.Helpers;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;

namespace MCSkinn.Scripts.Models
{
    public class TCNRenderBase
    {
        public bool IsMirrored;
        public Vector3 Offset;
        public ModelPart Part;
        public Vector3 Position;
        public Vector3 Rotation;
        public float Scale;
        public Vector3 Size;
        public Vector2 TextureOffset;

        public virtual void Parse(XmlNode node)
        {
            foreach (XmlNode child in node.ChildNodes)
            {
                string name = child.Name.ToLower();

                if (name == "animation")
                    continue; // skip animation
                else if (name == "ismirrored")
                    IsMirrored = bool.Parse(child.InnerText);
                else if (name == "offset")
                    Offset = Mesh.StringToVertex3(child.InnerText);
                else if (name == "position")
                    Position = Mesh.StringToVertex3(child.InnerText);
                else if (name == "rotation")
                    Rotation = Mesh.StringToVertex3(child.InnerText);
                else if (name == "size")
                    Size = Mesh.StringToVertex3(child.InnerText);
                else if (name == "scale")
                    Scale = float.Parse(child.InnerText, CultureInfo.InvariantCulture.NumberFormat);
                else if (name == "textureoffset")
                    TextureOffset = Mesh.StringToVertex2(child.InnerText);
                else if (name == "part")
                    Part = (ModelPart)Enum.Parse(typeof(ModelPart), child.InnerText);
            }
        }
    }

    internal class TCNRenderBox : TCNRenderBase
    {
        public bool IsDecorative;
        public bool IsFixed;
        public bool IsSolid = false;
        public bool IsSurface = false;
        public bool IsArmor = false;

        public override void Parse(XmlNode node)
        {
            base.Parse(node);

            foreach (XmlNode child in node.ChildNodes)
            {
                string name = child.Name.ToLower();

                if (name == "isdecorative")
                    IsDecorative = bool.Parse(child.InnerText);
                else if (name == "isfixed")
                    IsFixed = bool.Parse(child.InnerText);
                else if (name == "issolid")
                    IsSolid = bool.Parse(child.InnerText);
                else if (name == "isarmor")
                    IsArmor = bool.Parse(child.InnerText);
                else if (name == "issurface")
                    IsSurface = bool.Parse(child.InnerText);
            }
        }
    }

    public class TCNRenderPlane : TCNRenderBase
    {
        public int Side;

        public override void Parse(XmlNode node)
        {
            base.Parse(node);

            foreach (XmlNode child in node.ChildNodes)
            {
                string name = child.Name.ToLower();

                if (name == "side")
                    Side = int.Parse(child.InnerText);
            }
        }
    }

    public class TCNShape
    {
        private static readonly Dictionary<string, Type> _guidMap = new Dictionary<string, Type>();
        public TCNRenderBase RenderData;
        public string name;
        public string type;

        static TCNShape()
        {
            _guidMap.Add("d9e621f7-957f-4b77-b1ae-20dcd0da7751", typeof(TCNRenderBox));
            _guidMap.Add("ab894c83-e399-4236-808b-25a78d56f5e1", typeof(TCNRenderPlane));
        }

        public void Parse(XmlNode node)
        {
            foreach (XmlAttribute a in node.Attributes)
            {
                if (a.Name.ToLower() == "type")
                    type = a.Value;
                else if (a.Name.ToLower() == "name")
                    name = a.Value;
            }

            Type renderType = null;

            if (_guidMap.TryGetValue(type, out renderType))
            {
                RenderData = (TCNRenderBase)renderType.GetConstructors()[0].Invoke(null);
                RenderData.Parse(node);
            }
        }
    }

    public class TCNFolder
    {
        public string Name;

        public Vector3 Pivot = new Vector3(0, 0, 0);
        public Vector3 BindPoseRotation = new Vector3(0, 0, 0);


        public ObservableCollection<TCNShape> Shapes = new ObservableCollection<TCNShape>();
        public ObservableCollection<TCNFolder> Subfolders = new ObservableCollection<TCNFolder>();

        public string type;

        public TCNFolder Parent;

        public string ParentName
        {
            get => Parent == null ? "" : Parent.Name;
        }

        public void Parse(XmlNode node)
        {
            foreach (XmlAttribute a in node.Attributes)
            {
                if (a.Name.ToLower() == "type")
                    type = a.Value;
                else if (a.Name.ToLower() == "name")
                    Name = a.Value;
            }

            foreach (XmlNode child in node.ChildNodes)
            {
                string name = child.Name.ToLower();

                if (name == "shape")
                {
                    var shape = new TCNShape();
                    shape.Parse(child);
                    Shapes.Add(shape);
                }
            }

        }

        public override string ToString()
        {
            return Name + string.Format(" ({0} shapes, {1} folders)", Shapes.Count, Subfolders.Count);
        }
    }

    public class TCNModel
    {
        public string BaseClass;

        public List<TCNFolder> Geometry = new List<TCNFolder>();

        public string Name;
        public Vector2 TextureSize = new Vector2();
        public string DefaultTexture;

        public bool IsTCNFile;

        public void Parse(XmlNode node)
        {
            IsTCNFile = true;

            foreach (XmlNode child in node.ChildNodes)
            {
                string name = child.Name.ToLower();

                if (name == "baseclass")
                    BaseClass = child.InnerText;
                else if (name == "geometry")
                {
                    TCNFolder rootFolder = new TCNFolder();

                    foreach (XmlNode folderChild in child.ChildNodes)
                    {
                        if (folderChild.Name.ToLower() == "folder")
                        {
                            var folder = new TCNFolder();
                            folder.Parse(folderChild);
                            Geometry.Add(folder);
                        }
                        else if (folderChild.Name.ToLower() == "shape")
                        {
                            var shape = new TCNShape();
                            shape.Parse(folderChild);
                            rootFolder.Shapes.Add(shape);
                        }
                    }

                    if (rootFolder.Shapes.Count != 0)
                        Geometry.Add(rootFolder);
                }
                else if (name == "name")
                    Name = child.InnerText;
                else if (name == "texturesize")
                    TextureSize = Mesh.StringToVertex2(child.InnerText);
                else if (name == "defaulttexture")
                    DefaultTexture = child.InnerText;
            }
        }

        public void Parse(JToken t_bones)
        {
            IsTCNFile = false;

            if(t_bones.Type == JTokenType.Property)
            {
                JProperty p_bones = (JProperty)t_bones;

                Dictionary<string, TCNFolder> folders = new Dictionary<string, TCNFolder>();
                Dictionary<TCNFolder, string> folderParentNames = new Dictionary<TCNFolder, string>();


                foreach (JToken t_bonesChild in p_bones.First.Children())
                {
                    if(t_bonesChild is JObject)
                    {
                        JObject o_bonesChild= (JObject)t_bonesChild;
                        TCNFolder folder = new TCNFolder();
                  
                        foreach (JProperty p_objProp in o_bonesChild.Properties())
                        {

                            if (p_objProp.Name.ToLower() == "name")
                            {
                                folder.Name = ((JValue)p_objProp.Value).Value as string;
                            }
                            else if (p_objProp.Name.ToLower() == "pivot")
                            {
                                if (p_objProp.Value.Type == JTokenType.Array)
                                {
                                    JArray a_pivot = (JArray)p_objProp.Value;
                                    folder.Pivot.X = (float)a_pivot[0];
                                    folder.Pivot.Y = (float)a_pivot[1];
                                    folder.Pivot.Z = (float)a_pivot[2];
                                }                                
                            }
                            else if (p_objProp.Name.ToLower() == "parent")
                            {
                                if (!folderParentNames.ContainsKey(folder))
                                    folderParentNames.Add(folder, ((JValue)p_objProp.Value).Value as string);
                            }
                            else if (p_objProp.Name.ToLower() == "bind_pose_rotation")
                            {
                                if (p_objProp.Value.Type == JTokenType.Array)
                                {
                                    JArray a_bindposerotation = (JArray)p_objProp.Value;
                                    folder.BindPoseRotation.X = (float)a_bindposerotation[0];
                                    folder.BindPoseRotation.Y = (float)a_bindposerotation[1];
                                    folder.BindPoseRotation.Z = (float)a_bindposerotation[2];
                                }

                            }
                            else if (p_objProp.Name.ToLower() == "cubes")
                            {
                                if (p_objProp.Value.Type == JTokenType.Array)
                                {
                                    JArray a_pivot = (JArray)p_objProp.Value;
                                    
                                    foreach(JObject o_cube in a_pivot)
                                    {
                                        TCNShape shape = new TCNShape();
                                        shape.RenderData = new TCNRenderBox();

                                        foreach(JProperty p_cubeChild in o_cube.Properties())
                                        {
                                            switch(p_cubeChild.Name.ToLower())
                                            {
                                                case "origin":
                                                    if (p_cubeChild.Value.Type == JTokenType.Array)
                                                    {
                                                        JArray a_origin = (JArray)p_cubeChild.Value;
                                                        shape.RenderData.Position.X = (float)a_origin[0];
                                                        shape.RenderData.Position.Y = -(float)a_origin[1];
                                                        shape.RenderData.Position.Z = (float)a_origin[2];
                                                    }
                                                    break;
                                                case "size":
                                                    if (p_cubeChild.Value.Type == JTokenType.Array)
                                                    {
                                                        JArray a_origin = (JArray)p_cubeChild.Value;
                                                        shape.RenderData.Size.X = (float)a_origin[0];
                                                        shape.RenderData.Size.Y = (float)a_origin[1];
                                                        shape.RenderData.Size.Z = (float)a_origin[2];
                                                    }
                                                    break;
                                                case "uv":
                                                    if (p_cubeChild.Value.Type == JTokenType.Array)
                                                    {
                                                        JArray a_origin = (JArray)p_cubeChild.Value;
                                                        shape.RenderData.TextureOffset.X = (float)a_origin[0];
                                                        shape.RenderData.TextureOffset.Y = (float)a_origin[1];
                                                    }
                                                    break;
                                                case "inflate":
                                                    shape.RenderData.Scale = (float)Inkore.Coreworks.Helpers.TypeHelper.ToDouble(((JValue)p_cubeChild.Value).Value);
                                                    break;
                                                case "mirror":
                                                    if (((JValue)p_cubeChild.Value).Value is bool)
                                                        shape.RenderData.IsMirrored = (bool)((JValue)p_cubeChild.Value).Value;
                                                    else if(((JValue)p_cubeChild.Value).Value is string)
                                                        shape.RenderData.IsMirrored = Convert.ToBoolean(((JValue)p_cubeChild.Value).Value);
                                                    break;
                                            }
                                        }

                                        //shape.RenderData.Rotation = folder.BindPoseRotation;
                                        shape.RenderData.Position.Y = shape.RenderData.Position.Y - shape.RenderData.Size.Y;
                                        folder.Shapes.Add(shape);
                                    }
                                }
                            }

                        }

                        while (folders.ContainsKey(folder.Name))
                        {
                            folder.Name = folder.Name + "_2";
                        }
                        folders.Add(folder.Name, folder);
                    }
                }


                foreach (KeyValuePair<string, TCNFolder> pair in folders)
                {
                    if (folderParentNames.ContainsKey(pair.Value) && folders.ContainsKey(folderParentNames[pair.Value]))
                    {
                        folders[folderParentNames[pair.Value]].Subfolders.Add(pair.Value);
                        pair.Value.Parent = folders[folderParentNames[pair.Value]];
                    }
                    else
                    {
                        Geometry.Add(pair.Value);
                    }
                }
            }
        }

    }

    internal class TCNFile
    {
        public string Author;
        public string DateCreated;
        public string Description;

        public List<TCNModel> Models = new List<TCNModel>();

        public string Name;
        public string PreviewImage;
        public string ProjectName;
        public string ProjectType;
        public string Version;

        public string DisplayName;
        public bool IsTCNFile = false;


        public void Parse(XmlNode node)
        {
            if (node.Name != "Techne")
                return;

            IsTCNFile = true;

            foreach (XmlAttribute a in node.Attributes)
            {
                if (a.Name.ToLower() == "version")
                    Version = a.Value;
            }

            foreach (XmlNode child in node.ChildNodes)
            {
                string name = child.Name.ToLower();

                if (name == "author")
                    Author = child.InnerText;
                else if (name == "identifier")
                    Name = child.InnerText;
                else if (name == "datecreated")
                    DateCreated = child.InnerText;
                else if (name == "description")
                    Description = child.InnerText;
                else if (name == "models")
                {
                    foreach (XmlNode modelChild in child.ChildNodes)
                    {
                        if (modelChild.Name.ToLower() == "model")
                        {
                            var model = new TCNModel();
                            model.Parse(modelChild);
                            Models.Add(model);
                        }
                    }
                }
                else if (name == "previewimage")
                    PreviewImage = child.InnerText;
                else if (name == "projectname")
                    ProjectName = child.InnerText;
                else if (name == "projecttype")
                    ProjectType = child.InnerText;
                else if (name == "displayname")
                    DisplayName = child.InnerText;
            }
        }

        public void Parse(JObject jroot)
        {
            IsTCNFile = false;

            foreach (KeyValuePair<string, JToken> pairRoot in jroot)
            {
                if(pairRoot.Key.ToLower()== "format_version")
                {
                    Version= pairRoot.Value.ToString();
                }
                if (pairRoot.Key.ToLower() == "display_name")
                {
                    DisplayName = pairRoot.Value.ToString();
                }
                else if (pairRoot.Key.ToLower() == "minecraft:geometry")
                {
                    var tcnModel = new TCNModel();

                    foreach (JToken t_geometryChild in pairRoot.Value.Values())
                    {
                        if (t_geometryChild is JProperty)
                        {
                            JProperty p_geometryChild = t_geometryChild as JProperty;

                            if (p_geometryChild.Name.ToLower() == "description")
                            {
                                foreach (JToken t_DescriptionChild in p_geometryChild.First.Children())
                                {
                                    if (t_DescriptionChild is JProperty)
                                    {
                                        JProperty p_DescriptionChild = t_DescriptionChild as JProperty;

                                        switch (p_DescriptionChild.Name.ToLower())
                                        {
                                            case "identifier":
                                                tcnModel.Name =  Name = (p_DescriptionChild.Value as JValue)?.Value as string;
                                                break;
                                            case "texture_width":
                                                tcnModel.TextureSize.X = Inkore.Coreworks.Helpers.TypeHelper.ToInt32((p_DescriptionChild.Value as JValue)?.Value);
                                                break;
                                            case "texture_height":
                                                tcnModel.TextureSize.Y = Inkore.Coreworks.Helpers.TypeHelper.ToInt32((p_DescriptionChild.Value as JValue)?.Value);
                                                break;
                                            case "visible_bounds_offset":
                                                break;
                                            case "visible_bounds_width":
                                                break;
                                        }
                                    }
                                }

                            }
                            else if (p_geometryChild.Name == "bones")
                            {
                                tcnModel.Parse(p_geometryChild);
                            }
                        }
                    }

                    Models.Add(tcnModel);

                }
                else if (pairRoot.Key.ToLower().StartsWith("geometry."))
                {
                    var tcnModel = new TCNModel();

                    tcnModel.Name = Name = pairRoot.Key;


                    foreach (JToken t_geometryChild in pairRoot.Value.Children())
                    {


                        if (t_geometryChild is JProperty)
                        {
                            JProperty p_geometryChild = (JProperty)t_geometryChild;

                            switch (p_geometryChild.Name.ToLower())
                            {
                                case "texturewidth":
                                    tcnModel.TextureSize.X = Inkore.Coreworks.Helpers.TypeHelper.ToInt32((p_geometryChild.Value as JValue)?.Value);
                                    break;
                                case "textureheight":
                                    tcnModel.TextureSize.Y = Inkore.Coreworks.Helpers.TypeHelper.ToInt32((p_geometryChild.Value as JValue)?.Value);
                                    break;
                                case "visible_bounds_height":
                                    break;
                                case "visible_bounds_width":
                                    break;
                            }
                        }

                        if (t_geometryChild is JProperty)
                        {
                            JProperty p_geometryChild = t_geometryChild as JProperty;

                            if (p_geometryChild.Name.ToLower() == "description")
                            {
                                foreach (JToken t_DescriptionChild in p_geometryChild.First.Children())
                                {
                                    if (t_DescriptionChild is JProperty)
                                    {
                                        JProperty p_DescriptionChild = t_DescriptionChild as JProperty;

                                        switch (p_DescriptionChild.Name.ToLower())
                                        {
                                            case "identifier":
                                                Name = (p_DescriptionChild.Value as JValue)?.Value as string;
                                                break;
                                            case "texture_width":
                                                tcnModel.TextureSize.X = Inkore.Coreworks.Helpers.TypeHelper.ToInt32((p_DescriptionChild.Value as JValue)?.Value);
                                                break;
                                            case "texture_height":
                                                tcnModel.TextureSize.Y = Inkore.Coreworks.Helpers.TypeHelper.ToInt32((p_DescriptionChild.Value as JValue)?.Value);
                                                break;
                                            case "bones":
                                                tcnModel.Parse(p_DescriptionChild.Value);
                                                break;
                                            case "visible_bounds_width":
                                                break;
                                        }
                                    }
                                }

                            }
                            else if (p_geometryChild.Name == "bones")
                            {
                                tcnModel.Parse(p_geometryChild);
                            }
                        }
                    }

                    Models.Add(tcnModel);

                }
            }


            //foreach (XmlNode child in node.ChildNodes)
            //{
            //    string name = child.Name.ToLower();

            //    if (name == "author")
            //        Author = child.InnerText;
            //    else if (name == "datecreated")
            //        DateCreated = child.InnerText;
            //    else if (name == "description")
            //        Description = child.InnerText;
            //    else if (name == "models")
            //    {
            //        foreach (XmlNode modelChild in child.ChildNodes)
            //        {
            //            if (modelChild.Name.ToLower() == "model")
            //            {
            //                var model = new TCNModel();
            //                model.Parse(modelChild);
            //                Models.Add(model);
            //            }
            //        }
            //    }
            //    else if (name == "name")
            //        Name = child.InnerText;
            //    else if (name == "previewimage")
            //        PreviewImage = child.InnerText;
            //    else if (name == "projectname")
            //        ProjectName = child.InnerText;
            //    else if (name == "projecttype")
            //        ProjectType = child.InnerText;
            //}
        }


        public Model Convert()
        {

            var mb = new ModelLoader.ModelBase();

            foreach (TCNModel x in Models)
            {
                foreach (TCNFolder folder in x.Geometry)
                {
                    ConvertFolder(mb, folder, x);
                }
            }

            var model = mb.Compile(Name, 1, (int)Models[0].TextureSize.X, (int)Models[0].TextureSize.Y, Models[0].DefaultTexture, DisplayName, IsTCNFile);
            return model;
        }

        private void ConvertFolder(ModelLoader.ModelBase mb, TCNFolder folder, TCNModel x)
        {
            foreach (TCNShape z in folder.Shapes)
            {
                mb.textureWidth = (int)x.TextureSize.X;
                mb.textureHeight = (int)x.TextureSize.Y;

                if (z.RenderData == null)
                    continue;

                if (z.RenderData is TCNRenderBox)
                {
                    var box = (TCNRenderBox)z.RenderData;

                    var renderer = new ModelLoader.ModelRenderer(folder.Name, folder.ParentName, mb, (int)box.TextureOffset.X, (int)box.TextureOffset.Y);
                    renderer.part = box.Part;
                    renderer.addBox(box.Offset.X, box.Offset.Y, box.Offset.Z, (int)box.Size.X, (int)box.Size.Y,
                                (int)box.Size.Z, box.IsMirrored, box.Scale, z.name, box.IsSurface);
                    renderer.setRotationPoint(box.Position.X, box.Position.Y, box.Position.Z);
                    renderer.rotateAngleX = MathHelper.DegreesToRadians(box.Rotation.X);
                    renderer.rotateAngleY = MathHelper.DegreesToRadians(box.Rotation.Y);
                    renderer.rotateAngleZ = MathHelper.DegreesToRadians(box.Rotation.Z);
                    renderer.isSolid = box.IsSolid;
                    renderer.isArmor = box.IsArmor;
                }
            }

            foreach(TCNFolder subfolder in folder.Subfolders)
            {
                ConvertFolder(mb, subfolder, x);
            }
        }
    }

    public class ModelFormatTCN : IModelFormat
    {
        #region IModelFormat Members

        public Model Load(string fileName)
        {
            XmlDocument docXml = null;
            JObject docJson;

            var tcnModel = new TCNFile();

            if (fileName.EndsWith(".tcn"))
            {
                var file = new ZipFile(new FileStream(fileName, FileMode.Open, FileAccess.Read));

                var enumerator = file.GetEnumerator();

                while (enumerator.MoveNext())
                {
                    if (((ZipEntry)enumerator.Current).Name.EndsWith(".xml"))
                    {
                        docXml = new XmlDocument();
                        docXml.Load(file.GetInputStream((ZipEntry)enumerator.Current));
                       
                        tcnModel.Parse(docXml.DocumentElement);

                        break;
                    }
                }
            }
            else if (fileName.EndsWith(".xml"))
            {
                docXml = new XmlDocument();
                docXml.Load(fileName);
                tcnModel.Parse(docXml.DocumentElement);
            }
            else if (fileName.EndsWith(".json"))
            {
                docJson=JObject.Parse(File.ReadAllText(fileName));
                tcnModel.Parse(docJson);
            }
            //else
            //throw new FileLoadException();


            if(tcnModel.Models.Count == 0)
            {
                return null;
            }

            return tcnModel.Convert();
        }

        #endregion
    }
}