//
//    MCSkinn, A modern Minecraft 3D skin manager/editor for Windows by NotYoojun.!
//    Copyright © iNKORE! 2023
//
//    The copy of source (only the public part) can be used anywhere with a credit to MCSkinn page at your own risk
//    https://github.com/InkoreStudios/MCSkinn
//

using Inkore.Common;
using Inkore.Coreworks.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Xml.Linq;

namespace MCSkinn.Scripts.Paril.Settings
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class SavableAttribute : Attribute
    {
        public string Group { get; set; }
        public SavableAttribute(string group = "")
        {
            Group = group;
        }
    }

    public interface ISavable
    {
        string SaveHeader { get; }
    }

    public interface ITypeSerializer
    {
        string Serialize(object obj);
        object Deserialize(string str);
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class TypeSerializerAttribute : Attribute
    {
        public readonly bool DeserializeDefault;
        public readonly string TypeName;

        public TypeSerializerAttribute(Type type, bool deserializeDefault)
        {
            TypeName = type.FullName;
            DeserializeDefault = deserializeDefault;
        }
    }

    public class StringSerializer
    {
        public virtual string Serialize(object field, object obj)
        {
            try
            {
                if (field is PropertyInfo)
                {
                    var info = (PropertyInfo)field;

                    if (info.GetCustomAttributes(typeof(TypeSerializerAttribute), false).Length != 0)
                    {
                        var converter = (TypeSerializerAttribute)info.GetCustomAttributes(typeof(TypeSerializerAttribute), false)[0];
                        Type type = Type.GetType(converter.TypeName);

                        if (type != null)
                        {
                            var conv = (ITypeSerializer)type.GetConstructors()[0].Invoke(null);
                            return conv.Serialize(obj);
                        }
                    }
                }

                if (obj == null)
                    return "";

                return TypeDescriptor.GetConverter(obj.GetType()).ConvertToString(obj);
            }
            catch (Exception ex)
            {
                if (field == null)
                    throw new Exception("Wat, field is null");
                else
                {
                    throw new Exception(
                        "Failed to serialize member " + ((MemberInfo)field).Name + " [" + ((MemberInfo)field).MemberType.ToString() +
                        "]", ex);
                }
            }
        }

        public virtual object Deserialize(object field, string str, Type t)
        {
            if (field is PropertyInfo)
            {
                var info = (PropertyInfo)field;

                if (info.GetCustomAttributes(typeof(TypeSerializerAttribute), false).Length != 0)
                {
                    var converter = (TypeSerializerAttribute)info.GetCustomAttributes(typeof(TypeSerializerAttribute), false)[0];
                    Type type = Type.GetType(converter.TypeName);

                    if (type != null)
                    {
                        var conv = (ITypeSerializer)type.GetConstructors()[0].Invoke(null);
                        return conv.Deserialize(str);
                    }
                }
            }

            return TypeDescriptor.GetConverter(t).ConvertFromString(str);
        }
    }

    public class StringArraySerializer : ITypeSerializer
    {
        #region ITypeSerializer Members

        public string Serialize(object obj)
        {
            var arr = (string[])obj;
            string combined = "";

            foreach (string c in arr)
            {
                if (combined == "")
                    combined += c;
                else
                    combined += ";" + c;
            }

            return combined;
        }

        public object Deserialize(string str)
        {
            return str.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
        }

        #endregion
    }

    public class Settings
    {
        private readonly StringSerializer _serializer = new StringSerializer();

        public XElement Save(Type type)
        {
            //var writer = new StreamWriter(fileName);

            XElement root = new XElement("Settings");
            Dictionary<string, XElement> groups = new Dictionary<string, XElement>();

            foreach (PropertyInfo prop in type.GetProperties(BindingFlags.Static | BindingFlags.Public))
            {
                object[] attrs = prop.GetCustomAttributes(typeof(SavableAttribute), false);
                if (attrs != null && attrs.Length > 0 && attrs[0] is SavableAttribute)
                {
                    SavableAttribute attr = (SavableAttribute)attrs[0];
                    string str = _serializer.Serialize(prop, prop.GetValue(null, null));
                    string group = attr.Group.IsNullOrEmptyOrWhitespace() ? "Global" : attr.Group;

                    if (!groups.ContainsKey(group))
                    {
                        XElement ele = new XElement(group);
                        groups.Add(group, ele);
                        root.Add(ele);
                    }

                    groups[group].Add(new XElement(prop.Name, str));
                    //writer.WriteLine(prop.Name + "=" + str);
                }
            }

            return root;
        }

        public void LoadDefaults(Type type)
        {
            foreach (PropertyInfo prop in type.GetProperties(BindingFlags.Static | BindingFlags.Public))
            {
                object[] attribs = prop.GetCustomAttributes(typeof(DefaultValueAttribute), false);
                if (attribs.Length != 0)
                {
                    var dva = (DefaultValueAttribute)attribs[0];

                    object[] converters = prop.GetCustomAttributes(typeof(TypeSerializerAttribute), false);

                    if (converters.Length != 0)
                    {
                        var serialize = (TypeSerializerAttribute)converters[0];

                        if (serialize.DeserializeDefault)
                            prop.SetValue(null, _serializer.Deserialize(prop, dva.Value.ToString(), prop.PropertyType), null);
                        else
                            prop.SetValue(null, dva.Value, null);
                    }
                    else
                        prop.SetValue(null, dva.Value, null);
                }
            }
        }

        public void Load(XElement root, Type type)
        {
            Dictionary<string, PropertyInfo> props = new Dictionary<string, PropertyInfo>();

            foreach (PropertyInfo prop in type.GetProperties(BindingFlags.Static | BindingFlags.Public))
            {
                if (prop.GetCustomAttributes(typeof(SavableAttribute), false).Length != 0)
                {
                    props.Add(prop.Name, prop);
                }
            }


            foreach (XElement e1 in root.Elements())
            {
                foreach(XElement e2 in e1.Elements())
                {
                    try
                    {
                        if (props.ContainsKey(e2.Name.LocalName))
                        {
                            PropertyInfo prop = props[e2.Name.LocalName];
                            object val = _serializer.Deserialize(prop, e2.Value, prop.PropertyType);
                            prop.SetValue(null, val, null);

                            Program.Log(LogType.Load, string.Format("Loaded setting '{0}' ('{1}')", prop.Name, e2.Value), "at MCSkinn.Scripts.Paril.Settings.Settings.Load(string)");
                        }
                    }
                    catch (Exception ex) { Program.Log(ex, false); }
                }
            }

            props = null;
        }
    }
}