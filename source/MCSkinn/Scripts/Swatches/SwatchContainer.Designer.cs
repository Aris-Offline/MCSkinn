using Inkore.Coreworks.Localization;
using MCSkinn.Scripts.Paril.Controls;

namespace MCSkinn
{
    partial class SwatchContainer
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new System.Windows.Forms.Panel();
            vScrollBar1 = new System.Windows.Forms.VScrollBar();
            panel2 = new System.Windows.Forms.Panel();
            comboBox1 = new System.Windows.Forms.ComboBox();
            nativeToolStrip1 = new NativeToolStrip();
            newSwatchToolStripButton = new System.Windows.Forms.ToolStripButton();
            deleteSwatchToolStripButton = new System.Windows.Forms.ToolStripButton();
            renameSwatchToolStripButton3 = new System.Windows.Forms.ToolStripButton();
            convertSwatchStripButton = new System.Windows.Forms.ToolStripSplitButton();
            textBox1 = new System.Windows.Forms.TextBox();
            toolStrip1 = new NativeToolStrip();
            toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            editModeToolStripButton = new System.Windows.Forms.ToolStripButton();
            addSwatchToolStripButton = new System.Windows.Forms.ToolStripButton();
            removeSwatchToolStripButton = new System.Windows.Forms.ToolStripButton();
            languageProvider1 = new LanguageProvider();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            nativeToolStrip1.SuspendLayout();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)languageProvider1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.SystemColors.Window;
            panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panel1.Controls.Add(vScrollBar1);
            panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Location = new System.Drawing.Point(0, 79);
            panel1.Margin = new System.Windows.Forms.Padding(6);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(495, 272);
            panel1.TabIndex = 1;
            panel1.Paint += panel1_Paint;
            // 
            // vScrollBar1
            // 
            vScrollBar1.Dock = System.Windows.Forms.DockStyle.Right;
            vScrollBar1.Location = new System.Drawing.Point(476, 0);
            vScrollBar1.Maximum = 10;
            vScrollBar1.Name = "vScrollBar1";
            vScrollBar1.Size = new System.Drawing.Size(17, 270);
            vScrollBar1.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Controls.Add(comboBox1);
            panel2.Controls.Add(nativeToolStrip1);
            panel2.Dock = System.Windows.Forms.DockStyle.Top;
            panel2.Location = new System.Drawing.Point(0, 0);
            panel2.Margin = new System.Windows.Forms.Padding(6);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(495, 46);
            panel2.TabIndex = 4;
            // 
            // comboBox1
            // 
            comboBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            comboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.ItemHeight = 13;
            comboBox1.Location = new System.Drawing.Point(0, 0);
            comboBox1.Margin = new System.Windows.Forms.Padding(6);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new System.Drawing.Size(308, 19);
            comboBox1.TabIndex = 4;
            comboBox1.DrawItem += comboBox1_DrawItem;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // nativeToolStrip1
            // 
            nativeToolStrip1.AllowMerge = false;
            nativeToolStrip1.AutoSize = false;
            nativeToolStrip1.Dock = System.Windows.Forms.DockStyle.Right;
            nativeToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            nativeToolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            nativeToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { newSwatchToolStripButton, deleteSwatchToolStripButton, renameSwatchToolStripButton3, convertSwatchStripButton });
            nativeToolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            nativeToolStrip1.Location = new System.Drawing.Point(308, 0);
            nativeToolStrip1.Name = "nativeToolStrip1";
            nativeToolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 4, 0);
            nativeToolStrip1.Size = new System.Drawing.Size(187, 46);
            nativeToolStrip1.TabIndex = 3;
            nativeToolStrip1.Text = "nativeToolStrip1";
            // 
            // newSwatchToolStripButton
            // 
            newSwatchToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            newSwatchToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            newSwatchToolStripButton.Image = Properties.Resources.newswatch;
            newSwatchToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            newSwatchToolStripButton.Name = "newSwatchToolStripButton";
            languageProvider1.SetPropertyNames(newSwatchToolStripButton, "Text");
            newSwatchToolStripButton.Size = new System.Drawing.Size(34, 28);
            newSwatchToolStripButton.Text = "M_NEWPALETTE";
            newSwatchToolStripButton.Click += newSwatchToolStripButton_Click;
            // 
            // deleteSwatchToolStripButton
            // 
            deleteSwatchToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            deleteSwatchToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            deleteSwatchToolStripButton.Image = Properties.Resources.deleteswatch;
            deleteSwatchToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            deleteSwatchToolStripButton.Name = "deleteSwatchToolStripButton";
            languageProvider1.SetPropertyNames(deleteSwatchToolStripButton, "Text");
            deleteSwatchToolStripButton.Size = new System.Drawing.Size(34, 28);
            deleteSwatchToolStripButton.Text = "M_DELETEPALETTE";
            deleteSwatchToolStripButton.Click += deleteSwatchToolStripButton_Click;
            // 
            // renameSwatchToolStripButton3
            // 
            renameSwatchToolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            renameSwatchToolStripButton3.Image = Properties.Resources.renameswatch;
            renameSwatchToolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            renameSwatchToolStripButton3.Name = "renameSwatchToolStripButton3";
            languageProvider1.SetPropertyNames(renameSwatchToolStripButton3, "Text");
            renameSwatchToolStripButton3.Size = new System.Drawing.Size(34, 28);
            renameSwatchToolStripButton3.Text = "M_RENAMEPALETTE";
            renameSwatchToolStripButton3.Click += renameSwatchToolStripButton3_Click;
            // 
            // convertSwatchStripButton
            // 
            convertSwatchStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            convertSwatchStripButton.Image = Properties.Resources.convertswatch;
            convertSwatchStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            convertSwatchStripButton.Name = "convertSwatchStripButton";
            languageProvider1.SetPropertyNames(convertSwatchStripButton, "Text");
            convertSwatchStripButton.Size = new System.Drawing.Size(45, 28);
            convertSwatchStripButton.Text = "M_CONVERTSWATCH";
            convertSwatchStripButton.ButtonClick += convertSwatchStripButton_ButtonClick;
            // 
            // textBox1
            // 
            textBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            textBox1.Location = new System.Drawing.Point(0, 2);
            textBox1.Margin = new System.Windows.Forms.Padding(6);
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(272, 30);
            textBox1.TabIndex = 5;
            textBox1.Visible = false;
            textBox1.KeyPress += textBox1_KeyPress;
            textBox1.Leave += textBox1_Leave;
            // 
            // toolStrip1
            // 
            toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripButton1, toolStripButton2, toolStripSeparator1, editModeToolStripButton, addSwatchToolStripButton, removeSwatchToolStripButton });
            toolStrip1.Location = new System.Drawing.Point(0, 46);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 4, 0);
            toolStrip1.Size = new System.Drawing.Size(495, 33);
            toolStrip1.TabIndex = 2;
            toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton1.Image = Properties.Resources.ZoomOutHS;
            toolStripButton1.ImageTransparentColor = System.Drawing.Color.Black;
            toolStripButton1.Name = "toolStripButton1";
            languageProvider1.SetPropertyNames(toolStripButton1, "Text");
            toolStripButton1.Size = new System.Drawing.Size(34, 28);
            toolStripButton1.Text = "T_TREE_ZOOMOUT";
            toolStripButton1.Click += toolStripButton1_Click;
            // 
            // toolStripButton2
            // 
            toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton2.Image = Properties.Resources.ZoomInHS;
            toolStripButton2.ImageTransparentColor = System.Drawing.Color.Black;
            toolStripButton2.Name = "toolStripButton2";
            languageProvider1.SetPropertyNames(toolStripButton2, "Text");
            toolStripButton2.Size = new System.Drawing.Size(34, 28);
            toolStripButton2.Text = "T_TREE_ZOOMIN";
            toolStripButton2.Click += toolStripButton2_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(6, 33);
            // 
            // editModeToolStripButton
            // 
            editModeToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            editModeToolStripButton.Image = Properties.Resources.pipette;
            editModeToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            editModeToolStripButton.Name = "editModeToolStripButton";
            languageProvider1.SetPropertyNames(editModeToolStripButton, "Text");
            editModeToolStripButton.Size = new System.Drawing.Size(34, 28);
            editModeToolStripButton.Text = "T_SWATCHEDIT";
            editModeToolStripButton.Click += editModeToolStripButton_Click;
            // 
            // addSwatchToolStripButton
            // 
            addSwatchToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            addSwatchToolStripButton.Image = Properties.Resources._112_Plus_Green_16x16_72;
            addSwatchToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            addSwatchToolStripButton.Name = "addSwatchToolStripButton";
            languageProvider1.SetPropertyNames(addSwatchToolStripButton, "Text");
            addSwatchToolStripButton.Size = new System.Drawing.Size(34, 28);
            addSwatchToolStripButton.Text = "T_ADDSWATCH";
            addSwatchToolStripButton.Click += addSwatchToolStripButton_Click;
            // 
            // removeSwatchToolStripButton
            // 
            removeSwatchToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            removeSwatchToolStripButton.Image = Properties.Resources._112_Minus_Orange_16x16_72;
            removeSwatchToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            removeSwatchToolStripButton.Name = "removeSwatchToolStripButton";
            languageProvider1.SetPropertyNames(removeSwatchToolStripButton, "Text");
            removeSwatchToolStripButton.Size = new System.Drawing.Size(34, 28);
            removeSwatchToolStripButton.Text = "T_DELETESWATCH";
            removeSwatchToolStripButton.Click += removeSwatchToolStripButton_Click;
            // 
            // languageProvider1
            // 
            languageProvider1.BaseControl = this;
            // 
            // SwatchContainer
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            Controls.Add(textBox1);
            Controls.Add(panel1);
            Controls.Add(toolStrip1);
            Controls.Add(panel2);
            Margin = new System.Windows.Forms.Padding(6);
            Name = "SwatchContainer";
            Size = new System.Drawing.Size(495, 351);
            Load += SwatchContainer_Load;
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            nativeToolStrip1.ResumeLayout(false);
            nativeToolStrip1.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)languageProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private NativeToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        public LanguageProvider languageProvider1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton editModeToolStripButton;
        private System.Windows.Forms.ToolStripButton removeSwatchToolStripButton;
        private System.Windows.Forms.ToolStripButton addSwatchToolStripButton;
        private NativeToolStrip nativeToolStrip1;
        private System.Windows.Forms.ToolStripButton newSwatchToolStripButton;
        private System.Windows.Forms.ToolStripButton deleteSwatchToolStripButton;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ToolStripButton renameSwatchToolStripButton3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ToolStripSplitButton convertSwatchStripButton;
    }
}
