using MCSkinn.Scripts.Languages;

namespace MCSkinn
{
    partial class PencilOptions
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
            checkBox1 = new System.Windows.Forms.CheckBox();
            languageProvider1 = new LanguageProvider();
            label1 = new System.Windows.Forms.Label();
            BrushPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)languageProvider1).BeginInit();
            SuspendLayout();
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new System.Drawing.Point(13, 58);
            checkBox1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            checkBox1.Name = "checkBox1";
            languageProvider1.SetPropertyNames(checkBox1, "Text");
            checkBox1.Size = new System.Drawing.Size(188, 28);
            checkBox1.TabIndex = 0;
            checkBox1.Text = "O_INCREMENTAL";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // languageProvider1
            // 
            languageProvider1.BaseControl = this;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(13, 17);
            label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            label1.Name = "label1";
            languageProvider1.SetPropertyNames(label1, "Text");
            label1.Size = new System.Drawing.Size(91, 24);
            label1.TabIndex = 4;
            label1.Text = "G_BRUSH";
            // 
            // BrushPanel
            // 
            BrushPanel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            BrushPanel.Location = new System.Drawing.Point(94, 8);
            BrushPanel.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            BrushPanel.Name = "BrushPanel";
            BrushPanel.Size = new System.Drawing.Size(246, 40);
            BrushPanel.TabIndex = 3;
            // 
            // PencilOptions
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.SystemColors.Window;
            Controls.Add(BrushPanel);
            Controls.Add(checkBox1);
            Controls.Add(label1);
            Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            Name = "PencilOptions";
            Size = new System.Drawing.Size(354, 99);
            Load += PencilOptions_Load;
            ((System.ComponentModel.ISupportInitialize)languageProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox1;
        public LanguageProvider languageProvider1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel BrushPanel;
    }
}
