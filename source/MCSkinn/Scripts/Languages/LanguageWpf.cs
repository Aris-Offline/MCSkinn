using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MCSkinn.Scripts.Languages
{
    public static class LanguageWpf
    {
        public static Dictionary<DependencyObject,List<LanguageWpfRegistration>> Registrations=new Dictionary<DependencyObject, List<LanguageWpfRegistration>>();

        public static void Register(DependencyObject frameworkElement, DependencyProperty dependencyProperty)
        {
            Register(frameworkElement, dependencyProperty, frameworkElement.GetValue(dependencyProperty) as string);
        }
        public static void Register(DependencyObject frameworkElement, DependencyProperty dependencyProperty, string value)
        {
            if(!Registrations.ContainsKey(frameworkElement))
            {
                Registrations.Add(frameworkElement,new List<LanguageWpfRegistration>());
            }
            LanguageWpfRegistration reg = new LanguageWpfRegistration() { Element = frameworkElement, Property = dependencyProperty, ID = value };
            Registrations[frameworkElement].Add(reg);

            if (Program.CurrentLanguage != null)
                Refresh(reg);
        }
        public static void Unegister(DependencyObject frameworkElement)
        {
            if (Registrations.ContainsKey(frameworkElement))
            {
                Registrations.Remove(frameworkElement);
            }
        }

        public static void Refresh(DependencyObject element)
        {
            if (Registrations.ContainsKey(element))
            {
                foreach (LanguageWpfRegistration r in Registrations[element])
                {
                    Refresh(r);
                }
            }
        }
        public static void Refresh(LanguageWpfRegistration reg)
        {
            reg.Element.SetValue(reg.Property, Program.GetLanguageString(reg.ID));

        }
        public static void Refresh()
        {
            foreach (DependencyObject k in Registrations.Keys)
            {
                Refresh(k);
            }
        }

        //public static DependencyProperty LinkToProperty = DependencyProperty.RegisterAttached("LinkTo", typeof(DependencyProperty), typeof(FrameworkElement), new PropertyMetadata(null, LinkToProperty_ValueChanged));

        //private static void LinkToProperty_ValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    if (!(d is FrameworkElement))
        //        return;

        //    if(e.OldValue != null)
        //    {
        //        if (Registrations.ContainsKey(d as FrameworkElement))
        //        {
        //            foreach(LanguageWpfRegistration r in Registrations[d as FrameworkElement])
        //            {
        //                if(r.Property==e.OldValue as DependencyProperty)
        //                {
        //                    Registrations[d as FrameworkElement].Remove(r);
        //                    break;
        //                }
        //            }
        //        }
        //    }

        //    Register(d as FrameworkElement, e.NewValue as DependencyProperty);
        //}

        //public static DependencyProperty GetLinkTo(FrameworkElement d)
        //{
        //    return (DependencyProperty)d.GetValue(LinkToProperty);
        //}
        //public static void SetLinkTo(FrameworkElement d, DependencyProperty linkTo)
        //{
        //    d.SetValue(LinkToProperty, linkTo);
        //}

    }

    public struct LanguageWpfRegistration
    {
        public DependencyObject Element { get; set; }

        public DependencyProperty Property { get; set; }
        public string ID { get; set; }
    }

}
