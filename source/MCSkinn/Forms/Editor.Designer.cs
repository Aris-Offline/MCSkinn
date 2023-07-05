using MCSkinn.Forms.Controls;
using Inkore.Coreworks.Localization;
using MCSkinn.Scripts.lemon42.Colors;
using MCSkinn.Scripts.Paril.Controls;
using System.Windows.Forms;

namespace MCSkinn
{
    partial class Editor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            ColorManager colorManager3 = new ColorManager();
            ColorManager colorManager4 = new ColorManager();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Editor));
            mainMenuStrip = new NativeMenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            saveAsToolStripMenuItem = new ToolStripMenuItem();
            saveAllToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator5 = new ToolStripSeparator();
            exitToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            undoToolStripMenuItem = new ToolStripMenuItem();
            redoToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator7 = new ToolStripSeparator();
            toolToolStripMenuItem = new ToolStripMenuItem();
            viewToolStripMenuItem = new ToolStripMenuItem();
            modeToolStripMenuItem = new ToolStripMenuItem();
            perspectiveToolStripMenuItem = new ToolStripMenuItem();
            textureToolStripMenuItem = new ToolStripMenuItem();
            hybridViewToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            threeDToolStripMenuItem = new ToolStripMenuItem();
            grassToolStripMenuItem = new ToolStripMenuItem();
            ghostHiddenPartsToolStripMenuItem = new ToolStripMenuItem();
            antialiasingToolStripMenuItem = new ToolStripMenuItem();
            xToolStripMenuItem4 = new ToolStripMenuItem();
            xToolStripMenuItem = new ToolStripMenuItem();
            xToolStripMenuItem1 = new ToolStripMenuItem();
            xToolStripMenuItem2 = new ToolStripMenuItem();
            xToolStripMenuItem3 = new ToolStripMenuItem();
            twoDToolStripMenuItem = new ToolStripMenuItem();
            alphaCheckerboardToolStripMenuItem = new ToolStripMenuItem();
            backgroundsToolStripMenuItem = new ToolStripMenuItem();
            textureOverlayToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator16 = new ToolStripSeparator();
            mDYNAMICOVERLAYToolStripMenuItem = new ToolStripMenuItem();
            mLINECOLORToolStripMenuItem = new ColorToolStripMenuItem();
            mTEXTCOLORToolStripMenuItem = new ColorToolStripMenuItem();
            mLINESIZEToolStripMenuItem = new NumericUpDownMenuItem();
            mOVERLAYTEXTSIZEToolStripMenuItem = new NumericUpDownMenuItem();
            toolStripMenuItem2 = new ToolStripSeparator();
            toolStripSeparator11 = new ToolStripSeparator();
            gridEnabledToolStripMenuItem = new ToolStripMenuItem();
            mGRIDOPACITYToolStripMenuItem = new NumericUpDownMenuItem();
            mGRIDCOLORToolStripMenuItem = new ColorToolStripMenuItem();
            mSHAREDToolStripMenuItem = new ToolStripMenuItem();
            mINFINITEMOUSEToolStripMenuItem = new ToolStripMenuItem();
            mRENDERSTATSToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator8 = new ToolStripSeparator();
            transparencyModeToolStripMenuItem = new ToolStripMenuItem();
            offToolStripMenuItem = new ToolStripMenuItem();
            helmetOnlyToolStripMenuItem = new ToolStripMenuItem();
            allToolStripMenuItem = new ToolStripMenuItem();
            visiblePartsToolStripMenuItem = new ToolStripMenuItem();
            headToolStripMenuItem = new ToolStripMenuItem();
            chestToolStripMenuItem = new ToolStripMenuItem();
            leftArmToolStripMenuItem = new ToolStripMenuItem();
            rightArmToolStripMenuItem = new ToolStripMenuItem();
            leftLegToolStripMenuItem = new ToolStripMenuItem();
            rightLegToolStripMenuItem = new ToolStripMenuItem();
            helmetToolStripMenuItem = new ToolStripMenuItem();
            chestArmorToolStripMenuItem = new ToolStripMenuItem();
            leftArmArmorToolStripMenuItem = new ToolStripMenuItem();
            rightArmArmorToolStripMenuItem = new ToolStripMenuItem();
            leftLegArmorToolStripMenuItem = new ToolStripMenuItem();
            rightLegArmorToolStripMenuItem = new ToolStripMenuItem();
            optionsToolStripMenuItem = new ToolStripMenuItem();
            keyboardShortcutsToolStripMenuItem = new ToolStripMenuItem();
            backgroundColorToolStripMenuItem = new ToolStripMenuItem();
            mSKINDIRSToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator12 = new ToolStripSeparator();
            useTextureBasesMenuItem = new ToolStripMenuItem();
            toolStripMenuItem4 = new ToolStripSeparator();
            languageToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            checkForUpdatesToolStripMenuItem = new ToolStripMenuItem();
            automaticallyCheckForUpdatesToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripSeparator();
            planetMinecraftSubmissionToolStripMenuItem = new ToolStripMenuItem();
            officialMinecraftForumsThreadToolStripMenuItem = new ToolStripMenuItem();
            contextMenuStrip1 = new ContextMenuStrip(components);
            importHereToolStripMenuItem = new ToolStripMenuItem();
            newSkinToolStripMenuItem = new ToolStripMenuItem();
            newFolderToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator10 = new ToolStripSeparator();
            changeNameToolStripMenuItem = new ToolStripMenuItem();
            deleteToolStripMenuItem = new ToolStripMenuItem();
            cloneToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem3 = new ToolStripSeparator();
            mDECRESToolStripMenuItem = new ToolStripMenuItem();
            mINCRESToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem5 = new ToolStripSeparator();
            mFETCHNAMEToolStripMenuItem = new ToolStripMenuItem();
            bROWSEIDToolStripMenuItem = new ToolStripMenuItem();
            splitContainer1 = new VisibleSplitContainer();
            splitContainer3 = new VisibleSplitContainer();
            LibraryPanel = new Panel();
            toolStrip2 = new ToolStrip();
            treeView2 = new TreeView();
            treeView1 = new TreeView();
            hScrollBar1 = new HScrollBar();
            colorPanel = new ColorPanel();
            panel4 = new Panel();
            splitContainer4 = new SplitContainer();
            ToolsPanel = new Panel();
            label1 = new Label();
            ViewportPanel = new Panel();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            toolStrip1 = new ToolStrip();
            saveToolStripButton = new ToolStripButton();
            saveAlltoolStripButton = new ToolStripButton();
            toolStripSeparator6 = new ToolStripSeparator();
            undoToolStripButton = new ToolStripSplitButton();
            redoToolStripButton = new ToolStripSplitButton();
            toolStripSeparator1 = new ToolStripSeparator();
            toolStripSeparator2 = new ToolStripSeparator();
            perspectiveToolStripButton = new ToolStripButton();
            orthographicToolStripButton = new ToolStripButton();
            hybridToolStripButton = new ToolStripButton();
            toolStripSeparator4 = new ToolStripSeparator();
            resetCameraToolStripButton = new ToolStripButton();
            screenshotToolStripButton = new ToolStripButton();
            toolStripSeparator9 = new ToolStripSeparator();
            toggleHeadToolStripButton = new ToolStripButton();
            toggleChestToolStripButton = new ToolStripButton();
            toggleLeftArmToolStripButton = new ToolStripButton();
            toggleRightArmToolStripButton = new ToolStripButton();
            toggleLeftLegToolStripButton = new ToolStripButton();
            toggleRightLegToolStripButton = new ToolStripButton();
            toggleHelmetToolStripButton = new ToolStripButton();
            toggleChestArmorToolStripButton = new ToolStripButton();
            toggleLeftArmArmorToolStripButton = new ToolStripButton();
            toggleRightArmArmorToolStripButton = new ToolStripButton();
            toggleLeftLegArmorToolStripButton = new ToolStripButton();
            toggleRightLegArmorToolStripButton = new ToolStripButton();
            toolStripSeparator15 = new ToolStripSeparator();
            toolStripDropDownButton1 = new ToolStripDropDownButton();
            toolStripButton7 = new ToolStripButton();
            toolStripButton1 = new ToolStripButton();
            labelEditTextBox = new System.Windows.Forms.TextBox();
            miniToolStrip = new ToolStrip();
            languageProvider1 = new LanguageProvider();
            mainMenuStrip.SuspendLayout();
            contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer3).BeginInit();
            splitContainer3.Panel1.SuspendLayout();
            splitContainer3.Panel2.SuspendLayout();
            splitContainer3.SuspendLayout();
            LibraryPanel.SuspendLayout();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer4).BeginInit();
            splitContainer4.Panel1.SuspendLayout();
            splitContainer4.Panel2.SuspendLayout();
            splitContainer4.SuspendLayout();
            ToolsPanel.SuspendLayout();
            statusStrip1.SuspendLayout();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)languageProvider1).BeginInit();
            SuspendLayout();
            // 
            // mainMenuStrip
            // 
            mainMenuStrip.AllowMerge = false;
            mainMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            mainMenuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem, viewToolStripMenuItem, optionsToolStripMenuItem, helpToolStripMenuItem });
            mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            mainMenuStrip.Name = "mainMenuStrip";
            mainMenuStrip.Padding = new Padding(10, 4, 0, 4);
            mainMenuStrip.Size = new System.Drawing.Size(872, 29);
            mainMenuStrip.TabIndex = 1;
            mainMenuStrip.Text = "mainMenuStrip";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { saveToolStripMenuItem, saveAsToolStripMenuItem, saveAllToolStripMenuItem, toolStripSeparator5, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            languageProvider1.SetPropertyNames(fileToolStripMenuItem, "Text");
            fileToolStripMenuItem.Size = new System.Drawing.Size(60, 21);
            fileToolStripMenuItem.Text = "M_FILE";
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Image = Properties.Resources.saveHS;
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            languageProvider1.SetPropertyNames(saveToolStripMenuItem, "Text");
            saveToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            saveToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            saveToolStripMenuItem.Text = "M_SAVE";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // saveAsToolStripMenuItem
            // 
            saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            languageProvider1.SetPropertyNames(saveAsToolStripMenuItem, "Text");
            saveAsToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;
            saveAsToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            saveAsToolStripMenuItem.Text = "M_SAVEAS";
            saveAsToolStripMenuItem.Click += saveAsToolStripMenuItem_Click;
            // 
            // saveAllToolStripMenuItem
            // 
            saveAllToolStripMenuItem.Image = Properties.Resources.SaveAllHS;
            saveAllToolStripMenuItem.Name = "saveAllToolStripMenuItem";
            languageProvider1.SetPropertyNames(saveAllToolStripMenuItem, "Text");
            saveAllToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.A;
            saveAllToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            saveAllToolStripMenuItem.Text = "M_SAVEALL";
            saveAllToolStripMenuItem.Click += saveAllToolStripMenuItem_Click;
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new System.Drawing.Size(219, 6);
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            languageProvider1.SetPropertyNames(exitToolStripMenuItem, "Text");
            exitToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            exitToolStripMenuItem.Text = "M_EXIT";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { undoToolStripMenuItem, redoToolStripMenuItem, toolStripSeparator7, toolToolStripMenuItem });
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            languageProvider1.SetPropertyNames(editToolStripMenuItem, "Text");
            editToolStripMenuItem.Size = new System.Drawing.Size(64, 21);
            editToolStripMenuItem.Text = "M_EDIT";
            // 
            // undoToolStripMenuItem
            // 
            undoToolStripMenuItem.Image = Properties.Resources.Edit_UndoHS;
            undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            languageProvider1.SetPropertyNames(undoToolStripMenuItem, "Text");
            undoToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Z;
            undoToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            undoToolStripMenuItem.Text = "M_UNDO";
            undoToolStripMenuItem.Click += undoToolStripMenuItem_Click;
            // 
            // redoToolStripMenuItem
            // 
            redoToolStripMenuItem.Image = Properties.Resources.Edit_RedoHS;
            redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            languageProvider1.SetPropertyNames(redoToolStripMenuItem, "Text");
            redoToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Y;
            redoToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            redoToolStripMenuItem.Text = "M_REDO";
            redoToolStripMenuItem.Click += redoToolStripMenuItem_Click;
            // 
            // toolStripSeparator7
            // 
            toolStripSeparator7.Name = "toolStripSeparator7";
            toolStripSeparator7.Size = new System.Drawing.Size(172, 6);
            // 
            // toolToolStripMenuItem
            // 
            toolToolStripMenuItem.Name = "toolToolStripMenuItem";
            languageProvider1.SetPropertyNames(toolToolStripMenuItem, "Text");
            toolToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            toolToolStripMenuItem.Text = "M_TOOL";
            // 
            // viewToolStripMenuItem
            // 
            viewToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { modeToolStripMenuItem, toolStripSeparator3, threeDToolStripMenuItem, twoDToolStripMenuItem, mSHAREDToolStripMenuItem, toolStripSeparator8, transparencyModeToolStripMenuItem, visiblePartsToolStripMenuItem });
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            languageProvider1.SetPropertyNames(viewToolStripMenuItem, "Text");
            viewToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            viewToolStripMenuItem.Text = "M_VIEW";
            // 
            // modeToolStripMenuItem
            // 
            modeToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { perspectiveToolStripMenuItem, textureToolStripMenuItem, hybridViewToolStripMenuItem });
            modeToolStripMenuItem.Name = "modeToolStripMenuItem";
            languageProvider1.SetPropertyNames(modeToolStripMenuItem, "Text");
            modeToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            modeToolStripMenuItem.Text = "M_MODE";
            // 
            // perspectiveToolStripMenuItem
            // 
            perspectiveToolStripMenuItem.Image = Properties.Resources.Video;
            perspectiveToolStripMenuItem.Name = "perspectiveToolStripMenuItem";
            languageProvider1.SetPropertyNames(perspectiveToolStripMenuItem, "Text");
            perspectiveToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Q;
            perspectiveToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            perspectiveToolStripMenuItem.Text = "M_PERSPECTIVE";
            perspectiveToolStripMenuItem.Click += perspectiveToolStripMenuItem_Click;
            // 
            // textureToolStripMenuItem
            // 
            textureToolStripMenuItem.Image = Properties.Resources.image;
            textureToolStripMenuItem.Name = "textureToolStripMenuItem";
            languageProvider1.SetPropertyNames(textureToolStripMenuItem, "Text");
            textureToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.W;
            textureToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            textureToolStripMenuItem.Text = "M_TEXTURE";
            textureToolStripMenuItem.Click += textureToolStripMenuItem_Click;
            // 
            // hybridViewToolStripMenuItem
            // 
            hybridViewToolStripMenuItem.Image = Properties.Resources.hybrid;
            hybridViewToolStripMenuItem.Name = "hybridViewToolStripMenuItem";
            languageProvider1.SetPropertyNames(hybridViewToolStripMenuItem, "Text");
            hybridViewToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.H;
            hybridViewToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            hybridViewToolStripMenuItem.Text = "M_HYBRID";
            hybridViewToolStripMenuItem.Click += hybridViewToolStripMenuItem_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new System.Drawing.Size(171, 6);
            // 
            // threeDToolStripMenuItem
            // 
            threeDToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { grassToolStripMenuItem, ghostHiddenPartsToolStripMenuItem, antialiasingToolStripMenuItem });
            threeDToolStripMenuItem.Name = "threeDToolStripMenuItem";
            languageProvider1.SetPropertyNames(threeDToolStripMenuItem, "Text");
            threeDToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            threeDToolStripMenuItem.Text = "M_3D";
            // 
            // grassToolStripMenuItem
            // 
            grassToolStripMenuItem.Checked = true;
            grassToolStripMenuItem.CheckState = CheckState.Checked;
            grassToolStripMenuItem.Name = "grassToolStripMenuItem";
            languageProvider1.SetPropertyNames(grassToolStripMenuItem, "Text");
            grassToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.G;
            grassToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            grassToolStripMenuItem.Text = "M_GRASS";
            grassToolStripMenuItem.Click += grassToolStripMenuItem_Click;
            // 
            // ghostHiddenPartsToolStripMenuItem
            // 
            ghostHiddenPartsToolStripMenuItem.Name = "ghostHiddenPartsToolStripMenuItem";
            languageProvider1.SetPropertyNames(ghostHiddenPartsToolStripMenuItem, "Text");
            ghostHiddenPartsToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.G;
            ghostHiddenPartsToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            ghostHiddenPartsToolStripMenuItem.Text = "M_GHOST";
            ghostHiddenPartsToolStripMenuItem.Click += ghostHiddenPartsToolStripMenuItem_Click;
            // 
            // antialiasingToolStripMenuItem
            // 
            antialiasingToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { xToolStripMenuItem4, xToolStripMenuItem, xToolStripMenuItem1, xToolStripMenuItem2, xToolStripMenuItem3 });
            antialiasingToolStripMenuItem.Name = "antialiasingToolStripMenuItem";
            languageProvider1.SetPropertyNames(antialiasingToolStripMenuItem, "Text");
            antialiasingToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            antialiasingToolStripMenuItem.Text = "M_MULTISAMPLING";
            // 
            // xToolStripMenuItem4
            // 
            xToolStripMenuItem4.Name = "xToolStripMenuItem4";
            xToolStripMenuItem4.Size = new System.Drawing.Size(89, 22);
            xToolStripMenuItem4.Text = "0x";
            xToolStripMenuItem4.Click += xToolStripMenuItem4_Click;
            // 
            // xToolStripMenuItem
            // 
            xToolStripMenuItem.Name = "xToolStripMenuItem";
            xToolStripMenuItem.Size = new System.Drawing.Size(89, 22);
            xToolStripMenuItem.Text = "1x";
            xToolStripMenuItem.Click += xToolStripMenuItem_Click;
            // 
            // xToolStripMenuItem1
            // 
            xToolStripMenuItem1.Name = "xToolStripMenuItem1";
            xToolStripMenuItem1.Size = new System.Drawing.Size(89, 22);
            xToolStripMenuItem1.Text = "2x";
            xToolStripMenuItem1.Click += xToolStripMenuItem1_Click;
            // 
            // xToolStripMenuItem2
            // 
            xToolStripMenuItem2.Name = "xToolStripMenuItem2";
            xToolStripMenuItem2.Size = new System.Drawing.Size(89, 22);
            xToolStripMenuItem2.Text = "4x";
            xToolStripMenuItem2.Click += xToolStripMenuItem2_Click;
            // 
            // xToolStripMenuItem3
            // 
            xToolStripMenuItem3.Name = "xToolStripMenuItem3";
            xToolStripMenuItem3.Size = new System.Drawing.Size(89, 22);
            xToolStripMenuItem3.Text = "8x";
            xToolStripMenuItem3.Click += xToolStripMenuItem3_Click;
            // 
            // twoDToolStripMenuItem
            // 
            twoDToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { alphaCheckerboardToolStripMenuItem, backgroundsToolStripMenuItem, toolStripSeparator11, gridEnabledToolStripMenuItem, mGRIDOPACITYToolStripMenuItem, mGRIDCOLORToolStripMenuItem });
            twoDToolStripMenuItem.Name = "twoDToolStripMenuItem";
            languageProvider1.SetPropertyNames(twoDToolStripMenuItem, "Text");
            twoDToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            twoDToolStripMenuItem.Text = "M_2D";
            // 
            // alphaCheckerboardToolStripMenuItem
            // 
            alphaCheckerboardToolStripMenuItem.Name = "alphaCheckerboardToolStripMenuItem";
            languageProvider1.SetPropertyNames(alphaCheckerboardToolStripMenuItem, "Text");
            alphaCheckerboardToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.A;
            alphaCheckerboardToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            alphaCheckerboardToolStripMenuItem.Text = "M_ALPHACHECKER";
            alphaCheckerboardToolStripMenuItem.Click += alphaCheckerboardToolStripMenuItem_Click;
            // 
            // backgroundsToolStripMenuItem
            // 
            backgroundsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { textureOverlayToolStripMenuItem, toolStripSeparator16, mDYNAMICOVERLAYToolStripMenuItem, mLINECOLORToolStripMenuItem, mTEXTCOLORToolStripMenuItem, mLINESIZEToolStripMenuItem, mOVERLAYTEXTSIZEToolStripMenuItem, toolStripMenuItem2 });
            backgroundsToolStripMenuItem.Name = "backgroundsToolStripMenuItem";
            languageProvider1.SetPropertyNames(backgroundsToolStripMenuItem, "Text");
            backgroundsToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            backgroundsToolStripMenuItem.Text = "M_OVERLAY";
            // 
            // textureOverlayToolStripMenuItem
            // 
            textureOverlayToolStripMenuItem.Name = "textureOverlayToolStripMenuItem";
            languageProvider1.SetPropertyNames(textureOverlayToolStripMenuItem, "Text");
            textureOverlayToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.O;
            textureOverlayToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            textureOverlayToolStripMenuItem.Text = "M_ENABLEOVERLAY";
            textureOverlayToolStripMenuItem.Click += textureOverlayToolStripMenuItem_Click;
            // 
            // toolStripSeparator16
            // 
            toolStripSeparator16.Name = "toolStripSeparator16";
            toolStripSeparator16.Size = new System.Drawing.Size(237, 6);
            // 
            // mDYNAMICOVERLAYToolStripMenuItem
            // 
            mDYNAMICOVERLAYToolStripMenuItem.Name = "mDYNAMICOVERLAYToolStripMenuItem";
            languageProvider1.SetPropertyNames(mDYNAMICOVERLAYToolStripMenuItem, "Text");
            mDYNAMICOVERLAYToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            mDYNAMICOVERLAYToolStripMenuItem.Text = "M_DYNAMICOVERLAY";
            mDYNAMICOVERLAYToolStripMenuItem.Click += mDYNAMICOVERLAYToolStripMenuItem_Click;
            // 
            // mLINECOLORToolStripMenuItem
            // 
            mLINECOLORToolStripMenuItem.Name = "mLINECOLORToolStripMenuItem";
            languageProvider1.SetPropertyNames(mLINECOLORToolStripMenuItem, "Text");
            mLINECOLORToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            mLINECOLORToolStripMenuItem.Text = "M_LINECOLOR";
            mLINECOLORToolStripMenuItem.Click += mLINECOLORToolStripMenuItem_Click;
            // 
            // mTEXTCOLORToolStripMenuItem
            // 
            mTEXTCOLORToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            mTEXTCOLORToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            mTEXTCOLORToolStripMenuItem.Name = "mTEXTCOLORToolStripMenuItem";
            languageProvider1.SetPropertyNames(mTEXTCOLORToolStripMenuItem, "Text");
            mTEXTCOLORToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            mTEXTCOLORToolStripMenuItem.Text = "M_TEXTCOLOR";
            mTEXTCOLORToolStripMenuItem.TextDirection = ToolStripTextDirection.Horizontal;
            mTEXTCOLORToolStripMenuItem.Click += mTEXTCOLORToolStripMenuItem_Click;
            // 
            // mLINESIZEToolStripMenuItem
            // 
            mLINESIZEToolStripMenuItem.Name = "mLINESIZEToolStripMenuItem";
            languageProvider1.SetPropertyNames(mLINESIZEToolStripMenuItem, "Text");
            mLINESIZEToolStripMenuItem.Size = new System.Drawing.Size(140, 23);
            mLINESIZEToolStripMenuItem.Text = "M_LINESIZE";
            // 
            // mOVERLAYTEXTSIZEToolStripMenuItem
            // 
            mOVERLAYTEXTSIZEToolStripMenuItem.Name = "mOVERLAYTEXTSIZEToolStripMenuItem";
            languageProvider1.SetPropertyNames(mOVERLAYTEXTSIZEToolStripMenuItem, "Text");
            mOVERLAYTEXTSIZEToolStripMenuItem.Size = new System.Drawing.Size(140, 23);
            mOVERLAYTEXTSIZEToolStripMenuItem.Text = "M_OVERLAYTEXTSIZE";
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new System.Drawing.Size(237, 6);
            // 
            // toolStripSeparator11
            // 
            toolStripSeparator11.Name = "toolStripSeparator11";
            toolStripSeparator11.Size = new System.Drawing.Size(228, 6);
            // 
            // gridEnabledToolStripMenuItem
            // 
            gridEnabledToolStripMenuItem.Name = "gridEnabledToolStripMenuItem";
            languageProvider1.SetPropertyNames(gridEnabledToolStripMenuItem, "Text");
            gridEnabledToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            gridEnabledToolStripMenuItem.Text = "M_GRIDENABLED";
            gridEnabledToolStripMenuItem.Click += gridEnabledToolStripMenuItem_Click;
            // 
            // mGRIDOPACITYToolStripMenuItem
            // 
            mGRIDOPACITYToolStripMenuItem.Name = "mGRIDOPACITYToolStripMenuItem";
            languageProvider1.SetPropertyNames(mGRIDOPACITYToolStripMenuItem, "Text");
            mGRIDOPACITYToolStripMenuItem.Size = new System.Drawing.Size(140, 23);
            mGRIDOPACITYToolStripMenuItem.Text = "M_GRIDOPACITY";
            // 
            // mGRIDCOLORToolStripMenuItem
            // 
            mGRIDCOLORToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            mGRIDCOLORToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            mGRIDCOLORToolStripMenuItem.Name = "mGRIDCOLORToolStripMenuItem";
            languageProvider1.SetPropertyNames(mGRIDCOLORToolStripMenuItem, "Text");
            mGRIDCOLORToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            mGRIDCOLORToolStripMenuItem.Text = "M_GRIDCOLOR";
            mGRIDCOLORToolStripMenuItem.TextDirection = ToolStripTextDirection.Horizontal;
            mGRIDCOLORToolStripMenuItem.Click += mGRIDCOLORToolStripMenuItem_Click;
            // 
            // mSHAREDToolStripMenuItem
            // 
            mSHAREDToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { mINFINITEMOUSEToolStripMenuItem, mRENDERSTATSToolStripMenuItem });
            mSHAREDToolStripMenuItem.Name = "mSHAREDToolStripMenuItem";
            languageProvider1.SetPropertyNames(mSHAREDToolStripMenuItem, "Text");
            mSHAREDToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            mSHAREDToolStripMenuItem.Text = "M_SHARED";
            // 
            // mINFINITEMOUSEToolStripMenuItem
            // 
            mINFINITEMOUSEToolStripMenuItem.Name = "mINFINITEMOUSEToolStripMenuItem";
            languageProvider1.SetPropertyNames(mINFINITEMOUSEToolStripMenuItem, "Text");
            mINFINITEMOUSEToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            mINFINITEMOUSEToolStripMenuItem.Text = "M_INFINITEMOUSE";
            mINFINITEMOUSEToolStripMenuItem.Click += mINFINITEMOUSEToolStripMenuItem_Click;
            // 
            // mRENDERSTATSToolStripMenuItem
            // 
            mRENDERSTATSToolStripMenuItem.Name = "mRENDERSTATSToolStripMenuItem";
            languageProvider1.SetPropertyNames(mRENDERSTATSToolStripMenuItem, "Text");
            mRENDERSTATSToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            mRENDERSTATSToolStripMenuItem.Text = "M_RENDERSTATS";
            mRENDERSTATSToolStripMenuItem.Click += mRENDERSTATSToolStripMenuItem_Click;
            // 
            // toolStripSeparator8
            // 
            toolStripSeparator8.Name = "toolStripSeparator8";
            toolStripSeparator8.Size = new System.Drawing.Size(171, 6);
            // 
            // transparencyModeToolStripMenuItem
            // 
            transparencyModeToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { offToolStripMenuItem, helmetOnlyToolStripMenuItem, allToolStripMenuItem });
            transparencyModeToolStripMenuItem.Name = "transparencyModeToolStripMenuItem";
            languageProvider1.SetPropertyNames(transparencyModeToolStripMenuItem, "Text");
            transparencyModeToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            transparencyModeToolStripMenuItem.Text = "M_TRANSMODE";
            // 
            // offToolStripMenuItem
            // 
            offToolStripMenuItem.Name = "offToolStripMenuItem";
            languageProvider1.SetPropertyNames(offToolStripMenuItem, "Text");
            offToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.J;
            offToolStripMenuItem.Size = new System.Drawing.Size(255, 22);
            offToolStripMenuItem.Text = "M_OFF";
            offToolStripMenuItem.Click += offToolStripMenuItem_Click;
            // 
            // helmetOnlyToolStripMenuItem
            // 
            helmetOnlyToolStripMenuItem.Name = "helmetOnlyToolStripMenuItem";
            languageProvider1.SetPropertyNames(helmetOnlyToolStripMenuItem, "Text");
            helmetOnlyToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.O;
            helmetOnlyToolStripMenuItem.Size = new System.Drawing.Size(255, 22);
            helmetOnlyToolStripMenuItem.Text = "M_HELMETONLY";
            helmetOnlyToolStripMenuItem.Click += helmetOnlyToolStripMenuItem_Click;
            // 
            // allToolStripMenuItem
            // 
            allToolStripMenuItem.Name = "allToolStripMenuItem";
            languageProvider1.SetPropertyNames(allToolStripMenuItem, "Text");
            allToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.P;
            allToolStripMenuItem.Size = new System.Drawing.Size(255, 22);
            allToolStripMenuItem.Text = "M_ALL";
            allToolStripMenuItem.Click += allToolStripMenuItem_Click;
            // 
            // visiblePartsToolStripMenuItem
            // 
            visiblePartsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { headToolStripMenuItem, chestToolStripMenuItem, leftArmToolStripMenuItem, rightArmToolStripMenuItem, leftLegToolStripMenuItem, rightLegToolStripMenuItem, helmetToolStripMenuItem, chestArmorToolStripMenuItem, leftArmArmorToolStripMenuItem, rightArmArmorToolStripMenuItem, leftLegArmorToolStripMenuItem, rightLegArmorToolStripMenuItem });
            visiblePartsToolStripMenuItem.Name = "visiblePartsToolStripMenuItem";
            languageProvider1.SetPropertyNames(visiblePartsToolStripMenuItem, "Text");
            visiblePartsToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            visiblePartsToolStripMenuItem.Text = "M_VISIBLEPARTS";
            // 
            // headToolStripMenuItem
            // 
            headToolStripMenuItem.Image = Properties.Resources.show_head;
            headToolStripMenuItem.Name = "headToolStripMenuItem";
            languageProvider1.SetPropertyNames(headToolStripMenuItem, "Text");
            headToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.D1;
            headToolStripMenuItem.Size = new System.Drawing.Size(287, 22);
            headToolStripMenuItem.Text = "M_HEAD";
            headToolStripMenuItem.Click += headToolStripMenuItem_Click;
            // 
            // chestToolStripMenuItem
            // 
            chestToolStripMenuItem.Image = Properties.Resources.show_chest;
            chestToolStripMenuItem.Name = "chestToolStripMenuItem";
            languageProvider1.SetPropertyNames(chestToolStripMenuItem, "Text");
            chestToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.D2;
            chestToolStripMenuItem.Size = new System.Drawing.Size(287, 22);
            chestToolStripMenuItem.Text = "M_CHEST";
            chestToolStripMenuItem.Click += chestToolStripMenuItem_Click;
            // 
            // leftArmToolStripMenuItem
            // 
            leftArmToolStripMenuItem.Image = Properties.Resources.show_left_arm;
            leftArmToolStripMenuItem.Name = "leftArmToolStripMenuItem";
            languageProvider1.SetPropertyNames(leftArmToolStripMenuItem, "Text");
            leftArmToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.D3;
            leftArmToolStripMenuItem.Size = new System.Drawing.Size(287, 22);
            leftArmToolStripMenuItem.Text = "M_LEFTARM";
            leftArmToolStripMenuItem.Click += leftArmToolStripMenuItem_Click;
            // 
            // rightArmToolStripMenuItem
            // 
            rightArmToolStripMenuItem.Image = Properties.Resources.show_right_arm;
            rightArmToolStripMenuItem.Name = "rightArmToolStripMenuItem";
            languageProvider1.SetPropertyNames(rightArmToolStripMenuItem, "Text");
            rightArmToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.D4;
            rightArmToolStripMenuItem.Size = new System.Drawing.Size(287, 22);
            rightArmToolStripMenuItem.Text = "M_RIGHTARM";
            rightArmToolStripMenuItem.Click += rightArmToolStripMenuItem_Click;
            // 
            // leftLegToolStripMenuItem
            // 
            leftLegToolStripMenuItem.Image = Properties.Resources.show_left_leg;
            leftLegToolStripMenuItem.Name = "leftLegToolStripMenuItem";
            languageProvider1.SetPropertyNames(leftLegToolStripMenuItem, "Text");
            leftLegToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.D5;
            leftLegToolStripMenuItem.Size = new System.Drawing.Size(287, 22);
            leftLegToolStripMenuItem.Text = "M_LEFTLEG";
            leftLegToolStripMenuItem.Click += leftLegToolStripMenuItem_Click;
            // 
            // rightLegToolStripMenuItem
            // 
            rightLegToolStripMenuItem.Image = Properties.Resources.show_right_leg;
            rightLegToolStripMenuItem.Name = "rightLegToolStripMenuItem";
            languageProvider1.SetPropertyNames(rightLegToolStripMenuItem, "Text");
            rightLegToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.D6;
            rightLegToolStripMenuItem.Size = new System.Drawing.Size(287, 22);
            rightLegToolStripMenuItem.Text = "M_RIGHTLEG";
            rightLegToolStripMenuItem.Click += rightLegToolStripMenuItem_Click;
            // 
            // helmetToolStripMenuItem
            // 
            helmetToolStripMenuItem.Image = Properties.Resources.show_helmet;
            helmetToolStripMenuItem.Name = "helmetToolStripMenuItem";
            languageProvider1.SetPropertyNames(helmetToolStripMenuItem, "Text");
            helmetToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.D7;
            helmetToolStripMenuItem.Size = new System.Drawing.Size(287, 22);
            helmetToolStripMenuItem.Text = "M_HELMET";
            helmetToolStripMenuItem.Click += helmetToolStripMenuItem_Click;
            // 
            // chestArmorToolStripMenuItem
            // 
            chestArmorToolStripMenuItem.Image = Properties.Resources.show_chest_armor;
            chestArmorToolStripMenuItem.Name = "chestArmorToolStripMenuItem";
            languageProvider1.SetPropertyNames(chestArmorToolStripMenuItem, "Text");
            chestArmorToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.D8;
            chestArmorToolStripMenuItem.Size = new System.Drawing.Size(287, 22);
            chestArmorToolStripMenuItem.Text = "M_CHESTARMOR";
            chestArmorToolStripMenuItem.Click += chestArmorToolStripMenuItem_Click;
            // 
            // leftArmArmorToolStripMenuItem
            // 
            leftArmArmorToolStripMenuItem.Image = Properties.Resources.show_left_arm_armor;
            leftArmArmorToolStripMenuItem.Name = "leftArmArmorToolStripMenuItem";
            languageProvider1.SetPropertyNames(leftArmArmorToolStripMenuItem, "Text");
            leftArmArmorToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.D9;
            leftArmArmorToolStripMenuItem.Size = new System.Drawing.Size(287, 22);
            leftArmArmorToolStripMenuItem.Text = "M_LEFTARMARMOR";
            leftArmArmorToolStripMenuItem.Click += leftArmArmorToolStripMenuItem_Click;
            // 
            // rightArmArmorToolStripMenuItem
            // 
            rightArmArmorToolStripMenuItem.Image = Properties.Resources.show_right_arm_armor;
            rightArmArmorToolStripMenuItem.Name = "rightArmArmorToolStripMenuItem";
            languageProvider1.SetPropertyNames(rightArmArmorToolStripMenuItem, "Text");
            rightArmArmorToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.D0;
            rightArmArmorToolStripMenuItem.Size = new System.Drawing.Size(287, 22);
            rightArmArmorToolStripMenuItem.Text = "M_RIGHTARMARMOR";
            rightArmArmorToolStripMenuItem.Click += rightArmArmorToolStripMenuItem_Click;
            // 
            // leftLegArmorToolStripMenuItem
            // 
            leftLegArmorToolStripMenuItem.Image = Properties.Resources.show_left_leg_armor;
            leftLegArmorToolStripMenuItem.Name = "leftLegArmorToolStripMenuItem";
            languageProvider1.SetPropertyNames(leftLegArmorToolStripMenuItem, "Text");
            leftLegArmorToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.OemMinus;
            leftLegArmorToolStripMenuItem.Size = new System.Drawing.Size(287, 22);
            leftLegArmorToolStripMenuItem.Text = "M_LEFTLEGARMOR";
            leftLegArmorToolStripMenuItem.Click += leftLegArmorToolStripMenuItem_Click;
            // 
            // rightLegArmorToolStripMenuItem
            // 
            rightLegArmorToolStripMenuItem.Image = Properties.Resources.show_right_leg_armor;
            rightLegArmorToolStripMenuItem.Name = "rightLegArmorToolStripMenuItem";
            languageProvider1.SetPropertyNames(rightLegArmorToolStripMenuItem, "Text");
            rightLegArmorToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Oemplus;
            rightLegArmorToolStripMenuItem.Size = new System.Drawing.Size(287, 22);
            rightLegArmorToolStripMenuItem.Text = "M_RIGHTLEGARMOR";
            rightLegArmorToolStripMenuItem.Click += rightLegArmorToolStripMenuItem_Click;
            // 
            // optionsToolStripMenuItem
            // 
            optionsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { keyboardShortcutsToolStripMenuItem, backgroundColorToolStripMenuItem, mSKINDIRSToolStripMenuItem, toolStripSeparator12, useTextureBasesMenuItem, toolStripMenuItem4, languageToolStripMenuItem });
            optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            languageProvider1.SetPropertyNames(optionsToolStripMenuItem, "Text");
            optionsToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            optionsToolStripMenuItem.Text = "M_OPTIONS";
            // 
            // keyboardShortcutsToolStripMenuItem
            // 
            keyboardShortcutsToolStripMenuItem.Name = "keyboardShortcutsToolStripMenuItem";
            languageProvider1.SetPropertyNames(keyboardShortcutsToolStripMenuItem, "Text");
            keyboardShortcutsToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            keyboardShortcutsToolStripMenuItem.Text = "M_KEYSHORTCUTS";
            keyboardShortcutsToolStripMenuItem.Click += keyboardShortcutsToolStripMenuItem_Click;
            // 
            // backgroundColorToolStripMenuItem
            // 
            backgroundColorToolStripMenuItem.Name = "backgroundColorToolStripMenuItem";
            languageProvider1.SetPropertyNames(backgroundColorToolStripMenuItem, "Text");
            backgroundColorToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            backgroundColorToolStripMenuItem.Text = "M_BGCOLOR";
            backgroundColorToolStripMenuItem.Click += backgroundColorToolStripMenuItem_Click;
            // 
            // mSKINDIRSToolStripMenuItem
            // 
            mSKINDIRSToolStripMenuItem.Name = "mSKINDIRSToolStripMenuItem";
            languageProvider1.SetPropertyNames(mSKINDIRSToolStripMenuItem, "Text");
            mSKINDIRSToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            mSKINDIRSToolStripMenuItem.Text = "M_SKINDIRS";
            mSKINDIRSToolStripMenuItem.Click += mSKINDIRSToolStripMenuItem_Click;
            // 
            // toolStripSeparator12
            // 
            toolStripSeparator12.Name = "toolStripSeparator12";
            toolStripSeparator12.Size = new System.Drawing.Size(203, 6);
            // 
            // useTextureBasesMenuItem
            // 
            useTextureBasesMenuItem.Name = "useTextureBasesMenuItem";
            languageProvider1.SetPropertyNames(useTextureBasesMenuItem, "Text");
            useTextureBasesMenuItem.Size = new System.Drawing.Size(206, 22);
            useTextureBasesMenuItem.Text = "M_USETEXTUREBASES";
            useTextureBasesMenuItem.Click += useTextureBasesMenuItem_Click;
            // 
            // toolStripMenuItem4
            // 
            toolStripMenuItem4.Name = "toolStripMenuItem4";
            toolStripMenuItem4.Size = new System.Drawing.Size(203, 6);
            // 
            // languageToolStripMenuItem
            // 
            languageToolStripMenuItem.Name = "languageToolStripMenuItem";
            languageProvider1.SetPropertyNames(languageToolStripMenuItem, "Text");
            languageToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            languageToolStripMenuItem.Text = "M_LANGUAGE";
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { checkForUpdatesToolStripMenuItem, automaticallyCheckForUpdatesToolStripMenuItem, toolStripMenuItem1, planetMinecraftSubmissionToolStripMenuItem, officialMinecraftForumsThreadToolStripMenuItem });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            languageProvider1.SetPropertyNames(helpToolStripMenuItem, "Text");
            helpToolStripMenuItem.Size = new System.Drawing.Size(66, 21);
            helpToolStripMenuItem.Text = "M_HELP";
            // 
            // checkForUpdatesToolStripMenuItem
            // 
            checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
            languageProvider1.SetPropertyNames(checkForUpdatesToolStripMenuItem, "Text");
            checkForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            checkForUpdatesToolStripMenuItem.Text = "M_CHECKUPDATES";
            checkForUpdatesToolStripMenuItem.Click += checkForUpdatesToolStripMenuItem_Click;
            // 
            // automaticallyCheckForUpdatesToolStripMenuItem
            // 
            automaticallyCheckForUpdatesToolStripMenuItem.Name = "automaticallyCheckForUpdatesToolStripMenuItem";
            languageProvider1.SetPropertyNames(automaticallyCheckForUpdatesToolStripMenuItem, "Text");
            automaticallyCheckForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            automaticallyCheckForUpdatesToolStripMenuItem.Text = "M_AUTOUPDATE";
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new System.Drawing.Size(264, 6);
            // 
            // planetMinecraftSubmissionToolStripMenuItem
            // 
            planetMinecraftSubmissionToolStripMenuItem.Name = "planetMinecraftSubmissionToolStripMenuItem";
            planetMinecraftSubmissionToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            planetMinecraftSubmissionToolStripMenuItem.Text = "PlanetMinecraft Submission";
            planetMinecraftSubmissionToolStripMenuItem.Click += planetMinecraftSubmissionToolStripMenuItem_Click;
            // 
            // officialMinecraftForumsThreadToolStripMenuItem
            // 
            officialMinecraftForumsThreadToolStripMenuItem.Name = "officialMinecraftForumsThreadToolStripMenuItem";
            officialMinecraftForumsThreadToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            officialMinecraftForumsThreadToolStripMenuItem.Text = "Official Minecraft Forums Thread";
            officialMinecraftForumsThreadToolStripMenuItem.Click += officialMinecraftForumsThreadToolStripMenuItem_Click;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { importHereToolStripMenuItem, newSkinToolStripMenuItem, newFolderToolStripMenuItem, toolStripSeparator10, changeNameToolStripMenuItem, deleteToolStripMenuItem, cloneToolStripMenuItem, toolStripMenuItem3, mDECRESToolStripMenuItem, mINCRESToolStripMenuItem, toolStripMenuItem5, mFETCHNAMEToolStripMenuItem, bROWSEIDToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new System.Drawing.Size(213, 322);
            contextMenuStrip1.Opening += contextMenuStrip1_Opening;
            // 
            // importHereToolStripMenuItem
            // 
            importHereToolStripMenuItem.Image = Properties.Resources._112_ArrowCurve_Blue_Left_16x16_72;
            importHereToolStripMenuItem.Name = "importHereToolStripMenuItem";
            languageProvider1.SetPropertyNames(importHereToolStripMenuItem, "Text");
            importHereToolStripMenuItem.Size = new System.Drawing.Size(212, 30);
            importHereToolStripMenuItem.Text = "M_IMPORT_HERE";
            importHereToolStripMenuItem.Click += importHereToolStripMenuItem_Click;
            // 
            // newSkinToolStripMenuItem
            // 
            newSkinToolStripMenuItem.Image = Properties.Resources.newskin;
            newSkinToolStripMenuItem.Name = "newSkinToolStripMenuItem";
            languageProvider1.SetPropertyNames(newSkinToolStripMenuItem, "Text");
            newSkinToolStripMenuItem.Size = new System.Drawing.Size(212, 30);
            newSkinToolStripMenuItem.Text = "M_NEWSKIN_HERE";
            newSkinToolStripMenuItem.Click += toolStripMenuItem4_Click;
            // 
            // newFolderToolStripMenuItem
            // 
            newFolderToolStripMenuItem.Image = Properties.Resources.NewFolderHS;
            newFolderToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            newFolderToolStripMenuItem.Name = "newFolderToolStripMenuItem";
            languageProvider1.SetPropertyNames(newFolderToolStripMenuItem, "Text");
            newFolderToolStripMenuItem.Size = new System.Drawing.Size(212, 30);
            newFolderToolStripMenuItem.Text = "M_NEWFOLDER_HERE";
            newFolderToolStripMenuItem.Click += toolStripMenuItem1_Click;
            // 
            // toolStripSeparator10
            // 
            toolStripSeparator10.Name = "toolStripSeparator10";
            toolStripSeparator10.Size = new System.Drawing.Size(209, 6);
            // 
            // changeNameToolStripMenuItem
            // 
            changeNameToolStripMenuItem.Image = Properties.Resources.Rename;
            changeNameToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            changeNameToolStripMenuItem.Name = "changeNameToolStripMenuItem";
            languageProvider1.SetPropertyNames(changeNameToolStripMenuItem, "Text");
            changeNameToolStripMenuItem.Size = new System.Drawing.Size(212, 30);
            changeNameToolStripMenuItem.Text = "M_RENAME";
            changeNameToolStripMenuItem.Click += changeNameToolStripMenuItem_Click;
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.Image = Properties.Resources.delete;
            deleteToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            languageProvider1.SetPropertyNames(deleteToolStripMenuItem, "Text");
            deleteToolStripMenuItem.Size = new System.Drawing.Size(212, 30);
            deleteToolStripMenuItem.Text = "M_DELETE";
            deleteToolStripMenuItem.Click += deleteToolStripMenuItem_Click;
            // 
            // cloneToolStripMenuItem
            // 
            cloneToolStripMenuItem.Image = Properties.Resources.clone;
            cloneToolStripMenuItem.Name = "cloneToolStripMenuItem";
            languageProvider1.SetPropertyNames(cloneToolStripMenuItem, "Text");
            cloneToolStripMenuItem.Size = new System.Drawing.Size(212, 30);
            cloneToolStripMenuItem.Text = "M_CLONE";
            cloneToolStripMenuItem.Click += cloneToolStripMenuItem_Click;
            // 
            // toolStripMenuItem3
            // 
            toolStripMenuItem3.Name = "toolStripMenuItem3";
            toolStripMenuItem3.Size = new System.Drawing.Size(209, 6);
            // 
            // mDECRESToolStripMenuItem
            // 
            mDECRESToolStripMenuItem.Image = Properties.Resources.incres;
            mDECRESToolStripMenuItem.Name = "mDECRESToolStripMenuItem";
            languageProvider1.SetPropertyNames(mDECRESToolStripMenuItem, "Text");
            mDECRESToolStripMenuItem.Size = new System.Drawing.Size(212, 30);
            mDECRESToolStripMenuItem.Text = "T_DECRES";
            mDECRESToolStripMenuItem.Click += mDECRESToolStripMenuItem_Click;
            // 
            // mINCRESToolStripMenuItem
            // 
            mINCRESToolStripMenuItem.Image = Properties.Resources.decres;
            mINCRESToolStripMenuItem.Name = "mINCRESToolStripMenuItem";
            languageProvider1.SetPropertyNames(mINCRESToolStripMenuItem, "Text");
            mINCRESToolStripMenuItem.Size = new System.Drawing.Size(212, 30);
            mINCRESToolStripMenuItem.Text = "T_INCRES";
            mINCRESToolStripMenuItem.Click += mINCRESToolStripMenuItem_Click;
            // 
            // toolStripMenuItem5
            // 
            toolStripMenuItem5.Name = "toolStripMenuItem5";
            toolStripMenuItem5.Size = new System.Drawing.Size(209, 6);
            // 
            // mFETCHNAMEToolStripMenuItem
            // 
            mFETCHNAMEToolStripMenuItem.Image = Properties.Resources.import_from_mc;
            mFETCHNAMEToolStripMenuItem.Name = "mFETCHNAMEToolStripMenuItem";
            languageProvider1.SetPropertyNames(mFETCHNAMEToolStripMenuItem, "Text");
            mFETCHNAMEToolStripMenuItem.Size = new System.Drawing.Size(212, 30);
            mFETCHNAMEToolStripMenuItem.Text = "M_FETCH_NAME";
            mFETCHNAMEToolStripMenuItem.Click += mFETCHNAMEToolStripMenuItem_Click;
            // 
            // bROWSEIDToolStripMenuItem
            // 
            bROWSEIDToolStripMenuItem.Image = Properties.Resources.browseto;
            bROWSEIDToolStripMenuItem.Name = "bROWSEIDToolStripMenuItem";
            bROWSEIDToolStripMenuItem.Size = new System.Drawing.Size(212, 30);
            bROWSEIDToolStripMenuItem.Text = "BROWSE_ID";
            bROWSEIDToolStripMenuItem.Click += bROWSEIDToolStripMenuItem_Click;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.FixedPanel = FixedPanel.Panel1;
            splitContainer1.Location = new System.Drawing.Point(0, 29);
            splitContainer1.Margin = new Padding(6);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(splitContainer3);
            splitContainer1.Panel1MinSize = 302;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(panel4);
            splitContainer1.Size = new System.Drawing.Size(872, 493);
            splitContainer1.SplitterDistance = 302;
            splitContainer1.SplitterWidth = 8;
            splitContainer1.TabIndex = 4;
            // 
            // splitContainer3
            // 
            splitContainer3.Dock = DockStyle.Fill;
            splitContainer3.FixedPanel = FixedPanel.Panel2;
            splitContainer3.Location = new System.Drawing.Point(0, 0);
            splitContainer3.Margin = new Padding(6);
            splitContainer3.Name = "splitContainer3";
            splitContainer3.Orientation = Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            splitContainer3.Panel1.Controls.Add(LibraryPanel);
            splitContainer3.Panel1.Controls.Add(hScrollBar1);
            // 
            // splitContainer3.Panel2
            // 
            splitContainer3.Panel2.Controls.Add(colorPanel);
            splitContainer3.Size = new System.Drawing.Size(302, 493);
            splitContainer3.SplitterDistance = 206;
            splitContainer3.SplitterWidth = 8;
            splitContainer3.TabIndex = 1;
            // 
            // LibraryPanel
            // 
            LibraryPanel.Controls.Add(toolStrip2);
            LibraryPanel.Controls.Add(treeView2);
            LibraryPanel.Controls.Add(treeView1);
            LibraryPanel.Location = new System.Drawing.Point(39, 38);
            LibraryPanel.Margin = new Padding(4);
            LibraryPanel.Name = "LibraryPanel";
            LibraryPanel.Size = new System.Drawing.Size(297, 306);
            LibraryPanel.TabIndex = 8;
            // 
            // toolStrip2
            // 
            toolStrip2.ImageScalingSize = new System.Drawing.Size(24, 24);
            toolStrip2.Location = new System.Drawing.Point(0, 0);
            toolStrip2.Name = "toolStrip2";
            toolStrip2.Size = new System.Drawing.Size(297, 33);
            toolStrip2.TabIndex = 2;
            toolStrip2.Visible = false;
            // 
            // treeView2
            // 
            treeView2.BorderStyle = BorderStyle.None;
            treeView2.FullRowSelect = true;
            treeView2.HideSelection = false;
            treeView2.HotTracking = true;
            treeView2.ItemHeight = 28;
            treeView2.Location = new System.Drawing.Point(110, 13);
            treeView2.Margin = new Padding(6);
            treeView2.Name = "treeView2";
            treeView2.ShowLines = false;
            treeView2.Size = new System.Drawing.Size(86, 81);
            treeView2.TabIndex = 0;
            // 
            // treeView1
            // 
            treeView1.AllowDrop = true;
            treeView1.BorderStyle = BorderStyle.None;
            treeView1.DrawMode = TreeViewDrawMode.OwnerDrawAll;
            treeView1.Enabled = false;
            treeView1.Font = new System.Drawing.Font("Microsoft YaHei UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            treeView1.FullRowSelect = true;
            treeView1.HideSelection = false;
            treeView1.HotTracking = true;
            treeView1.Indent = 20;
            treeView1.ItemHeight = 40;
            treeView1.Location = new System.Drawing.Point(12, 13);
            treeView1.Margin = new Padding(0);
            treeView1.Name = "treeView1";
            treeView1.ShowNodeToolTips = true;
            treeView1.Size = new System.Drawing.Size(92, 84);
            treeView1.Sorted = true;
            treeView1.TabIndex = 1;
            treeView1.MouseDown += treeView1_MouseDown;
            treeView1.MouseUp += treeView1_MouseUp;
            // 
            // hScrollBar1
            // 
            hScrollBar1.Dock = DockStyle.Bottom;
            hScrollBar1.LargeChange = 1;
            hScrollBar1.Location = new System.Drawing.Point(0, 189);
            hScrollBar1.Maximum = 0;
            hScrollBar1.Name = "hScrollBar1";
            hScrollBar1.Size = new System.Drawing.Size(302, 17);
            hScrollBar1.TabIndex = 7;
            hScrollBar1.Scroll += hScrollBar1_Scroll;
            // 
            // colorPanel
            // 
            colorPanel.BackColor = System.Drawing.Color.White;
            colorPanel.Dock = DockStyle.Fill;
            colorPanel.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            colorPanel.Location = new System.Drawing.Point(0, 0);
            colorPanel.Margin = new Padding(8);
            colorPanel.Name = "colorPanel";
            colorManager3.CurrentSpace = ColorManager.ColorSpace.RGB;
            colorPanel.SelectedColor = colorManager3;
            colorPanel.Selection = 0;
            colorPanel.Size = new System.Drawing.Size(302, 279);
            colorPanel.TabIndex = 0;
            colorManager4.CurrentSpace = ColorManager.ColorSpace.RGB;
            colorPanel.UnselectedColor = colorManager4;
            // 
            // panel4
            // 
            panel4.Controls.Add(splitContainer4);
            panel4.Controls.Add(statusStrip1);
            panel4.Controls.Add(toolStrip1);
            panel4.Dock = DockStyle.Fill;
            panel4.Location = new System.Drawing.Point(0, 0);
            panel4.Margin = new Padding(6);
            panel4.Name = "panel4";
            panel4.Size = new System.Drawing.Size(562, 493);
            panel4.TabIndex = 1;
            // 
            // splitContainer4
            // 
            splitContainer4.BorderStyle = BorderStyle.Fixed3D;
            splitContainer4.Dock = DockStyle.Fill;
            splitContainer4.Location = new System.Drawing.Point(0, 31);
            splitContainer4.Margin = new Padding(6);
            splitContainer4.Name = "splitContainer4";
            splitContainer4.Orientation = Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            splitContainer4.Panel1.Controls.Add(ToolsPanel);
            splitContainer4.Panel1MinSize = 0;
            // 
            // splitContainer4.Panel2
            // 
            splitContainer4.Panel2.Controls.Add(ViewportPanel);
            splitContainer4.Size = new System.Drawing.Size(562, 440);
            splitContainer4.SplitterDistance = 90;
            splitContainer4.SplitterIncrement = 5;
            splitContainer4.SplitterWidth = 2;
            splitContainer4.TabIndex = 6;
            // 
            // ToolsPanel
            // 
            ToolsPanel.AutoScroll = true;
            ToolsPanel.BackColor = System.Drawing.SystemColors.Window;
            ToolsPanel.Controls.Add(label1);
            ToolsPanel.Dock = DockStyle.Fill;
            ToolsPanel.Location = new System.Drawing.Point(0, 0);
            ToolsPanel.Margin = new Padding(4);
            ToolsPanel.Name = "ToolsPanel";
            ToolsPanel.Size = new System.Drawing.Size(558, 86);
            ToolsPanel.TabIndex = 0;
            // 
            // label1
            // 
            label1.Dock = DockStyle.Fill;
            label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            label1.Location = new System.Drawing.Point(0, 0);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(558, 86);
            label1.TabIndex = 0;
            label1.Text = "label1";
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ViewportPanel
            // 
            ViewportPanel.Dock = DockStyle.Fill;
            ViewportPanel.Location = new System.Drawing.Point(0, 0);
            ViewportPanel.Margin = new Padding(4);
            ViewportPanel.Name = "ViewportPanel";
            ViewportPanel.Size = new System.Drawing.Size(558, 344);
            ViewportPanel.TabIndex = 0;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new System.Drawing.Point(0, 471);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(2, 0, 24, 0);
            statusStrip1.Size = new System.Drawing.Size(562, 22);
            statusStrip1.SizingGrip = false;
            statusStrip1.TabIndex = 0;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Overflow = ToolStripItemOverflow.Never;
            toolStripStatusLabel1.Size = new System.Drawing.Size(228, 17);
            toolStripStatusLabel1.Text = "Look down here for important things!";
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            toolStrip1.Items.AddRange(new ToolStripItem[] { saveToolStripButton, saveAlltoolStripButton, toolStripSeparator6, undoToolStripButton, redoToolStripButton, toolStripSeparator1, toolStripSeparator2, perspectiveToolStripButton, orthographicToolStripButton, hybridToolStripButton, toolStripSeparator4, resetCameraToolStripButton, screenshotToolStripButton, toolStripSeparator9, toggleHeadToolStripButton, toggleChestToolStripButton, toggleLeftArmToolStripButton, toggleRightArmToolStripButton, toggleLeftLegToolStripButton, toggleRightLegToolStripButton, toggleHelmetToolStripButton, toggleChestArmorToolStripButton, toggleLeftArmArmorToolStripButton, toggleRightArmArmorToolStripButton, toggleLeftLegArmorToolStripButton, toggleRightLegArmorToolStripButton, toolStripSeparator15, toolStripDropDownButton1, toolStripButton7, toolStripButton1 });
            toolStrip1.Location = new System.Drawing.Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Padding = new Padding(0, 0, 3, 0);
            toolStrip1.Size = new System.Drawing.Size(562, 31);
            toolStrip1.TabIndex = 5;
            toolStrip1.Text = "toolStrip1";
            // 
            // saveToolStripButton
            // 
            saveToolStripButton.AutoToolTip = false;
            saveToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            saveToolStripButton.Image = Properties.Resources.saveHS;
            saveToolStripButton.ImageScaling = ToolStripItemImageScaling.None;
            saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            saveToolStripButton.Name = "saveToolStripButton";
            languageProvider1.SetPropertyNames(saveToolStripButton, "Text");
            saveToolStripButton.Size = new System.Drawing.Size(23, 28);
            saveToolStripButton.Text = "T_SAVE";
            saveToolStripButton.Click += saveToolStripButton_Click;
            // 
            // saveAlltoolStripButton
            // 
            saveAlltoolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            saveAlltoolStripButton.Image = Properties.Resources.SaveAllHS;
            saveAlltoolStripButton.ImageScaling = ToolStripItemImageScaling.None;
            saveAlltoolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            saveAlltoolStripButton.Name = "saveAlltoolStripButton";
            languageProvider1.SetPropertyNames(saveAlltoolStripButton, "Text");
            saveAlltoolStripButton.Size = new System.Drawing.Size(23, 28);
            saveAlltoolStripButton.Text = "T_SAVEALL";
            saveAlltoolStripButton.Click += saveAlltoolStripButton_Click;
            // 
            // toolStripSeparator6
            // 
            toolStripSeparator6.Name = "toolStripSeparator6";
            toolStripSeparator6.Size = new System.Drawing.Size(6, 31);
            // 
            // undoToolStripButton
            // 
            undoToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            undoToolStripButton.Image = Properties.Resources.Edit_UndoHS;
            undoToolStripButton.ImageScaling = ToolStripItemImageScaling.None;
            undoToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            undoToolStripButton.Name = "undoToolStripButton";
            languageProvider1.SetPropertyNames(undoToolStripButton, "Text");
            undoToolStripButton.Size = new System.Drawing.Size(32, 28);
            undoToolStripButton.Text = "T_UNDO";
            undoToolStripButton.ButtonClick += undoToolStripButton_ButtonClick;
            // 
            // redoToolStripButton
            // 
            redoToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            redoToolStripButton.Image = Properties.Resources.Edit_RedoHS;
            redoToolStripButton.ImageScaling = ToolStripItemImageScaling.None;
            redoToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            redoToolStripButton.Name = "redoToolStripButton";
            languageProvider1.SetPropertyNames(redoToolStripButton, "Text");
            redoToolStripButton.Size = new System.Drawing.Size(32, 28);
            redoToolStripButton.Text = "T_REDO";
            redoToolStripButton.ButtonClick += redoToolStripButton_ButtonClick;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // perspectiveToolStripButton
            // 
            perspectiveToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            perspectiveToolStripButton.Image = Properties.Resources.Video;
            perspectiveToolStripButton.ImageScaling = ToolStripItemImageScaling.None;
            perspectiveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            perspectiveToolStripButton.Name = "perspectiveToolStripButton";
            languageProvider1.SetPropertyNames(perspectiveToolStripButton, "Text");
            perspectiveToolStripButton.Size = new System.Drawing.Size(23, 28);
            perspectiveToolStripButton.Text = "T_PERSPECTIVE";
            perspectiveToolStripButton.Click += perspectiveToolStripButton_Click;
            // 
            // orthographicToolStripButton
            // 
            orthographicToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            orthographicToolStripButton.Image = Properties.Resources.image;
            orthographicToolStripButton.ImageScaling = ToolStripItemImageScaling.None;
            orthographicToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            orthographicToolStripButton.Name = "orthographicToolStripButton";
            languageProvider1.SetPropertyNames(orthographicToolStripButton, "Text");
            orthographicToolStripButton.Size = new System.Drawing.Size(23, 28);
            orthographicToolStripButton.Text = "T_TEXTURE";
            orthographicToolStripButton.Click += orthographicToolStripButton_Click;
            // 
            // hybridToolStripButton
            // 
            hybridToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            hybridToolStripButton.Image = Properties.Resources.hybrid;
            hybridToolStripButton.ImageScaling = ToolStripItemImageScaling.None;
            hybridToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            hybridToolStripButton.Name = "hybridToolStripButton";
            languageProvider1.SetPropertyNames(hybridToolStripButton, "Text");
            hybridToolStripButton.Size = new System.Drawing.Size(23, 28);
            hybridToolStripButton.Text = "T_HYBRID";
            hybridToolStripButton.Click += hybridToolStripButton_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
            // 
            // resetCameraToolStripButton
            // 
            resetCameraToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            resetCameraToolStripButton.Image = Properties.Resources.samesize;
            resetCameraToolStripButton.ImageScaling = ToolStripItemImageScaling.None;
            resetCameraToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            resetCameraToolStripButton.Name = "resetCameraToolStripButton";
            languageProvider1.SetPropertyNames(resetCameraToolStripButton, "Text");
            resetCameraToolStripButton.Size = new System.Drawing.Size(23, 28);
            resetCameraToolStripButton.Text = "T_RESETCAMERA";
            resetCameraToolStripButton.Click += resetCameraToolStripButton_Click;
            // 
            // screenshotToolStripButton
            // 
            screenshotToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            screenshotToolStripButton.Image = Properties.Resources.camera;
            screenshotToolStripButton.ImageScaling = ToolStripItemImageScaling.None;
            screenshotToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            screenshotToolStripButton.Name = "screenshotToolStripButton";
            languageProvider1.SetPropertyNames(screenshotToolStripButton, "Text");
            screenshotToolStripButton.Size = new System.Drawing.Size(23, 28);
            screenshotToolStripButton.Text = "T_SCREENSHOT";
            screenshotToolStripButton.Click += screenshotToolStripButton_Click;
            // 
            // toolStripSeparator9
            // 
            toolStripSeparator9.Name = "toolStripSeparator9";
            toolStripSeparator9.Size = new System.Drawing.Size(6, 31);
            // 
            // toggleHeadToolStripButton
            // 
            toggleHeadToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toggleHeadToolStripButton.Image = Properties.Resources.show_head;
            toggleHeadToolStripButton.ImageScaling = ToolStripItemImageScaling.None;
            toggleHeadToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            toggleHeadToolStripButton.Name = "toggleHeadToolStripButton";
            languageProvider1.SetPropertyNames(toggleHeadToolStripButton, "Text");
            toggleHeadToolStripButton.Size = new System.Drawing.Size(23, 28);
            toggleHeadToolStripButton.Text = "T_TOGGLEHEAD";
            toggleHeadToolStripButton.Click += toggleHeadToolStripButton_Click;
            // 
            // toggleChestToolStripButton
            // 
            toggleChestToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toggleChestToolStripButton.Image = Properties.Resources.show_chest;
            toggleChestToolStripButton.ImageScaling = ToolStripItemImageScaling.None;
            toggleChestToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            toggleChestToolStripButton.Name = "toggleChestToolStripButton";
            languageProvider1.SetPropertyNames(toggleChestToolStripButton, "Text");
            toggleChestToolStripButton.Size = new System.Drawing.Size(23, 28);
            toggleChestToolStripButton.Text = "T_TOGGLECHEST";
            toggleChestToolStripButton.Click += toggleChestToolStripButton_Click;
            // 
            // toggleLeftArmToolStripButton
            // 
            toggleLeftArmToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toggleLeftArmToolStripButton.Image = Properties.Resources.show_left_arm;
            toggleLeftArmToolStripButton.ImageScaling = ToolStripItemImageScaling.None;
            toggleLeftArmToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            toggleLeftArmToolStripButton.Name = "toggleLeftArmToolStripButton";
            languageProvider1.SetPropertyNames(toggleLeftArmToolStripButton, "Text");
            toggleLeftArmToolStripButton.Size = new System.Drawing.Size(23, 28);
            toggleLeftArmToolStripButton.Text = "T_TOGGLELEFTARM";
            toggleLeftArmToolStripButton.Click += toggleLeftArmToolStripButton_Click;
            // 
            // toggleRightArmToolStripButton
            // 
            toggleRightArmToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toggleRightArmToolStripButton.Image = Properties.Resources.show_right_arm;
            toggleRightArmToolStripButton.ImageScaling = ToolStripItemImageScaling.None;
            toggleRightArmToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            toggleRightArmToolStripButton.Name = "toggleRightArmToolStripButton";
            languageProvider1.SetPropertyNames(toggleRightArmToolStripButton, "Text");
            toggleRightArmToolStripButton.Size = new System.Drawing.Size(23, 28);
            toggleRightArmToolStripButton.Text = "T_TOGGLERIGHTARM";
            toggleRightArmToolStripButton.Click += toggleRightArmToolStripButton_Click;
            // 
            // toggleLeftLegToolStripButton
            // 
            toggleLeftLegToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toggleLeftLegToolStripButton.Image = Properties.Resources.show_left_leg;
            toggleLeftLegToolStripButton.ImageScaling = ToolStripItemImageScaling.None;
            toggleLeftLegToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            toggleLeftLegToolStripButton.Name = "toggleLeftLegToolStripButton";
            languageProvider1.SetPropertyNames(toggleLeftLegToolStripButton, "Text");
            toggleLeftLegToolStripButton.Size = new System.Drawing.Size(23, 28);
            toggleLeftLegToolStripButton.Text = "T_TOGGLELEFTLEG";
            toggleLeftLegToolStripButton.Click += toggleLeftLegToolStripButton_Click;
            // 
            // toggleRightLegToolStripButton
            // 
            toggleRightLegToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toggleRightLegToolStripButton.Image = Properties.Resources.show_right_leg;
            toggleRightLegToolStripButton.ImageScaling = ToolStripItemImageScaling.None;
            toggleRightLegToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            toggleRightLegToolStripButton.Name = "toggleRightLegToolStripButton";
            languageProvider1.SetPropertyNames(toggleRightLegToolStripButton, "Text");
            toggleRightLegToolStripButton.Size = new System.Drawing.Size(23, 28);
            toggleRightLegToolStripButton.Text = "T_TOGGLERIGHTLEG";
            toggleRightLegToolStripButton.Click += toggleRightLegToolStripButton_Click;
            // 
            // toggleHelmetToolStripButton
            // 
            toggleHelmetToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toggleHelmetToolStripButton.Image = Properties.Resources.show_helmet;
            toggleHelmetToolStripButton.ImageScaling = ToolStripItemImageScaling.None;
            toggleHelmetToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            toggleHelmetToolStripButton.Name = "toggleHelmetToolStripButton";
            languageProvider1.SetPropertyNames(toggleHelmetToolStripButton, "Text");
            toggleHelmetToolStripButton.Size = new System.Drawing.Size(23, 28);
            toggleHelmetToolStripButton.Text = "T_TOGGLEHELMET";
            toggleHelmetToolStripButton.Click += toggleHelmetToolStripButton_Click;
            // 
            // toggleChestArmorToolStripButton
            // 
            toggleChestArmorToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toggleChestArmorToolStripButton.Image = Properties.Resources.show_chest_armor;
            toggleChestArmorToolStripButton.ImageScaling = ToolStripItemImageScaling.None;
            toggleChestArmorToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            toggleChestArmorToolStripButton.Name = "toggleChestArmorToolStripButton";
            languageProvider1.SetPropertyNames(toggleChestArmorToolStripButton, "Text");
            toggleChestArmorToolStripButton.Size = new System.Drawing.Size(23, 28);
            toggleChestArmorToolStripButton.Text = "T_TOGGLECHESTARMOR";
            toggleChestArmorToolStripButton.Click += toggleChestArmorToolStripButton_Click;
            // 
            // toggleLeftArmArmorToolStripButton
            // 
            toggleLeftArmArmorToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toggleLeftArmArmorToolStripButton.Image = Properties.Resources.show_left_arm_armor;
            toggleLeftArmArmorToolStripButton.ImageScaling = ToolStripItemImageScaling.None;
            toggleLeftArmArmorToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            toggleLeftArmArmorToolStripButton.Name = "toggleLeftArmArmorToolStripButton";
            languageProvider1.SetPropertyNames(toggleLeftArmArmorToolStripButton, "Text");
            toggleLeftArmArmorToolStripButton.Size = new System.Drawing.Size(23, 28);
            toggleLeftArmArmorToolStripButton.Text = "T_TOGGLELEFTARMARMOR";
            toggleLeftArmArmorToolStripButton.Click += toggleLeftArmArmorToolStripButton_Click;
            // 
            // toggleRightArmArmorToolStripButton
            // 
            toggleRightArmArmorToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toggleRightArmArmorToolStripButton.Image = Properties.Resources.show_right_arm_armor;
            toggleRightArmArmorToolStripButton.ImageScaling = ToolStripItemImageScaling.None;
            toggleRightArmArmorToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            toggleRightArmArmorToolStripButton.Name = "toggleRightArmArmorToolStripButton";
            languageProvider1.SetPropertyNames(toggleRightArmArmorToolStripButton, "Text");
            toggleRightArmArmorToolStripButton.Size = new System.Drawing.Size(23, 28);
            toggleRightArmArmorToolStripButton.Text = "T_TOGGLERIGHTARMARMOR";
            toggleRightArmArmorToolStripButton.Click += toggleRightArmArmorToolStripButton_Click;
            // 
            // toggleLeftLegArmorToolStripButton
            // 
            toggleLeftLegArmorToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toggleLeftLegArmorToolStripButton.Image = Properties.Resources.show_left_leg_armor;
            toggleLeftLegArmorToolStripButton.ImageScaling = ToolStripItemImageScaling.None;
            toggleLeftLegArmorToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            toggleLeftLegArmorToolStripButton.Name = "toggleLeftLegArmorToolStripButton";
            languageProvider1.SetPropertyNames(toggleLeftLegArmorToolStripButton, "Text");
            toggleLeftLegArmorToolStripButton.Size = new System.Drawing.Size(23, 28);
            toggleLeftLegArmorToolStripButton.Text = "T_TOGGLELEFTLEGARMOR";
            toggleLeftLegArmorToolStripButton.Click += toggleLeftLegArmorToolStripButton_Click;
            // 
            // toggleRightLegArmorToolStripButton
            // 
            toggleRightLegArmorToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toggleRightLegArmorToolStripButton.Image = Properties.Resources.show_right_leg_armor;
            toggleRightLegArmorToolStripButton.ImageScaling = ToolStripItemImageScaling.None;
            toggleRightLegArmorToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            toggleRightLegArmorToolStripButton.Name = "toggleRightLegArmorToolStripButton";
            languageProvider1.SetPropertyNames(toggleRightLegArmorToolStripButton, "Text");
            toggleRightLegArmorToolStripButton.Size = new System.Drawing.Size(23, 28);
            toggleRightLegArmorToolStripButton.Text = "T_TOGGLERIGHTLEGARMOR";
            toggleRightLegArmorToolStripButton.Click += toggleRightLegArmorToolStripButton_Click;
            // 
            // toolStripSeparator15
            // 
            toolStripSeparator15.Name = "toolStripSeparator15";
            toolStripSeparator15.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripDropDownButton1
            // 
            toolStripDropDownButton1.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripDropDownButton1.Enabled = false;
            toolStripDropDownButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripDropDownButton1.Image");
            toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            toolStripDropDownButton1.Size = new System.Drawing.Size(62, 28);
            toolStripDropDownButton1.Text = "Human";
            // 
            // toolStripButton7
            // 
            toolStripButton7.Alignment = ToolStripItemAlignment.Right;
            toolStripButton7.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton7.Image = Properties.Resources.WindowsHS;
            toolStripButton7.ImageScaling = ToolStripItemImageScaling.None;
            toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButton7.Name = "toolStripButton7";
            languageProvider1.SetPropertyNames(toolStripButton7, "Text");
            toolStripButton7.Size = new System.Drawing.Size(23, 28);
            toolStripButton7.Text = "W_POPOUT";
            toolStripButton7.Click += toolStripButton7_Click;
            // 
            // toolStripButton1
            // 
            toolStripButton1.Alignment = ToolStripItemAlignment.Right;
            toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton1.Image = Properties.Resources.arrow_Up_16xLG;
            toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButton1.Name = "toolStripButton1";
            languageProvider1.SetPropertyNames(toolStripButton1, "Text");
            toolStripButton1.Size = new System.Drawing.Size(28, 28);
            toolStripButton1.Text = "W_OPTIONS";
            toolStripButton1.Click += toolStripButton1_Click;
            // 
            // labelEditTextBox
            // 
            labelEditTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            labelEditTextBox.Location = new System.Drawing.Point(807, 2);
            labelEditTextBox.Margin = new Padding(6);
            labelEditTextBox.Name = "labelEditTextBox";
            labelEditTextBox.Size = new System.Drawing.Size(172, 20);
            labelEditTextBox.TabIndex = 5;
            labelEditTextBox.Visible = false;
            labelEditTextBox.KeyDown += labelEditTextBox_KeyDown;
            labelEditTextBox.KeyPress += labelEditTextBox_KeyPress;
            labelEditTextBox.KeyUp += labelEditTextBox_KeyUp;
            labelEditTextBox.Leave += labelEditTextBox_Leave;
            // 
            // miniToolStrip
            // 
            miniToolStrip.AutoSize = false;
            miniToolStrip.CanOverflow = false;
            miniToolStrip.Dock = DockStyle.None;
            miniToolStrip.Enabled = false;
            miniToolStrip.GripStyle = ToolStripGripStyle.Hidden;
            miniToolStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            miniToolStrip.ImeMode = ImeMode.On;
            miniToolStrip.Location = new System.Drawing.Point(1, 209);
            miniToolStrip.Name = "miniToolStrip";
            miniToolStrip.Size = new System.Drawing.Size(294, 25);
            miniToolStrip.TabIndex = 6;
            // 
            // languageProvider1
            // 
            languageProvider1.BaseControl = this;
            // 
            // Editor
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new System.Drawing.Size(872, 522);
            Controls.Add(labelEditTextBox);
            Controls.Add(splitContainer1);
            Controls.Add(mainMenuStrip);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = mainMenuStrip;
            Margin = new Padding(6);
            Name = "Editor";
            ShowIcon = false;
            Text = "MCSkinn Preload Window";
            WindowState = FormWindowState.Minimized;
            Load += Editor_Load;
            DpiChanged += Editor_DpiChanged;
            mainMenuStrip.ResumeLayout(false);
            mainMenuStrip.PerformLayout();
            contextMenuStrip1.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            splitContainer3.Panel1.ResumeLayout(false);
            splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer3).EndInit();
            splitContainer3.ResumeLayout(false);
            LibraryPanel.ResumeLayout(false);
            LibraryPanel.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            splitContainer4.Panel1.ResumeLayout(false);
            splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer4).EndInit();
            splitContainer4.ResumeLayout(false);
            ToolsPanel.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)languageProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        public HScrollBar hScrollBar1;
        public LanguageProvider languageProvider1;
        public NativeMenuStrip mainMenuStrip;
        public ToolStripMenuItem fileToolStripMenuItem;
        public ToolStripMenuItem viewToolStripMenuItem;
        public ToolStripMenuItem exitToolStripMenuItem;
        public ToolStripMenuItem threeDToolStripMenuItem;
        public ToolStripMenuItem grassToolStripMenuItem;
        public ToolStripMenuItem twoDToolStripMenuItem;
        public ToolStripMenuItem alphaCheckerboardToolStripMenuItem;
        public ToolStripSeparator toolStripSeparator8;
        public ToolStripMenuItem transparencyModeToolStripMenuItem;
        public ToolStripMenuItem offToolStripMenuItem;
        public ToolStripMenuItem helmetOnlyToolStripMenuItem;
        public ToolStripMenuItem allToolStripMenuItem;
        public ToolStripMenuItem visiblePartsToolStripMenuItem;
        public ToolStripMenuItem headToolStripMenuItem;
        public ToolStripMenuItem helmetToolStripMenuItem;
        public ToolStripMenuItem chestToolStripMenuItem;
        public ToolStripMenuItem leftArmToolStripMenuItem;
        public ToolStripMenuItem rightArmToolStripMenuItem;
        public ToolStripMenuItem leftLegToolStripMenuItem;
        public ToolStripMenuItem rightLegToolStripMenuItem;
        public ToolStripMenuItem modeToolStripMenuItem;
        public ToolStripMenuItem perspectiveToolStripMenuItem;
        public ToolStripMenuItem textureToolStripMenuItem;
        public ToolStripSeparator toolStripSeparator3;
        public ToolStripMenuItem helpToolStripMenuItem;
        public ToolStripMenuItem checkForUpdatesToolStripMenuItem;
        public ToolStripMenuItem editToolStripMenuItem;
        public ToolStripMenuItem undoToolStripMenuItem;
        public ToolStripMenuItem redoToolStripMenuItem;
        public ToolStripSeparator toolStripSeparator7;
        public ToolStripMenuItem toolToolStripMenuItem;
        public ToolStripMenuItem optionsToolStripMenuItem;
        public ToolStripMenuItem keyboardShortcutsToolStripMenuItem;
        public ToolStripMenuItem backgroundColorToolStripMenuItem;
        public ToolStripMenuItem saveToolStripMenuItem;
        public ToolStripMenuItem saveAsToolStripMenuItem;
        public ToolStripMenuItem saveAllToolStripMenuItem;
        public ToolStripSeparator toolStripSeparator5;
        public ContextMenuStrip contextMenuStrip1;
        public ToolStripMenuItem changeNameToolStripMenuItem;
        public ToolStripMenuItem deleteToolStripMenuItem;
        public ToolStripMenuItem cloneToolStripMenuItem;
        public ToolStripMenuItem automaticallyCheckForUpdatesToolStripMenuItem;
        public System.Windows.Forms.TextBox labelEditTextBox;
        public VisibleSplitContainer splitContainer1;
        public VisibleSplitContainer splitContainer3;
        public ToolStripMenuItem importHereToolStripMenuItem;
        public ToolStripMenuItem newFolderToolStripMenuItem;
        public ToolStripSeparator toolStripSeparator10;
        public ToolStripMenuItem ghostHiddenPartsToolStripMenuItem;
        public ToolStripMenuItem backgroundsToolStripMenuItem;
        public ToolStripMenuItem textureOverlayToolStripMenuItem;
        public ToolStripSeparator toolStripMenuItem2;
        public ToolStripMenuItem antialiasingToolStripMenuItem;
        public ToolStripMenuItem xToolStripMenuItem;
        public ToolStripMenuItem xToolStripMenuItem1;
        public ToolStripMenuItem xToolStripMenuItem2;
        public ToolStripMenuItem xToolStripMenuItem3;
        public ToolStripMenuItem xToolStripMenuItem4;
        public SplitContainer splitContainer4;
        public ToolStripButton saveToolStripButton;
        public ToolStripButton saveAlltoolStripButton;
        public ToolStripSeparator toolStripSeparator6;
        public ToolStripSeparator toolStripSeparator1;
        public ToolStripSeparator toolStripSeparator2;
        public ToolStripButton perspectiveToolStripButton;
        public ToolStripButton orthographicToolStripButton;
        public ToolStripSeparator toolStripSeparator4;
        public ToolStripButton screenshotToolStripButton;
        public ToolStripSeparator toolStripSeparator9;
        public ToolStripButton toggleHeadToolStripButton;
        public ToolStripButton toggleChestArmorToolStripButton;
        public ToolStripButton toggleChestToolStripButton;
        public ToolStripButton toggleLeftArmToolStripButton;
        public ToolStripButton toggleRightArmToolStripButton;
        public ToolStripButton toggleLeftLegToolStripButton;
        public ToolStripButton toggleRightLegToolStripButton;
        public ToolStripButton hybridToolStripButton;
        public ToolStripMenuItem hybridViewToolStripMenuItem;
        public StatusStrip statusStrip1;
        public ToolStripStatusLabel toolStripStatusLabel1;
        public ToolStripSeparator toolStripMenuItem3;
        public ToolStripMenuItem mDECRESToolStripMenuItem;
        public ToolStripMenuItem mINCRESToolStripMenuItem;
        public ToolStripMenuItem newSkinToolStripMenuItem;
        public ToolStripSeparator toolStripMenuItem5;
        public ToolStripMenuItem mFETCHNAMEToolStripMenuItem;
        public ToolStripMenuItem mSKINDIRSToolStripMenuItem;
        public ToolStripSeparator toolStripSeparator15;
        public ToolStripDropDownButton toolStripDropDownButton1;
        public ToolStripButton resetCameraToolStripButton;
        public ToolStripSeparator toolStripSeparator16;
        public ToolStripMenuItem mDYNAMICOVERLAYToolStripMenuItem;
        public ColorToolStripMenuItem mTEXTCOLORToolStripMenuItem;
        public NumericUpDownMenuItem mLINESIZEToolStripMenuItem;
        public NumericUpDownMenuItem mOVERLAYTEXTSIZEToolStripMenuItem;
        public ColorToolStripMenuItem mLINECOLORToolStripMenuItem;
        public ToolStripSplitButton undoToolStripButton;
        public ToolStripSplitButton redoToolStripButton;
        public ToolStripMenuItem mSHAREDToolStripMenuItem;
        public ToolStripMenuItem mINFINITEMOUSEToolStripMenuItem;
        public ToolStripMenuItem bROWSEIDToolStripMenuItem;
        public ToolStripMenuItem mRENDERSTATSToolStripMenuItem;
        public ToolStripButton toolStripButton7;
        public Panel panel4;
        public ColorPanel colorPanel;
        public ToolStripSeparator toolStripSeparator11;
        public ToolStripMenuItem gridEnabledToolStripMenuItem;
        public NumericUpDownMenuItem mGRIDOPACITYToolStripMenuItem;
        public ColorToolStripMenuItem mGRIDCOLORToolStripMenuItem;
        public TreeView treeView1;
        public TreeView treeView2;
        public ToolStrip miniToolStrip;
        public ToolStripSeparator toolStripMenuItem1;
        public ToolStripMenuItem officialMinecraftForumsThreadToolStripMenuItem;
        public ToolStripMenuItem planetMinecraftSubmissionToolStripMenuItem;
        public ToolStripButton toggleHelmetToolStripButton;
        public ToolStripButton toggleLeftArmArmorToolStripButton;
        public ToolStripButton toggleRightArmArmorToolStripButton;
        public ToolStripButton toggleLeftLegArmorToolStripButton;
        public ToolStripButton toggleRightLegArmorToolStripButton;
        public ToolStripMenuItem chestArmorToolStripMenuItem;
        public ToolStripMenuItem leftArmArmorToolStripMenuItem;
        public ToolStripMenuItem rightArmArmorToolStripMenuItem;
        public ToolStripMenuItem leftLegArmorToolStripMenuItem;
        public ToolStripMenuItem rightLegArmorToolStripMenuItem;
        public ToolStripSeparator toolStripMenuItem4;
        public ToolStripMenuItem languageToolStripMenuItem;
        public ToolStripButton toolStripButton1;
        public ToolStripSeparator toolStripSeparator12;
        public ToolStripMenuItem useTextureBasesMenuItem;
        public Panel ViewportPanel;
        public Panel ToolsPanel;
        public Panel LibraryPanel;
        private Label label1;
        private ToolStrip toolStrip2;
        private ToolStrip toolStrip1;
    }
}
