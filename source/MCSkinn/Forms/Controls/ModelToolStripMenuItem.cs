using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using MCSkinn.Scripts.Paril.OpenGL;

namespace MCSkinn.Forms.Controls
{
    public class ModelToolStripMenuItem : MenuItem
    {
        public Model Model;
        public bool IsMain;
        public Action<Model> Callback;
        public MenuItem ParentItem;
        static ModelToolStripMenuItem()
        {
        }

        public ModelToolStripMenuItem(Model model, bool main, Action<Model> callback, string prefix, MenuItem parent) :
            base()
        {
            Model = model;
            Name = GetName(model.Name, prefix);
            IsMain = main;
            Header = Model.DisplayName;
            Callback = callback;
            ParentItem = parent;         
            IsCheckable = false;

            Tag = model;

            FontWeight = FontWeights.Normal;

            try
            {
                this.Style = App.Current.FindResource("DefaultMenuItemStyle") as Style;
            }
            catch (Exception ex)
            {
                Program.Log(ex, false);
            }

            if (IsMain)
                Model.DropDownItem = this;
        }

        protected override void OnClick ()
        {
            {
               Callback(Model);
                base.OnClick();
            }
        }

        public static string GetName(string model, string prefix)
        {
            return prefix + GetNumberAlpha(model);
        }
        public static string GetNumberAlpha(string source)
        {
            string pattern = "[A-Za-z0-9]";
            string strRet = "";
            MatchCollection results = Regex.Matches(source, pattern);
            foreach (var v in results)
            {
                strRet += v.ToString();
            }
            return strRet;
        }
    }
}
