using MCSkinn.Forms.Controls;
using MCSkinn.Scripts;
using MCSkinn.Scripts.Setting;
using MCSkinn.Scripts.Tools;
using ModernWpf.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
        }

        private void Window_BackRequested(object sender, ModernWpf.Controls.BackRequestedEventArgs e)
        {

        }

        private void AppBarButton_Save_Click(object sender, RoutedEventArgs e)
        {
            Program.Form_Editor?.PerformSave();

        }

        public void InitializeHosts()
        {
            Program.Form_Editor?.splitContainer3.Panel2.Controls.Remove(Program.Form_Editor?.colorPanel);
            WindowsFormsHost_Pattle.Child = Program.Form_Editor?.colorPanel;

            Program.Form_Editor?.splitContainer3.Panel1.Controls.Remove(Program.Form_Editor?.treeView1);
            WindowsFormsHost_Library.Child = Program.Form_Editor?.treeView1;

            Program.Form_Editor?.splitContainer3.Panel1.Controls.Remove(Program.Form_Editor?.treeView1);
            WindowsFormsHost_Library.Child = Program.Form_Editor?.treeView1;

            Program.Form_Editor?.splitContainer3.Panel1.Controls.Remove(Program.Form_Editor?.treeView1);
            WindowsFormsHost_Library.Child = Program.Form_Editor?.treeView1;

            Program.Form_Editor?.splitContainer4.Panel2.Controls.Remove(Program.Form_Editor?.ViewportPanel);
            WindowsFormsHost_Viewport.Child = Program.Form_Editor?.ViewportPanel;

            Program.Form_Editor?.splitContainer4.Panel1.Controls.Remove(Program.Form_Editor?.ToolsPanel);
            WindowsFormsHost_Tool.Child = Program.Form_Editor?.ToolsPanel;

        }

        private void AppBarButton_SaveAll_Click(object sender, RoutedEventArgs e)
        {
            Program.Form_Editor?.PerformSaveAll();
        }

        private void Window_SourceInitialized(object sender, EventArgs e)
        {
            // 获取窗体句柄
            //IntPtr hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;

            // 获得窗体的 样式
            //long oldstyle = NativeMethods.GetWindowLong(hwnd, NativeMethods.GWL_STYLE);

            // 更改窗体的样式为无边框窗体
            //NativeMethods.SetWindowLong(hwnd, NativeMethods.GWL_STYLE, oldstyle & ~NativeMethods.WS_CAPTION);

            // SetWindowLong(hwnd, GWL_EXSTYLE, oldstyle & ~WS_EX_LAYERED);
            // 1 | 2 << 8 | 3 << 16  r=1,g=2,b=3 详见winuse.h文件
            // 设置窗体为透明窗体
            //NativeMethods.SetLayeredWindowAttributes(hwnd, 255, 0, NativeMethods.LWA_ALPHA);

            //// 创建圆角窗体  12 这个值可以根据自身项目进行设置
            //NativeMethods.SetWindowRgn(hwnd, NativeMethods.CreateRoundRectRgn(0, 0, Convert.ToInt32(this.ActualWidth), Convert.ToInt32(this.ActualHeight), 12, 12), true);
        }

        private void AppBarToggleButton_Viewport_View_Click(object sender, RoutedEventArgs e)
        {
            AppBarToggleButton_Viewport_View.IsChecked = false;
            AppBarToggleButton_Viewport_Pen.IsChecked = false;
            AppBarToggleButton_Viewport_Eraser.IsChecked = false;
            AppBarToggleButton_Viewport_Dropper.IsChecked = false;
            AppBarToggleButton_Viewport_Doge.IsChecked = false;
            AppBarToggleButton_Viewport_Darken.IsChecked = false;
            AppBarToggleButton_Viewport_Fill.IsChecked = false;
            AppBarToggleButton_Viewport_Noise.IsChecked = false;
            AppBarToggleButton_Viewport_Stamp.IsChecked = false;


            switch((sender as AppBarToggleButton)?.Tag as string)
            {
                case "VIEW":
                    Program.Form_Editor?.SetSelectedTool(Program.Form_Editor?.ToolCamera);
                    AppBarToggleButton_Viewport_View.IsChecked = true;
                    break;
                case "PEN":
                    Program.Form_Editor?.SetSelectedTool(Program.Form_Editor?.ToolPencil);
                    AppBarToggleButton_Viewport_Pen.IsChecked = true;
                    break;
                case "ERASER":
                    Program.Form_Editor?.SetSelectedTool(Program.Form_Editor?.ToolEraser);
                    AppBarToggleButton_Viewport_Eraser.IsChecked = true;
                    break;
                case "DROPPER":
                    Program.Form_Editor?.SetSelectedTool(Program.Form_Editor?.ToolDropper);
                    AppBarToggleButton_Viewport_Dropper.IsChecked = true;
                    break;
                case "DODGE":
                    Program.Form_Editor?.SetSelectedTool(Program.Form_Editor?.ToolDodge);
                    AppBarToggleButton_Viewport_Doge.IsChecked = true;
                    break;
                case "DARKEN":
                    Program.Form_Editor?.SetSelectedTool(Program.Form_Editor?.ToolDarken);
                    AppBarToggleButton_Viewport_Darken.IsChecked = true;
                    break;
                case "FILL":
                    Program.Form_Editor?.SetSelectedTool(Program.Form_Editor?.ToolFill);
                    AppBarToggleButton_Viewport_Fill.IsChecked = true;
                    break;
                case "NOISE":
                    Program.Form_Editor?.SetSelectedTool(Program.Form_Editor?.ToolNoise);
                    AppBarToggleButton_Viewport_Noise.IsChecked = true;
                    break;
                case "STAMP":
                    Program.Form_Editor?.SetSelectedTool(Program.Form_Editor?.ToolStamp);
                    AppBarToggleButton_Viewport_Stamp.IsChecked = true;
                    break;
            }

        }

        private void TabControl_Viewport_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Program.Form_Editor?.Renderer == null)
            {
                e.Handled = true;
                return;
            }

            try
            {
                switch (TabControl_Viewport.SelectedIndex)
                {
                    case 0:
                        Program.Form_Editor?.SetViewMode(ViewMode.Perspective);
                        break;
                    case 1:
                        Program.Form_Editor?.SetViewMode(ViewMode.Orthographic);
                        break;
                    case 2:
                        Program.Form_Editor?.SetViewMode(ViewMode.Hybrid);
                        break;
                }
            }
            catch
            {
                e.Handled = true;
            }
        }


        private void TabControl_Pattle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Program.Form_Editor.colorPanel.Selection = TabControl_Pattle.SelectedIndex;
        }

        private void Button_LibraryToolbox_ZoomIn_Click(object sender, RoutedEventArgs e)
        {
            Program.Form_Editor?.PerformTreeViewZoomIn();
        }

        private void Button_LibraryToolbox_ZoomOut_Click(object sender, RoutedEventArgs e)
        {
            Program.Form_Editor?.PerformTreeViewZoomOut();

        }

        private void Button_LibraryToolbox_Import_Click(object sender, RoutedEventArgs e)
        {
            Program.Form_Editor?.PerformImportSkin();

        }

        private void Button_LibraryToolbox_New_Click(object sender, RoutedEventArgs e)
        {
            Program.Form_Editor?.PerformNewSkin();

        }

        private void Button_LibraryToolbox_NewFolder_Click(object sender, RoutedEventArgs e)
        {
            Program.Form_Editor?.PerformNewFolder();

        }

        private void MenuItem_LibraryFlyout_Rename_Click(object sender, RoutedEventArgs e)
        {
            Program.Form_Editor?.PerformNameChange();
        }

        private void MenuItem_LibraryFlyout_Delete_Click(object sender, RoutedEventArgs e)
        {
            Program.Form_Editor?.PerformDeleteSkin();

        }

        private void MenuItem_LibraryFlyout_ResolutionD_Click(object sender, RoutedEventArgs e)
        {
            Program.Form_Editor?.PerformDecreaseResolution();

        }

        private void MenuItem_LibraryFlyout_ResolutionI_Click(object sender, RoutedEventArgs e)
        {
            Program.Form_Editor?.PerformIncreaseResolution();

        }

        private void MenuItem_LibraryFlyout_Clone_Click(object sender, RoutedEventArgs e)
        {
            Program.Form_Editor?.PerformCloneSkin();

        }

        private void AppBarButton_Undo_Click(object sender, RoutedEventArgs e)
        {
            Program.Form_Editor?.PerformUndo();

        }

        private void AppBarButton_Redo_Click(object sender, RoutedEventArgs e)
        {
            Program.Form_Editor?.PerformRedo();

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_File_SaveAs_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_File_Exit_Click(object sender, RoutedEventArgs e)
        {
            Exit();

        }
        public void Exit()
        {
            if (Program.Form_Editor?.FormClosing() == true)
            {
                Program.App_Main.Shutdown(0);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Program.Form_Editor?.FormClosing() == true)
            {
                Program.App_Main.Shutdown(0);
            }
            else
            {
                e.Cancel = true;
            }

        }

        private void AppBarButton_Viewport_ResetCamera_Click(object sender, RoutedEventArgs e)
        {
            Program.Form_Editor?.PerformResetCamera();

        }

        private void AppBarButton_Viewport_Screenshot_Click(object sender, RoutedEventArgs e)
        {
            if ((System.Windows.Forms.Control.ModifierKeys & Keys.Shift) != 0)
                Program.Form_Editor?.SaveScreenshot();
            else
                Program.Form_Editor?.TakeScreenshot();
        }
    }
}
