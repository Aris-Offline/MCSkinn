using Inkore.UI.WPF.Modern.Controls;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MCSkinn.Dialogs
{
    /// <summary>
    /// GeneralQuestionDialog.xaml 的交互逻辑
    /// </summary>
    public partial class GeneralQuestionDialog : ContentDialog
    {
        public GeneralQuestionDialog(string title, string content, ContentDialogButton defaultButton = ContentDialogButton.Primary, string button1text = "C_YES", string button2text = "C_NO", string buttonCanceltext = null)
        {
            InitializeComponent();

            this.Title = title;
            TextBlock_Content.Text = content;

            this.PrimaryButtonText = button1text;
            this.SecondaryButtonText = button2text;

            if (!string.IsNullOrEmpty(buttonCanceltext))
                this.CloseButtonText = Program.GetLanguageString(buttonCanceltext);

            this.DefaultButton = defaultButton;
        }
    }
}
