using MCSkinn.Dialogs;
using MCSkinn.Forms.Controls;
using MCSkinn.Scripts;
using iNKORE.Coreworks.Localization;
using MCSkinn.Scripts.Paril.OpenGL;
using MCSkinn.Scripts.Tools;
using iNKORE.UI.WPF.Modern.Controls;
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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Windows.Storage;
using Microsoft.AppCenter.Crashes;
using System.Windows.Media.Animation;
using iNKORE.UI.WPF.Modern.Media.Animation;

namespace MCSkinn
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Title = "MCSkinn - Version " + Program.VersionFull;

            //Program.Page_Editor.TextBlock_User.Text = Environment.UserName;

            //PageEditor.resizeTimer.Tick += ResizeTimer_Tick;
            //PageEditor.resizeTimer.Interval = TimeSpan.FromMilliseconds(100);
        }

        //private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        //{
        //    if (WindowsFormsHost_Viewport.Visibility != Visibility.Collapsed)
        //    {
        //        WindowsFormsHost_Viewport.Visibility = Visibility.Collapsed;
        //    }

        //    resizeDelay = 0;
        //    if (!resizeTimer.IsEnabled)
        //    {
        //        resizeTimer.Start();
        //    }

        //}
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Width >= System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width || Height >= System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height)
            {
                this.Width = 600;
                this.Height = 480;
                this.WindowState = WindowState.Maximized;
            }

            DoNavigate(Program.Page_Splash);
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            if (Program.Editor?.FormClosing() == true)
            {
                Program.App_Main.Shutdown(0);
            }
            else
            {
                e.Cancel = true;
            }

        }

        private void Window_Activated(object sender, EventArgs e)
        {
            if(!Program.IsNativeMicaSupported)
                TitlebarOpacityAnimation(1d);
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            if (!Program.IsNativeMicaSupported)
                TitlebarOpacityAnimation(0.45d);
        }

        public static Duration TitlebarOpacityAnimationDuration = new Duration(TimeSpan.FromMilliseconds(500));
        public bool TitlebarOpacityAnimation(double to)
        {
            FrameworkElement titleBar = (Frame_Main.Content as FrameworkElement)?.FindName("Border_AppTitlebar") as FrameworkElement;
            if (titleBar != null)
            {
                DoubleAnimation ani = new DoubleAnimation() { To = to, Duration = TitlebarOpacityAnimationDuration };
                titleBar.BeginAnimation(OpacityProperty, ani);

                return true;
            }
            else
            {
                return false;
            }
        }

        private const string NavigationConfirmation = "CONFIRM_NAVIGATE";
        public void DoNavigate(System.Windows.Controls.Page p)
        {
            Frame_Main.Navigate(p, NavigationConfirmation);
        }
        private void Frame_Main_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            if (e.ExtraData as string != NavigationConfirmation)
                e.Cancel = true;
        }

        private void Frame_Main_Navigated(object sender, NavigationEventArgs e)
        {
            if (this.IsActive)
                Window_Activated(this, e);
            else 
                Window_Deactivated(this, e);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            if (!GlobalSettings.CompatibilityMode)
            {
                iNKORE.UI.WPF.Modern.Controls.Primitives.WindowHelper.SetUseModernWindowStyle(this, true);
            }
            else
            {
                this.BorderBrush = null;
                this.BorderThickness = new Thickness(0);
                this.SetResourceReference(BackgroundProperty, iNKORE.UI.WPF.Modern.ThemeKeys.ApplicationPageBackgroundThemeBrushKey);
            }
        }
    }
}
