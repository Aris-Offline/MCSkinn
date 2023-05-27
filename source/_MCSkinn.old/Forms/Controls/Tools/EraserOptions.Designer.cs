using MCSkinn.Scripts.Languages;

namespace MCSkinn
{
    partial class EraserOptions
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
            languageProvider1 = new LanguageProvider();
            BrushPanel = new System.Windows.Forms.Panel();
            label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)languageProvider1).BeginInit();
            SuspendLayout();
            // 
            // languageProvider1
            // 
            languageProvider1.BaseControl = this;
            // 
            // BrushPanel
            // 
            BrushPanel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            BrushPanel.Location = new System.Drawing.Point(60, 6);
            BrushPanel.Name = "BrushPanel";
            BrushPanel.Size = new System.Drawing.Size(120, 28);
            BrushPanel.TabIndex = 5;
            // 
            // label1
            // 
            label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(8, 12);
            label1.Name = "label1";
            languageProvider1.SetPropertyNames(label1, "Text");
            label1.Size = new System.Drawing.Size(63, 17);
            label1.TabIndex = 6;
            label1.Text = "G_BRUSH";
            // 
            // EraserOptions
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.SystemColors.Window;
            Controls.Add(BrushPanel);
            Controls.Add(label1);
            Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            Name = "EraserOptions";
            Size = new System.Drawing.Size(475, 40);
            Load += EraserOptions_Load;
            ((System.ComponentModel.ISupportInitialize)languageProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        public LanguageProvider languageProvider1;
        private System.Windows.Forms.Panel BrushPanel;
        private System.Windows.Forms.Label label1;
    }
}
