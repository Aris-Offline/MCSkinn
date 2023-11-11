using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MCSkinn.Dialogs
{
    /// <summary>
    /// IncompleteInstallationDialog.xaml 的交互逻辑
    /// </summary>
    public partial class IncompleteInstallationDialog : Window
    {
        public IncompleteInstallationDialog()
        {
            InitializeComponent();

            switch(CultureInfo.CurrentUICulture.Name.ToLower())
            {
                case "zh-cn":
                case "zh-tw":
                case "zh-hk":
                    TabControl_Infos.SelectedItem = TabItem_Infos_ZH;
                    break;

                default:
                    TabControl_Infos.SelectedItem = TabItem_Infos_EN;
                    break;
            }
        }

        private void Button_Dismiss_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_MoreInfo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Hyperlink hl)
                {
                    Process.Start(new ProcessStartInfo(hl.NavigateUri.ToString()) { UseShellExecute = true });
                }
            }
            catch(Exception ex)
            {
                Program.RaiseException(ex);
            }
        }
    }
}
